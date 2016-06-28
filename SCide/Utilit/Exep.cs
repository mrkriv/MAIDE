using System;
using System.Drawing;
using System.ComponentModel;
using System.Reflection;
using System.Linq;
using System.Xml;
using System.Windows.Forms;
using System.Resources;

namespace ASM.Utilit
{
    public static class Exep
    {
        public delegate void ChangedProperty<T>(T value);
        public delegate void Clallback();

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

        public static Point CenterTop(this Rectangle self)
        {
            return new Point(self.Left + self.Width / 2, self.Top);
        }

        public static Point CenterBottom(this Rectangle self)
        {
            return new Point(self.Left + self.Width / 2, self.Bottom);
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

        public static Color GetMultiplay(this Color self, float mul)
        {
            return Color.FromArgb((int)(self.A * mul), (int)(self.R * mul), (int)(self.G * mul), (int)(self.B * mul));
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

        public static void ChangedPropertyEvent<T>(this object self, string propertyName, ChangedProperty<T> callback) where T : class
        {
            PropertyDescriptor prop = TypeDescriptor.GetProperties(self).Find(propertyName, true);
            prop.AddValueChanged(self, (s, e) => { callback(prop.GetValue(self) as T); });
        }

        public static void ChangedPropertyEvent(this object self, string propertyName, Clallback callback)
        {
            PropertyDescriptor prop = TypeDescriptor.GetProperties(self).Find(propertyName, true);
            prop.AddValueChanged(self, (s, e) => { callback(); });
        }

        public static void SetLanguage(this Control self, ResourceManager res)
        {
            foreach (Control c in self.Controls)
            {
                if (c is ToolStrip)
                    ((ToolStrip)c).SetLanguage(res);
                else
                    c.SetLanguage(res);
            }

            string text = res.GetString(self.Text);
            if (text != null)
                self.Text = text;
        }

        public static void SetLanguage(this ToolStrip self, ResourceManager res)
        {
            foreach (ToolStripItem c in self.Items)
                c.SetLanguage(res);

            string text = res.GetString(self.Text);
            if (text != null)
                self.Text = text;
        }

        public static void SetLanguage(this ToolStripItem self, ResourceManager res)
        {
            if (self is ToolStripDropDownButton)
            {
                foreach (ToolStripItem c in ((ToolStripDropDownButton)self).DropDownItems)
                    c.SetLanguage(res);
            }

            string text = res.GetString(self.Text);
            if (text != null)
                self.Text = text;
        }
    }
}