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
    public class Button : Control, IButtonControl, IUsePalette
    {
        public enum State
        {
            Normal,
            Hover,
            Down
        }

        private StringFormat textFormat = new StringFormat();
        private ContentAlignment textAlign;

        [Browsable(false)]
        public State Status { get; private set; }

        [Category("Appearance")]
        [DefaultValue(true)]
        public bool UsePalette { get; set; }

        [Category("Appearance")]
        [DefaultValue(0)]
        public int BorderWidth { get; set; }
        [Category("Appearance")]
        [DefaultValue(0)]
        public int BorderRounding { get; set; }
        [Category("Appearance")]
        [DefaultValue(typeof(Color), "240,240,240")]
        public Color BorderColorNormal { get; set; }
        [Category("Appearance")]
        [DefaultValue(typeof(Color), "240,240,240")]
        public Color BorderColorHover { get; set; }
        [Category("Appearance")]
        [DefaultValue(typeof(Color), "240,240,240")]
        public Color BorderColorDown { get; set; }

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

        [Category("Appearance")]
        [DefaultValue(true)]
        public bool DrawText { get; set; }
        
        [Category("Appearance")]
        [DefaultValue(ContentAlignment.MiddleCenter)]
        public ContentAlignment TextAlign
        {
            get { return textAlign; }
            set
            {
                if (textAlign == value)
                    return;

                int p = (int)Math.Log((double)value, 2);
                textFormat.Alignment = (StringAlignment)(p % 4);
                textFormat.LineAlignment = (StringAlignment)(p / 4);
                textAlign = value;
                Invalidate(false);
            }
        }

        public Button()
        {
            this.LoadDefaultProperties();
            
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            PropertyJoin.ChangedPropertyEvent(this, new string[] {
                "ColorNormal" ,
                "ImageNormal" ,
                "DrawText",
                "BorderColorNormal",
                "BorderWidth",
                "BorderRounding",
                "Text",
                //"ColorHover" ,
                //"ColorDown" ,
                //"ImageHover" ,
                //"ImageDown",
                //"BorderColorDown",
                //"BorderColorHover",
            }, Invalidate);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            SolidBrush brush = new SolidBrush(Status == State.Down ? ColorDown : Status == State.Hover ? ColorHover : ColorNormal);
            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);

            if (brush.Color.A > 0)
                e.Graphics.FillRectangle(brush, e.ClipRectangle);

            Image img = Status == State.Down ? ImageDown : Status == State.Hover ? ImageHover : ImageNormal;
            if (img != null)
                e.Graphics.DrawImage(img, 0, 0, Width, Height);

            if (BorderWidth != 0)
            {
                Pen pen = new Pen(Status == State.Down ? BorderColorDown : Status == State.Hover ? BorderColorHover : BorderColorNormal, BorderWidth);
                if (BorderRounding != 0)
                {
                    int b2 = BorderRounding /2;
                    e.Graphics.DrawLine(pen, b2, 0, rect.Right - b2, 0);
                    e.Graphics.DrawLine(pen, 0, b2, 0, rect.Bottom - b2);
                    e.Graphics.DrawLine(pen, b2, rect.Bottom, rect.Right - b2, rect.Bottom);
                    e.Graphics.DrawLine(pen, rect.Right, b2, rect.Right, rect.Bottom - b2);

                    e.Graphics.DrawArc(pen, rect.Right - BorderRounding, rect.Bottom - BorderRounding, BorderRounding, BorderRounding, 0, 90);
                    e.Graphics.DrawArc(pen, 0, rect.Bottom - BorderRounding, BorderRounding, BorderRounding, 90, 90);
                    e.Graphics.DrawArc(pen, 0, 0, BorderRounding, BorderRounding, 180, 90);
                    e.Graphics.DrawArc(pen, rect.Right - BorderRounding, 0, BorderRounding, BorderRounding, 270, 90);
                }
                else
                    e.Graphics.DrawRectangle(pen, rect);
            }

            if (DrawText)
            {
                brush.Color = ForeColor;
                e.Graphics.DrawString(Text, Font, brush, rect, textFormat);
            }
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

        public void AppyPalette()
        {
            ColorNormal = Palette.GetColor("ButtonNormal");
            ColorHover = Palette.GetColor("ButtonHover");
            ColorDown = Palette.GetColor("ButtonDown");
        }
    }
}