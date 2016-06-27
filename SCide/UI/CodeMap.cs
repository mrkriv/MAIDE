using ASM.Utilit;
using ASM.VM;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace ASM.UI
{
    public class CodeMap : Control
    {
        public string FileName { get; set; }
        public bool Modified { get; private set; }
        private CodeBlock mainBlock;
        private Point mousePos;

        public CodeMap()
        {
            DoubleBuffered = true;
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            
            mainBlock = createBlock();
            mainBlock.Title = "Main";
            mainBlock.Fill = true;
        }

        public CombineRows GetCode()
        {
            CombineRows cb = new CombineRows();

            foreach (CodeBlock b in Controls)
                cb.Add(b.CodeEditBox.Rows);

            return cb;
        }

        private CodeBlock createBlock()
        {
            CodeBlock block = new CodeBlock();
            block.CodeEditBox.TextChanged += CodeEditBox_TextChanged;
            block.Drag += Block_Drag;
            Controls.Add(block);
            return block;
        }

        private void Block_Drag(object sender, DragDropPanel.DragEventArgs e)
        {
            Invalidate(false);
        }

        private void CodeEditBox_TextChanged(object sender, CodeEditBox.TextChangedEventArgs e)
        {
            Modified = true;
            //string text = e.Row.ToString();
            //if(Refactor.GetLineOpType(text) == Type.Condition)
            //{
            //}
        }

        public void Save()
        {
            XmlDocument doc = new XmlDocument();
            doc.AppendChild(doc.CreateXmlDeclaration("1.0", "UTF-8", null));
            XmlNode root = doc.CreateNode(XmlNodeType.Element, "project", string.Empty);
            XmlNode n_blocks = doc.CreateNode(XmlNodeType.Element, "blocks", string.Empty);

            foreach (CodeBlock b in Controls)
                n_blocks.AppendChild(b.Save(doc));

            root.AppendChild(n_blocks);
            doc.AppendChild(root);

            StreamWriter fs = new StreamWriter(FileName, false, new System.Text.UTF8Encoding(false));
            doc.Save(fs);
            fs.Close();

            Modified = false;
        }

        public void Load(string fileName)
        {
            FileName = fileName;
            Modified = false;
            Controls.Clear();

            FileStream fs = File.OpenRead(fileName);
            char b1 = (char)fs.ReadByte();
            char b2 = (char)fs.ReadByte();
            fs.Seek(0, SeekOrigin.Begin);

            if (b1 == '<' && b2 == '?')
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(fs);

                XmlNode n_blocks = doc.DocumentElement.ChildNodes.Get("blocks");

                foreach (XmlNode b in n_blocks.ChildNodes)
                {
                    CodeBlock block = createBlock();
                    block.Load(b);
                }
            }
            else
            {
                byte[] data = new byte[fs.Length];
                fs.Read(data, 0, (int)fs.Length);
                
                mainBlock = createBlock();
                mainBlock.Title = "Main";
                mainBlock.CodeEditBox.Text = System.Text.Encoding.UTF8.GetString(data);
                mainBlock.Fill = true;
            }
            fs.Close();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            foreach (CodeBlock b in Controls)
            {
                if (b.ExtTrue != null)
                    e.Graphics.DrawCubicLine(Pens.Blue, b.Bounds.CenterBottom().Add(5, 0), b.ExtTrue.Bounds.CenterTop());
                if (b.ExtFalse != null)
                    e.Graphics.DrawCubicLine(Pens.Red, b.Bounds.CenterBottom().Substract(5, 0), b.ExtFalse.Bounds.CenterTop());
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                Point delta = e.Location.Substract(mousePos);
                foreach (Control c in Controls)
                    c.Location = c.Location.Add(delta);

                Invalidate();
            }
            else
                base.OnMouseMove(e);

            mousePos = e.Location;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            mousePos = e.Location;
            base.OnMouseDown(e);
        }
    }
}