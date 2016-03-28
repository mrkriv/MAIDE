using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ASM.UI
{
    public class MGroupBox : GroupBox
    {
        private Color borderColor;

        public Color BorderColor
        {
            get { return borderColor; }
            set { borderColor = value; }
        }

        public MGroupBox()
        {
            borderColor = Color.Black;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Size tSize = TextRenderer.MeasureText(Text, Font);

            Rectangle borderRect = e.ClipRectangle;
            borderRect.Y += tSize.Height / 2;
            borderRect.Height -= tSize.Height / 2;
            ControlPaint.DrawBorder(e.Graphics, borderRect, borderColor, ButtonBorderStyle.Solid);

            Rectangle textRect = e.ClipRectangle;
            textRect.X += 6;
            textRect.Width = tSize.Width;
            textRect.Height = tSize.Height;
            e.Graphics.FillRectangle(new SolidBrush(BackColor), textRect);
            e.Graphics.DrawString(Text, Font, new SolidBrush(ForeColor), textRect);
        }
    }
}
