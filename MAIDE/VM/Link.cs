using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAIDE.VM
{
    public struct Pointer
    {
        public int Line;
        public int Offest;
        public Register32 reg32;

        public Pointer(int line)
        {
            Line = line;
            Offest = 0;
            reg32 = null;
        }

        public int GetValue()
        {
            return reg32 == null ? Line : reg32.Value + Line + Offest;
        }

        public static explicit operator int(Pointer self)
        {
            return self.GetValue();
        }
    }
}
