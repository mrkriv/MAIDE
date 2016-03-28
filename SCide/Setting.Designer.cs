namespace ASM
{
    partial class Setting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Setting));
            this.done = new System.Windows.Forms.Button();
            this.exit = new System.Windows.Forms.Button();
            this.reset = new System.Windows.Forms.Button();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.mGroupBox1 = new ASM.UI.MGroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // done
            // 
            this.done.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.done.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.done.Location = new System.Drawing.Point(583, 401);
            this.done.Name = "done";
            this.done.Size = new System.Drawing.Size(91, 23);
            this.done.TabIndex = 0;
            this.done.Text = "Сохранить";
            this.done.UseVisualStyleBackColor = true;
            this.done.Click += new System.EventHandler(this.done_Click);
            // 
            // exit
            // 
            this.exit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.exit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exit.Location = new System.Drawing.Point(486, 401);
            this.exit.Name = "exit";
            this.exit.Size = new System.Drawing.Size(91, 23);
            this.exit.TabIndex = 1;
            this.exit.Text = "Отменить";
            this.exit.UseVisualStyleBackColor = true;
            this.exit.Click += new System.EventHandler(this.exit_Click);
            // 
            // reset
            // 
            this.reset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.reset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.reset.Location = new System.Drawing.Point(12, 401);
            this.reset.Name = "reset";
            this.reset.Size = new System.Drawing.Size(270, 23);
            this.reset.TabIndex = 2;
            this.reset.Text = "Востановить стандартные настройки";
            this.reset.UseVisualStyleBackColor = true;
            this.reset.Click += new System.EventHandler(this.reset_Click);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(193, 60);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown1.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "label1";
            // 
            // mGroupBox1
            // 
            this.mGroupBox1.BorderColor = System.Drawing.Color.Black;
            this.mGroupBox1.Location = new System.Drawing.Point(143, 124);
            this.mGroupBox1.Name = "mGroupBox1";
            this.mGroupBox1.Size = new System.Drawing.Size(200, 100);
            this.mGroupBox1.TabIndex = 5;
            this.mGroupBox1.TabStop = false;
            this.mGroupBox1.Text = "mGroupBox1";
            // 
            // Setting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(686, 436);
            this.Controls.Add(this.mGroupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.reset);
            this.Controls.Add(this.exit);
            this.Controls.Add(this.done);
            this.ForeColor = System.Drawing.Color.Gainsboro;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Setting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Настройки";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button done;
        private System.Windows.Forms.Button exit;
        private System.Windows.Forms.Button reset;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label1;
        private UI.MGroupBox mGroupBox1;
    }
}