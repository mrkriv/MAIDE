using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASM.VM
{
    public class RuntimeException : Exception
    {
        public int Row;

        public RuntimeException(string message, int row) : base(message)
        {
            Row = row;
        }
    }
}
