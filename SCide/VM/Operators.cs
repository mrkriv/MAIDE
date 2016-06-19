using System.Linq;
using System.ComponentModel;
using System.Reflection;
using System;

namespace ASM.VM
{
    public static class Operators
    {
        public static Core ActiveCore;
        public static readonly MethodInfo[] OperationsList;

        static Operators()
        {
            var mhod = typeof(Operators).GetMethods();
            OperationsList = mhod.Where(w => w.CustomAttributes.Any(e => e.AttributeType == typeof(DescriptionAttribute))).ToArray();
        }

        private static T reg<T>(string name) where T : Register
        {
            return (T)ActiveCore.GetRegister(name);
        }

        [Description("Выводит на консоль '{0}'-й байт из регистра 'a'")]
        public static void wd(int n)
        {
            n %= 4;
            int value = reg<Register32>("a").Value >> n * 8;
            Console.Write((char)(value % 256));
        }

        [Description("Записывает в регистр '{0}' байт считаный с консоли (смещение пока не работает)")]
        public static void rd(int offest)
        {
            reg<Register32>("a").Value = Console.ReadKey();
        }

        [Description("Загружает в регистр '{0}' 4 байта из памяти по адрессу '{1}'")]
        public static void ldb(Register32 reg, DataIndex index)
        {
            var buff = ActiveCore.Data.GetRange(index.Line, 4).ToArray();
            reg.Value = BitConverter.ToInt32(buff, 0);
        }

        [Description("Безусловный переход, в стек помещается текущий адресс")]
        public static void call(LineIndex index)
        {
            ActiveCore.Stack.Push(ActiveCore.ActiveIndex);
            ActiveCore.ActiveIndex = index.Line - 1;
        }

        [Description("Переходит по адресу взятому со стека, если стек пуст то завершает программу")]
        public static void ret()
        {
            if (ActiveCore.Stack.Count != 0)
                ActiveCore.ActiveIndex = ActiveCore.Stack.Pop();
            else
                ActiveCore.Stop();
        }

        [Description("Кладет на вершину стека все байты регистра '{0}'")]
        public static void push(Register32 reg)
        {
            ActiveCore.Stack.Push(reg.Value);
        }

        [Description("Снимает с вершины стека 32 байта и помещает их в регистр '{0}'")]
        public static void pop(Register32 reg)
        {
            if (ActiveCore.Stack.Count != 0)
                reg.Value = ActiveCore.Stack.Pop();
        }

        [Description("Копирует данные из регистра '{0}' в регистр '{1}'")]
        public static void mov(Register32 a, Register32 b)
        {
            a.Value = b.Value;
        }

        [Description("Выполняет сравнение регистра '{0}' с {1}")]
        public static void comp(Register32 reg, int value)
        {
            _comp(reg.Value - value);
        }

        [Description("Выполняет сравнение первого байта регистра '{0}' с {1}")]
        public static void compb(Register32 reg, int value)
        {
            _comp((char)reg.Value - (char)value);
        }

        [Description("Выполняет сравнение регистров '{0}' и {1}")]
        public static void compr(Register32 a, Register32 b)
        {
            _comp(a.Value - b.Value);
        }

        [Description("")]
        public static void addr(Register32 a, Register32 b)
        {
            _comp(a.Value + b.Value);
            a.Value += b.Value;
        }

        [Description("")]
        public static void subr(Register32 a, Register32 b)
        {
            _comp(a.Value - b.Value);
            a.Value -= b.Value;
        }

        [Description("")]
        public static void mulr(Register32 a, Register32 b)
        {
            _comp(a.Value * b.Value);
            a.Value *= b.Value;
        }

        [Description("")]
        public static void divr(Register32 a, Register32 b)
        {
            _comp(a.Value / b.Value);
            a.Value /= b.Value;
        }

        [Description("")]
        public static void add(Register32 reg, int value)
        {
            _comp(reg.Value + value);
            reg.Value += value;
        }

        [Description("")]
        public static void sub(Register32 reg, int value)
        {
            _comp(reg.Value - value);
            reg.Value -= value;
        }

        [Description("")]
        public static void mul(Register32 reg, int value)
        {
            _comp(reg.Value * value);
            reg.Value *= value;
        }

        [Description("")]
        public static void div(Register32 reg, int value)
        {
            _comp(reg.Value / value);
            reg.Value /= value;
        }

        [Description("Копирует значение из памяти в регистр '{0}'")]
        public static void ld(Register32 reg, int value)
        {
            reg.Value = value;
        }

        [Description("Сбрасывает регистр '{0}'")]
        public static void clear(Register32 reg)
        {
            reg.Value = 0;
        }

        [Description("Безусловный переход")]
        public static void jmp(LineIndex index)
        {
            ActiveCore.ActiveIndex = index.Line - 1;
        }

        [Description("Переход на заданую метку, если операнды равны")]
        public static void jeq(LineIndex index)
        {
            RegisterFlag reg = reg<RegisterFlag>("flag");
            if (reg.ZF)
                ActiveCore.ActiveIndex = index.Line - 1;
        }

        [Description("Переход на заданую метку, если первый операнд больше нуля")]
        public static void jgt(LineIndex index)
        {
            RegisterFlag reg = reg<RegisterFlag>("flag");
            if (!reg.ZF && reg.SF)
                ActiveCore.ActiveIndex = index.Line - 1;
        }

        [Description("Переход на заданую метку, если первый операнд меньше второго")]
        public static void jlt(LineIndex index)
        {
            RegisterFlag reg = reg<RegisterFlag>("flag");
            if (reg.SF != reg.OF)
                ActiveCore.ActiveIndex = index.Line - 1;
        }

        [Description("Переход на заданую метку, если первый операнд больше второго")]
        public static void jge(LineIndex index)
        {
            Register32 a = reg<Register32>("a");
            RegisterFlag reg = reg<RegisterFlag>("flag");
            if (reg.SF == reg.OF)
                ActiveCore.ActiveIndex = index.Line - 1;
        }

        [Description("Переход на заданую метку, если первый операнд больше второго")]
        public static void jпе(LineIndex index)
        {
            RegisterFlag reg = reg<RegisterFlag>("flag");
            if (!reg.ZF)
                ActiveCore.ActiveIndex = index.Line - 1;
        }

        [Description("Инкремент рагистра {0}")]
        public static void inc(Register32 a)
        {
            a.Value++;
            _comp(a.Value - a.Value);
        }

        [Description("Инкремент рагистра {0} и сравнение результата с регистром {1}")]
        public static void incr(Register32 a, Register32 b)
        {
            a.Value++;
            _comp(a.Value - b.Value);
        }

        [Description("Пустой такт")]
        public static void nop() { }

        private static void _comp(int value)
        {
            RegisterFlag reg = reg<RegisterFlag>("flag");
            reg.ZF = value == 0;
            reg.SF = value >= 0;
            reg.CF = false;
            reg.OF = false;
            reg.PF = value % 2 == 0;
        }
    }
}