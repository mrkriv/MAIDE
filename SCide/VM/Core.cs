using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Threading;
using ASM.UI;

namespace ASM.VM
{
    public class Core
    {
        public static readonly BindingSource Errors;

        public enum State
        {
            NoBuild,
            Ready,
            Launched,
            Pause,
            Finish,
            Error,
        }

        private List<Operation> source = new List<Operation>();
        private ManualResetEvent waitEvent;
        private CombineRows code;
        private EventHandler stateChanged;
        private State status;
        
        public Dictionary<string, int> Links = new Dictionary<string, int>();
        public Dictionary<string, int> DataByte = new Dictionary<string, int>();
        public List<Register> Registers;
        public List<byte> Data = new List<byte>();
        public Stack<int> Stack = new Stack<int>();
        public int ActiveIndex = 0;
        public int TotalTick { get; private set; }

        public State Status
        {
            get { return status; }
            private set
            {
                status = value;
                stateChanged.Invoke(this, null);
            }
        }

        public event EventHandler StateChanged
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

        public class Operation
        {
            public CodeEditBox.RowReadonly row;
            public string operation;
            public object[] args;
            public MethodInfo method;
        }

        static Core()
        {
            Errors = new BindingSource();
            Errors.Add(new ErrorMessageRow("", 0));
            Errors.RemoveAt(0);
        }

        public Core()
        {
            Operators.ActiveCore = this;

            stateChanged = (s, e) => { };
            waitEvent = new ManualResetEvent(true);
            RecreateRegisters();
            Properties.Settings.Default.SettingsSaving += SettingsSaving;
        }

        private void SettingsSaving(object sender, System.ComponentModel.CancelEventArgs e)
        {
            RecreateRegisters();
        }

        public void RecreateRegisters()
        {
            Registers = new List<Register>();

            var stetting = Properties.Settings.Default;
            if (stetting.Register32 != null)
            {
                foreach (string name in stetting.Register32)
                {
                    if (name != "")
                        Registers.Add(new Register32(name.Replace("\n", "")));
                }
            }
            if (stetting.Register16 != null)
            {
                foreach (string name in stetting.Register16)
                {
                    if (name != "")
                        Registers.Add(new Register16(name.Replace("\n", ""), new Register32("__crutch_" + name)));
                }
            }
            if (stetting.Register8 != null)
            {
                foreach (string name in stetting.Register8)
                {
                    if (name != "")
                        Registers.Add(new Register8(name.Replace("\n", ""), new Register32("__crutch_" + name)));
                }
            }

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
            if (status != State.Ready)
                return;

            Stack.Clear();
            ActiveIndex = 0;
            TotalTick = 0;
            waitEvent.Set();
            Status = State.Launched;

            int i = 500;
            while (Console.Instance == null)
            {
                Thread.Sleep(5);
                if (--i == 0)
                {
                    MessageBox.Show("Консоль не отвечает.");
                    return;
                }
            }

            while (ActiveIndex < source.Count && status == State.Launched)
            {
                Operation op = source[ActiveIndex];

                if (TotalTick > Properties.Settings.Default.TotalTickLimit)
                {
                    if (MessageBox.Show("Возможно, Ваша программа зациклилась.\nОстановть ее?", "Слишком много операций", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        Status = State.Error;
                        return;
                    }
                    TotalTick = -TotalTick;
                }

                if (op.row.IsFlag(CodeEditBox.RowFlag.Breakpoint))
                    Pause();

                op.row.SetFlag(CodeEditBox.RowFlag.Run);

                waitEvent.WaitOne();

                if (status == State.Launched)
                {
                    if (Program.Debug)
                        op.method.Invoke(null, op.args);
                    else
                    {
                        try
                        {
                            op.method.Invoke(null, op.args);
                        }
                        catch
                        {
                            MessageBox.Show(string.Format("Не обрабатываемая ошибка на строке {0}.\nОтладка не возможна.", op.row.Index + 1));
                            Status = State.Error;
                        }
                    }
                }

                op.row.ResetFlag(CodeEditBox.RowFlag.Run);

                ActiveIndex++;
                TotalTick++;
            }

            Status = State.Finish;

            Console.MoveCaretToEnd();
            Console.Write("\nPress any key to continue.");
            Console.ReadKey();
        }

        public bool Build(CombineRows rows)
        {
            Stop();
            source.Clear();
            Links.Clear();
            DataByte.Clear();
            Errors.Clear();
            Data.Clear();

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

            for (int i = 0; i < code.Length; i++)
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
                        addError(new ErrorMessageRow(string.Format("Метка '{0} уже определена.", text[0]), i));
                }
                else
                    data = text[0];

                text = data.Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);

