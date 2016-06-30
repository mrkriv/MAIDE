using System.Collections.Generic;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using MAIDE.VM;
using MAIDE.Utilit;
using System.Collections.Specialized;

namespace MAIDE.Modules
{
    [ModuleAtribute(dysplayName = "Регистры", defaultShow = false, dock = DockState.DockRightAutoHide)]
    public partial class RegistersWindow : DockContent
    {
        public static RegistersWindow Instance { get; private set; }

        public RegistersWindow()
        {
            InitializeComponent();
            Instance = this;

            MainForm.Instance.Core.ChangedPropertyEvent("RegNames32", regsCreate);
            MainForm.Instance.Core.ChangedPropertyEvent("RegNames16", regsCreate);
            MainForm.Instance.Core.ChangedPropertyEvent("RegNames8", regsCreate);
            MainForm.Instance.Core.StateChanged += Core_StateChanged;

            regsCreate();
        }

        private void Core_StateChanged(object sender, Core.StateChangedEventArgs e)
        {
            if (Visible && e.New == Core.State.Finish || e.New == Core.State.Pause)
            {
                foreach (RegisterControl c in table.Controls)
                    c.UpdateRegData();
            }
        }

        void regsCreate()
        {
            table.Controls.Clear();
            table.RowCount = 0;

            foreach (Register reg in MainForm.Instance.Core.Registers)
                table.Controls.Add(new RegisterControl(reg));
        }
    }
}