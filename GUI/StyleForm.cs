using MAIDE.Utilit;
using MAIDE.Utilit.WinAPI;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Point = System.Drawing.Point;

namespace MAIDE.UI
{
    public class StyleForm : LocForm, IUsePalette
    {
        private static int doubleClickTime;
        //private ShadowForm shadow;
        private FormWindowState oldFormState;
        private DateTime lastClickTime;

        [Category("Appearance")]
        [DefaultValue(true)]
        public bool UsePalette { get; set; }

        [DefaultValue(null)]
        [Category("Appearance")]
        public Image TitleIcon { get; set; }
        [Category("Appearance")]
        public Rectangle TitleIconTransform { get; set; }

        [Category("Appearance")]
        public Padding Border { get; set; }
        [Category("Appearance")]
        public Padding Shadow { get; set; }
        [DefaultValue(true)]
        [Category("Appearance")]
        public bool ShadowEnable { get; set; }

        [DefaultValue(true)]
        [Category("Appearance")]
        public bool ResizeEnable { get; set; }

        [DefaultValue(null)]
        [Category("Appearance")]
        public Control CloseButton { get; set; }
        [DefaultValue(null)]
        [Category("Appearance")]
        public Control MaxButton { get; set; }
        [DefaultValue(null)]
        [Category("Appearance")]
        public Control MinButton { get; set; }
        [DefaultValue(null)]
        [Category("Appearance")]
        public Control RestoreButton { get; set; }

        [Category("Appearance")]
        public Color BorderActiveColor { get; set; }
        [Category("Appearance")]
        public Color BorderDisableColor { get; set; }
        [Category("Appearance")]
        public Color ShadowActiveColor { get; set; }
        [Category("Appearance")]
        public Color ShadowDisableColor { get; set; }

        [Category("Appearance")]
        public Point FullModeButtonOffest { get; set; }

        public StyleForm()
        {
            this.LoadDefaultProperties();
            SetStyle(ControlStyles.OptimizedDoubleBuffer, false);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);

            doubleClickTime = API.GetDoubleClickTime();
            FormBorderStyle = FormBorderStyle.None;
            MaximumSize = Screen.PrimaryScreen.WorkingArea.Size;
            oldFormState = WindowState;
            ShowIcon = false;

            PropertyJoin.ChangedPropertyEvent(this, new string[] {
                "Text",
                "TitleIcon",
                "TitleIconTransform",
                "Border",
                "Shadow",
                "BorderActiveColor",
                "BorderDisableColor",
                "ShadowActiveColor",
                "ShadowDisableColor"
            }, Invalidate);
        }

        protected override void OnLoad(EventArgs e)
        {
            if (CloseButton != null)
                CloseButton.Click += (s, a) => { Close(); };
            if (MaxButton != null)
                MaxButton.Click += (s, a) => { WindowState = FormWindowState.Maximized; };
            if (MinButton != null)
                MinButton.Click += (s, a) => { WindowState = FormWindowState.Minimized; };
            if (RestoreButton != null)
                RestoreButton.Click += (s, a) => { WindowState = FormWindowState.Normal; };

            updateShadow();
            base.OnLoad(e);
        }

        private void updateShadow()
        {
            if (DesignMode)
                return;

            //if (Shadow.All != 0)
            //{
            //    if (shadow == null)
            //        shadow = new ShadowForm();

            //    shadow.Visible = WindowState == FormWindowState.Normal;
            //    if (shadow.Visible)
            //    {
            //        shadow.Location = Location.Substract(Shadow.Left, Shadow.Top);
            //        shadow.Size = Size.Add(Shadow.Horizontal, Shadow.Vertical);
            //    }
            //}
            //else if (shadow == null)
            //{
            //    shadow.Close();
            //    shadow = null;
            //}
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                if (ShadowEnable)
                    cp.ClassStyle |= 0x20000;
                return cp;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(BackColor);

            bool isActive = ActiveForm == this || DesignMode;

            if (TitleIcon != null)
                e.Graphics.DrawImage(TitleIcon, TitleIconTransform);

            Color foreColor = isActive ? ForeColor : ForeColor.Multiplay(.75f);
            e.Graphics.DrawString(Text, Font, new SolidBrush(foreColor), TitleIconTransform.CenterRight().Add(10, Font.Height / -2));

            if (WindowState == FormWindowState.Normal)
            {
                Brush brush = new SolidBrush(isActive ? BorderActiveColor : BorderDisableColor);
                e.Graphics.FillRectangle(brush, 0, 0, Width - 1, Border.Top);
                e.Graphics.FillRectangle(brush, 0, Height - Border.Bottom, Width, Height);
                e.Graphics.FillRectangle(brush, 0, 0, Border.Left, Height);
                e.Graphics.FillRectangle(brush, Width - Border.Left, 0, Width, Height);
            }
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            Invalidate(false);
        }

