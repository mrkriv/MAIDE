using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ASM.UI
{
    public partial class EllipseControl : UserControl
    {
        public EllipseControl()
        {
            InitializeComponent();
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            e.Graphics.FillEllipse(new SolidBrush(BackColor), Location.X, Location.Y, Width, Height);
        }

        protected override void OnPaint(PaintEventArgs e) { }
    }
}