                if (text.Length != 0)
                {
                    if (text[0] == "byte")
                    {
                        if (DataByte.ContainsKey(text[0]))
                            addError(new ErrorMessageRow(string.Format("Метка '{0} уже определена.", text[0]), i));
                        else
                        {
                            DataByte.Add(Links.Last().Key, Data.Count);
                            string[] values = text[1].Split(',');
                            foreach (string val in values)
                            {
                                string v = val.Trim(' ');
                                if (v.StartsWith("'"))
                                    Data.AddRange(v.Substring(1, v.Length - 2).Select(c => (byte)c));
                                else
                                    Data.Add((byte)char.Parse(v));
                            }
                        }
                    }
                    else
                        result.Add(code[i], text);
                }
            }

            if (result.Count == 0)
                addError(new ErrorMessageRow("Нужен код!", 0));

            return result;
        }

        public Operation Link(CodeEditBox.RowReadonly row, string[] text)
        {
            Operation op = new Operation();
            op.operation = text[0];
            op.row = row;

            foreach (MethodInfo method in Operators.OperationsList)
            {
                if (method.Name == op.operation)
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
            addError(new ErrorMessageRow(string.Format("Операция '{0}' не определена.", op.operation), row.Index + 1));
            return null;
        }

        public bool ParseOperation(Operation operation, string[] input)
        {
            ParameterInfo[] paramInfo = operation.method.GetParameters();
            List<object> output = new List<object>();
            ErrorMessageRow error = new ErrorMessageRow(null, operation.row.Index + 1);

            if (input.Length != paramInfo.Length)
                error.Message = string.Format("Операция '{0}' имеет {1} оргумент(а).", operation.operation, paramInfo.Length);

            for (int i = 0; i < input.Length && error.Message == null; i++)
            {
                Type needType = paramInfo[i].ParameterType;
                int oldCount = output.Count;

                if (needType.BaseType == typeof(Register))
                {
                    Register reg = GetRegister(input[i].ToLower());
                    if (reg == null)
                        error.Message = string.Format("Регистр '{0}' не сущесвует.", input[i]);

                    if (!needType.IsInstanceOfType(reg))
                        error.Message = string.Format("Регистр '{0}' не может использоватся здесь.", input[i]);

                    output.Add(reg);
                }
                else if (needType == typeof(int) || needType == typeof(short) || needType == typeof(char) || needType == typeof(byte))
                {
                    if (input[i][0] == '#')
                    {
                        int result;
                        input[i] = input[i].Substring(1);

                        if (!int.TryParse(input[i], out result))
                            error.Message = string.Format("'{0}' не является числом.", input[i]);
                        else
                            output.Add(Convert.ChangeType(result, needType));
                    }
                }
                else if (needType == typeof(DataIndex))
                {
                    if (!DataByte.ContainsKey(input[i]))
                        error.Message = string.Format("Метка '{0}' ресурса не определена.", input[i]);
                    else
                        output.Add(new DataIndex(DataByte[input[i]]));
                }
                else if (needType == typeof(LineIndex))
                {
                    if (input[i].Contains('['))
                    {
                        input[i] = input[i].Substring(0, input[i].Length - 1);
                        string[] temp = input[i].Split('[');
                        int line;
                        if (!int.TryParse(temp[0], out line))
                        {
                            if (temp[0][0] == '#')
                            {
                                temp[0] = temp[0].Substring(1);
                                if (!Links.ContainsKey(temp[0]))
                                    error.Message = string.Format("Метка '{0}' не определена.", temp[0]);
                                else
                                    line = Links[temp[0]];
                            }
                            else
                                error.Message = string.Format("Ничего не понятно, наверное, это эльфийский.");
                        }
                        Register32 reg = GetRegister(temp[1].ToLower()) as Register32;
                        if (reg == null)
                            error.Message = string.Format("Регистр '{0}' не сущесвует или не может использоватся здесь.", temp[1]);
                        else
                            output.Add(new LineIndex(line, reg));
                    }
                    else
                    {
                        if (input[i][0] == '#')
                            input[i] = input[i].Substring(1);

                        if (!Links.ContainsKey(input[i]))
                            error.Message = string.Format("Метка '{0}' не определена.", input[i]);
                        else
                            output.Add(new LineIndex(Links[input[i]]));
                    }
                }

                if (oldCount == output.Count && error.Message != "")
                    error.Message = string.Format("Недопустимый пораметр {0}.", input[i]);
            }

            operation.args = output.ToArray();

            if (error.Message == null)
                return true;

            addError(error);
            return false;
        }

        private void addError(ErrorMessageRow msg)
        {
            Errors.Add(msg);
        }

        public void Pause()
        {
            waitEvent.Reset();
            Status = State.Pause;
        }

        public void Resume()
        {
            if (status == State.Pause)
            {
                waitEvent.Set();
                Status = State.Launched;
            }
        }

        public void Stop()
        {
            if (status == State.Launched || status == State.Pause)
            {
                waitEvent.Set();
                Status = State.Ready;
            }
        }

        public void Destroy()
        {
            Status = State.NoBuild;
        }
    }
}