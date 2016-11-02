using System;

namespace MAIDE.VM
{
    public class RegisterFlag
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
    }

    public abstract class Register
    {
        public string Name { get; private set; }
        public string TValue
        {
            get { return ToString(); }
            set { InString(value); }
        }
        
        public Register(string name)
        {
            Name = name;
        }

        public abstract void InString(string str);
        public abstract byte[] GetByte();
        public abstract void SetByte(byte[] buff);
    }

    public class Register32 : Register
    {
        public int Value { get; set; }

        public Register32(string name) : base(name) { }

        public override string ToString()
        {
            return Value.ToString();
        }

        public override void InString(string str)
        {
            Value = int.Parse(str);
        }

        public override byte[] GetByte()
        {
            return BitConverter.GetBytes(Value);
        }

        public override void SetByte(byte[] buff)
        {
            Value = BitConverter.ToInt32(buff, 0);
        }
    }

    public class Register16 : Register
    {
        public short Value { get; set; }

        public Register16(string name) : base(name) { }

        public override string ToString()
        {
            return Value.ToString();
        }

        public override void InString(string str)
        {
            Value = short.Parse(str);
        }

        public override byte[] GetByte()
        {
            return BitConverter.GetBytes(Value);
        }

        public override void SetByte(byte[] buff)
        {
            Value = BitConverter.ToInt16(buff, 0);
        }
    }

    public class Register8 : Register
    {
        public char Value { get; set; }

        public Register8(string name) : base(name) { }

        public override string ToString()
        {
            return ((int)Value).ToString();
        }

        public override void InString(string str)
        {
            Value = (char)int.Parse(str);
        }

        public override byte[] GetByte()
        {
            return BitConverter.GetBytes(Value);
        }

        public override void SetByte(byte[] buff)
        {
            Value = BitConverter.ToChar(buff, 0);
        }
    }
}