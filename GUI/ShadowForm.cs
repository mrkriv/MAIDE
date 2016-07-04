using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MAIDE.Utilit.WinAPI;
using System.Windows.Forms;

namespace MAIDE.UI
{
    public class ShadowForm : Form
    {
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle = cp.ExStyle | Consts.WS_EX_TRANSPARENT | Consts.WS_EX_NOACTIVATE;
                return cp;
            }
        }

        public ShadowForm()
        {
            Opacity = 0.5;
            BackColor = Color.Gray;
            ShowInTaskbar = false;
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.Manual;
        }
    }
}