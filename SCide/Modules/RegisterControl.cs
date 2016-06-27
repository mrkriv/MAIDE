using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ASM.Utilit;
using ASM.VM;

namespace ASM.Modules
{
    public partial class RegisterControl : UserControl
    {
        private readonly int bitSize;
        public readonly System.Type RegType;

        public RegisterControl(Register reg)
        {
            InitializeComponent();

            l_name.Text = reg.Name;

            bitSize = reg is Register32 ? 32 : reg is Register16 ? 16 : reg is Register8 ? 8 : 0;
            bitPanel.Controls.Clear();

            for (int i = 0; i < bitSize; i++)
            {
                Button btn = new Button();
                btn.Size = new Size(5, bitPanel.Height);
                btn.Text = "0";
                btn.Click += Bit_Click;
                bitPanel.Controls.Add(btn);
            }

            Anchor = AnchorStyles.Left | AnchorStyles.Right;
        }

        private void Bit_Click(object _sender, EventArgs e)
        {
            Button sender = _sender as Button;
            sender.Text = sender.Text == "0" ? "1" : "0";
        }
    }
}