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
                    if (c is NumericUpDown)
                    {
                        ((NumericUpDown)c).Value = (int)value;
                        ((NumericUpDown)c).ValueChanged += (s, e) =>
                        {
                            prop.SetValue(Properties.Settings.Default, (int)((NumericUpDown)s).Value);
                        };
                    }
                    else if (c is TextBox)
                    {
                        TextBox tb = c as TextBox;
                        if (tb.Multiline)
                        {
                            tb.Text = "";
                            if (value != null)
                            {
                                foreach (var i in ((StringCollection)value))
                                    tb.Text += i + "\r\n";
                            }

                            tb.TextChanged += (s, e) =>
                            {
                                StringCollection coll = new StringCollection();
                                coll.AddRange(((TextBox)s).Text.Replace("\r", "").Split('\n'));
                                prop.SetValue(Properties.Settings.Default, coll);
                            };
                        }
                        else
                        {
                            tb.Text = (string)value;
                            tb.TextChanged += (s, e) =>
                            {
                                prop.SetValue(Properties.Settings.Default, ((TextBox)s).Text);
                            };
                        }
                    }
                    else if (c is CheckBox)
                    {
                        ((CheckBox)c).Checked = (bool)value;
                        ((CheckBox)c).CheckedChanged += (s, e) =>
                        {
                            prop.SetValue(Properties.Settings.Default, ((CheckBox)s).Checked);
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
            Properties.Settings.Default.Reload();
            Close();
            new Setting().Show();
        }
    }
}
