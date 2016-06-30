namespace ASM
{
    partial class Console
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
            this.viewport = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // viewport
            // 
            this.viewport.BackColor = System.Drawing.Color.Black;
            this.viewport.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.viewport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewport.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.viewport.ForeColor = System.Drawing.Color.White;
            this.viewport.Location = new System.Drawing.Point(0, 0);
            this.viewport.Multiline = true;
            this.viewport.Name = "viewport";
            this.viewport.ReadOnly = true;
            this.viewport.Size = new System.Drawing.Size(685, 320);
            this.viewport.TabIndex = 0;
            this.viewport.Text = "Test.";
            this.viewport.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.viewport_KeyPress);
            // 
            // Console
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(685, 320);
            this.Controls.Add(this.viewport);
            this.Name = "Console";
            this.ShowIcon = false;
            this.Text = "Console";
            this.Load += new System.EventHandler(this.Console_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox viewport;
    }
}