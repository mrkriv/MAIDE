using System;
using System.Drawing;
using System.ComponentModel;
using ASM.VM;

// Sub class mur mur mur mur
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

    public struct DataIndex
    {
        public int Line;
        public DataIndex(int line)
        {
            Line = line;
        }
    }

    public struct LineIndex
    {
        public int Line;
        public int Offest;
        public Register32 reg32;

        public LineIndex(int line, int offest = 0)
        {
            Line = line;
            Offest = offest;
            reg32 = null;
        }

        public LineIndex(int line, Register32 offest)
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

        public static Point Center(this Rectangle self)
        {
            return new Point(self.Left + self.Width / 2, self.Top + self.Height / 2);
        }

        public static PointF Center(this RectangleF self)
        {
            return new PointF(self.Left + self.Width / 2.0f, self.Top + self.Height / 2.0f);
        }

        public static void DrawTriangle(this Graphics self, Brush brush, int x, int y, int w, int h)
        {
            Point[] points = { new Point(x, y), new Point(x + w, y + h / 2), new Point(x, y + h) };
            self.FillPolygon(brush, points);
        }

        public static object GetDefault(this Type self)
        {
            if (self.IsValueType)
                return Activator.CreateInstance(self);
            return null;
        }

        public static Color GetMultiplay(this Color self, float mul)
        {
            return Color.FromArgb((int)(self.A * mul), (int)(self.R * mul), (int)(self.G * mul), (int)(self.B * mul));
        }

        public static T Clamp<T>(this T val, T min, T max) where T : IComparable<T>
        {
            if (val.CompareTo(min) < 0)
                return min;
            else if (val.CompareTo(max) > 0)
                return max;
            return val;
        }
    }
}