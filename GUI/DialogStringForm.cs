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
    public partial class DialogStringForm : DefaultForm
    {
        public DialogStringForm()
        {
            InitializeComponent();
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        public static DialogResult Show(string title, string text, ref string source)
        {
            return Show(title, text, '\0', ref source);
        }

        public static DialogResult Show(string title, string text, char passwordChar, ref string source)
        {
            DialogStringForm form = new DialogStringForm();
            form.Text = title;
            form.text.Text = text;
            form.edit.Text = source;
            form.edit.PasswordChar = passwordChar;

            DialogResult result = form.ShowDialog();
            source = form.edit.Text;
            return result;
        }

        private void DialogStringForm_Load(object sender, EventArgs e)
        {
            Focus();
            edit.Focus();
        }
    }
}
