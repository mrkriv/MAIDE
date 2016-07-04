using System;
using System.Drawing;
using System.Windows.Forms;

namespace MAIDE.UI
{
    public partial class EllipseControl : UserControl
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.FillEllipse(new SolidBrush(ForeColor), 0, 0, Width, Height);
        }
    }
}