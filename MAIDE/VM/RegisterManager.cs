using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MAIDE.Utilit;

namespace MAIDE.VM
{
    public static class RegisterManager
    {
        public static readonly List<Register> Registers;
        public static readonly RegisterFlag FlagReg;

        public static StringCollection RegNames32
        {
            get { return Properties.Settings.Default.Register32; }
            set { setRegs<Register32>(value); }
        }

        public static StringCollection RegNames16
        {
            get { return Properties.Settings.Default.Register16; }
            set { setRegs<Register16>(value); }
        }

        public static StringCollection RegNames8
        {
            get { return Properties.Settings.Default.Register8; }
            set { setRegs<Register8>(value); }
        }

        static RegisterManager()
        {
            FlagReg = new RegisterFlag();
            Registers = new List<Register>();

            var type = typeof(RegisterManager);
            PropertyJoin.Create(type, "RegNames32", Properties.Settings.Default, "Register32");
            PropertyJoin.Create(type, "RegNames16", Properties.Settings.Default, "Register16");
            PropertyJoin.Create(type, "RegNames8", Properties.Settings.Default, "Register8");
        }
        
        public static Register GetRegister(string name)
        {
            foreach (Register r in Registers)
            {
                if (r.Name == name)
                    return r;
            }
            return null;
        }

        private static void setRegs<T>(StringCollection regs) where T : Register
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
    }
}