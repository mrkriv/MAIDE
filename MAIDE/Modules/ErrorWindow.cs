using System;
using System.Windows.Forms;
using System.Linq;
using WeifenLuo.WinFormsUI.Docking;
using MAIDE.Utilit;
using System.Collections.Generic;

namespace MAIDE.Modules
{
    [ModuleAtribute(dysplayName = "Ошибки компиляции", defaultShow = true, dock = DockState.DockBottom)]
    public partial class ErrorWindow : DockContent
    {
        private List<ErrorMessage> data = new List<ErrorMessage>();

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
                {
                    dataGridView1.Rows.Clear();
                    data.Clear();
                }
                else
                    foreach (ErrorMessage er in e.NewItems)
                    {
                        dataGridView1.Rows.Add(er.Message, (er.Row + 1).ToString());
                        data.Add(er);
                    }
            }));
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                ErrorMessage er = data[dataGridView1.CurrentRow.Index];
                er.CodeBlock.GoTo(er.Row);
            }
        }
    }
}