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

namespace MAIDE.VM
{
    public class Core
    {
        public static readonly ObservableCollection<ErrorMessage> Errors = new ObservableCollection<ErrorMessage>();
        public static readonly string[] Letters = new string[] { "byte", "word", "ds", "dw", "db", "equ" };

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
        private CodeEditBox.RowReadonlyCollection code;
        private EventHandler<StateChangedEventArgs> stateChanged;
        private int total;
        private State status;
        private int dataZoneOffest;
        private byte[] data;
        private bool needPause;

        public readonly Dictionary<string, int> Links = new Dictionary<string, int>();
        public readonly List<Register> Registers = new List<Register>();
        public readonly Stack<int> Stack = new Stack<int>();
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
            public CodeEditBox.RowReadonly row;
            public string operation;
            public object[] args;
            public MethodInfo method;
        }

        public Core()
        {
            Operators.ActiveCore = this;

            stateChanged = (s, e) => { };
            waitEvent = new ManualResetEvent(true);
            PropertyJoin.Create(this, "RegNames32", Properties.Settings.Default, "Register32");
            PropertyJoin.Create(this, "RegNames16", Properties.Settings.Default, "Register16");
            PropertyJoin.Create(this, "RegNames8", Properties.Settings.Default, "Register8");
            Registers.Add(new RegisterFlag("flag"));
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

                    if (op.row.IsFlag(CodeEditBox.RowFlag.Breakpoint))
                        Pause();

                    op.row.SetFlag(CodeEditBox.RowFlag.Run);
                    waitEvent.WaitOne();

                    if (status == State.Launched)
                    {
                        try
                        {
                            op.method.Invoke(null, op.args);
                        }
                        catch (TargetInvocationException e)
                        {
                            if (e.InnerException is RuntimeException)
                                throw e.InnerException;
                            throw new RuntimeException(string.Format(Language.RuntimeException, op.method.Name), getCurrentRow());
                        }

                        if (ActiveIndex >= source.Count)
                            throw new RuntimeException(Language.RuntimeExceptionMemory, op.row.Index + 1);

                        if (needPause)
                        {
                            needPause = false;
                            Pause();
                        }

                        ActiveIndex++;
                        total++;
                    }
                    op.row.ResetFlag(CodeEditBox.RowFlag.Run);
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
            Links.Clear();
            Errors.Clear();
            code = rows;

            var lines = Preprocessor();

            if (Errors.Count != 0)
                return false;

            foreach (var line in lines)
            {
                Operation op = Link(line.Key, line.Value);
                if (op != null)
                    source.Add(op);
            }

            Status = Errors.Count == 0 ? State.Ready : State.Error;
            return Errors.Count == 0;
        }

        public Dictionary<CodeEditBox.RowReadonly, string[]> Preprocessor()
        {
            var result = new Dictionary<CodeEditBox.RowReadonly, string[]>();
            Dictionary<string, int> dataZone = new Dictionary<string, int>();
            List<byte> dataList = new List<byte>();

            for (int i = 0; i < code.Count(); i++)
            {
                string[] text = code[i].ToString().Replace('\t', ' ').Trim(' ').Split(Properties.Settings.Default.CommentChar)[0].Split(':');

                if (text.Length == 0)
                    continue;

                string data;
                if (text.Count() == 2)
                {
                    data = text[1];

                    if (!Links.ContainsKey(text[0]))
                        Links.Add(text[0], result.Count);
                    else
                        addError(new ErrorMessage(string.Format(Language.MarkerFound, text[0]), i, code[i].Owner));
                }
                else
                    data = text[0];

                text = data.Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);

                if (text.Length != 0)
                {
                    string lw = text[0].ToLower();
                    if (lw == "byte" || lw == "word" || lw == "dw" || lw == "db" || lw == "ds")
                    {
                        dataZone.Add(Links.Last().Key, dataList.Count);
                        string[] values = text[1].Split(',');
                        foreach (string val in values)
                        {
                            string v = val.Trim(' ');
                            if (lw == "byte" || lw == "db")
                            {
                                if (v[0] == '\'' || v[0] == '"')
                                    dataList.AddRange(v.Substring(1, v.Length - 2).Select(c => (byte)c));
                                else
                                    dataList.Add((byte)int.Parse(v));
                            }
                            else
                            {
                                int b;
                                if (int.TryParse(v, out b))
                                    dataList.AddRange(BitConverter.GetBytes(b));
                                else
                                    addError(new ErrorMessage(string.Format(Language.NotNumber, v), code[i].Index, code[i].Owner));
                            }
                        }
                    }
                    else if (lw == "equ")
                    {
                        int index;
                        if (int.TryParse(text[1], out index))
                            Links[Links.Keys.Last()] = index;
                        else
                            addError(new ErrorMessage(string.Format(Language.NotNumber, text[1]), code[i].Index, code[i].Owner));
                    }
                    else
                        result.Add(code[i], text);
                }
            }
            
