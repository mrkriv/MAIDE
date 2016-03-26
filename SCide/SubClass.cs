using System;
using System.Drawing;
using System.Windows.Forms;

namespace ASM
{
    public struct Line
    {
        public int Number;
        public string Text;

        public Line(string text, int line)
        {
            Number = line;
            Text = text;
        }
    }

    public class ErrorLine
    {
        public string Message { get; }
        public int Line { get; private set; }

        public ErrorLine(string message, int line)
        {
            Message = message;
            Line = line;
        }
    }

    public class InvokeError : Exception
    {
        public Line editorLine;
        public InvokeError(string message, Line line) : base(message)
        {
            editorLine = line;
        }
    }

    internal struct DataIndex
    {
        public int Line;
        public int Offest;
        public Register32 reg32;

        public DataIndex(int line, int offest = 0)
        {
            Line = line;
            Offest = offest;
            reg32 = null;
        }

        public DataIndex(int line, Register32 offest)
        {
            Line = line;
            Offest = 0;
            reg32 = offest;
        }
    }
}