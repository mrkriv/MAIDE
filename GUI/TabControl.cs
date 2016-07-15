using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using MAIDE.Utilit;

namespace MAIDE.UI
{
    public class TabControl : System.Windows.Forms.TabControl, IUsePalette
    {
        private Color backColor = Color.Empty;
        private StringFormat titleFormat = new StringFormat()
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center
        };

        [Category("Appearance")]
        [DefaultValue(typeof(Color), "150, 150, 150")]
        public Color BorderColor { get; set; }

        [Category("Appearance")]
        [DefaultValue(typeof(Color), "1, 122, 204")]
        public Color TabActiveColor { get; set; }

        [Category("Appearance")]
        [DefaultValue(typeof(Color), "45, 45, 48")]
        public Color TabDisableColor { get; set; }

        public override Color BackColor
        {
            get
            {
                if (backColor.Equals(Color.Empty))
                    return Parent != null ? Parent.BackColor : DefaultBackColor;
                return backColor;
            }
            set
            {
                if (backColor.Equals(value))
                    return;
                backColor = value;
                Invalidate();

                base.OnBackColorChanged(EventArgs.Empty);
            }
        }

        public TabControl()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.UserPaint, true);

            PropertyJoin.ChangedPropertyEvent(this, new string[] {
                "BorderColor",
                "TabActiveColor",
                "TabDisableColor"
            }, Invalidate);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.Clear(BackColor);

            if (TabCount == 0)
                return;

            Rectangle rect = SelectedTab.Bounds;
            rect.Inflate(3, 3);

            TabPage page = TabPages[SelectedIndex];
            using (SolidBrush brush = new SolidBrush(page.BackColor))
            {
                e.Graphics.FillRectangle(brush, rect);
                e.Graphics.DrawRectangle(new Pen(BorderColor), rect);

                for (int i = 0; i < TabCount; i++)
                {
                    page = TabPages[i];
                    rect = GetTabRect(i);
                    rect.Offset(-1, 0);

                    brush.Color = SelectedIndex == i ? TabActiveColor : TabDisableColor;
                    e.Graphics.FillRectangle(brush, rect);

                    if (SelectedIndex == i)
                        e.Graphics.DrawRectangle(new Pen(brush.Color), rect);

                    if (Alignment == TabAlignment.Left || Alignment == TabAlignment.Right)
                    {
                        float RotateAngle = Alignment == TabAlignment.Left ? 270 : 90;
                        PointF cp = new PointF(rect.Left + (rect.Width >> 1), rect.Top + (rect.Height >> 1));
                        e.Graphics.TranslateTransform(cp.X, cp.Y);
                        e.Graphics.RotateTransform(RotateAngle);
                        rect = new Rectangle(rect.Height / -2, rect.Width / -2, rect.Height, rect.Width);
                    }

                    brush.Color = page.ForeColor;
                    e.Graphics.DrawString(page.Text, Font, brush, rect, titleFormat);
                    e.Graphics.ResetTransform();
                }

                e.Graphics.DrawLine(new Pen(TabActiveColor, 2),
                    SelectedTab.Bounds.Location.Add(-3, -2),
                    SelectedTab.Bounds.Location.Add(SelectedTab.Bounds.Width + 4, -2));
            }
        }

        public void AppyPalette()
        {
            BackColor = Palette.Background;
            TabDisableColor = Palette.DockingTabDisable;
            TabActiveColor = Palette.DockingTabActive;
            BorderColor = Palette.GroupBorder;
            ForeColor = Palette.FontMain;
        }
    }
}