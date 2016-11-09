using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAIDE.VM
{
    public struct Pointer
    {
        public int Row;
        public Register32 regA;
        public Register32 regB;

        public Pointer(int row)
        {
            Row = row;
            regA = null;
            regB = null;
        }

        public int GetValue()
        {
            int pos = Row;

            if (regA != null)
                pos += regA.Value;

            if (regB != null)
                pos += regB.Value;

            return pos;
        }

        public static explicit operator int(Pointer self)
        {
            return self.GetValue();
        }
    }
}