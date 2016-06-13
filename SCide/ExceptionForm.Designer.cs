namespace ASM
{
    partial class ExceptionForm
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
            this.close = new System.Windows.Forms.Button();
            this.restart = new System.Windows.Forms.Button();
            this.ignore = new System.Windows.Forms.Button();
            this.log = new System.Windows.Forms.TextBox();
            this.hendel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // close
            // 
            this.close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.close.Location = new System.Drawing.Point(678, 259);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(128, 23);
            this.close.TabIndex = 0;
            this.close.Text = "Закрыть";
            this.close.UseVisualStyleBackColor = true;
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // restart
            // 
            this.restart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.restart.Location = new System.Drawing.Point(544, 259);
            this.restart.Name = "restart";
            this.restart.Size = new System.Drawing.Size(128, 23);
            this.restart.TabIndex = 1;
            this.restart.Text = "Перезапустить";
            this.restart.UseVisualStyleBackColor = true;
            this.restart.Click += new System.EventHandler(this.restart_Click);
            // 
            // ignore
            // 
            this.ignore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ignore.Location = new System.Drawing.Point(410, 259);
            this.ignore.Name = "ignore";
            this.ignore.Size = new System.Drawing.Size(128, 23);
            this.ignore.TabIndex = 2;
            this.ignore.Text = "Продолжить";
            this.ignore.UseVisualStyleBackColor = true;
            this.ignore.Click += new System.EventHandler(this.ignore_Click);
            // 
            // log
            // 
            this.log.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.log.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.log.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.log.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.log.ForeColor = System.Drawing.Color.Gainsboro;
            this.log.Location = new System.Drawing.Point(12, 47);
            this.log.Multiline = true;
            this.log.Name = "log";
            this.log.ReadOnly = true;
            this.log.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.log.Size = new System.Drawing.Size(794, 206);
            this.log.TabIndex = 3;
            // 
            // hendel
            // 
            this.hendel.AutoSize = true;
            this.hendel.Font = new System.Drawing.Font("Segoe Print", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hendel.ForeColor = System.Drawing.Color.Gainsboro;
            this.hendel.Location = new System.Drawing.Point(12, 3);
            this.hendel.Name = "hendel";
            this.hendel.Size = new System.Drawing.Size(310, 47);
            this.hendel.TabIndex = 4;
            this.hendel.Text = "Возникла проблемка";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Silver;
            this.label1.Location = new System.Drawing.Point(42, 264);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(361, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Если продолжить работу программы все может полететь в обливион";
            // 
            // ExceptionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(818, 294);
            this.ControlBox = false;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ignore);
            this.Controls.Add(this.restart);
            this.Controls.Add(this.close);
            this.Controls.Add(this.log);
            this.Controls.Add(this.hendel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExceptionForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button close;
        private System.Windows.Forms.Button restart;
        private System.Windows.Forms.Button ignore;
        private System.Windows.Forms.TextBox log;
        private System.Windows.Forms.Label hendel;
        private System.Windows.Forms.Label label1;
    }
}