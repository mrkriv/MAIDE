using System.Windows.Forms;

namespace MAIDE.Utilit
{
    public partial class InputHook
    {
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
