using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.IO;
using MAIDE.UI;
using System.Xml;
using System.Drawing;
using MAIDE.Utilit;
using System.Collections.Specialized;

namespace MAIDE
{
    internal sealed partial class DocumentForm : DockContentProxy
    {
        public string FileName { get; set; }
        public bool Modified { get; set; }

        public DocumentForm()
        {
            InitializeComponent();

            PropertyJoin.Create(CodeEditBox, "CommentChar", Properties.Settings.Default, "CommentChar");

            CodeEditBox.SetSyntaxColor(0, Color.FromArgb(86, 156, 214), null);
            CodeEditBox.SetSyntaxColor(1, Color.FromArgb(144, 105, 162), null);
            CodeEditBox.SetSyntaxColor(2, Color.FromArgb(78, 201, 176), null);
            CodeEditBox.SetSyntaxIcons(imgList);

            foreach (var op in VM.OperationManager.Operations)
                CodeEditBox.AddSyntaxPhrase(op.Name, 0);

            addSyntaxByRegLS(Properties.Settings.Default.Register32);
            addSyntaxByRegLS(Properties.Settings.Default.Register16);
            addSyntaxByRegLS(Properties.Settings.Default.Register8);
        }

        private void addSyntaxByRegLS(StringCollection regs)
        {
            if (regs != null)
            {
                foreach (var c in regs)
                    CodeEditBox.AddSyntaxPhrase(c, 2);
            }
        }

        public void LoadFile(string filePath)
        {
            FileName = filePath;
            Modified = false;

            Text = Path.GetFileName(filePath);
            CodeEditBox.ClearHistory();
            CodeEditBox.Text = "";

            FileStream fs = File.OpenRead(filePath);
            try
            {
                char b1 = (char)fs.ReadByte();
                char b2 = (char)fs.ReadByte();
                fs.Seek(0, SeekOrigin.Begin);

                if (b1 == '<' && b2 == '?')
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(fs);
                    
                    CodeEditBox.Text = doc.DocumentElement.ChildNodes.Get("code").InnerText;
                }
                else
                {
                    byte[] data = new byte[fs.Length];
                    fs.Read(data, 0, (int)fs.Length);

                    CodeEditBox.Text = System.Text.Encoding.UTF8.GetString(data);
                }
            }
            finally
            {
                fs.Close();
            }
        }

        public CodeEditBox.RowReadonlyCollection GetCode()
        {
            return CodeEditBox.Rows;
        }

        public void Save()
        {
            XmlDocument doc = new XmlDocument();
            doc.AppendChild(doc.CreateXmlDeclaration("1.0", "UTF-8", null));
            XmlNode root = doc.CreateNode(XmlNodeType.Element, "project", string.Empty);

            XmlNode code = doc.CreateNode(XmlNodeType.Element, "code", string.Empty);
            code.InnerText = CodeEditBox.Text;

            root.AppendChild(code);
            doc.AppendChild(root);

            StreamWriter fs = new StreamWriter(FileName, false, new System.Text.UTF8Encoding(false));
            doc.Save(fs);
            fs.Close();
            
            Log.AddMessage("{0} saved.", new FileInfo(FileName).Name);

            Modified = false;
        }

        public void SaveAs()
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileName = saveFileDialog.FileName;
                Save();
            }
        }

        private void DocumentForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Modified)
            {
                DialogResult dr = MessageBox.Show(this, Language.SaveQuery, "MAIDE", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
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