using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MAIDE.Utilit;

namespace MAIDE.UI
{
    public class MenuPaletteRenderer : ToolStripProfessionalRenderer
    {
        public MenuPaletteRenderer() : base(new MenuStripColorTable())
        {
            RoundedEdges = false;
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
            {
                e.Graphics.DrawLine(new Pen(Palette.GetColor("MenuBorder")), 0, 0, 0, (int)e.Graphics.ClipBounds.Bottom);
                e.Graphics.DrawLine(new Pen(Palette.GetColor("MenuPressed")), 1, 0, 1, (int)e.Graphics.ClipBounds.Bottom);
            }
        }
    }

    public class MenuStripColorTable : ProfessionalColorTable
    {
        public override Color MenuItemSelected
        {
            get { return Palette.GetColor("MenuSelected"); }
        }
        public override Color MenuItemPressedGradientEnd
        {
            get { return Palette.GetColor("MenuPressed"); }
        }
        public override Color MenuItemPressedGradientBegin
        {
            get { return Palette.GetColor("MenuPressed"); }
        }
        public override Color ButtonSelectedHighlight
        {
            get { return Palette.GetColor("MenuSelected"); }
        }
        public override Color MenuItemSelectedGradientBegin
        {
            get { return Palette.GetColor("MenuSelected"); }
        }
        public override Color MenuItemSelectedGradientEnd
        {
            get { return Palette.GetColor("MenuSelected"); }
        }
        public override Color ToolStripDropDownBackground
        {
            get { return Palette.GetColor("MenuPressed"); }
        }
        public override Color ToolStripBorder
        {
            get { return Palette.GetColor("MenuBorder"); }
        }
        public override Color ButtonSelectedBorder
        {
            get { return Color.Transparent; }
        }
        public override Color MenuBorder
        {
            get { return Palette.GetColor("MenuBorder"); }
        }
        public override Color MenuItemBorder
        {
            get { return ButtonSelectedBorder; }
        }
        public override Color StatusStripGradientBegin
        {
            get { return Palette.GetColor("Background"); }
        }
        public override Color StatusStripGradientEnd
        {
            get { return Palette.GetColor("Background"); }
        }
        public override Color ButtonSelectedGradientEnd
        {
            get { return Palette.GetColor("MenuSelected"); }
        }
        public override Color ButtonSelectedGradientMiddle
        {
            get { return Palette.GetColor("MenuSelected"); }
        }
        public override Color ButtonSelectedGradientBegin
        {
            get { return Palette.GetColor("MenuSelected"); }
        }
    }
}