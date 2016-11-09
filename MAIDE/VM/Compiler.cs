using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;
using Row = MAIDE.UI.CodeEditBox.RowReadonly;
using Rows = MAIDE.UI.CodeEditBox.RowReadonlyCollection;

namespace MAIDE.VM
{
    public class Compiler
    {
        private readonly Regex rx_section = new Regex(@"^\s*(?:(?<sect>\w+):)?\s*(?<body>[^;\n\r]+)?\s*(;[^;\n\r]*)?$", RegexOptions.Multiline);
        private readonly Regex rx_command = new Regex(@"^(?<opcod>\w+)\s*(?<p1>[\w#\[\]]+)?\s*(?<undef1>[\S^,]+)?(?:,\s*(?<p2>[\w#\[\]]+)\s*)?(?<undef2>\S+)?\s*$", RegexOptions.Multiline);
        private readonly Regex rx_pointer = new Regex(@"(?<relativ>#)?(?<name>\w+)(?:\[(?<reg1>\w)\])?(?:\[(?<reg2>\w)\])?");
        private readonly Regex rx_regsize = new Regex(@"\D+(\d+)");
        private readonly Regex rx_onlyspace = new Regex(@"^\s+", RegexOptions.Multiline);
        private readonly Dictionary<Type, Func<Row, Type, string, object>> parseType;
        private readonly Core core;
        private Dictionary<string, int> sections;
        private BinaryWriter writer;
        private BinaryWriter writerDedugTable;

        public Compiler(Core core)
        {
            this.core = core;

            parseType = new Dictionary<Type, Func<Row, Type, string, object>>();
            parseType.Add(typeof(int), parseNumber);
            parseType.Add(typeof(short), parseNumber);
            parseType.Add(typeof(char), parseNumber);
            parseType.Add(typeof(byte), parseNumber);
            parseType.Add(typeof(Register8), parseRegister);
            parseType.Add(typeof(Register16), parseRegister);
            parseType.Add(typeof(Register32), parseRegister);
            parseType.Add(typeof(Pointer), parsePointer);
        }

        public bool Build(Rows rows, Stream stream, bool createDebugTable)
        {
            sections = new Dictionary<string, int>();
            writer = new BinaryWriter(stream);

            writerDedugTable = createDebugTable ? new BinaryWriter(new MemoryStream()) : null;

            Log.Errors.Clear();

            var bodys = preprocessor(rows);

            foreach (var tuple in bodys)
                parseBody(tuple.Item1, tuple.Item2);

            if (createDebugTable)
            {
                writer.Write((byte)0xFF);
                var br = new BinaryReader(writerDedugTable.BaseStream);
                br.BaseStream.Position = 0;
                writer.Write(br.ReadBytes((int)br.BaseStream.Length));
            }

            return Log.Errors.Count == 0;
        }

        private Tuple<Row, string>[] preprocessor(Rows rows)
        {
            var bodys = new List<Tuple<Row, string>>();

            foreach (var row in rows)
            {
                Match m_sect = rx_section.Match(row);
                if (m_sect.Success)
                {
                    var sect = m_sect.Groups["sect"];
                    var body = m_sect.Groups["body"];

                    if (sect.Success)
                        parseSect(row, sect.Value);
                    if (body.Success)
                        bodys.Add(new Tuple<Row, string>(row, body.Value));
                }
                else if (!rx_onlyspace.IsMatch(row))
                    Log.AddError(row, "Набор символов");
            }

            return bodys.ToArray();
        }

        private void parseSect(Row row, string sect)
        {
            if (sections.ContainsKey(sect))
                Log.AddError(row, "Метка {0} уже существует", sect);
            else
                sections.Add(sect, row.Index);
        }

