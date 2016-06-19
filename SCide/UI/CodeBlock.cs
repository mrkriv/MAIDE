using System;
using System.Windows.Forms.VisualStyles;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASM.UI
{
    public class CodeBlock : DragDropPanel
    {
        private static CodeBlock mainBlock;
        private System.Windows.Forms.Button fillSetButton;
        private CodeEditBox codeEditBox;

        public static CodeBlock MainBlock
        {
            get { return mainBlock; }
            set { mainBlock = value; }
        }

        public CodeBlock()
        {
            InitializeComponent();
            new PropertyJoin(codeEditBox, "CommentChar", Properties.Settings.Default, "CommentChar");
        }

        public void GoTo(int line)
        {
            codeEditBox.GoTo(line);
        }

        public void SetCode(string text)
        {
            codeEditBox.Text = text;
            codeEditBox.ClearHistory();
        }

        public string GetCode()
        {
            return codeEditBox.Text;
        }

        public CodeEditBox.RowReadonlyCollection GetCodeRows()
        {
            return codeEditBox.Rows;
        }

        private void fillSetButton_Click(object sender, EventArgs e)
        {
            SetFillMode(Dock == System.Windows.Forms.DockStyle.None);
        }

        public void SetFillMode(bool enable)
        {
            if (enable)
            {
                Dock = System.Windows.Forms.DockStyle.Fill;
                DragDropEnable = false;
            }
            else
            {
                Dock = System.Windows.Forms.DockStyle.None;
                DragDropEnable = true;
            }
        }

        private void InitializeComponent()
        {
            this.codeEditBox = new ASM.UI.CodeEditBox();
            this.fillSetButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // caption
            // 
            this.caption.Size = new System.Drawing.Size(454, 25);
            // 
            // codeEditBox
            // 
            this.codeEditBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.codeEditBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.codeEditBox.Font = new System.Drawing.Font("Consolas", 10F);
            this.codeEditBox.ForeColor = System.Drawing.Color.Gainsboro;
            this.codeEditBox.Location = new System.Drawing.Point(0, 0);
            this.codeEditBox.Name = "codeEditBox";
            this.codeEditBox.SelectEnd = new System.Drawing.Point(0, 0);
            this.codeEditBox.SelectStart = new System.Drawing.Point(0, 0);
            this.codeEditBox.Size = new System.Drawing.Size(476, 257);
            this.codeEditBox.TabIndex = 2;
            this.codeEditBox.TextBaseBrush = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.codeEditBox.Zoom = 1F;
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
            // CodeBlock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Content = this.codeEditBox;
            this.Name = "CodeBlock";
            this.RightSlot = this.fillSetButton;
            this.Size = new System.Drawing.Size(476, 283);
            this.ResumeLayout(false);

        }
    }
}
