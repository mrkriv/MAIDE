namespace MAIDE.UI
{
    partial class DefaultForm
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.BtnClose = new MAIDE.UI.Button();
            this.BtnMax = new MAIDE.UI.Button();
            this.BtnMin = new MAIDE.UI.Button();
            this.BtnRestore = new MAIDE.UI.Button();
            this.SuspendLayout();
            // 
            // BtnClose
            // 
            this.BtnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.BtnClose.ColorDown = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.BtnClose.ColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.BtnClose.ColorNormal = System.Drawing.Color.Empty;
            this.BtnClose.ImageDown = global::MAIDE.UI.Resource.BtnClose_Normal;
            this.BtnClose.ImageHover = global::MAIDE.UI.Resource.BtnClose_Normal;
            this.BtnClose.ImageNormal = global::MAIDE.UI.Resource.BtnClose_Normal;
            this.BtnClose.Location = new System.Drawing.Point(314, 1);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(34, 26);
            this.BtnClose.TabIndex = 34;
            // 
            // BtnMax
            // 
            this.BtnMax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnMax.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.BtnMax.ColorDown = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.BtnMax.ColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.BtnMax.ColorNormal = System.Drawing.Color.Empty;
            this.BtnMax.ImageDown = global::MAIDE.UI.Resource.BtnMax_Normal;
            this.BtnMax.ImageHover = global::MAIDE.UI.Resource.BtnMax_Normal;
            this.BtnMax.ImageNormal = global::MAIDE.UI.Resource.BtnMax_Normal;
            this.BtnMax.Location = new System.Drawing.Point(281, 1);
            this.BtnMax.Name = "BtnMax";
            this.BtnMax.Size = new System.Drawing.Size(34, 26);
            this.BtnMax.TabIndex = 36;
            // 
            // BtnMin
            // 
            this.BtnMin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnMin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.BtnMin.ColorDown = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.BtnMin.ColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.BtnMin.ColorNormal = System.Drawing.Color.Empty;
            this.BtnMin.ImageDown = global::MAIDE.UI.Resource.BtnMin_Normal;
            this.BtnMin.ImageHover = global::MAIDE.UI.Resource.BtnMin_Normal;
            this.BtnMin.ImageNormal = global::MAIDE.UI.Resource.BtnMin_Normal;
            this.BtnMin.Location = new System.Drawing.Point(246, 1);
            this.BtnMin.Name = "BtnMin";
            this.BtnMin.Size = new System.Drawing.Size(34, 26);
            this.BtnMin.TabIndex = 37;
            // 
            // BtnRestore
            // 
            this.BtnRestore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnRestore.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.BtnRestore.ColorDown = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.BtnRestore.ColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.BtnRestore.ColorNormal = System.Drawing.Color.Empty;
            this.BtnRestore.ImageDown = global::MAIDE.UI.Resource.BtnR_Normal;
            this.BtnRestore.ImageHover = global::MAIDE.UI.Resource.BtnR_Normal;
            this.BtnRestore.ImageNormal = global::MAIDE.UI.Resource.BtnR_Normal;
            this.BtnRestore.Location = new System.Drawing.Point(280, 1);
            this.BtnRestore.Name = "BtnRestore";
            this.BtnRestore.Size = new System.Drawing.Size(34, 26);
            this.BtnRestore.TabIndex = 38;
            // 
            // DefaultForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.Border = new System.Windows.Forms.Padding(1);
            this.BorderActiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.BorderDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.ClientSize = new System.Drawing.Size(349, 154);
            this.CloseButton = this.BtnClose;
            this.Controls.Add(this.BtnRestore);
            this.Controls.Add(this.BtnMin);
            this.Controls.Add(this.BtnMax);
            this.Controls.Add(this.BtnClose);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(156)))));
            this.MaxButton = this.BtnMax;
            this.MinButton = this.BtnMin;
            this.MinimumSize = new System.Drawing.Size(195, 35);
            this.Name = "DefaultForm";
            this.Padding = new System.Windows.Forms.Padding(2, 32, 2, 2);
            this.RestoreButton = this.BtnRestore;
            this.Text = "Form";
            this.TitleIconTransform = new System.Drawing.Rectangle(12, 5, 24, 24);
            this.ResumeLayout(false);

        }

        #endregion

        public UI.Button BtnClose;
        public UI.Button BtnMax;
        public UI.Button BtnMin;
        public UI.Button BtnRestore;
    }
}