        private void parseBody(Row row, string body)
        {
            Match m_body = rx_command.Match(body);

            if (!m_body.Success)
                return;

            var opcod = m_body.Groups["opcod"];
            var arg1 = m_body.Groups["p1"];
            var arg2 = m_body.Groups["p2"];
            var undef1 = m_body.Groups["undef1"];
            var undef2 = m_body.Groups["undef2"];

            if (undef1.Success)
                Log.AddError(row, "Неожиданный символ '{0}'", undef1);
            if (undef2.Success)
                Log.AddError(row, "Неожиданный символ '{0}'", undef2);

            var method = OperationManager.GetMethod(opcod.Value);
            if (method == null)
            {
                Log.AddError(row, "Оператор '{0}' не определен", opcod.Value);
                return;
            }

            var op = new Operation(method);

            int argCount = 0;
            argCount += arg1.Success ? 1 : 0;
            argCount += arg2.Success ? 1 : 0;

            var args = op.Method.GetParameters();
            if (args.Count() != argCount)
            {
                Log.AddError(row, "Оператор '{0}' имеет {1} аргумент(ов)", opcod.Value, args.Count());
                return;
            }

            if (arg1.Success)
                op.Args[0] = parseArgument(row, args[0].ParameterType, arg1.Value);
            if (arg2.Success)
                op.Args[1] = parseArgument(row, args[1].ParameterType, arg2.Value);

            if (Log.Errors.Count != 0)
                return;

            if (writerDedugTable != null)
            {
                writerDedugTable.Write((short)writer.BaseStream.Position);
                writerDedugTable.Write((short)row.Index);
            }
            OperationManager.Code(writer, op);
        }

        private object parseArgument(Row row, Type target, string arg)
        {
            Func<Row, Type, string, object> parser = null;

            if (!parseType.TryGetValue(target, out parser))
            {
                Log.AddError(row, "Нет парсера для '{0}', пните разраба", target.FullName);
                return null;
            }

            return parser(row, target, arg);
        }

        private object parseRegister(Row row, Type type, string value)
        {
            var reg = RegisterManager.GetRegister(value);

            if (reg == null)
            {
                Log.AddError(row, "Регистр '{0}' не существует", value);
                return null;
            }
            if (reg.GetType() != type)
            {
                string size = rx_regsize.Match(type.Name).Groups[1].Value;
                Log.AddError(row, "Ожидался {1} битный регистр", size);
                return null;
            }

            return reg;
        }

        private object parseNumber(Row row, Type type, string value)
        {
            int result;
            
            if (!int.TryParse(value.Trim('#'), out result))
            {
                Log.AddError(row, "Выражение '{0}' не является числом", value);
                return null;
            }

            return Convert.ChangeType(result, type);
        }

        private object parsePointer(Row row, Type type, string value)
        {
            Match m_body = rx_pointer.Match(value);

            if (!m_body.Success)
            {
                Log.AddError(row, "Не удалось распознать выражение '{0}'", value);
                return null;
            }

            var relativ = m_body.Groups["relativ"].Success;
            var name = m_body.Groups["name"].Value;
            var reg1 = m_body.Groups["reg1"];
            var reg2 = m_body.Groups["reg2"];

            int point = 0;
            if (!int.TryParse(name, out point))
            {
                if (!sections.ContainsKey(name))
                {
                    Log.AddError(row, "Метка '{0}' не найдена", name);
                    return null;
                }
                point = sections[name];
            }

            Pointer result = new Pointer(point);

            if (reg1.Success)
            {
                result.regA = (Register32)RegisterManager.GetRegister(reg1.Value);
                if (result.regA == null)
                {
                    Log.AddError(row, "Регистр '{0}' не найден или не является 32 разрядным", reg1.Value);
                    return null;
                }
            }

            if (reg2.Success)
            {
                result.regB = (Register32)RegisterManager.GetRegister(reg2.Value);
                if (result.regB == null)
                {
                    Log.AddError(row, "Регистр '{0}' не найден или не является 32 разрядным", reg2.Value);
                    return null;
                }
            }

            return result;
        }
    }
}