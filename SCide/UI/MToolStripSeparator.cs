using System.Drawing;
using System.Windows.Forms.Design;
using System.Windows.Forms;

namespace ASM.UI
{
    [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ToolStrip | ToolStripItemDesignerAvailability.ContextMenuStrip)]
    public class MToolStripSeparator : ToolStripSeparator
    {
        public bool IsVertical
        {
            get
            {
                ToolStrip parent = Parent;

                if (parent == null)
                    parent = Owner;

                ToolStripDropDownMenu dropDownMenu = parent as ToolStripDropDownMenu;
                if (dropDownMenu != null)
                    return false;

                switch (parent.LayoutStyle)
                {
                    case ToolStripLayoutStyle.VerticalStackWithOverflow:
                        return false;
                    case ToolStripLayoutStyle.HorizontalStackWithOverflow:
                    case ToolStripLayoutStyle.Flow:
                    case ToolStripLayoutStyle.Table:
                    default:
                        return true;
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (Owner != null)
            {
                e.Graphics.Clear(BackColor);
                Point center = e.ClipRectangle.Center();
                if (IsVertical)
                {
                    e.Graphics.DrawLine(new Pen(ForeColor.GetMultiplay(.5f), 1), center.X-1, e.ClipRectangle.Top, center.X-1, e.ClipRectangle.Bottom);
                    e.Graphics.DrawLine(new Pen(ForeColor, 1), center.X, e.ClipRectangle.Top, center.X, e.ClipRectangle.Bottom);
                }
                else
                    e.Graphics.DrawLine(new Pen(ForeColor, 1), e.ClipRectangle.Left, center.Y, e.ClipRectangle.Right, center.Y);
            }
        }
    }
}