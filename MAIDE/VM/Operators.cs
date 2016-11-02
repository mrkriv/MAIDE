using System.Linq;
using System.Reflection;
using MAIDE;

namespace MAIDE.VM
{
    public static class Operators
    {
        public static Core ActiveCore;
        public static readonly MethodInfo[] OperationsList;

        static Operators()
        {
            var mhod = typeof(Operators).GetMethods();
            OperationsList = mhod.Where(w => w.CustomAttributes.Any(e => e.AttributeType == typeof(DescriptorAttribute))).ToArray();
        }

        private static T reg<T>(string name) where T : Register
        {
            return (T)ActiveCore.GetRegister(name);
        }

        [Descriptor(Type.Action, "Выводит на консоль '{0}'-й байт из регистра 'a'")]
        public static void wd(int n)
        {
            n %= 4;
            int value = reg<Register32>("a").Value >> n * 8;
            Console.Write((char)(value % 256));
        }

        [Descriptor(Type.Action, "Записывает в регистр '{0}' байт считаный с консоли (смещение пока не работает)")]
        public static void rd(int offest)
        {
            reg<Register32>("a").Value = Console.ReadKey();
        }

        [Descriptor(Type.Action, "Загружает в регистр '{0}' 4 байта из памяти по адресу '{1}'")]
        public static void ldb(Register32 reg, Link index)
        {
            reg.Value = ActiveCore.GetWord(index.GetValue());
        }

        [Descriptor(Type.Jump, "Безусловный переход, в стек помещается текущий адрес")]
        public static void call(Link index)
        {
            ActiveCore.Stack.Push(ActiveCore.ActiveIndex);
            ActiveCore.ActiveIndex = index.Line - 1;
        }

        [Descriptor(Type.Jump, "Переходит по адресу взятому со стека, если стек пуст то завершает программу")]
        public static void ret()
        {
            if (ActiveCore.Stack.Count != 0)
                ActiveCore.ActiveIndex = ActiveCore.Stack.Pop();
            else
                ActiveCore.Stop();
        }

        [Descriptor(Type.Action, "Кладет на вершину стека все байты регистра '{0}'")]
        public static void push(Register32 reg)
        {
            ActiveCore.Stack.Push(reg.Value);
        }

        [Descriptor(Type.Action, "Снимает с вершины стека 32 байта и помещает их в регистр '{0}'")]
        public static void pop(Register32 reg)
        {
            if (ActiveCore.Stack.Count != 0)
                reg.Value = ActiveCore.Stack.Pop();
        }

        [Descriptor(Type.Action, "Копирует данные из регистра '{0}' в регистр '{1}'")]
        public static void mov(Register32 a, Register32 b)
        {
            a.Value = b.Value;
        }

        [Descriptor(Type.Action, "Выполняет сравнение регистра '{0}' с {1}")]
        public static void comp(Register32 reg, int value)
        {
            _comp(reg.Value - value);
        }

        [Descriptor(Type.Action, "Выполняет сравнение первого байта регистра '{0}' с {1}")]
        public static void compb(Register32 reg, int value)
        {
            _comp((char)reg.Value - (char)value);
        }

        [Descriptor(Type.Action, "Выполняет сравнение регистров '{0}' и {1}")]
        public static void compr(Register32 a, Register32 b)
        {
            _comp(a.Value - b.Value);
        }

        [Descriptor(Type.Action, "")]
        public static void addr(Register32 a, Register32 b)
        {
            _comp(a.Value + b.Value);
            a.Value += b.Value;
        }

        [Descriptor(Type.Action, "")]
        public static void subr(Register32 a, Register32 b)
        {
            _comp(a.Value - b.Value);
            a.Value -= b.Value;
        }

        [Descriptor(Type.Action, "")]
        public static void mulr(Register32 a, Register32 b)
        {
            _comp(a.Value * b.Value);
            a.Value *= b.Value;
        }

        [Descriptor(Type.Action, "")]
        public static void divr(Register32 a, Register32 b)
        {
            _comp(a.Value / b.Value);
            a.Value /= b.Value;
        }

        [Descriptor(Type.Action, "")]
        public static void add(Register32 reg, int value)
        {
            _comp(reg.Value + value);
            reg.Value += value;
        }

        [Descriptor(Type.Action, "")]
        public static void sub(Register32 reg, int value)
        {
            _comp(reg.Value - value);
            reg.Value -= value;
        }

        [Descriptor(Type.Action, "")]
        public static void mul(Register32 reg, int value)
        {
            _comp(reg.Value * value);
            reg.Value *= value;
        }

        [Descriptor(Type.Action, "")]
        public static void div(Register32 reg, int value)
        {
            _comp(reg.Value / value);
            reg.Value /= value;
        }

        [Descriptor(Type.Action, "Копирует значение из памяти в регистр '{0}'")]
        public static void ld(Register32 reg, int value)
        {
            reg.Value = value;
        }

        [Descriptor(Type.Action, "Сбрасывает регистр '{0}'")]
        public static void clear(Register32 reg)
        {
            reg.Value = 0;
        }

        [Descriptor(Type.Jump, "Безусловный переход")]
        public static void jmp(Link index)
        {
            ActiveCore.ActiveIndex = index.Line - 1;
        }

        [Descriptor(Type.Condition, "Переход на заданную метку, если операнды равны")]
        public static void jeq(Link index)
        {
            if (ActiveCore.FlagReg.ZF)
                ActiveCore.ActiveIndex = index.Line - 1;
        }

        [Descriptor(Type.Condition, "Переход на заданную метку, если первый операнд больше нуля")]
        public static void jgt(Link index)
        {
            if (!ActiveCore.FlagReg.ZF && ActiveCore.FlagReg.SF)
                ActiveCore.ActiveIndex = index.Line - 1;
        }

        [Descriptor(Type.Condition, "Переход на заданную метку, если первый операнд меньше второго")]
        public static void jlt(Link index)
        {
            if (ActiveCore.FlagReg.SF != ActiveCore.FlagReg.OF)
                ActiveCore.ActiveIndex = index.Line - 1;
        }

        [Descriptor(Type.Condition, "Переход на заданную метку, если первый операнд больше второго")]
        public static void jge(Link index)
        {
            Register32 a = reg<Register32>("a");
            if (ActiveCore.FlagReg.SF == ActiveCore.FlagReg.OF)
                ActiveCore.ActiveIndex = index.Line - 1;
        }

        [Descriptor(Type.Condition, "Переход на заданную метку, если первый операнд больше второго")]
        public static void jпе(Link index)
        {
            if (!ActiveCore.FlagReg.ZF)
                ActiveCore.ActiveIndex = index.Line - 1;
        }

        [Descriptor(Type.Condition, "Инкремент регистра {0}")]
        public static void inc(Register32 a)
        {
            a.Value++;
            _comp(a.Value - a.Value);
        }

        [Descriptor(Type.Condition, "Инкремент регистра {0} и сравнение результата с регистром {1}")]
        public static void incr(Register32 a, Register32 b)
        {
            a.Value++;
            _comp(a.Value - b.Value);
        }

        [Descriptor(Type.Action, "Пустой такт")]
        public static void nop() { }

        private static void _comp(int value)
        {
            ActiveCore.FlagReg.ZF = value == 0;
            ActiveCore.FlagReg.SF = value >= 0;
            ActiveCore.FlagReg.CF = false;
            ActiveCore.FlagReg.OF = false;
            ActiveCore.FlagReg.PF = value % 2 == 0;
        }
    }
}