namespace MAIDE.UI
{
    partial class DialogForm
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
            this.YesNo = new System.Windows.Forms.TableLayoutPanel();
            this.btnDone = new MAIDE.UI.Button();
            this.btnCancel = new MAIDE.UI.Button();
            this.text = new System.Windows.Forms.Label();
            this.Ok = new System.Windows.Forms.TableLayoutPanel();
            this.button1 = new MAIDE.UI.Button();
            this.YesNoCancel = new System.Windows.Forms.TableLayoutPanel();
            this.button4 = new MAIDE.UI.Button();
            this.button3 = new MAIDE.UI.Button();
            this.button2 = new MAIDE.UI.Button();
            this.OKCancel = new System.Windows.Forms.TableLayoutPanel();
            this.button5 = new MAIDE.UI.Button();
            this.button6 = new MAIDE.UI.Button();
            this.RetryCancel = new System.Windows.Forms.TableLayoutPanel();
            this.button7 = new MAIDE.UI.Button();
            this.button8 = new MAIDE.UI.Button();
            this.AbortRetryIgnore = new System.Windows.Forms.TableLayoutPanel();
            this.button9 = new MAIDE.UI.Button();
            this.button10 = new MAIDE.UI.Button();
            this.button11 = new MAIDE.UI.Button();
            this.YesNo.SuspendLayout();
            this.Ok.SuspendLayout();
            this.YesNoCancel.SuspendLayout();
            this.OKCancel.SuspendLayout();
            this.RetryCancel.SuspendLayout();
            this.AbortRetryIgnore.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnClose
            // 
            this.BtnClose.Location = new System.Drawing.Point(324, 1);
            // 
            // BtnMax
            // 
            this.BtnMax.Location = new System.Drawing.Point(291, 1);
            this.BtnMax.Visible = false;
            // 
            // BtnMin
            // 
            this.BtnMin.Location = new System.Drawing.Point(256, 1);
            this.BtnMin.Visible = false;
            // 
            // BtnRestore
            // 
            this.BtnRestore.Location = new System.Drawing.Point(290, 1);
            this.BtnRestore.Visible = false;
            // 
            // YesNo
            // 
            this.YesNo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.YesNo.ColumnCount = 2;
            this.YesNo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.YesNo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.YesNo.Controls.Add(this.btnDone, 0, 0);
            this.YesNo.Controls.Add(this.btnCancel, 0, 0);
            this.YesNo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.YesNo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            this.YesNo.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.YesNo.Location = new System.Drawing.Point(1, 282);
            this.YesNo.Margin = new System.Windows.Forms.Padding(0);
            this.YesNo.Name = "YesNo";
            this.YesNo.RowCount = 1;
            this.YesNo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.YesNo.Size = new System.Drawing.Size(357, 32);
            this.YesNo.TabIndex = 42;
            // 
            // btnDone
            // 
            this.btnDone.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.btnDone.ColorDown = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.btnDone.ColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(62)))), ((int)(((byte)(64)))));
            this.btnDone.ColorNormal = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.btnDone.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDone.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            this.btnDone.Location = new System.Drawing.Point(178, 0);
            this.btnDone.Margin = new System.Windows.Forms.Padding(0);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(179, 32);
            this.btnDone.TabIndex = 44;
            this.btnDone.Text = "Yes";
            this.btnDone.Click += new System.EventHandler(this.btnYes_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.btnCancel.ColorDown = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.btnCancel.ColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(62)))), ((int)(((byte)(64)))));
            this.btnCancel.ColorNormal = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            this.btnCancel.Location = new System.Drawing.Point(0, 0);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(178, 32);
            this.btnCancel.TabIndex = 43;
            this.btnCancel.Text = "No";
            this.btnCancel.Click += new System.EventHandler(this.btnNo_Click);
            // 
            // text
            // 
            this.text.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.text.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            this.text.Location = new System.Drawing.Point(16, 32);
            this.text.Name = "text";
            this.text.Size = new System.Drawing.Size(326, 54);
            this.text.TabIndex = 43;
            this.text.Text = "Text";
            this.text.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Ok
            // 
            this.Ok.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.Ok.ColumnCount = 2;
            this.Ok.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.Ok.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.Ok.Controls.Add(this.button1, 1, 0);
            this.Ok.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Ok.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            this.Ok.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.Ok.Location = new System.Drawing.Point(1, 250);
            this.Ok.Margin = new System.Windows.Forms.Padding(0);
            this.Ok.Name = "Ok";
            this.Ok.RowCount = 1;
            this.Ok.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.Ok.Size = new System.Drawing.Size(357, 32);
            this.Ok.TabIndex = 44;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.button1.ColorDown = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.button1.ColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(62)))), ((int)(((byte)(64)))));
            this.button1.ColorNormal = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.button1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            this.button1.Location = new System.Drawing.Point(178, 0);
            this.button1.Margin = new System.Windows.Forms.Padding(0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(179, 32);
            this.button1.TabIndex = 44;
            this.button1.Text = "OK";
            this.button1.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // YesNoCancel
            // 
            this.YesNoCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.YesNoCancel.ColumnCount = 3;
            this.YesNoCancel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.YesNoCancel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.YesNoCancel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.YesNoCancel.Controls.Add(this.button4, 1, 0);
            this.YesNoCancel.Controls.Add(this.button3, 0, 0);
            this.YesNoCancel.Controls.Add(this.button2, 2, 0);
            this.YesNoCancel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.YesNoCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            this.YesNoCancel.Location = new System.Drawing.Point(1, 218);
            this.YesNoCancel.Margin = new System.Windows.Forms.Padding(0);
            this.YesNoCancel.Name = "YesNoCancel";
            this.YesNoCancel.RowCount = 1;
            this.YesNoCancel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.YesNoCancel.Size = new System.Drawing.Size(357, 32);
            this.YesNoCancel.TabIndex = 45;
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.button4.ColorDown = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.button4.ColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(62)))), ((int)(((byte)(64)))));
            this.button4.ColorNormal = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.button4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            this.button4.Location = new System.Drawing.Point(119, 0);
            this.button4.Margin = new System.Windows.Forms.Padding(0);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(119, 32);
            this.button4.TabIndex = 46;
            this.button4.Text = "Retry";
            this.button4.Click += new System.EventHandler(this.btnRetry_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.button3.ColorDown = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.button3.ColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(62)))), ((int)(((byte)(64)))));
            this.button3.ColorNormal = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.button3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            this.button3.Location = new System.Drawing.Point(0, 0);
            this.button3.Margin = new System.Windows.Forms.Padding(0);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(119, 32);
            this.button3.TabIndex = 45;
            this.button3.Text = "No";
            this.button3.Click += new System.EventHandler(this.btnNo_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.button2.ColorDown = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.button2.ColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(62)))), ((int)(((byte)(64)))));
            this.button2.ColorNormal = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.button2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            this.button2.Location = new System.Drawing.Point(238, 0);
            this.button2.Margin = new System.Windows.Forms.Padding(0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(119, 32);
            this.button2.TabIndex = 44;
            this.button2.Text = "Yes";
            this.button2.Click += new System.EventHandler(this.btnYes_Click);
            // 
            // OKCancel
            // 
            this.OKCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.OKCancel.ColumnCount = 2;
            this.OKCancel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.OKCancel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.OKCancel.Controls.Add(this.button5, 0, 0);
            this.OKCancel.Controls.Add(this.button6, 0, 0);
            this.OKCancel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.OKCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            this.OKCancel.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.OKCancel.Location = new System.Drawing.Point(1, 186);
            this.OKCancel.Margin = new System.Windows.Forms.Padding(0);
            this.OKCancel.Name = "OKCancel";
            this.OKCancel.RowCount = 1;
            this.OKCancel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.OKCancel.Size = new System.Drawing.Size(357, 32);
            this.OKCancel.TabIndex = 46;
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.button5.ColorDown = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.button5.ColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(62)))), ((int)(((byte)(64)))));
            this.button5.ColorNormal = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.button5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            this.button5.Location = new System.Drawing.Point(178, 0);
            this.button5.Margin = new System.Windows.Forms.Padding(0);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(179, 32);
            this.button5.TabIndex = 44;
            this.button5.Text = "OK";
            this.button5.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.button6.ColorDown = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.button6.ColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(62)))), ((int)(((byte)(64)))));
            this.button6.ColorNormal = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.button6.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            this.button6.Location = new System.Drawing.Point(0, 0);
            this.button6.Margin = new System.Windows.Forms.Padding(0);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(178, 32);
            this.button6.TabIndex = 43;
            this.button6.Text = "Cancel";
            this.button6.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // RetryCancel
            // 
            this.RetryCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.RetryCancel.ColumnCount = 2;
            this.RetryCancel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.RetryCancel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.RetryCancel.Controls.Add(this.button7, 0, 0);
            this.RetryCancel.Controls.Add(this.button8, 0, 0);
            this.RetryCancel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.RetryCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            this.RetryCancel.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.RetryCancel.Location = new System.Drawing.Point(1, 154);
            this.RetryCancel.Margin = new System.Windows.Forms.Padding(0);
            this.RetryCancel.Name = "RetryCancel";
            this.RetryCancel.RowCount = 1;
            this.RetryCancel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.RetryCancel.Size = new System.Drawing.Size(357, 32);
            this.RetryCancel.TabIndex = 47;
            // 
            // button7
            // 
            this.button7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.button7.ColorDown = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.button7.ColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(62)))), ((int)(((byte)(64)))));
            this.button7.ColorNormal = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.button7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            this.button7.Location = new System.Drawing.Point(178, 0);
            this.button7.Margin = new System.Windows.Forms.Padding(0);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(179, 32);
            this.button7.TabIndex = 44;
            this.button7.Text = "Retry";
            this.button7.Click += new System.EventHandler(this.btnRetry_Click);
            // 
            // button8
            // 
            this.button8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.button8.ColorDown = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.button8.ColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(62)))), ((int)(((byte)(64)))));
            this.button8.ColorNormal = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.button8.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            this.button8.Location = new System.Drawing.Point(0, 0);
            this.button8.Margin = new System.Windows.Forms.Padding(0);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(178, 32);
            this.button8.TabIndex = 43;
            this.button8.Text = "Cancel";
            this.button8.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // AbortRetryIgnore
            // 
            this.AbortRetryIgnore.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.AbortRetryIgnore.ColumnCount = 3;
            this.AbortRetryIgnore.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.AbortRetryIgnore.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.AbortRetryIgnore.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.AbortRetryIgnore.Controls.Add(this.button9, 1, 0);
            this.AbortRetryIgnore.Controls.Add(this.button10, 0, 0);
            this.AbortRetryIgnore.Controls.Add(this.button11, 2, 0);
            this.AbortRetryIgnore.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.AbortRetryIgnore.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            this.AbortRetryIgnore.Location = new System.Drawing.Point(1, 122);
            this.AbortRetryIgnore.Margin = new System.Windows.Forms.Padding(0);
            this.AbortRetryIgnore.Name = "AbortRetryIgnore";
            this.AbortRetryIgnore.RowCount = 1;
            this.AbortRetryIgnore.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.AbortRetryIgnore.Size = new System.Drawing.Size(357, 32);
            this.AbortRetryIgnore.TabIndex = 48;
            // 
            // button9
            // 
            this.button9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.button9.ColorDown = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.button9.ColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(62)))), ((int)(((byte)(64)))));
            this.button9.ColorNormal = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.button9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            this.button9.Location = new System.Drawing.Point(119, 0);
            this.button9.Margin = new System.Windows.Forms.Padding(0);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(119, 32);
            this.button9.TabIndex = 46;
            this.button9.Text = "Retry";
            this.button9.Click += new System.EventHandler(this.btnRetry_Click);
            // 
            // button10
            // 
            this.button10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.button10.ColorDown = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.button10.ColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(62)))), ((int)(((byte)(64)))));
            this.button10.ColorNormal = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.button10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            this.button10.Location = new System.Drawing.Point(0, 0);
            this.button10.Margin = new System.Windows.Forms.Padding(0);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(119, 32);
            this.button10.TabIndex = 45;
            this.button10.Text = "Abort";
            this.button10.Click += new System.EventHandler(this.btnAbort_Click);
            // 
            // button11
            // 
            this.button11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.button11.ColorDown = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.button11.ColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(62)))), ((int)(((byte)(64)))));
            this.button11.ColorNormal = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.button11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            this.button11.Location = new System.Drawing.Point(238, 0);
            this.button11.Margin = new System.Windows.Forms.Padding(0);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(119, 32);
            this.button11.TabIndex = 44;
            this.button11.Text = "Ignore";
            this.button11.Click += new System.EventHandler(this.btnIgnore_Click);
            // 
            // DialogForm
            // 
            this.AcceptButton = this.btnDone;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(359, 315);
            this.Controls.Add(this.AbortRetryIgnore);
            this.Controls.Add(this.RetryCancel);
            this.Controls.Add(this.OKCancel);
            this.Controls.Add(this.YesNoCancel);
            this.Controls.Add(this.Ok);
            this.Controls.Add(this.YesNo);
            this.Controls.Add(this.text);
            this.MaxButton = null;
            this.MinButton = null;
            this.MinimumSize = new System.Drawing.Size(359, 134);
            this.Name = "DialogForm";
            this.ResizeEnable = false;
            this.RestoreButton = null;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Caption";
            this.TitleIconTransform = new System.Drawing.Rectangle(-5, 0, 0, 24);
            this.Controls.SetChildIndex(this.text, 0);
            this.Controls.SetChildIndex(this.YesNo, 0);
            this.Controls.SetChildIndex(this.Ok, 0);
            this.Controls.SetChildIndex(this.YesNoCancel, 0);
            this.Controls.SetChildIndex(this.BtnClose, 0);
            this.Controls.SetChildIndex(this.BtnMax, 0);
            this.Controls.SetChildIndex(this.BtnMin, 0);
            this.Controls.SetChildIndex(this.BtnRestore, 0);
            this.Controls.SetChildIndex(this.OKCancel, 0);
            this.Controls.SetChildIndex(this.RetryCancel, 0);
            this.Controls.SetChildIndex(this.AbortRetryIgnore, 0);
            this.YesNo.ResumeLayout(false);
            this.Ok.ResumeLayout(false);
            this.YesNoCancel.ResumeLayout(false);
            this.OKCancel.ResumeLayout(false);
            this.RetryCancel.ResumeLayout(false);
            this.AbortRetryIgnore.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel YesNo;
        private Button btnDone;
        private Button btnCancel;
        private System.Windows.Forms.Label text;
        private System.Windows.Forms.TableLayoutPanel Ok;
        private Button button1;
        private System.Windows.Forms.TableLayoutPanel YesNoCancel;
        private Button button3;
        private Button button2;
        private Button button4;
        private System.Windows.Forms.TableLayoutPanel OKCancel;
        private Button button5;
        private Button button6;
        private System.Windows.Forms.TableLayoutPanel RetryCancel;
        private Button button7;
        private Button button8;
        private System.Windows.Forms.TableLayoutPanel AbortRetryIgnore;
        private Button button9;
        private Button button10;
        private Button button11;
    }
}