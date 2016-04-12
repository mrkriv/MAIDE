using System.Drawing;

namespace ASM.UI
{
    partial class CodeEditBox
    {
        public class Symbol
        {
            public char Value;
            public Color Color;
            public int Render_old_X;
            public int Render_old_Y;
            public int Render_old_Width;

            public Symbol(char value) : this(value, Color.White)
            { }

            public Symbol(char value, Color color)
            {
                Value = value;
                Color = color;
            }

            public static implicit operator char(Symbol x)
            {
                return x.Value;
            }

            public static implicit operator Symbol(char x)
            {
                return new Symbol(x);
            }

            public override string ToString()
            {
                return Value.ToString();
            }
        }
    }
}
