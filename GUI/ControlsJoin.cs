using System;
using System.Collections.Specialized;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using MAIDE.Utilit;

namespace MAIDE.UI
{
    public static class ControlsJoin
    {
        /// <summary>
        /// Связывает элемент управления со свойством, имя свойства должно находится в Tag элемента.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="root">Элемент управления</param>
        /// <param name="obj">Обьект, содержищий свойства</param>
        /// <param name="appyChildren">Связвть дочение елементы</param>
        public static void Join<T>(Control root, T obj, bool appyChildren = true)
        {
            var methods = typeof(ControlsJoin).GetMethods(BindingFlags.Public | BindingFlags.Static).Where(p => p.Name.StartsWith("JoinTo", StringComparison.CurrentCultureIgnoreCase)).ToArray();

            foreach (Control c in root.Controls)
            {
                string tag = c.Tag as string;
                if (!string.IsNullOrEmpty(tag))
                {
                    PropertyInfo prop = typeof(T).GetProperty(tag);
                    if (prop != null)
                    {
                        MethodInfo caster = methods.FirstOrDefault(p => p.Name.EndsWith(prop.PropertyType.Name, StringComparison.CurrentCultureIgnoreCase));
                        if (caster != null)
                            caster.Invoke(null, new object[] { c, prop, obj });
                    }
                }
                else
                    Join(c, obj, appyChildren);
            }
        }

        public static void JoinToString(TextBox control, PropertyInfo property, object Object)
        {
            if (control == null)
                return;

            control.Text = (string)property.GetValue(Object);
            control.TextChanged += (s, e) =>
                property.SetValue(Object, control.Text);
        }

        public static void JoinToChar(TextBox control, PropertyInfo property, object Object)
        {
            if (control == null)
                return;

            control.Text = property.GetValue(Object).ToString();
            control.TextChanged += (s, e) =>
                property.SetValue(Object, control.Text.FirstOrDefault());
        }

        public static void JoinToInt(NumericUpDown control, PropertyInfo property, object Object)
        {
            if (control == null)
                return;

            control.Value = (int)property.GetValue(Object);
            control.ValueChanged += (s, e) =>
                property.SetValue(Object, (int)control.Value);
        }

        public static void JoinToStringCollection(TextBox control, PropertyInfo property, object Object)
        {
            if (control == null)
                return;

            control.Text = "";
            StringCollection value = (StringCollection)property.GetValue(Object);
            if (value != null)
            {
                foreach (var i in value)
                {
                    if (i.Length != 0)
                        control.Text += i + "\r\n";
                }
                if (control.Text.Length != 0)
                    control.Text = control.Text.Substring(0, control.Text.Length - 1);
            }

            control.TextChanged += (s, e) =>
            {
                StringCollection coll = new StringCollection();
                coll.AddRange(control.Text.Replace("\r", "").Split('\n'));
                property.SetValue(Object, coll);
            };
        }

        public static void JoinToBool(CheckBox control, PropertyInfo property, object Object)
        {
            if (control == null)
                return;

            control.Checked = (bool)property.GetValue(Object);
            control.CheckedChanged += (s, e) =>
                property.SetValue(Object, control.Checked);
        }

        public static void JoinToColor(Button control, PropertyInfo property, object Object)
        {
            if (control == null)
                return;

            control.ColorNormal = (Color)property.GetValue(Object);
            control.ColorHover = control.ColorNormal.Multiplay(1.2f);
            control.ColorDown = control.ColorNormal.Multiplay(1.4f);

            control.Click += (s, e) =>
            {
                ColorDialog dlg = new ColorDialog();
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    property.SetValue(Object, dlg.Color);
                    control.ColorNormal = dlg.Color;
                    control.ColorHover = dlg.Color.Multiplay(1.2f);
                    control.ColorDown = dlg.Color.Multiplay(1.4f);
                }
            };
        }
    }
}