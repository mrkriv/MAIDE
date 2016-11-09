namespace MAIDE.VM
{
    public static class Operators
    {
        public static Core Core;

        private static T reg<T>(string name) where T : Register
        {
            return (T)RegisterManager.GetRegister(name);
        }

        [Descriptor(OperationType.Action, "Выводит на консоль '{0}'-й байт из регистра 'a'")]
        public static void wd(int n)
        {
            n %= 4;
            int value = reg<Register32>("a").Value >> n * 8;
            Console.Write((char)(value % 256));
        }

        [Descriptor(OperationType.Action, "Записывает в регистр '{0}' байт считаный с консоли (смещение пока не работает)")]
        public static void rd(int offest)
        {
            reg<Register32>("a").Value = Console.ReadKey();
        }

        [Descriptor(OperationType.Action, "Загружает в регистр '{0}' 4 байта из памяти по адресу '{1}'")]
        public static void ldb(Register32 reg, Pointer index)
        {
            //reg.Value = ActiveCore.GetWord(index.GetValue());
        }

        [Descriptor(OperationType.Jump, "Безусловный переход, в стек помещается текущий адрес")]
        public static void call(Pointer index)
        {
            Core.Stack.Push(Core.Pointer);
            Core.Pointer = index.Row - 1;
        }

        [Descriptor(OperationType.Jump, "Переходит по адресу взятому со стека, если стек пуст то завершает программу")]
        public static void ret()
        {
            if (Core.Stack.Count != 0)
                Core.Pointer = Core.Stack.Pop();
            else
                Core.Stop();
        }

        [Descriptor(OperationType.Action, "Кладет на вершину стека все байты регистра '{0}'")]
        public static void push(Register32 reg)
        {
            Core.Stack.Push(reg.Value);
        }

        [Descriptor(OperationType.Action, "Снимает с вершины стека 32 байта и помещает их в регистр '{0}'")]
        public static void pop(Register32 reg)
        {
            if (Core.Stack.Count != 0)
                reg.Value = Core.Stack.Pop();
        }

        [Descriptor(OperationType.Action, "Копирует данные из регистра '{0}' в регистр '{1}'")]
        public static void mov(Register32 a, Register32 b)
        {
            a.Value = b.Value;
        }

        [Descriptor(OperationType.Action, "Выполняет сравнение регистра '{0}' с {1}")]
        public static void comp(Register32 reg, int value)
        {
            _comp(reg.Value - value);
        }

        [Descriptor(OperationType.Action, "Выполняет сравнение первого байта регистра '{0}' с {1}")]
        public static void compb(Register32 reg, int value)
        {
            _comp((char)reg.Value - (char)value);
        }

        [Descriptor(OperationType.Action, "Выполняет сравнение регистров '{0}' и {1}")]
        public static void compr(Register32 a, Register32 b)
        {
            _comp(a.Value - b.Value);
        }

        [Descriptor(OperationType.Action, "")]
        public static void addr(Register32 a, Register32 b)
        {
            _comp(a.Value + b.Value);
            a.Value += b.Value;
        }

        [Descriptor(OperationType.Action, "")]
        public static void subr(Register32 a, Register32 b)
        {
            _comp(a.Value - b.Value);
            a.Value -= b.Value;
        }

        [Descriptor(OperationType.Action, "")]
        public static void mulr(Register32 a, Register32 b)
        {
            _comp(a.Value * b.Value);
            a.Value *= b.Value;
        }

        [Descriptor(OperationType.Action, "")]
        public static void divr(Register32 a, Register32 b)
        {
            _comp(a.Value / b.Value);
            a.Value /= b.Value;
        }

        [Descriptor(OperationType.Action, "")]
        public static void add(Register32 reg, int value)
        {
            _comp(reg.Value + value);
            reg.Value += value;
        }

        [Descriptor(OperationType.Action, "")]
        public static void sub(Register32 reg, int value)
        {
            _comp(reg.Value - value);
            reg.Value -= value;
        }

        [Descriptor(OperationType.Action, "")]
        public static void mul(Register32 reg, int value)
        {
            _comp(reg.Value * value);
            reg.Value *= value;
        }

        [Descriptor(OperationType.Action, "")]
        public static void div(Register32 reg, int value)
        {
            _comp(reg.Value / value);
            reg.Value /= value;
        }

        [Descriptor(OperationType.Action, "Копирует значение из памяти в регистр '{0}'")]
        public static void ld(Register32 reg, int value)
        {
            reg.Value = value;
        }

        [Descriptor(OperationType.Action, "Сбрасывает регистр '{0}'")]
        public static void clear(Register32 reg)
        {
            reg.Value = 0;
        }

        [Descriptor(OperationType.Jump, "Безусловный переход")]
        public static void jmp(Pointer index)
        {
            Core.Pointer = index.Row - 1;
        }

        [Descriptor(OperationType.Condition, "Переход на заданную метку, если операнды равны")]
        public static void jeq(Pointer index)
        {
            if (RegisterManager.FlagReg.ZF)
                Core.Pointer = index.Row - 1;
        }

        [Descriptor(OperationType.Condition, "Переход на заданную метку, если первый операнд больше нуля")]
        public static void jgt(Pointer index)
        {
            if (!RegisterManager.FlagReg.ZF && RegisterManager.FlagReg.SF)
                Core.Pointer = index.Row - 1;
        }

        [Descriptor(OperationType.Condition, "Переход на заданную метку, если первый операнд меньше второго")]
        public static void jlt(Pointer index)
        {
            if (RegisterManager.FlagReg.SF != RegisterManager.FlagReg.OF)
                Core.Pointer = index.Row - 1;
        }

        [Descriptor(OperationType.Condition, "Переход на заданную метку, если первый операнд больше второго")]
        public static void jge(Pointer index)
        {
            Register32 a = reg<Register32>("a");
            if (RegisterManager.FlagReg.SF == RegisterManager.FlagReg.OF)
                Core.Pointer = index.Row - 1;
        }

        [Descriptor(OperationType.Condition, "Переход на заданную метку, если первый операнд больше второго")]
        public static void jпе(Pointer index)
        {
            if (!RegisterManager.FlagReg.ZF)
                Core.Pointer = index.Row - 1;
        }

        [Descriptor(OperationType.Condition, "Инкремент регистра {0}")]
        public static void inc(Register32 a)
        {
            a.Value++;
            _comp(a.Value - a.Value);
        }

        [Descriptor(OperationType.Condition, "Инкремент регистра {0} и сравнение результата с регистром {1}")]
        public static void incr(Register32 a, Register32 b)
        {
            a.Value++;
            _comp(a.Value - b.Value);
        }

        [Descriptor(OperationType.Action, "Пустой такт")]
        public static void nop() { }

        private static void _comp(int value)
        {
            RegisterManager.FlagReg.ZF = value == 0;
            RegisterManager.FlagReg.SF = value >= 0;
            RegisterManager.FlagReg.CF = false;
            RegisterManager.FlagReg.OF = false;
            RegisterManager.FlagReg.PF = value % 2 == 0;
        }
    }
}