using System;
using System.Windows.Forms;
using System.Linq;
using System.Reflection;
using System.Collections.Specialized;
using MAIDE.Utilit;
using MAIDE.UI;
using System.Drawing;

namespace MAIDE
{
    partial class Setting : DefaultForm
    {
        public Setting()
        {
            InitializeComponent();

            if (!DesignMode)
                ControlsJoin.Join(this, Properties.Settings.Default);
        }

        private void exit_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reload();
            Log.AddMessage("Setting reload");
            Close();
        }

        private void done_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
            Log.AddMessage("Setting saved");
            Close();
        }

        private void reset_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reset();
            Log.AddMessage("Setting reset");
            Close();
            new Setting().Show();
        }
    }
}