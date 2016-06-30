using MAIDE.UI;
using MAIDE.VM;
using System;
using System.Collections;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MAIDE.Modules
{
    public partial class RegisterControl : UserControl
    {
        private BitArray data;
        private const int Step = 7;
        private const int BitWidth = 5;
        private const int BitHeight = 10;
        private Pen fontPen;
        private Brush fontBrush;

        public readonly Register Register;

        public RegisterControl(Register reg)
        {
            InitializeComponent();

            int size = reg is Register32 ? 32 : reg is Register16 ? 16 : reg is Register8 ? 8 : 0;
            data = new BitArray(size);

            Name = reg.Name;
            Register = reg;

            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);

            Anchor = AnchorStyles.Left | AnchorStyles.Right;
            OnForeColorChanged(null);
            UpdateRegData();
        }

        public void UpdateRegData()
        {
            byte[] bits = Register.GetByte();
            number.Text = Register.ToString();
            ascii.Text = Encoding.ASCII.GetString(bits);

            data = new BitArray(bits);
            Invalidate(false);
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            UpdateRegData();
        }

        protected override void OnForeColorChanged(EventArgs e)
        {
            base.OnForeColorChanged(e);
            fontPen = new Pen(ForeColor);
            fontBrush = new SolidBrush(ForeColor);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            int offestTop = Font.Height + 5;

            for (int i = 0; i < data.Length; i++)
            {
                int x = i * Step;
                e.Graphics.FillRectangle(data[i] ? Brushes.Green : Brushes.Red, x, offestTop, BitWidth, BitHeight);
                x += BitWidth / 2;
                e.Graphics.DrawLine(fontPen, x, BitHeight + offestTop + (i % 8 == 0 ? 2 : 4), x, BitHeight + offestTop + 6);
            }

            int y = BitHeight + offestTop + 7;
            e.Graphics.DrawLine(fontPen, BitWidth / 2, y, data.Length * Step - (BitWidth - BitWidth / 2), y);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            int offestTop = Font.Height + 5;
            if (e.Y > offestTop && e.Y < offestTop + BitHeight)
            {
                int i = e.X / Step;
                if (i < data.Length)
                {
                    data[i] = !data[i];

                    byte[] buff = new byte[data.Count / 8];
                    data.CopyTo(buff, 0);
                    Register.SetByte(buff);

                    UpdateRegData();
                }
            }
        }

        private void ascii_DoubleClick(object sender, EventArgs e)
        {
            if (OverlayEditBox.Show(sender as Control, "Text") == DialogResult.OK)
            {
                byte[] buff = new byte[4];
                for (int i = 0; i < ascii.Text.Length; i++)
                    buff[i] = (byte)ascii.Text[i];

                data = new BitArray(buff);
                Register.SetByte(buff);
                Invalidate(false);
            }
        }

        private void number_DoubleClick(object sender, EventArgs e)
        {
            if (OverlayEditBox.Show(sender as Control, "Text") == DialogResult.OK)
            {
                byte[] buff = BitConverter.GetBytes(int.Parse(number.Text));
                data = new BitArray(buff);
                Register.SetByte(buff);
                Invalidate(false);
            }
        }
    }
}