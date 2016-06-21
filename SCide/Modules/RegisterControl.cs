using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ASM.Modules
{
    public partial class RegisterControl : UserControl
    {
        int bitSize;

        [DefaultValue(32)]
        [Category("Appearance")]
        public int BitSize
        {
            get { return bitSize; }
            set
            {
                bitSize = value;
                rebuild();
            }
        }

        public RegisterControl()
        {
            InitializeComponent();
            this.LoadDefaultProperties();
        }

        private void rebuild()
        {
            bitPanel.Controls.Clear();
            for (int i = 0; i < bitSize; i++)
            {
                Button btn = new Button();
                btn.Size = new Size(bitPanel.Height, bitPanel.Height);
                btn.Text = "0";
                btn.Click += Bit_Click;
                bitPanel.Controls.Add(btn);
            }
        }

        private void Bit_Click(object _sender, EventArgs e)
        {
            Button sender = _sender as Button;
            sender.Text = sender.Text == "0" ? "1" : "0";
        }
    }
}