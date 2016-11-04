using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Threading;
using MAIDE.UI;
using MAIDE.Utilit;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace MAIDE.VM
{
    public class Core
    {
        public static readonly ObservableCollection<ErrorMessage> Errors = new ObservableCollection<ErrorMessage>();
        public static readonly string[] Letters = new string[] { "byte", "word", "ds", "dw", "db", "equ" };
        private static readonly Regex rx_section = new Regex(@"^\s*(?<sect>\w+:)?\s*(?<body>[^;\n\r]+)?\s*(;[^;\n\r]*)?$", RegexOptions.Multiline);
        private static readonly Regex rx_command = new Regex(@"^(?<opcod>\w+)\s*(?<p1>\w+)?\s*(?<undef1>[\S^,]+)?(?:,\s*(?<p2>[\w#]+)\s*)?(?<undef2>\S+)?\s*$", RegexOptions.Multiline);
        private static readonly Regex rx_regsize = new Regex(@"\D+(\d+)");
        private static readonly Regex rx_onlyspace = new Regex(@"\s*");

        public enum State
        {
            NoBuild,
            Ready,
            Launched,
            Pause,
            Finish,
            Error,
        }

        public class StateChangedEventArgs : EventArgs
        {
            public State Old { get; set; }
            public State New { get; set; }

            public StateChangedEventArgs(State In, State To)
            {
                Old = In;
                New = To;
            }
        }

        private List<Operation> source = new List<Operation>();
        private ManualResetEvent waitEvent;
        private EventHandler<StateChangedEventArgs> stateChanged;
        private int total;
        private State status;
        private bool needPause;
        private readonly Dictionary<Type, Func<CodeEditBox.RowReadonly, Type, string, object>> parseType;

        public readonly Dictionary<string, int> Sections;
        public readonly List<Register> Registers;
        public readonly RegisterFlag FlagReg;
        public readonly Stack<int> Stack;
        public int ActiveIndex = 0;

        public State Status
        {
            get { return status; }
            private set
            {
                if (status != value)
                {
                    State old = status;
                    status = value;
                    stateChanged.Invoke(this, new StateChangedEventArgs(old, value));
                }
            }
        }

        public event EventHandler<StateChangedEventArgs> StateChanged
        {
            add
            {
                lock (this) { stateChanged += value; }
            }
            remove
            {
                lock (this) { stateChanged -= value; }
            }
        }

        public StringCollection RegNames32
        {
            get { return Properties.Settings.Default.Register32; }
            set { setRegs<Register32>(value); }
        }

        public StringCollection RegNames16
        {
            get { return Properties.Settings.Default.Register16; }
            set { setRegs<Register16>(value); }
        }

        public StringCollection RegNames8
        {
            get { return Properties.Settings.Default.Register8; }
            set { setRegs<Register8>(value); }
        }

        private void setRegs<T>(StringCollection regs) where T : Register
        {
            if (regs != null)
            {
                Registers.RemoveAll(reg => reg is T);
                foreach (string name in regs)
                {
                    if (string.IsNullOrWhiteSpace(name))
                        continue;

                    T reg = Activator.CreateInstance(typeof(T), new object[] { name.Replace("\n", "") }) as T;
                    Registers.Add(reg);
                }
            }
        }

        public class Operation
        {
            public readonly CodeEditBox.RowReadonly Row;
            public readonly object[] Args;
            public readonly MethodInfo Method;

            public Operation(CodeEditBox.RowReadonly row, MethodInfo method)
            {
                Row = row;
                Method = method;
                Args = new object[method.GetParameters().Count()];
            }
        }

        public Core()
        {
            Stack = new Stack<int>();
            FlagReg = new RegisterFlag();
            Registers = new List<Register>();
            Sections = new Dictionary<string, int>();

            stateChanged = (s, e) => { };
            waitEvent = new ManualResetEvent(true);
            PropertyJoin.Create(this, "RegNames32", Properties.Settings.Default, "Register32");
            PropertyJoin.Create(this, "RegNames16", Properties.Settings.Default, "Register16");
            PropertyJoin.Create(this, "RegNames8", Properties.Settings.Default, "Register8");

            parseType = new Dictionary<Type, Func<CodeEditBox.RowReadonly, Type, string, object>>();
            parseType.Add(typeof(int), parseNumber);
            parseType.Add(typeof(short), parseNumber);
            parseType.Add(typeof(char), parseNumber);
            parseType.Add(typeof(byte), parseNumber);
            parseType.Add(typeof(Register8), parseRegister);
            parseType.Add(typeof(Register16), parseRegister);
            parseType.Add(typeof(Register32), parseRegister);

            Operators.ActiveCore = this;
        }

        public Register GetRegister(string name)
        {
            foreach (Register r in Registers)
            {
                if (r.Name == name)
                    return r;
            }
            return null;
        }

        public void Invoke()
        {
            if (status != State.Ready && status != State.Finish)
                return;

            Stack.Clear();
            ActiveIndex = 0;
            total = 0;
            needPause = false;
            waitEvent.Set();
            Status = State.Launched;

            int i = 500;
            while (Console.Instance == null)
            {
                Thread.Sleep(5);
                if (--i == 0)
                {
                    MessageBox.Show(Language.ConsoleDontAnswer);
                    return;
                }
            }

            Console.Clear();

            try
            {
                while (ActiveIndex < source.Count && (status == State.Launched || status == State.Pause))
                {
                    Operation op = source[ActiveIndex];

                    if (total > Properties.Settings.Default.TotalTickLimit)
                    {
                        if (MessageBox.Show(Language.ProgrammLoop, Language.ProgrammLoopTitle, MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            Status = State.Error;
                            return;
                        }
                        total = -total;
                    }

                    if (op.Row.IsFlag(CodeEditBox.RowFlag.Breakpoint))
                        Pause();

                    op.Row.SetFlag(CodeEditBox.RowFlag.Run);
                    waitEvent.WaitOne();

                    if (status == State.Launched)
                    {
                        try
                        {
                            op.Method.Invoke(null, op.Args);
                        }
                        catch (TargetInvocationException e)
                        {
                            if (e.InnerException is RuntimeException)
                                throw e.InnerException;
                            throw new RuntimeException(string.Format(Language.RuntimeException, op.Method.Name), getCurrentRow());
                        }

                        if (ActiveIndex >= source.Count)
                            throw new RuntimeException(Language.RuntimeExceptionMemory, op.Row.Index + 1);

                        if (needPause)
                        {
                            needPause = false;
                            Pause();
                        }

                        ActiveIndex++;
                        total++;
                    }
                    op.Row.ResetFlag(CodeEditBox.RowFlag.Run);
                }
                Status = State.Finish;
            }
            catch (RuntimeException e)
            {
                Console.WriteLine(string.Format(Language.RuntimeExceptionRow, e.Row, e.Message));
                Status = State.Error;
            }

            Console.Write(Language.PressAnyKey);
            Console.MoveCaretToEnd();
            Console.ReadKey();
        }

        public bool Build(CodeEditBox.RowReadonlyCollection rows)
        {
            Stop();
            source.Clear();
            Sections.Clear();
            Errors.Clear();

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
                        parseBody(row, body.Value);
                }
                else if (!rx_onlyspace.IsMatch(row))
                    AddError(row, "Набор символов");
            }

            Status = Errors.Count == 0 ? State.Ready : State.Error;
            return Errors.Count == 0;
        }

        private void parseSect(CodeEditBox.RowReadonly row, string sect)
        {
            if (Sections.ContainsKey(sect))
                AddError(row, "Метка {0} уже существует", sect);
            else
                Sections.Add(sect, row.Index);
        }

        private void parseBody(CodeEditBox.RowReadonly row, string body)
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
                AddError(row, "Неожиданный символ '{0}'", undef1);
            if (undef2.Success)
                AddError(row, "Неожиданный символ '{0}'", undef2);

            var op = Operators.OperationsList.FirstOrDefault(m => m.Name.Equals(opcod.Value, StringComparison.OrdinalIgnoreCase));
            if (op == null)
            {
                AddError(row, "Оператор '{0}' не определен", opcod.Value);
                return;
            }

            int argCount = 0;
            argCount += arg1.Success ? 1 : 0;
            argCount += arg2.Success ? 1 : 0;

            var args = op.GetParameters();
            if (args.Count() != argCount)
            {
                AddError(row, "Оператор '{0}' имеет {1} аргумент(ов)", opcod.Value, args.Count());
                return;
            }

            Operation operation = new Operation(row, op);
            if (arg1.Success)
                operation.Args[0] = parseArgument(operation, args[0].ParameterType, arg1.Value);
            if (arg2.Success)
                operation.Args[1] = parseArgument(operation, args[1].ParameterType, arg2.Value);

            if (Errors.Count == 0)
                source.Add(operation);
        }

        private object parseArgument(Operation operation, Type target, string arg)
        {
            Func<CodeEditBox.RowReadonly, Type, string, object> parser = null;

            if (!parseType.TryGetValue(target, out parser))
            {
                AddError(operation.Row, "Кто то забыл добавить парсер для '{0}', пните разраба", target.FullName);
                return null;
            }

            return parser(operation.Row, target, arg);
        }

        private object parseRegister(CodeEditBox.RowReadonly row, Type type, string value)
        {
            var reg = GetRegister(value);

            if (reg == null)
            {
                AddError(row, "Регистр '{0}' не существует", value);
                return null;
            }
            if (reg.GetType() != type)
            {
                string size = rx_regsize.Match(type.Name).Groups[1].Value;
                AddError(row, "Регистр '{0}' не {1}х битный", value, size);
                return null;
            }

            return reg;
        }

        private object parseNumber(CodeEditBox.RowReadonly row, Type type, string value)
        {
            var result = value.MathToDouble();

            if (!result.HasValue)
            {
                AddError(row, "Выражение '{0}' не является числом", value);
                return null;
            }

            return Convert.ChangeType(result.Value, type);
        }

        public void AddError(CodeEditBox.RowReadonly row, string message, params object[] args)
        {
            string msg = string.Format(message, args);
            Errors.Add(new ErrorMessage(msg, row.Index, row.Owner));
        }
        
        private int getCurrentRow()
        {
            return source[ActiveIndex].Row.Index + 1;
        }

        public void Pause()
        {
            waitEvent.Reset();
            Status = State.Pause;
        }

        public void Resume(bool oneOperation = false)
        {
            if (status == State.Pause)
            {
                Status = State.Launched;
                needPause = oneOperation;
                waitEvent.Set();
            }
        }

        public void Stop()
        {
            if (status == State.Launched || status == State.Pause || status == State.Finish)
            {
                if (ActiveIndex < source.Count)
                    source[ActiveIndex].Row.ResetFlag(CodeEditBox.RowFlag.Run);

                Status = State.Ready;
                waitEvent.Set();
            }
        }
    }
}