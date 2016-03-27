using System;
using System.Windows.Forms;

namespace ASM
{
    public partial class Setting : Form
    {
        public Setting()
        {
            InitializeComponent();
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
