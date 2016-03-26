using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ASM
{
    public partial class ExceptionForm : Form
    {
        [DllImport("gdi32")]
        private extern static int SetDIBitsToDevice(HandleRef hDC, int xDest, int yDest, int dwWidth, int dwHeight, int XSrc, int YSrc, int uStartScan, int cScanLines, ref int lpvBits, ref BITMAPINFO lpbmi, uint fuColorUse);

        [StructLayout(LayoutKind.Sequential)]
        private struct BITMAPINFOHEADER
        {
            public int bihSize;
            public int bihWidth;
            public int bihHeight;
            public short bihPlanes;
            public short bihBitCount;
            public int bihCompression;
            public int bihSizeImage;
            public double bihXPelsPerMeter;
            public double bihClrUsed;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct BITMAPINFO
        {
            public BITMAPINFOHEADER biHeader;
            public int biColors;
        }

        private Exception error;
        private Graphics graphics;
        private Graphics gfx;
        private Bitmap bitmap;
        private HandleRef hDCRef;
        private int[] bitData;
        private BITMAPINFO bitmapInfo;

        public ExceptionForm(Exception e)
        {
            InitializeComponent();

            MinimumSize = Size;
            DoubleBuffered = true;
            Disposed += ExceptionForm_Disposed;

            log.Text = e.StackTrace;
            error = e;

            graphics = CreateGraphics();
            hDCRef = new HandleRef(graphics, graphics.GetHdc());
            bitmap = new Bitmap(Width, Height, PixelFormat.Format32bppArgb);
            gfx = Graphics.FromImage(bitmap);
            bitData = new int[Width * Height];

            bitmapInfo = new BITMAPINFO
            {
                biHeader = new BITMAPINFOHEADER()
                {
                    bihBitCount = 32,
                    bihPlanes = 1,
                    bihSize = 40,
                    bihWidth = Width,
                    bihHeight = -Height,
                    bihSizeImage = (Width * Height) / 4
                }
            };
            Controls[Controls.Count - 1].Paint += gOverlay1_Paint;
            Controls[Controls.Count - 1].Invalidate();
        }

        private void gOverlay1_Paint(object sender, PaintEventArgs e)
        {
            var rune = Properties.Resources.rune;
            e.Graphics.DrawImage(rune, Width - rune.Width - 30, 0);

            BitmapData BD = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            Marshal.Copy(BD.Scan0, bitData, 0, Width * Height);
            SetDIBitsToDevice(hDCRef, 0, 0, Width, Height, 0, 0, 0, Height, ref bitData[0], ref bitmapInfo, 0);
            bitmap.UnlockBits(BD);
        }

        private void close_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Abort;
            Close();
        }

        private void restart_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Retry;
            Close();
        }

        private void ignore_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Ignore;
            Close();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            if (DialogResult == DialogResult.Cancel)
                e.Cancel = true;
        }

        private void ExceptionForm_Disposed(object sender, EventArgs e)
        {
            graphics.Dispose();
        }
    }
}