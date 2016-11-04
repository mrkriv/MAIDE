using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MAIDE.Utilit;

namespace MAIDE.VM
{
    public class Refactor
    {
        public static OperationType GetLineOpType(string row)
        {
            string[] text = row.Replace('\t', ' ').Trim(' ').Split(Properties.Settings.Default.CommentChar)[0].Split(':');
            row = text.Last().Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();
            if (row != null)
            {
                foreach (var method in Operators.OperationsList)
                {
                    if (method.Name == row)
                        return method.GetAttribute<DescriptorAttribute>().Type;
                }
            }
            return OperationType.None;
        }
    }
}