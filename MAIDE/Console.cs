using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ASM
{
    public partial class Console : Form
    {
        public static Console Instance { get; private set; }
        private static ManualResetEvent waitEvent;
        private static bool readMode = false;
        private static bool readOnly = false;
        private static string readData;

        private Console()
        {
            InitializeComponent();

            waitEvent = new ManualResetEvent(true);
            viewport.Text = "";
        }

        private void Console_Load(object sender, EventArgs e)
        {
            Instance = this;
        }

        public static void Create()
        {
            if (Instance == null)
                new Console().Show();
        }

        public static void Clear()
        {
            Create();
            waitEvent.Reset();
            Instance.BeginInvoke((Action)(() => {
                Instance.viewport.Text = "";
            }));
        }

        public static char ReadKey()
        {
            if (Instance != null)
            {
                readOnly = true;
                readMode = true;
                readData = "";
                waitEvent.Reset();
                waitEvent.WaitOne();

                if (readData.Length != 0)
                    return readData[0];
            }
            return '\0';
        }

        public static string ReadLine()
        {
            if (Instance == null)
                return "";

            readOnly = false;
            readMode = true;
            readData = "";
            waitEvent.Reset();
            waitEvent.WaitOne();

            return readData;
        }
        
        private void viewport_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!readMode)
                return;
            
            if (readOnly)
            {
                readData += e.KeyChar;
                waitEvent.Set();
                return;
            }

            switch (e.KeyChar)
            {
                case (char)8:
                    viewport.Text = viewport.Text.Substring(0, viewport.Text.Length - 1);
                    readData = readData.Substring(0, readData.Length - 1);
                    break;
                case (char)13:
                    waitEvent.Set();
                    break;
                case (char)27:
                    //Escape
                    break;
                default:
                    viewport.Text += e.KeyChar;
                    readData += e.KeyChar;
                    break;
            }
        }

        public static void Write(char c)
        {
            Instance.BeginInvoke((Action)(() => {
                Instance.viewport.Text += c;
            }));
        }

        public static void Write(string text)
        {
            Instance.BeginInvoke((Action)(() => {
                Instance.viewport.Text += text;
            }));
        }

        public static void WriteLine(string text)
        {
            Instance.BeginInvoke((Action)(() => {
                Instance.viewport.Text += text + "\r\n";
            }));
        }

        public static void MoveCaretToEnd()
        {
            Instance.BeginInvoke((Action)(() => {
                if (Instance.viewport.Text.Length != 0)
                {
                    Instance.viewport.SelectionStart = Instance.viewport.Text.Length - 1;
                    Instance.viewport.SelectionLength = 0;
                }
            }));
        }

        public static void Destroy()
        {
            if (Instance == null)
                return;

            waitEvent.Set();
            Instance.BeginInvoke((Action)Instance.Close);
            Instance = null;
        }

        protected override void OnClosed(EventArgs e)
        {
            Instance = null;
            base.OnClosed(e);
        }
    }
}