        protected override void OnDeactivate(EventArgs e)
        {
            base.OnDeactivate(e);
            Invalidate(false);
        }

        private bool isHitTitle(Point location)
        {
            if (WindowState == FormWindowState.Normal)
                return location.Y > Border.Top && location.Y <= Padding.Top;
            else
                return location.Y <= Padding.Top;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (ResizeEnable && WindowState == FormWindowState.Normal)
            {
                if (e.Location.X <= Border.Left)
                {
                    if (e.Location.Y <= Border.Top)
                        Cursor = Cursors.SizeNWSE;
                    else if (e.Location.Y > Height - Border.Bottom - 1)
                        Cursor = Cursors.SizeNESW;
                    else
                        Cursor = Cursors.SizeWE;
                }
                else if (e.Location.X >= Width - Border.Right - 1)
                {
                    if (e.Location.Y <= Border.Top)
                        Cursor = Cursors.SizeNESW;
                    else if (e.Location.Y > Height - Border.Bottom - 1)
                        Cursor = Cursors.SizeNWSE;
                    else
                        Cursor = Cursors.SizeWE;
                }
                else if (e.Location.Y <= Border.Top || e.Location.Y >= Height - Border.Bottom - 1)
                    Cursor = Cursors.SizeNS;
                else
                    Cursor = Cursors.Default;
            }
            else
                Cursor = Cursors.Default;

            base.OnMouseMove(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            Cursor = Cursors.Default;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                HitTestValues hit = HitTestValues.HTNOWHERE;

                if (isHitTitle(e.Location))
                {
                    if ((DateTime.Now - lastClickTime).TotalMilliseconds <= doubleClickTime)
                    {
                        if (MaxButton != null && RestoreButton != null)
                            WindowState = WindowState == FormWindowState.Normal ? FormWindowState.Maximized : FormWindowState.Normal;
                    }
                    else
                        hit = HitTestValues.HTCAPTION;
                    lastClickTime = DateTime.Now;
                }
                else if (ResizeEnable && WindowState == FormWindowState.Normal)
                {
                    if (e.Location.X <= Border.Left)
                    {
                        if (e.Location.Y <= Border.Top)
                            hit = HitTestValues.HTTOPLEFT;
                        else if (e.Location.Y > Height - Border.Bottom - 1)
                            hit = HitTestValues.HTBOTTOMLEFT;
                        else
                            hit = HitTestValues.HTLEFT;
                    }
                    else if (e.Location.X >= Width - Border.Right - 1)
                    {
                        if (e.Location.Y <= Border.Top)
                            hit = HitTestValues.HTTOPRIGHT;
                        else if (e.Location.Y > Height - Border.Bottom - 1)
                            hit = HitTestValues.HTBOTTOMRIGHT;
                        else
                            hit = HitTestValues.HTRIGHT;
                    }
                    else if (e.Location.Y <= Border.Top)
                        hit = HitTestValues.HTTOP;
                    else if (e.Location.Y >= Height - Border.Bottom - 1)
                        hit = HitTestValues.HTBOTTOM;
                }

                if (hit != HitTestValues.HTNOWHERE && !IsDisposed)
                {
                    API.ReleaseCapture();
                    API.SendMessage(Handle, Consts.WM_NCLBUTTONDOWN, (int)hit, 0);
                }
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (!DesignMode && Visible)
            {
                if (oldFormState != WindowState)
                {
                    bool fullMode = WindowState == FormWindowState.Normal;
                    Point delta = fullMode ? FullModeButtonOffest : FullModeButtonOffest.Multiplay(-1);

                    if (CloseButton != null)
                        CloseButton.Location = CloseButton.Location.Add(delta);

                    if (MinButton != null)
                        MinButton.Location = MinButton.Location.Add(delta);

                    if (MaxButton != null)
                    {
                        MaxButton.Visible = fullMode;
                        MaxButton.Location = MaxButton.Location.Add(delta);
                    }

                    if (RestoreButton != null)
                    {
                        RestoreButton.Visible = !fullMode;
                        RestoreButton.Location = RestoreButton.Location.Add(delta);
                    }

                    oldFormState = WindowState;
                }
            }

            Invalidate(false);
        }

        public void AppyPalette()
        {
            BackColor = Palette.GetColor("Background");
            BorderActiveColor = Palette.GetColor("WindowFrameActive");
            BorderDisableColor = Palette.GetColor("WindowFrameDisable");
            ForeColor = Palette.GetColor("FontTitle");
        }
    }
}