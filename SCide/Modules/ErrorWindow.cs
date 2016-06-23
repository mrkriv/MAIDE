using System;
using System.Windows.Forms;
using System.Linq;
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
        }

        private void Errors_Changed(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Invoke((Action)(() =>
            {
                if (e.Action.ToString() == "Reset")
                    dataGridView1.Rows.Clear();
                else
                    foreach (ErrorMessageRow er in e.NewItems)
                        dataGridView1.Rows.Add(er.Message, er.Row.ToString());
            }));
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
                MainForm.Instance.ActiveDocument.CodeBlock.GoTo(int.Parse((string)dataGridView1.CurrentRow.Cells[1].Value) - 1);
        }
    }
}