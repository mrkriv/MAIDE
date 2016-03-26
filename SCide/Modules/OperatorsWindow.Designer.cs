namespace ASM
{
    partial class OperatorsWindow
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.list = new System.Windows.Forms.ListBox();
            this.mask = new System.Windows.Forms.TextBox();
            this.activeName = new System.Windows.Forms.GroupBox();
            this.activeDec = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.activeName.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.list);
            this.splitContainer1.Panel1.Controls.Add(this.mask);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.activeName);
            this.splitContainer1.Size = new System.Drawing.Size(245, 395);
            this.splitContainer1.SplitterDistance = 195;
            this.splitContainer1.TabIndex = 4;
            // 
            // list
            // 
            this.list.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.list.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.list.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.list.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.list.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            this.list.FormattingEnabled = true;
            this.list.ItemHeight = 15;
            this.list.Location = new System.Drawing.Point(0, 20);
            this.list.Name = "list";
            this.list.Size = new System.Drawing.Size(245, 165);
            this.list.TabIndex = 4;
            this.list.SelectedIndexChanged += new System.EventHandler(this.list_SelectedIndexChanged);
            // 
            // mask
            // 
            this.mask.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(55)))));
            this.mask.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mask.Dock = System.Windows.Forms.DockStyle.Top;
            this.mask.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            this.mask.Location = new System.Drawing.Point(0, 0);
            this.mask.Name = "mask";
            this.mask.Size = new System.Drawing.Size(245, 20);
            this.mask.TabIndex = 3;
            this.mask.TextChanged += new System.EventHandler(this.mask_TextChanged);
            // 
            // activeName
            // 
            this.activeName.Controls.Add(this.activeDec);
            this.activeName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.activeName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.activeName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            this.activeName.Location = new System.Drawing.Point(0, 0);
            this.activeName.Name = "activeName";
            this.activeName.Padding = new System.Windows.Forms.Padding(7, 3, 3, 3);
            this.activeName.Size = new System.Drawing.Size(245, 196);
            this.activeName.TabIndex = 4;
            this.activeName.TabStop = false;
            // 
            // activeDec
            // 
            this.activeDec.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.activeDec.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.activeDec.Dock = System.Windows.Forms.DockStyle.Fill;
            this.activeDec.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.activeDec.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            this.activeDec.Location = new System.Drawing.Point(7, 18);
            this.activeDec.Margin = new System.Windows.Forms.Padding(0);
            this.activeDec.Name = "activeDec";
            this.activeDec.ReadOnly = true;
            this.activeDec.Size = new System.Drawing.Size(235, 175);
            this.activeDec.TabIndex = 1;
            this.activeDec.Text = "";
            // 
            // OperatorsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(245, 395);
            this.Controls.Add(this.splitContainer1);
            this.Name = "OperatorsWindow";
            this.TabText = "OperatorsWindows";
            this.Text = "OperatorsWindows";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.activeName.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox mask;
        private System.Windows.Forms.GroupBox activeName;
        private System.Windows.Forms.ListBox list;
        private System.Windows.Forms.RichTextBox activeDec;
    }
}