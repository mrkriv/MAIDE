using System;
using System.Drawing;
using ASM.VM;

namespace ASM
{
    public class ErrorMessageRow
    {
        public string Message { get; set; }
        public int Index { get; set; }

        public ErrorMessageRow(string message, int index)
        {
            Message = message;
            Index = index;
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
        public static Point Add(this Point a, Point b)
        {
            return new Point(a.X + b.X, a.Y + b.Y);
        }

        public static Point Add(this Point a, int x, int y)
        {
            return new Point(a.X + x, a.Y + y);
        }

        public static Point Substract(this Point a, int x, int y)
        {
            return new Point(a.X - x, a.Y - y);
        }

        public static Point Substract(this Point a, Point b)
        {
            return new Point(a.X - b.X, a.Y - b.Y);
        }
    }
}