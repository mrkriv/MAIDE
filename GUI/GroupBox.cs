using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using MAIDE.Utilit;

namespace MAIDE.UI
{
    public class GroupBox : System.Windows.Forms.GroupBox, IUsePalette
    {
        [Category("Appearance")]
        [DefaultValue(true)]
        public bool UsePalette { get; set; }

        [Category("Appearance")]
        [DefaultValue(typeof(Color), "0, 0, 0")]
        public Color BorderColor { get; set; }

        public GroupBox()
        {
            this.LoadDefaultProperties();

            PropertyJoin.ChangedPropertyEvent(this, new string[] {
                "BorderColor",
            }, Invalidate);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Size tSize = TextRenderer.MeasureText(Text + " ", Font);

            Rectangle borderRect = new Rectangle(0, tSize.Height / 2, Width, Height - tSize.Height / 2);
            ControlPaint.DrawBorder(e.Graphics, borderRect, BorderColor, ButtonBorderStyle.Solid);

            Rectangle textRect = new Rectangle(6, 0, tSize.Width, tSize.Height);
            e.Graphics.FillRectangle(new SolidBrush(BackColor), textRect);
            e.Graphics.DrawString(Text, Font, new SolidBrush(ForeColor), textRect);
        }

        public void AppyPalette()
        {
            BackColor = Palette.GetColor("Background");
            ForeColor = Palette.GetColor("FontMain");
            BorderColor = Palette.GetColor("GroupBorder");
        }
    }
}