using System.Collections.Generic;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using ASM.VM;
using ASM.Utilit;
using System.Collections.Specialized;

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

            MainForm.Instance.Core.ChangedPropertyEvent("RegNames32", regsUpdate);
            MainForm.Instance.Core.ChangedPropertyEvent("RegNames16", regsUpdate);
            MainForm.Instance.Core.ChangedPropertyEvent("RegNames8", regsUpdate);
            regsUpdate();
        }

        void regsUpdate()
        {
            table.Controls.Clear();

            foreach (Register reg in MainForm.Instance.Core.Registers)
                table.Controls.Add(new RegisterControl(reg));
        }
    }
}