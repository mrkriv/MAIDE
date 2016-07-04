using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MAIDE.UI
{
    public class GroupBox : System.Windows.Forms.GroupBox
    {
        private Color borderColor;

        [Category("Appearance")]
        public Color BorderColor
        {
            get { return borderColor; }
            set
            {
                if (borderColor == value)
                    return;

                borderColor = value;
                Invalidate(false);
            }
        }

        public GroupBox()
        {
            borderColor = Color.Black;
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            e.Graphics.Clear(BackColor);

            Size tSize = TextRenderer.MeasureText(Text + " ", Font);

            Rectangle borderRect = new Rectangle(0, tSize.Height / 2, Width, Height - tSize.Height / 2);
            ControlPaint.DrawBorder(e.Graphics, borderRect, borderColor, ButtonBorderStyle.Solid);

            Rectangle textRect = new Rectangle(6, 0, tSize.Width, tSize.Height);
            e.Graphics.FillRectangle(new SolidBrush(BackColor), textRect);
            e.Graphics.DrawString(Text, Font, new SolidBrush(ForeColor), textRect);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
        }
    }
}