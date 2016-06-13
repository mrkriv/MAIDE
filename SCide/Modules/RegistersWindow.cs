using System.Collections.Generic;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace ASM
{
    [ModuleAtribute(dysplayName = "Регистры", defaultShow = false, dock = DockState.DockRightAutoHide)]
    public partial class RegistersWindow : DockContent
    {
        public static BindingSource Binding { get; private set; }
        public static RegistersWindow Instance { get; private set; }

        public RegistersWindow()
        {
            InitializeComponent();

            Binding = new BindingSource();
            Binding.AllowNew = true;
            Binding.DataSource = new List<VM.Register>();

            CheckForIllegalCrossThreadCalls = false;

            dataGridView1.DataSource = Binding;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.RowHeadersVisible = false;
            Instance = this;
        }

        public new void Refresh()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = Binding;
        }
    }
}