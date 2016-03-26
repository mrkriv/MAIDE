using System;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.Collections.Generic;
using System.Drawing;

namespace ASM
{
    internal sealed partial class DocumentForm : DockContent
    {
        public List<ErrorLine> Errors = new List<ErrorLine>();
        public string FilePath { get; set; }

        public DocumentForm()
        {
            InitializeComponent();
            Code.TextChanged += (s, e) => { AddOrRemoveAsteric(); };
        }

        private void AddOrRemoveAsteric()
        {
            if (Code.Modified)
            {
                if (!Text.EndsWith(" *"))
                    Text += " *";
            }
            else
            {
                if (Text.EndsWith(" *"))
                    Text = Text.Substring(0, Text.Length - 2);
            }
        }

        public bool Save()
        {
            if (string.IsNullOrEmpty(FilePath))
                return SaveAs();

            return Save(FilePath);
        }

        public bool Save(string filePath)
        {
            using (FileStream fs = File.Create(filePath))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                    bw.Write(Code.Text.ToCharArray(), 0, Code.Text.Length - 1);
            }

            Code.Modified = false;
            return true;
        }

        public bool SaveAs()
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                FilePath = saveFileDialog.FileName;
                return Save(FilePath);
            }

            return false;
        }

        private void DocumentForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Code.Modified)
            {
                string message = string.Format("The _text in the {0} file has changed.\n\nDo you want to save the changes?", Text.TrimEnd(' ', '*'));
                DialogResult dr = MessageBox.Show(this, message, "ASM", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
                if (dr == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }
                else if (dr == DialogResult.Yes)
                {
                    e.Cancel = !Save();
                    return;
                }
            }
        }
    }
}