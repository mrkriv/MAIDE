using MAIDE.Utilit;
using System;
using System.Collections.Specialized;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;

namespace MAIDE.UI
{
    public class CodeBlock : DragDropPanel
    {
        private bool fill;
        private Button fillSetButton;
        public CodeEditBox CodeEditBox;
        public CodeBlock ExtTrue;
        private ImageList imgList;
        private System.ComponentModel.IContainer components;
        public CodeBlock ExtFalse;

        public bool Fill
        {
            get { return fill; }
            set
            {
                Dock = value ? DockStyle.Fill : DockStyle.None;
                DragDropEnable = !value;
                fill = value;
                if (fill)
                    BringToFront();
            }
        }

        public string Title
        {
            get { return title.Text; }
            set { title.Text = value; }
        }

        public CodeBlock()
        {
            InitializeComponent();
            PropertyJoin.Create(CodeEditBox, "CommentChar", Properties.Settings.Default, "CommentChar");

            Fill = false;

            CodeEditBox.SetSyntaxColor(0, Color.FromArgb(86, 156, 214), null);
            CodeEditBox.SetSyntaxColor(2, Color.FromArgb(78, 201, 176), null);
            CodeEditBox.SetSyntaxIcons(imgList);

            foreach (var op in VM.Operators.OperationsList)
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

        private void fillSetButton_Click(object sender, EventArgs e)
        {
            Fill = !Fill;
        }

        private void caption_DoubleClick(object sender, EventArgs e)
        {
            OverlayEditBox.Show(sender as Control, "Text");
        }

        public XmlNode Save(XmlDocument doc)
        {
            XmlNode node = doc.CreateNode(XmlNodeType.Element, title.Text.ToString(), string.Empty);

            node.Attributes.Append(doc.AddAttribute("x", Location.X.ToString()));
            node.Attributes.Append(doc.AddAttribute("y", Location.Y.ToString()));
            node.Attributes.Append(doc.AddAttribute("fill", Fill.ToString()));
            node.InnerText = CodeEditBox.Text;

            return node;
        }

        public new void Load(XmlNode node)
        {
            Fill = bool.Parse(node.Attributes["fill"].Value);
            Location = new Point(
                int.Parse(node.Attributes["x"].Value),
                int.Parse(node.Attributes["y"].Value));

            title.Text = node.Name;
            CodeEditBox.Text = node.InnerText;
            CodeEditBox.ClearHistory();
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CodeBlock));
            this.CodeEditBox = new MAIDE.UI.CodeEditBox();
            this.fillSetButton = new System.Windows.Forms.Button();
            this.imgList = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // caption
            // 
            this.title.Size = new System.Drawing.Size(1049, 25);
            this.title.DoubleClick += new System.EventHandler(this.caption_DoubleClick);
            // 
            // CodeEditBox
            // 
            this.CodeEditBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.CodeEditBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CodeEditBox.Font = new System.Drawing.Font("Consolas", 10F);
            this.CodeEditBox.ForeColor = System.Drawing.Color.Gainsboro;
            this.CodeEditBox.Location = new System.Drawing.Point(0, 0);
            this.CodeEditBox.Name = "CodeEditBox";
            this.CodeEditBox.SelectEnd = new System.Drawing.Point(0, 0);
            this.CodeEditBox.SelectStart = new System.Drawing.Point(0, 0);
            this.CodeEditBox.Size = new System.Drawing.Size(476, 257);
            this.CodeEditBox.TabIndex = 2;
            this.CodeEditBox.TextBaseBrush = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.CodeEditBox.Zoom = 1F;
            // 
            // fillSetButton
            // 
            this.fillSetButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fillSetButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.fillSetButton.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fillSetButton.Location = new System.Drawing.Point(0, 0);
            this.fillSetButton.Name = "fillSetButton";
            this.fillSetButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.fillSetButton.Size = new System.Drawing.Size(22, 22);
            this.fillSetButton.TabIndex = 4;
            this.fillSetButton.TabStop = false;
            this.fillSetButton.Text = "#";
            this.fillSetButton.UseVisualStyleBackColor = true;
            this.fillSetButton.Click += new System.EventHandler(this.fillSetButton_Click);
            // 
            // imgList
            // 
            this.imgList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgList.ImageStream")));
            this.imgList.TransparentColor = System.Drawing.Color.Transparent;
            this.imgList.Images.SetKeyName(0, "0");
            this.imgList.Images.SetKeyName(1, "1");
            this.imgList.Images.SetKeyName(2, "2");
            // 
            // CodeBlock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Content = this.CodeEditBox;
            this.Name = "CodeBlock";
            this.RightSlot = this.fillSetButton;
            this.Size = new System.Drawing.Size(476, 283);
            this.ResumeLayout(false);
        }
    }
}