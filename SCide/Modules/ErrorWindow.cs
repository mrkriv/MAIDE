using System;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using ASM.Utilit;

namespace ASM
{
    [ModuleAtribute(dysplayName = "Ошибки компиляции", defaultShow = true, dock = DockState.DockBottom)]
    public partial class ErrorWindow : DockContent
    {
        public ErrorWindow()
        {
            InitializeComponent();
            VM.Core.Errors.CollectionChanged += Errors_Changed;

            dataGridView1.DataSource = new BindingSource(VM.Core.Errors, null);
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.RowHeadersVisible = false;
        }

        private void Errors_Changed(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
                MainForm.Instance.ActiveDocument.CodeBlock.GoTo(((ErrorMessageRow)dataGridView1.CurrentRow.DataBoundItem).Row - 1);
        }
    }
}