using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASM.VM
{
    public abstract class Register
    {
        public string Name { get; private set; }
        public string TValue
        {
            get { return toString(); }
            set { inString(value); }
        }

        public Register(string name)
        {
            Name = name;
        }

        protected abstract string toString();
        protected abstract void inString(string str);
    }

    public class Register32 : Register
    {
        public int Value;

        public Register32(string name) : base(name)
        {
        }

        protected override string toString()
        {
            return Value.ToString();
        }

        protected override void inString(string str)
        {
            Value = int.Parse(str);
        }
    }

    public class Register16 : Register
    {
        private readonly Register32 owner;
        private readonly int offest;

        public Int16 value
        {
            get { return (Int16)owner.Value; }
            set { owner.Value = value; }
        }

        public Register16(string name, Register32 owner, int offest = 0) : base(name)
        {
            this.offest = offest;
            this.owner = owner;
        }

        protected override string toString()
        {
            return value.ToString();
        }

        protected override void inString(string str)
        {
            value = Int16.Parse(str);
        }
    }

    public class Register8 : Register
    {
        private readonly Register32 owner;
        private readonly int offest;

        public char value
        {
            get { return (char)owner.Value; }
            set { owner.Value = value; }
        }

        public Register8(string name, Register32 owner, int offest = 0) : base(name)
        {
            this.offest = offest;
            this.owner = owner;
        }

        protected override string toString()
        {
            return ((int)value).ToString();
        }

        protected override void inString(string str)
        {
            value = (char)int.Parse(str);
        }
    }

    public class RegisterFlag : Register
    {
        /// <summary>Carry Flag</summary>
        public bool CF = false;
        /// <summary>Parity Flag</summary>
        public bool PF = false;
        /// <summary>Auxiliary Carry Flag</summary>
        public bool AF = false;
        /// <summary>Zero Flag</summary>
        public bool ZF = false;
        /// <summary>Sign Flag</summary>
        public bool SF = false;
        /// <summary>Trap Flag</summary>
        public bool TF = false;
        /// <summary>Interrupt Enable Flag</summary>
        public bool IF = false;
        /// <summary>Direction Flag</summary>
        public bool DF = false;
        /// <summary>Overflow Flag</summary>
        public bool OF = false;
        /// <summary>Privilege Level</summary>
        public bool IOPL = false;
        /// <summary>Nested Task</summary>
        public bool NT = false;
        /// <summary>Resume Flag</summary>
        public bool RF = false;
        /// <summary>Virtual-8086 Mode</summary>
        public bool VM = false;
        /// <summary>Alignment Check</summary>
        public bool AC = false;
        /// <summary>Virtual Interrupt Flag</summary>
        public bool VIF = false;
        /// <summary>Virtual Interrupt Pending</summary>
        public bool VIP = false;
        /// <summary>CPUID enable Flag</summary>
        public bool ID = false;

        public RegisterFlag(string name) : base(name)
        {
        }

        protected override string toString()
        {
            return "More...";
        }

        protected override void inString(string str)
        {
        }
    }
}