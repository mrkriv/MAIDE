using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using ASM.UI;

namespace ASM
{
    internal sealed partial class DocumentForm : DockContent
    {
        public List<ErrorMessageRow> Errors = new List<ErrorMessageRow>();
        public string FilePath { get; set; }
        private List<CodeBlock> blocks = new List<CodeBlock>();
        public CodeBlock CodeBlock;

        public DocumentForm()
        {
            InitializeComponent();
            CodeBlock.MainBlock = addBlock();
            CodeBlock.MainBlock.SetFillMode(true);
        }

        public void LoadFile(string filePath)
        {
            CodeBlock.MainBlock.SetCode(File.ReadAllText(filePath));

            Text = Path.GetFileName(filePath);
            FilePath = filePath;
        }

        private CodeBlock addBlock()
        {
            CodeBlock bl = new CodeBlock();
            Controls.Add(bl);
            blocks.Add(bl);
            CodeBlock = bl;
            return bl;
        }

        public CombineRows CombineCode()
        {
            CombineRows cb = new CombineRows();

            foreach (var b in blocks)
                cb.Add(b.GetCodeRows());

            return cb;
        }

        private void AddOrRemoveAsteric()
        {
            //if (Code.Modified)
            //{
            //    if (!Text.EndsWith(" *"))
            //        Text += " *";
            //}
            //else
            //{
            //    if (Text.EndsWith(" *"))
            //        Text = Text.Substring(0, Text.Length - 2);
            //}
        }

        public bool Save()
        {
            if (string.IsNullOrEmpty(FilePath))
                return SaveAs();

            return Save(FilePath);
        }

        public bool Save(string filePath)
        {
            File.WriteAllText(filePath, blocks[0].GetCode());
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
           // if (Code.Modified)
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