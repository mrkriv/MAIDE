namespace MAIDE
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DocumentForm));
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.CodeEditBox = new MAIDE.UI.CodeEditBox();
            this.imgList = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Filter = "Assembler|*.asm|All files|*.*";
            // 
            // CodeEditBox
            // 
            this.CodeEditBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.CodeEditBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CodeEditBox.Font = new System.Drawing.Font("Consolas", 10F);
            this.CodeEditBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            this.CodeEditBox.Location = new System.Drawing.Point(0, 0);
            this.CodeEditBox.Name = "CodeEditBox";
            this.CodeEditBox.SelectEnd = new System.Drawing.Point(0, 0);
            this.CodeEditBox.SelectStart = new System.Drawing.Point(0, 0);
            this.CodeEditBox.Size = new System.Drawing.Size(612, 344);
            this.CodeEditBox.TabIndex = 4;
            this.CodeEditBox.TextBaseBrush = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.CodeEditBox.Zoom = 1F;
            // 
            // imgList
            // 
            this.imgList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgList.ImageStream")));
            this.imgList.TransparentColor = System.Drawing.Color.Transparent;
            this.imgList.Images.SetKeyName(0, "0");
            this.imgList.Images.SetKeyName(1, "1");
            this.imgList.Images.SetKeyName(2, "2");
            // 
            // DocumentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(29)))));
            this.ClientSize = new System.Drawing.Size(612, 344);
            this.Controls.Add(this.CodeEditBox);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Name = "DocumentForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DocumentForm_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion
        
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        public UI.CodeEditBox CodeEditBox;
        private System.Windows.Forms.ImageList imgList;
    }
}