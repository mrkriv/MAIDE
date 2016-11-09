using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MAIDE.VM
{
    public class Operation
    {
        public readonly object[] Args;
        public readonly MethodInfo Method;
        public int Length;

        public Operation(MethodInfo method)
        {
            Method = method;
            Args = new object[method.GetParameters().Count()];
            Length = 1 + Args.Length;
        }
    }
}