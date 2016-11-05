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
        private readonly Regex rx_section = new Regex(@"^\s*(?<sect>\w+:)?\s*(?<body>[^;\n\r]+)?\s*(;[^;\n\r]*)?$", RegexOptions.Multiline);
        private readonly Regex rx_command = new Regex(@"^(?<opcod>\w+)\s*(?<p1>\w+)?\s*(?<undef1>[\S^,]+)?(?:,\s*(?<p2>[\w#]+)\s*)?(?<undef2>\S+)?\s*$", RegexOptions.Multiline);
        private readonly Regex rx_regsize = new Regex(@"\D+(\d+)");
        private readonly Regex rx_onlyspace = new Regex(@"^\s+", RegexOptions.Multiline);
        private readonly Dictionary<Type, Func<Row, Type, string, object>> parseType;
        private readonly Core core;
        private Dictionary<string, int> sections;
        private BinaryWriter writer;

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

        public bool Build(Rows rows, Stream stream)
        {
            sections = new Dictionary<string, int>();
            writer = new BinaryWriter(stream);

            Log.Errors.Clear();

            var bodys = preprocessor(rows);

            foreach (var tuple in bodys)
                parseBody(tuple.Item1, tuple.Item2);

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

            if (Log.Errors.Count == 0)
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

            if (!int.TryParse(value, out result))
            {
                Log.AddError(row, "Выражение '{0}' не является числом", value);
                return null;
            }

            return Convert.ChangeType(result, type);
        }

        private object parsePointer(Row row, Type type, string value)
        {
            int result;

            if (!int.TryParse(value, out result))
            {
                Log.AddError(row, "Выражение '{0}' не является числом", value);
                return null;
            }

            return Convert.ChangeType(result, type);
        }
    }
}