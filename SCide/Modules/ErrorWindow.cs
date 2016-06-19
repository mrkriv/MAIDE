using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace ASM
{
    [ModuleAtribute(dysplayName = "Ошибки компиляции", defaultShow = true, dock = DockState.DockBottom)]
    public partial class ErrorWindow : DockContent
    {
        public ErrorWindow()
        {
            InitializeComponent();

            dataGridView1.DataSource = VM.Core.Errors;
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.RowHeadersVisible = false;
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
                MainForm.Instance.ActiveDocument.CodeBlock.GoTo(((ErrorMessageRow)dataGridView1.CurrentRow.DataBoundItem).Row - 1);
        }
    }
}