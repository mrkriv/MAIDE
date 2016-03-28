using System;
using System.Linq;
using System.Windows.Forms;
using ASM.UI;
using System.Reflection;

namespace ASM
{
    public partial class Setting : Form
    {
        public Setting()
        {
            InitializeComponent();
            Initialize(this);
        }

        static void Initialize(Control root)
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
                        ((TextBox)c).Text = (string)value;
                        ((TextBox)c).TextChanged += (s, e) =>
                        {
                            prop.SetValue(Properties.Settings.Default, ((TextBox)s).Text);
                        };
                    }
                }
                else
                    Initialize(c);
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
