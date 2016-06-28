using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.IO;

namespace ASM
{
    internal sealed partial class DocumentForm : DockContent
    {
        public string FileName { get; set; }

        public DocumentForm()
        {
            InitializeComponent();
        }

        public void LoadFile(string filePath)
        {
            Text = Path.GetFileName(filePath);
            map.Load(filePath);
        }

        public CombineRows GetCode()
        {
            return map.GetCode();
        }

        public void Save()
        {
            map.Save();
        }

        public void SaveAs()
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                map.FileName = saveFileDialog.FileName;
                Save();
            }
        }

        private void DocumentForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (map.Modified)
            {
                DialogResult dr = MessageBox.Show(this, Language.SaveQuery, "ASM", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
                if (dr == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }
                else if (dr == DialogResult.Yes)
                    Save();
            }
        }
    }
}