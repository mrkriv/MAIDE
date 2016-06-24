using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ASM.Utilit
{
    public partial class InputHook
    {
        [StructLayout(LayoutKind.Sequential)]
        private struct Point
        {
            public int X;
            public int Y;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct MouseLLHookStruct
        {
            public Point Point;
            public int MouseData;
            public int Flags;
            public int Time;
            public int ExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct KeyboardHookStruct
        {
            public int VirtualKeyCode;
            public int ScanCode;
            public int Flags;
            public int Time;
            public int ExtraInfo;
        }

        public class MouseEventExtArgs : MouseEventArgs
        {
            private bool m_Handled;

            public MouseEventExtArgs(MouseButtons buttons, int clicks, int x, int y, int delta)
                : base(buttons, clicks, x, y, delta)
            { }

            internal MouseEventExtArgs(MouseEventArgs e) : base(e.Button, e.Clicks, e.X, e.Y, e.Delta)
            { }

            public bool Handled
            {
                get { return m_Handled; }
                set { m_Handled = value; }
            }
        }
    }
}
