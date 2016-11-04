using System.ComponentModel;
using System.Resources;
using System.Diagnostics;

namespace MAIDE.VM
{
    public enum OperationType
    {
        None,
        Action,
        Condition,
        Jump,
    }

    public class DescriptorAttribute : DescriptionAttribute
    {
        public readonly OperationType Type;

        public DescriptorAttribute(OperationType type, string description) : base(description)
        {
            Type = type;
        }
    }
}