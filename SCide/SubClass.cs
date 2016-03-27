using System;
using System.Drawing;
using System.Runtime.InteropServices;
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

    public static class Exep
    {
        //public static void Swap<T>(this T a, ref T b)
        //{
        //    var size = Marshal.SizeOf(typeof(T));
        //    var ptr = Marshal.AllocHGlobal(size);
        //    Marshal.StructureToPtr(b, ptr, false);
        //    b = a;
        //    Marshal.PtrToStructure(ptr, a);
        //    Marshal.FreeHGlobal(ptr);
        //}
    }
}