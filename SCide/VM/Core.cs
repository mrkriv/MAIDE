using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Threading;
using ASM.UI;

namespace ASM.VM
{
    internal class Core
    {
        public static readonly BindingSource Errors;

        private List<Operation> source = new List<Operation>();
        private ManualResetEvent waitEvent;
        private CombineRows code;

        public Dictionary<string, int> Links = new Dictionary<string, int>();
        public Dictionary<int, List<byte>> DataByte = new Dictionary<int, List<byte>>();
        public List<Register> Registers;
        public Stack<int> Stack = new Stack<int>();
        public int ActiveIndex = 0;
        public int TotalTick { get; private set; }
        public bool IsFinished { get; private set; }
        public bool IsPaused { get; private set; }
        public bool IsReady { get; private set; }

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
            if (!IsReady)
                return;

            Stack.Clear();
            ActiveIndex = 0;
            TotalTick = 0;
            IsFinished = false;
            Resume();

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

            while (ActiveIndex < source.Count && !IsFinished)
            {
                Operation op = source[ActiveIndex];

                if (TotalTick > Properties.Settings.Default.TotalTickLimit)
                {
                    if (MessageBox.Show("Возможно, Ваша программа зациклилась.\nОстановть ее?", "Слишком много операций", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        IsFinished = true;
                        return;
                    }
                    TotalTick = -TotalTick;
                }

                if (op.row.Flag)
                    Pause();

                waitEvent.WaitOne();

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
                        IsFinished = true;
                    }
                }

                ActiveIndex++;
                TotalTick++;
            }

            IsFinished = true;

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
            IsReady = false;

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

            IsReady = Errors.Count == 0;
            return IsReady;
        }

        public Dictionary<CodeEditBox.RowReadonly, string[]> Preprocessor()
        {
            var result = new Dictionary<CodeEditBox.RowReadonly, string[]>();

            for (int i = 0; i < code.Length; i++)
            {
                string[] text = code[i].ToString().Replace('\t', ' ').Trim(' ').Split(';')[0].Split(':');
                string data;

                if (text[0] == "")
                    continue;

                if (text.Length != 1)
                {
                    data = text[1];

                    if (!Links.ContainsKey(text[0]))
                        Links.Add(text[0], result.Count);
                    else
                        Errors.Add(new ErrorMessageRow(string.Format("Метка '{0} уже определена.", text[0]), i));
                }
                else
                    data = text[0];
                 
                text = data.Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);

                if (text.Length != 0)
                    result.Add(code[i], text);
            }

            if (result.Count == 0)
                Errors.Add(new ErrorMessageRow("Нужен код!", 0));

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
            Errors.Add(new ErrorMessageRow(string.Format("Операция '{0}' не определена.", op.operation), row.Index+1));
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
                                error.Message = string.Format("Ничего не понятно, наверное это эльфийский.");
                        }
                        Register32 reg = GetRegister(temp[1].ToLower()) as Register32;
                        if (reg == null)
                            error.Message = string.Format("Регистр '{0}' не сущесвует или не может использоватся здесь.", temp[1]);
                        else
                            output.Add(new DataIndex(line, reg));
                    }
                    else
                    {
                        if (input[i][0] == '#')
                            input[i] = input[i].Substring(1);

                        if (!Links.ContainsKey(input[i]))
                            error.Message = string.Format("Метка '{0}' не определена.", input[i]);
                        else
                            output.Add(new DataIndex(Links[input[i]]));
                    }
                }

                if (oldCount == output.Count)
                    error.Message = string.Format("Не допустимый пораметр {0}.", operation.operation, input[i]);
            }

            operation.args = output.ToArray();

            if (error.Message == null)
                return true;

            Errors.Add(error);
            return false;
        }

        public void Pause()
        {
            waitEvent.Reset();
            IsPaused = true;
        }

        public void Resume()
        {
            waitEvent.Set();
            IsPaused = false;
        }

        public void Stop()
        {
            Resume();
            IsFinished = true;
        }

        public void Destroy()
        {
            Stop();
            IsPaused = false;
            IsReady = false;
        }
    }
}