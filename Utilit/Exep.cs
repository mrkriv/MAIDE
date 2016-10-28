using System;
using System.Drawing;
using System.ComponentModel;
using System.Reflection;
using System.Linq;
using System.Xml;
using System.Windows.Forms;
using System.Resources;

namespace MAIDE.Utilit
{
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

        public static Point ReplaceX(this Point a, int x)
        {
            return new Point(x, a.Y);
        }

        public static Point ReplaceY(this Point a, int y)
        {
            return new Point(a.X, y);
        }

        public static Point Multiplay(this Point a, Point b)
        {
            return new Point(a.X * b.X, a.Y * b.Y);
        }

        public static Point Multiplay(this Point a, int x, int y)
        {
            return new Point(a.X * x, a.Y * y);
        }

        public static Point Multiplay(this Point a, int b)
        {
            return new Point(a.X * b, a.Y * b);
        }

        public static Point Center(this Rectangle self)
        {
            return new Point(self.Left + self.Width / 2, self.Top + self.Height / 2);
        }

        public static Point CenterTop(this Rectangle self)
        {
            return new Point(self.Left + self.Width / 2, self.Top);
        }

        public static Point CenterBottom(this Rectangle self)
        {
            return new Point(self.Left + self.Width / 2, self.Bottom);
        }

        public static Point CenterLeft(this Rectangle self)
        {
            return new Point(self.Left, self.Top + self.Height / 2);
        }

        public static Point CenterRight(this Rectangle self)
        {
            return new Point(self.Right, self.Top + self.Height / 2);
        }

        public static PointF Center(this RectangleF self)
        {
            return new PointF(self.Left + self.Width / 2.0f, self.Top + self.Height / 2.0f);
        }

        public static Size Add(this Size a, Size b)
        {
            return new Size(a.Width + b.Width, a.Height + b.Height);
        }

        public static Size Add(this Size a, int w, int h)
        {
            return new Size(a.Width + w, a.Height + h);
        }

        public static Size Substract(this Size a, int w, int h)
        {
            return new Size(a.Width - w, a.Height - h);
        }

        public static Size Substract(this Size a, Size b)
        {
            return new Size(a.Width - b.Width, a.Height - b.Height);
        }

        public static void DrawTriangle(this Graphics self, Brush brush, int x, int y, int w, int h)
        {
            Point[] points = { new Point(x, y), new Point(x + w, y + h / 2), new Point(x, y + h) };
            self.FillPolygon(brush, points);
        }

        public static void DrawCubicLine(this Graphics self, Pen pen, Point a, Point b)
        {
            Point[] points = { a, new Point(a.X, (b.Y + a.Y) / 2), new Point(b.X, (b.Y + a.Y) / 2), b };
            self.DrawCurve(pen, points);
        }

        public static object GetDefault(this Type self)
        {
            if (self.IsValueType)
                return Activator.CreateInstance(self);
            return null;
        }

        public static Color Multiplay(this Color self, float mul)
        {
            return Color.FromArgb(
                (int)(self.A * mul).Clamp(0, 255),
                (int)(self.R * mul).Clamp(0, 255),
                (int)(self.G * mul).Clamp(0, 255),
                (int)(self.B * mul).Clamp(0, 255));
        }

        public static Color Add(this Color self, float k)
        {
            return Color.FromArgb(
                (int)(self.A + k).Clamp(0, 255),
                (int)(self.R + k).Clamp(0, 255),
                (int)(self.G + k).Clamp(0, 255),
                (int)(self.B + k).Clamp(0, 255));
        }

        public static Color Add(this Color self, Color b)
        {
            return Color.FromArgb(
                (self.A + b.A).Clamp(0, 255),
                (self.R + b.R).Clamp(0, 255),
                (self.G + b.G).Clamp(0, 255),
                (self.B + b.B).Clamp(0, 255));
        }

        public static T Clamp<T>(this T self, T min, T max) where T : IComparable<T>
        {
            if (self.CompareTo(min) < 0)
                return min;
            else if (self.CompareTo(max) > 0)
                return max;
            return self;
        }

        /// <summary>
        /// Устанавливает значения всем свойствам с атрибутом DefaultValue
        /// </summary>
        public static void LoadDefaultProperties<T>(this T self)
        {
            foreach (var inf in typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var atrs = inf.GetCustomAttributes(typeof(DefaultValueAttribute), true);
                if (atrs.Length != 0)
                    inf.SetValue(self, ((DefaultValueAttribute)atrs.First()).Value, BindingFlags.SetProperty, null, null, null);
            }
        }

        /// <summary>
        /// Устанавливает значения всем статическим свойствам с атрибутом DefaultValue
        /// </summary>
        public static void LoadDefaultProperties(Type type)
        {
            foreach (var inf in type.GetProperties(BindingFlags.Public | BindingFlags.Static))
            {
                var atrs = inf.GetCustomAttributes(typeof(DefaultValueAttribute), true);
                if (atrs.Length != 0)
                    inf.SetValue(null, ((DefaultValueAttribute)atrs.First()).Value, BindingFlags.SetProperty, null, null, null);
            }
        }

        public static XmlAttribute AddAttribute(this XmlDocument self, string name, string value)
        {
            XmlAttribute atrib = self.CreateAttribute(name);
            atrib.Value = value;
            return atrib;
        }

        public static T GetAttribute<T>(this MethodInfo self) where T : Attribute
        {
            return self.GetCustomAttributes<T>(false).FirstOrDefault();
        }

        /// <summary>
        /// Возвращает значение DisplayNameAttribute установленного для типа (null - если атрибут отсутствует)
        /// </summary>
        public static string GetDisplayName(this Type self)
        {
            DisplayNameAttribute atrib = self.GetCustomAttribute<DisplayNameAttribute>();
            return atrib != null ? atrib.DisplayName : self.Name;
        }

        public static string GetValue(this XmlNodeList self, string name)
        {
            foreach (XmlNode node in self)
            {
                if (node.Name == name)
                    return node.Value;
            }
            return null;
        }

        public static XmlNode Get(this XmlNodeList self, string name)
        {
            foreach (XmlNode node in self)
            {
                if (node.Name == name)
                    return node;
            }
            return null;
        }
    }
}