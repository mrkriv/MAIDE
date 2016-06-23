using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Threading;
using ASM.UI;
using ASM.Utilit;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace ASM.VM
{
    public class Core
    {
        public static readonly ObservableCollection<ErrorMessageRow> Errors = new ObservableCollection<ErrorMessageRow>();

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
        private int total;
        private State status;
        private int dataZoneOffest;
        private byte[] data;

        public readonly Dictionary<string, int> Links = new Dictionary<string, int>();
        public readonly List<Register> Registers = new List<Register>();
        public readonly Stack<int> Stack = new Stack<int>();
        public int ActiveIndex = 0;

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

        public StringCollection RegNames32
        {
            get { return Properties.Settings.Default.Register32; }
            set
            {
                if (value != null)
                {
                    Registers.RemoveAll(reg => reg is Register32);
                    foreach (string name in value)
                    {
                        if (name != "")
                            Registers.Add(new Register32(name.Replace("\n", "")));
                    }
                }
            }
        }

        public StringCollection RegNames16
        {
            get { return Properties.Settings.Default.Register16; }
            set
            {
                Registers.RemoveAll(reg => reg is Register16);
                if (value != null)
                {
                    foreach (string name in value)
                    {
                        if (name != "")
                            Registers.Add(new Register16(name.Replace("\n", ""), new Register32("__crutch_" + name)));
                    }
                }
            }
        }

        public StringCollection RegNames8
        {
            get { return Properties.Settings.Default.Register8; }
            set
            {
                if (value != null)
                {
                    Registers.RemoveAll(reg => reg is Register8);
                    foreach (string name in value)
                    {
                        if (name != "")
                            Registers.Add(new Register8(name.Replace("\n", ""), new Register32("__crutch_" + name)));
                    }
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
            new PropertyJoin(this, "RegNames32", Properties.Settings.Default, "Register32");
            new PropertyJoin(this, "RegNames16", Properties.Settings.Default, "Register16");
            new PropertyJoin(this, "RegNames8", Properties.Settings.Default, "Register8");
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

            try
            {
                while (ActiveIndex < source.Count && status == State.Launched)
                {
                    Operation op = source[ActiveIndex];

                    if (total > Properties.Settings.Default.TotalTickLimit)
                    {
                        if (MessageBox.Show("Возможно, Ваша программа зациклилась.\nОстановть ее?", "Слишком много операций", MessageBoxButtons.YesNo) == DialogResult.Yes)
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

                    try
                    {
                        op.method.Invoke(null, op.args);
                    }
                    catch (TargetInvocationException e)
                    {
                        if (e.InnerException is RuntimeException)
                            throw e.InnerException;
                        throw new RuntimeException("Не обрабатываемая ошибка при выполнении инструкуии " + op.method.Name, getCurrentRow());
                    }
                    finally
                    {
                        op.row.ResetFlag(CodeEditBox.RowFlag.Run);
                    }

                    if (ActiveIndex >= source.Count)
                        throw new RuntimeException("Не возможно выполнить инструкцию, эта пямять не для инструкций", op.row.Index + 1);

                    ActiveIndex++;
                    total++;
                }
                Status = State.Finish;
            }
            catch (RuntimeException e)
            {
                Console.WriteLine("Строка " + e.Row + ": " + e.Message + "\n");
                Status = State.Error;
            }

            Console.Write("Нажмите любую клавишу для продолжения");
            Console.MoveCaretToEnd();
            Console.ReadKey();
        }

        public bool Build(CombineRows rows)
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
                    if (text[0] == "byte" || text[0] == "word")
                    {
                        dataZone.Add(Links.Last().Key, dataList.Count);
                        string[] values = text[1].Split(',');
                        foreach (string val in values)
                        {
                            string v = val.Trim(' ');
                            if (text[0] == "byte")
                            {
                                if (v[0] == '\'' || v[0] == '"')
                                    dataList.AddRange(v.Substring(1, v.Length - 2).Select(c => (byte)c));
                                else
                                    dataList.Add((byte)int.Parse(v));
                            }
                            else
                                dataList.AddRange(BitConverter.GetBytes(int.Parse(v)));
                        }
                    }
                    else if (text[0] == "equ")
                        Links[Links.Keys.Last()] = int.Parse(text[1]);
                    else
                        result.Add(code[i], text);
                }
            }

            if (result.Count == 0)
                addError(new ErrorMessageRow("Нужен код!", 0));
            
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
                string value = input[i];

                value = value.TrimStart('#');
                if (needType.BaseType == typeof(Register))
                {
                    Register reg = GetRegister(value.ToLower());
                    if (reg == null)
                        error.Message = string.Format("Регистр '{0}' не сущесвует.", value);

                    if (!needType.IsInstanceOfType(reg))
                        error.Message = string.Format("Регистр '{0}' не может использоватся здесь.", value);

                    output.Add(reg);
                }
                else if (needType == typeof(int) || needType == typeof(short) || needType == typeof(char) || needType == typeof(byte))
                {
                    int result;
                    if (!int.TryParse(value, out result))
                        error.Message = string.Format("'{0}' не является числом.", value);
                    else
                        output.Add(Convert.ChangeType(result, needType));
                }
                else if (needType == typeof(Link))
                {
                    string[] temp = value.Split('[');
                    Link link = new Link();
                    if (!int.TryParse(temp[0], out link.Line))
                    {
                        if (!Links.ContainsKey(temp[0]))
                            error.Message = string.Format("Метка '{0}' не определена.", temp[0]);
                        else
                            link.Line = Links[temp[0]];
                    }
                    if (temp.Length == 2)
                    {
                        temp[1] = temp[1].Remove(temp[1].Length - 1).ToLower();
                        Register32 reg = GetRegister(temp[1]) as Register32;
                        if (reg == null)
                            error.Message = string.Format("Регистр '{0}' не сущесвует или не может использоватся здесь.", temp[1]);
                        else
                            link.reg32 = reg;
                    }
                    output.Add(link);
                }

                if (oldCount == output.Count && error.Message != "")
                    error.Message = string.Format("Недопустимый пораметр {0}.", value);
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
                throw new RuntimeException("Память с инструкциями не доступна здесь", getCurrentRow());
            if (adress >= data.Length)
                throw new RuntimeException("Адресс за пределами выделенной памяти", getCurrentRow());
            return adress;
        }

        private int getCurrentRow()
        {
            return source[ActiveIndex].row.Index + 1;
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