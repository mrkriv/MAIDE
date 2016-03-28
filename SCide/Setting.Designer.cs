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
            this.mGroupBox1 = new ASM.UI.MGroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.mGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // done
            // 
            this.done.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.done.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.done.Location = new System.Drawing.Point(557, 401);
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
            this.exit.Location = new System.Drawing.Point(460, 401);
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
            // mGroupBox1
            // 
            this.mGroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mGroupBox1.BorderColor = System.Drawing.Color.White;
            this.mGroupBox1.Controls.Add(this.label1);
            this.mGroupBox1.Controls.Add(this.numericUpDown1);
            this.mGroupBox1.Location = new System.Drawing.Point(12, 12);
            this.mGroupBox1.Name = "mGroupBox1";
            this.mGroupBox1.Size = new System.Drawing.Size(636, 383);
            this.mGroupBox1.TabIndex = 5;
            this.mGroupBox1.TabStop = false;
            this.mGroupBox1.Text = "Основные параметры";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(345, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Количество тактов, сведетельстуеще о зацикливании программы";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDown1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(32)))));
            this.numericUpDown1.ForeColor = System.Drawing.Color.Gainsboro;
            this.numericUpDown1.Increment = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDown1.Location = new System.Drawing.Point(545, 30);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            -727379968,
            232,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(85, 20);
            this.numericUpDown1.TabIndex = 5;
            this.numericUpDown1.Tag = "TotalTickLimit";
            this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // Setting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(660, 436);
            this.Controls.Add(this.mGroupBox1);
            this.Controls.Add(this.reset);
            this.Controls.Add(this.exit);
            this.Controls.Add(this.done);
            this.ForeColor = System.Drawing.Color.Gainsboro;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Setting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Настройки";
            this.mGroupBox1.ResumeLayout(false);
            this.mGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button done;
        private System.Windows.Forms.Button exit;
        private System.Windows.Forms.Button reset;
        private UI.MGroupBox mGroupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
    }
}