using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASM
{
    public class ErrorMessageRow
    {
        public string Message { get; set; }
        public int Row { get; set; }

        public ErrorMessageRow(string message, int index)
        {
            Message = message;
            Row = index;
        }
    }
}
