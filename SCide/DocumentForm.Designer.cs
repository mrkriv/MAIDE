namespace ASM
{
    partial class DocumentForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.Code = new ASM.UI.CodeEditBox();
            this.SuspendLayout();
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Filter = "Assembler|*.asm|All files|*.*";
            // 
            // Code
            // 
            this.Code.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.Code.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Code.Font = new System.Drawing.Font("Consolas", 10F);
            this.Code.ForeColor = System.Drawing.Color.Gainsboro;
            this.Code.Location = new System.Drawing.Point(0, 0);
            this.Code.Name = "Code";
            this.Code.SelectEnd = new System.Drawing.Point(0, 0);
            this.Code.SelectStart = new System.Drawing.Point(0, 0);
            this.Code.Size = new System.Drawing.Size(292, 266);
            this.Code.TabIndex = 0;
            this.Code.TextBaseBrush = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.Code.Zoom = 1F;
            // 
            // DocumentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Controls.Add(this.Code);
            this.Name = "DocumentForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DocumentForm_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion
        
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        public ASM.UI.CodeEditBox Code;
    }
}