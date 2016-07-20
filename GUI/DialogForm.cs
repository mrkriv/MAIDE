using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MAIDE.UI
{
    public partial class DialogForm : DefaultForm
    {
        private DialogForm()
        {
            InitializeComponent();
            Size = MinimumSize;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;
            Close();
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
            Close();
        }

        private void btnRetry_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Retry;
            Close();
        }

        private void btnAbort_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Abort;
            Close();
        }

        private void btnIgnore_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Ignore;
            Close();
        }

        public static DialogResult Show(string title, string text)
        {
            return Show(title, text, MessageBoxButtons.OK);
        }

        public static DialogResult Show(string title, string text, MessageBoxButtons buttons)
        {
            DialogForm form = new DialogForm();
            form.text.Text = text;
            form.Text = title;

            form.AbortRetryIgnore.Visible = buttons == MessageBoxButtons.AbortRetryIgnore;
            form.OKCancel.Visible = buttons == MessageBoxButtons.OKCancel;
            form.RetryCancel.Visible = buttons == MessageBoxButtons.RetryCancel;
            form.YesNo.Visible = buttons == MessageBoxButtons.YesNo;
            form.YesNoCancel.Visible = buttons == MessageBoxButtons.YesNoCancel;
            form.Ok.Visible = buttons == MessageBoxButtons.OK;

            return form.ShowDialog();
        }
    }
}