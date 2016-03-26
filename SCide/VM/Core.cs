using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Threading;

namespace ASM
{
    internal class Core
    {
        public static readonly BindingSource Errors;

        private List<Operation> source;
        private ManualResetEvent waitEvent;
        private CodeEditBox code;

        public const int TotalTickLimit = 1500;
        public Dictionary<string, int> Links;
        public List<Register> Registers;
        public Dictionary<int, List<byte>> DataByte;
        public Stack<int> Stack;
        public bool isFinished = false;
        public bool isPaused = false;
        public int ActiveIndex = 0;
        public int TotalTick = 0;

        public class Operation
        {
            public Line line;
            public string name;
            public object[] arg;
            public MethodInfo method;
        }

        static Core()
        {
            Errors = new BindingSource();
            Errors.Add(new ErrorLine("", 0));
            Errors.RemoveAt(0);
        }

        public Core()
        {
            Operators.ActiveCore = this;

            waitEvent = new ManualResetEvent(true);
            Stack = new Stack<int>();
            Registers = new List<Register>();
            DataByte = new Dictionary<int, List<byte>>();
            Links = new Dictionary<string, int>();

            Registers.Add(new Register32("a"));
            Registers.Add(new Register32("b"));
            Registers.Add(new Register32("c"));
            Registers.Add(new Register32("d"));
            Registers.Add(new Register32("x"));
            Registers.Add(new Register32("n"));

            for (int r = 0; r <= 11; r++)
                Registers.Add(new Register32("r" + r));

            Registers.Add(new RegisterFlag("flag"));
        }

        public Register getRegister(string name)
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
            while (Console.Instance == null)
                Thread.Sleep(5);

            ActiveIndex = 0;
            TotalTick = 0;

            while (ActiveIndex < source.Count && !isFinished)
                Loop();

            isFinished = true;

            Console.MoveCaretToEnd();
            Console.Write("\nPress any key to continue.");
            Console.ReadKey();
        }

        void Loop()
        {
            Operation op = source[ActiveIndex];

            if (TotalTick > TotalTickLimit)
            {
                if (MessageBox.Show("Возможно, ваша программа зациклилась.\nОстановть ее?", "Слишком много операций", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    isFinished = true;
                    return;
                }
                TotalTick = -TotalTick;
            }

            //if (markers.Find(m => m.Number == 0) != null)
            //    Pause();

            waitEvent.WaitOne();

            ///*try
            //{*/
            op.method.Invoke(null, op.arg);
            //}
            //    catch
            //    {
            //        MessageBox.Show(string.Format("Не обрабатываемая ошибка на строке {0}.\nОтладка не возможна.", ActiveIndex));
            //        isFinished = true;
            //    }

            ActiveIndex++;
            TotalTick++;
        }

        public bool Build(CodeEditBox codeBox)
        {
            Errors.Clear();
            code = codeBox;

            var lines = Preprocessor();

            if (Errors.Count != 0)
                return false;

            source = new List<Operation>(lines.Count);

            foreach (var line in lines)
            {
                Operation op = Link(line.Key, line.Value);
                if (op != null)
                    source.Add(op);
            }

            return Errors.Count == 0;
        }

        public Dictionary<Line, string[]> Preprocessor()
        {
            Dictionary<Line, string[]> result = new Dictionary<Line, string[]>();

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
                        Errors.Add(new ErrorLine(string.Format("Метка '{0} уже определена.", text[0]), i));
                }
                else
                    data = text[0];
                 
                text = data.Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);

                if (text.Length != 0)
                    result.Add(new Line(code[i].ToString(), i), text);
            }

            if (result.Count == 0)
                Errors.Add(new ErrorLine("Нужен код!", 0));

            return result;
        }

        public Operation Link(Line line, string[] text)
        {
            Operation op = new Operation();
            op.name = text[0];
            op.line = line;

            foreach (MethodInfo method in Operators.OperationsList)
            {
                if (method.Name == op.name)
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

            Errors.Add(new ErrorLine(string.Format("Операция '{0}' не определена.", op.name), line.Number));
            return null;
        }

        public bool ParseOperation(Operation operation, string[] input)
        {
            ParameterInfo[] paramInfo = operation.method.GetParameters();
            List<object> output = new List<object>();
            ErrorLine error = null;

            if (input.Length != paramInfo.Length)
                error = new ErrorLine(string.Format("Операция '{0}' имеет {1} оргумент(а).", operation.name, paramInfo.Length), operation.line.Number);

            for (int i = 0; i < input.Length && error == null; i++)
            {
                Type needType = paramInfo[i].ParameterType;
                int oldCount = output.Count;

                if (needType.BaseType == typeof(Register))
                {
                    Register reg = getRegister(input[i].ToLower());
                    if (reg == null)
                        error = new ErrorLine(string.Format("Регистр '{0}' не сущесвует.", input[i]), operation.line.Number);

                    if (!needType.IsInstanceOfType(reg))
                        error = (new ErrorLine(string.Format("Регистр '{0}' не может использоватся здесь.", input[i]), operation.line.Number));

                    output.Add(reg);
                }
                else if (needType == typeof(int) || needType == typeof(short) || needType == typeof(char) || needType == typeof(byte))
                {
                    if (input[i][0] == '#')
                    {
                        int result;
                        input[i] = input[i].Substring(1);

                        if (!int.TryParse(input[i], out result))
                            error = new ErrorLine(string.Format("'{0}' не является числом.", input[i]), operation.line.Number);
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
                                    error = new ErrorLine(string.Format("Метка '{0}' не определена.", temp[0]), operation.line.Number);
                                else
                                    line = Links[temp[0]];
                            }
                            else
                                Errors.Add(new ErrorLine(string.Format("Ничего не понятно, наверное это эльфийский."), operation.line.Number));
                        }
                        Register32 reg = getRegister(temp[1].ToLower()) as Register32;
                        if (reg == null)
                            error = new ErrorLine(string.Format("Регистр '{0}' не сущесвует или не может использоватся здесь.", temp[1]), operation.line.Number);
                        else
                            output.Add(new DataIndex(line, reg));
                    }
                    else
                    {
                        if (input[i][0] == '#')
                            input[i] = input[i].Substring(1);

                        if (!Links.ContainsKey(input[i]))
                            error = new ErrorLine(string.Format("Метка '{0}' не определена.", input[i]), operation.line.Number);
                        else
                            output.Add(new DataIndex(Links[input[i]]));
                    }
                }

                if (oldCount == output.Count)
                    error = new ErrorLine(string.Format("Не допустимый пораметр {0}.", operation.name, input[i]), operation.line.Number);
            }

            operation.arg = output.ToArray();

            if (error == null)
                return true;

            Errors.Add(error);
            return false;
        }

        public void Pause()
        {
            waitEvent.Reset();
            isPaused = true;
        }

        public void Resume()
        {
            waitEvent.Set();
            isPaused = false;
        }
    }
}