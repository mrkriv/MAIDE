using System.ComponentModel;
using System.Resources;
using System.Diagnostics;

namespace ASM.VM
{
    public enum Type
    {
        None,
        Action,
        Condition,
        Jump,
    }

    public class DescriptorAttribute : DescriptionAttribute
    {
        public readonly Type Type;

        public DescriptorAttribute(Type type, string description) : base(description)
        {
            Type = type;
        }
    }
}