            dataZoneOffest = result.Count;
            foreach(var k in dataZone)
                Links[k.Key] = k.Value + dataZoneOffest;

            dataList.AddRange(new byte[] { 0, 0, 0 });
            data = dataList.ToArray();

            return result;
        }

        public Operation Link(CodeEditBox.RowReadonly row, string[] text)
        {
            Operation op = new Operation();
            op.operation = text[0];
            op.row = row;

            foreach (MethodInfo method in Operators.OperationsList)
            {
                if (method.Name.Equals(op.operation, StringComparison.OrdinalIgnoreCase))
                {
                    string[] args;
                    if (text.Length > 1)
                    {
                        args = text[1].Split(',');
                        for (int i = 0; i < args.Length; i++)
                            args[i] = args[i].Trim();
                    }
                    else
                        args = new string[0];

                    op.method = method;
                    return ParseOperation(op, args) ? op : null;
                }
            }
            addError(new ErrorMessage(string.Format(Language.OperationsNotFound, op.operation), row.Index, row.Owner));
            return null;
        }

        public bool ParseOperation(Operation operation, string[] input)
        {
            ParameterInfo[] paramInfo = operation.method.GetParameters();
            List<object> output = new List<object>();
            ErrorMessage error = new ErrorMessage(null, operation.row.Index, operation.row.Owner);

            if (input.Length != paramInfo.Length)
                error.Message = string.Format(Language.OperationsArgumentError, operation.operation, paramInfo.Length);

            for (int i = 0; i < input.Length && error.Message == null; i++)
            {
                System.Type needType = paramInfo[i].ParameterType;
                int oldCount = output.Count;
                string value = input[i];

                value = value.TrimStart('#');
                if (needType.BaseType == typeof(Register))
                {
                    Register reg = GetRegister(value.ToLower());
                    if (reg == null)
                        error.Message = string.Format(Language.RegisterNotFound, value);

                    if (!needType.IsInstanceOfType(reg))
                        error.Message = string.Format(Language.RegisterDontUseHere, value);

                    output.Add(reg);
                }
                else if (needType == typeof(int) || needType == typeof(short) || needType == typeof(char) || needType == typeof(byte))
                {
                    double? result = value.MathToDouble();
                    if (result != null)
                        output.Add(Convert.ChangeType(result, needType));
                    else
                        error.Message = string.Format(Language.NotNumber, value);
                }
                else if (needType == typeof(Link))
                {
                    string[] temp = value.Split('[');
                    Link link = new Link();
                    double? row = value.MathToDouble();
                    if (row != null)
                        link.Line = (int)row;
                    else
                    {
                        if (!Links.ContainsKey(temp[0]))
                            error.Message = string.Format(Language.MarkerNotFound, temp[0]);
                        else
                            link.Line = Links[temp[0]];
                    }
                    if (temp.Length == 2)
                    {
                        temp[1] = temp[1].Remove(temp[1].Length - 1).ToLower();
                        Register32 reg = GetRegister(temp[1]) as Register32;
                        if (reg == null)
                            error.Message = string.Format(Language.RegisterNotFoundOrDontUse, temp[1]);
                        else
                            link.reg32 = reg;
                    }
                    output.Add(link);
                }

                if (oldCount == output.Count && error.Message != "")
                    error.Message = string.Format(Language.InvalidParameter, value);
            }

            operation.args = output.ToArray();

            if (error.Message == null)
                return true;

            addError(error);
            return false;
        }

        public byte GetByte(int adress)
        {
            return data[GetDataAdress(adress)];
        }

        public int GetWord(int adress)
        {
            return BitConverter.ToInt32(data, GetDataAdress(adress));
        }

        public int GetDataAdress(int adress)
        {
            adress -= dataZoneOffest;
            if (adress < 0)
                throw new RuntimeException(Language.IndexInInstuctionMemory, getCurrentRow());
            if (adress >= data.Length)
                throw new RuntimeException(Language.IndexMemoryOf, getCurrentRow());
            return adress;
        }

        private int getCurrentRow()
        {
            return source[ActiveIndex].row.Index + 1;
        }

        private void addError(ErrorMessage msg)
        {
            Errors.Add(msg);
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
                    source[ActiveIndex].row.ResetFlag(CodeEditBox.RowFlag.Run);

                Status = State.Ready;
                waitEvent.Set();
            }
        }
    }
}