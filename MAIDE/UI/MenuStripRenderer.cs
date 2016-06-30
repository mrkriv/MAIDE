using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ASM.Utilit;

namespace ASM.UI
{
    public class MenuStripRenderer : ToolStripProfessionalRenderer
    {
        public MenuStripRenderer() : base(new MenuStripColorTable())
        {
            RoundedEdges = false;
        }

        public static void SetStyle(ToolStripItem item)
        {
            item.BackColor = Color.FromArgb(27, 27, 28);

            if (item is ToolStripSeparator)
                item.ForeColor = Color.FromArgb(50, 50, 54);
            else
                item.ForeColor = Color.FromArgb(241, 241, 241);

            item.Height = 22;
        }

        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
        {
            e.Graphics.Clear(e.Item.BackColor);
            RectangleF rect = e.Graphics.ClipBounds;
            PointF center = rect.Center();

            if (e.Vertical)
                e.Graphics.DrawLine(new Pen(e.Item.ForeColor), center.X, rect.Top + 2, center.X, rect.Bottom - 2);
            else
                e.Graphics.DrawLine(new Pen(e.Item.ForeColor), rect.Left, center.Y, rect.Right, center.Y);
        }

        protected override void OnRenderStatusStripSizingGrip(ToolStripRenderEventArgs e)
        {
            e.Graphics.Clear(e.ToolStrip.BackColor);
        }

        protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
        {
            base.OnRenderButtonBackground(e);
            if (((ToolStripButton)e.Item).Checked)
                e.Graphics.Clear(e.Item.BackColor);
        }

        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        {
            if (e.ToolStrip.IsDropDown)
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(27, 27, 28)), 0, 0, 2, (int)e.Graphics.ClipBounds.Bottom);
        }
    }
    
    public class MenuStripColorTable : ProfessionalColorTable
    {
        public override Color MenuItemSelected
        {
            get { return Color.FromArgb(51, 51, 52); }
        }
        public override Color MenuItemPressedGradientEnd
        {
            get { return Color.FromArgb(27, 27, 28); }
        }
        public override Color MenuItemPressedGradientBegin
        {
            get { return Color.FromArgb(27, 27, 28); }
        }
        public override Color ButtonSelectedHighlight
        {
            get { return Color.FromArgb(51, 51, 52); }
        }
        public override Color MenuItemSelectedGradientBegin
        {
            get { return Color.FromArgb(51, 51, 52); }
        }
        public override Color MenuItemSelectedGradientEnd
        {
            get { return Color.FromArgb(51, 51, 52); }
        }
        public override Color ToolStripDropDownBackground
        {
            get { return Color.FromArgb(27, 27, 28); }
        }
        public override Color ToolStripBorder
        {
            get { return Color.FromArgb(27, 27, 28); }
        }
        public override Color ButtonSelectedBorder
        {
            get { return Color.Transparent; }
        }
        public override Color ButtonSelectedGradientBegin
        {
            get { return Color.FromArgb(62, 62, 64); }
        }
        public override Color MenuBorder
        {
            get { return Color.FromArgb(51, 51, 55); }
        }
        public override Color MenuItemBorder
        {
            get { return ButtonSelectedBorder; }
        }
        public override Color StatusStripGradientBegin
        {
            get { return Color.FromArgb(45, 45, 48); }
        }
        public override Color StatusStripGradientEnd
        {
            get { return Color.FromArgb(45, 45, 48); }
        }
        public override Color ButtonSelectedGradientEnd
        {
            get { return Color.FromArgb(51, 51, 52); }
        }
        public override Color ButtonSelectedGradientMiddle
        {
            get { return Color.FromArgb(51, 51, 52); }
        }
    }
}
