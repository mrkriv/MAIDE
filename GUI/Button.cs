using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MAIDE.Utilit;
using System.Windows.Forms;

namespace MAIDE.UI
{
    [DefaultEvent("Click")]
    [DefaultProperty("Text")]
    public class Button : UserControl, IButtonControl
    {
        public enum State
        {
            Normal,
            Hover,
            Down
        }

        [Browsable(false)]
        public State Status { get; private set; }

        [Category("Appearance")]
        [DefaultValue(typeof(Color), "0,0,0,0")]
        public Color ColorNormal { get; set; }
        [Category("Appearance")]
        [DefaultValue(typeof(Color), "0,0,0,0")]
        public Color ColorHover { get; set; }
        [Category("Appearance")]
        [DefaultValue(typeof(Color), "0,0,0,0")]
        public Color ColorDown { get; set; }

        [DefaultValue(null)]
        [Category("Appearance")]
        public Image ImageNormal { get; set; }
        [DefaultValue(null)]
        [Category("Appearance")]
        public Image ImageHover { get; set; }
        [DefaultValue(null)]
        [Category("Appearance")]
        public Image ImageDown { get; set; }

        [Category("CatBehavior")]
        [DefaultValue(DialogResult.None)]
        public DialogResult DialogResult { get; set; }

        public Button()
        {
            BackColor = Color.FromArgb(0, 0, 0, 0);
            ColorNormal = ColorNormal;
            ColorHover = ColorNormal;
            ColorDown = ColorNormal;

            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            PropertyJoin.ChangedPropertyEvent(this, new string[] {
                "ColorNormal" ,
                "ColorHover" ,
                "ColorDown" ,
                "ImageNormal" ,
                "ImageHover" ,
                "ImageDown" }, Invalidate);
        }
        
        protected override void OnPaint(PaintEventArgs e)
        {
            SolidBrush b = new SolidBrush(Status == State.Down ? ColorDown : Status == State.Hover ? ColorHover : ColorNormal);
            if (b.Color.A > 0)
                e.Graphics.FillRectangle(b, e.ClipRectangle);

            Image img = Status == State.Down ? ImageDown : Status == State.Hover ? ImageHover : ImageNormal;
            if (img != null)
                e.Graphics.DrawImage(img, 0, 0, Width, Height);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            Status = State.Hover;
            Invalidate(false);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            Status = State.Normal;
            Invalidate(false);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            Status = State.Down;
            Invalidate(false);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            Status = State.Hover;
            Invalidate(false);
        }

        public void NotifyDefault(bool value) { }

        public void PerformClick()
        {
            if (CanSelect)
                InvokeOnClick(this, EventArgs.Empty);
        }
    }
}