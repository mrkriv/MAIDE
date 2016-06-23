using System.Collections.Generic;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using ASM.VM;
using ASM.Utilit;

namespace ASM.Modules
{
    [ModuleAtribute(dysplayName = "Регистры", defaultShow = false, dock = DockState.DockRightAutoHide)]
    public partial class RegistersWindow : DockContent
    {
        public static RegistersWindow Instance { get; private set; }

        public RegistersWindow()
        {
            InitializeComponent();
            Instance = this;

        }

        void addReg(Register reg)
        {
            RegisterControl rc = new RegisterControl();
            rc.BitSize = reg is Register32 ? 32 : reg is Register32 ? 16 : 8;
            table.Controls.Add(rc);
        }
    }
}