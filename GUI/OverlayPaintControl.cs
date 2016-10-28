using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MAIDE.UI
{
    public class OverlayPaintControl : Control
    {
        private PaintEventHandler overlayPaintEvent;
        private bool isInit;

        public event PaintEventHandler OverlayPaint
        {
            add
            {
                if (overlayPaintEvent != null)
                    overlayPaintEvent += value;
                else
                    overlayPaintEvent = new PaintEventHandler(value);
            }
            remove
            {
                overlayPaintEvent -= value;
            }
        }

        public OverlayPaintControl()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
        }

        private void init()
        {
            Parent.Paint += Parent_Paint;
            Parent.Layout += Parent_Layout;

            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);

            OnResize(null);
            isInit = true;
        }

        protected override void OnParentChanged(EventArgs e)
        {
            if (!isInit && Parent != null)
                init();

            base.OnParentChanged(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (overlayPaintEvent == null)
                return;
            
            Rectangle rect = new Rectangle(0, 0, Parent.Width, Parent.Height);
            BufferedGraphics buffer = BufferedGraphicsManager.Current.Allocate(Parent.CreateGraphics(), rect);

            overlayPaintEvent.Invoke(this, new PaintEventArgs(buffer.Graphics, rect));

            buffer.Render();
        }

        private void Parent_Paint(object sender, PaintEventArgs e)
        {
            Invalidate(false);
        }

        private void Parent_Layout(object sender, LayoutEventArgs e)
        {
            BringToFront();
        }

        protected override void OnResize(EventArgs e)
        {
            Size = new Size(1, 1);
        }
    }
}