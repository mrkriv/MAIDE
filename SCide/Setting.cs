using System;
using System.Windows.Forms;
using System.Linq;
using System.Reflection;
using System.Collections.Specialized;

namespace ASM
{
    public partial class Setting : Form
    {
        public Setting()
        {
            InitializeComponent();
            InitializeInclude(this);
        }

        static void InitializeInclude(Control root)
        {
            foreach (Control c in root.Controls)
            {
                string tag = c.Tag as string;
                if (!string.IsNullOrEmpty(tag))
                {
                    PropertyInfo prop = typeof(Properties.Settings).GetProperty(tag);
                    if (prop == null)
                        continue;

                    object value = prop.GetValue(Properties.Settings.Default);

                    if (prop.PropertyType == typeof(string))
                    {
                        TextBox tb = c as TextBox;
                        tb.Text = (string)value;
                        tb.TextChanged += (s, e) =>
                        {
                            prop.SetValue(Properties.Settings.Default, ((TextBox)s).Text);
                        };
                    }
                    else if (prop.PropertyType == typeof(char))
                    {
                        TextBox tb = c as TextBox;
                        tb.Text = value.ToString();
                        tb.TextChanged += (s, e) =>
                        {
                            prop.SetValue(Properties.Settings.Default, ((TextBox)s).Text.FirstOrDefault());
                        };
                    }
                    else if (prop.PropertyType == typeof(int))
                    {
                        NumericUpDown control = c as NumericUpDown;
                        control.Value = (int)value;
                        control.ValueChanged += (s, e) =>
                        {
                            prop.SetValue(Properties.Settings.Default, (int)control.Value);
                        };
                    }
                    else if (prop.PropertyType == typeof(StringCollection))
                    {
                        TextBox tb = c as TextBox;
                        tb.Text = "";
                        if (value != null)
                        {
                            foreach (var i in ((StringCollection)value))
                            {
                                if (i.Length != 0)
                                    tb.Text += i + "\r\n";
                            }
                            if (tb.Text.Length != 0)
                                tb.Text = tb.Text.Substring(0, tb.Text.Length - 1);
                        }

                        tb.TextChanged += (s, e) =>
                        {
                            StringCollection coll = new StringCollection();
                            coll.AddRange(((TextBox)s).Text.Replace("\r", "").Split('\n'));
                            prop.SetValue(Properties.Settings.Default, coll);
                        };
                    }
                    else if (prop.PropertyType == typeof(bool))
                    {
                        CheckBox control = c as CheckBox;
                        control.Checked = (bool)value;
                        control.CheckedChanged += (s, e) =>
                        {
                            prop.SetValue(Properties.Settings.Default, control.Checked);
                        };
                    }
                }
                else
                    InitializeInclude(c);
            }
        }

        private void exit_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reload();
            Close();
        }

        private void done_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
            Close();
        }

        private void reset_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reset();
            Close();
            new Setting().Show();
        }
    }
}
