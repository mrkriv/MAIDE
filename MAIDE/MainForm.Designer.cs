namespace MAIDE
{
    partial class MainForm
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
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.MainMenu = new System.Windows.Forms.ToolStrip();
            this.toolStripSplitFile = new System.Windows.Forms.ToolStripDropDownButton();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MianMenuOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.MianMenuSave = new System.Windows.Forms.ToolStripMenuItem();
            this.MianMenuSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.MianMenuAll = new System.Windows.Forms.ToolStripMenuItem();
            this.MianMenuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewDropDown = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dockPanel = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.BuildMenuRun = new System.Windows.Forms.ToolStripButton();
            this.BuildMenuBuild = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.BuildMenuStop = new System.Windows.Forms.ToolStripButton();
            this.BuildMenuPause = new System.Windows.Forms.ToolStripButton();
            this.BuildMenuResume = new System.Windows.Forms.ToolStripButton();
            this.BuildMenuResumeOne = new System.Windows.Forms.ToolStripButton();
            this.BuildMenuRestart = new System.Windows.Forms.ToolStripButton();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.status = new System.Windows.Forms.ToolStripStatusLabel();
            this.BtnSetting = new MAIDE.UI.Button();
            this.MainMenu.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnClose
            // 
            this.BtnClose.Location = new System.Drawing.Point(825, 1);
            // 
            // BtnMax
            // 
            this.BtnMax.Location = new System.Drawing.Point(792, 1);
            // 
            // BtnMin
            // 
            this.BtnMin.Location = new System.Drawing.Point(758, 1);
            // 
            // BtnRestore
            // 
            this.BtnRestore.Location = new System.Drawing.Point(792, 1);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "Assembler|*.asm|All files|*.*";
            // 
            // MainMenu
            // 
            this.MainMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.MainMenu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            this.MainMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSplitFile,
            this.ViewDropDown,
            this.toolStripDropDownButton1});
            this.MainMenu.Location = new System.Drawing.Point(1, 32);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(858, 25);
            this.MainMenu.TabIndex = 25;
            // 
            // toolStripSplitFile
            // 
            this.toolStripSplitFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.toolStripSplitFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripSplitFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.MianMenuOpen,
            this.toolStripSeparator1,
            this.MianMenuSave,
            this.MianMenuSaveAs,
            this.MianMenuAll,
            this.MianMenuExit});
            this.toolStripSplitFile.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            this.toolStripSplitFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitFile.Name = "toolStripSplitFile";
            this.toolStripSplitFile.ShowDropDownArrow = false;
            this.toolStripSplitFile.Size = new System.Drawing.Size(29, 22);
            this.toolStripSplitFile.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(27)))), ((int)(((byte)(28)))));
            this.newToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.newToolStripMenuItem.Text = "CreateNew";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // MianMenuOpen
            // 
            this.MianMenuOpen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(27)))), ((int)(((byte)(28)))));
            this.MianMenuOpen.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            this.MianMenuOpen.Name = "MianMenuOpen";
            this.MianMenuOpen.ShortcutKeyDisplayString = "";
            this.MianMenuOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.MianMenuOpen.Size = new System.Drawing.Size(184, 22);
            this.MianMenuOpen.Text = "Open";
            this.MianMenuOpen.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(27)))), ((int)(((byte)(28)))));
            this.toolStripSeparator1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(55)))));
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(181, 6);
            // 
            // MianMenuSave
            // 
            this.MianMenuSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(27)))), ((int)(((byte)(28)))));
            this.MianMenuSave.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            this.MianMenuSave.Name = "MianMenuSave";
            this.MianMenuSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.MianMenuSave.Size = new System.Drawing.Size(184, 22);
            this.MianMenuSave.Text = "Save";
            this.MianMenuSave.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // MianMenuSaveAs
            // 
            this.MianMenuSaveAs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(27)))), ((int)(((byte)(28)))));
            this.MianMenuSaveAs.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            this.MianMenuSaveAs.Name = "MianMenuSaveAs";
            this.MianMenuSaveAs.Size = new System.Drawing.Size(184, 22);
            this.MianMenuSaveAs.Text = "SaveAs";
            this.MianMenuSaveAs.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // MianMenuAll
            // 
            this.MianMenuAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(27)))), ((int)(((byte)(28)))));
            this.MianMenuAll.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            this.MianMenuAll.Name = "MianMenuAll";
            this.MianMenuAll.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.MianMenuAll.Size = new System.Drawing.Size(184, 22);
            this.MianMenuAll.Text = "SaveAll";
            this.MianMenuAll.Click += new System.EventHandler(this.saveAllStripMenuItem_Click);
            // 
            // MianMenuExit
            // 
            this.MianMenuExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(27)))), ((int)(((byte)(28)))));
            this.MianMenuExit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            this.MianMenuExit.Name = "MianMenuExit";
            this.MianMenuExit.Size = new System.Drawing.Size(184, 22);
            this.MianMenuExit.Text = "Exit";
            this.MianMenuExit.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // ViewDropDown
            // 
            this.ViewDropDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.ViewDropDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ViewDropDown.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            this.ViewDropDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ViewDropDown.Name = "ViewDropDown";
            this.ViewDropDown.ShowDropDownArrow = false;
            this.ViewDropDown.Size = new System.Drawing.Size(60, 22);
            this.ViewDropDown.Text = "Windows";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.toolStripDropDownButton1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.ShowDropDownArrow = false;
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(36, 22);
            this.toolStripDropDownButton1.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(27)))), ((int)(((byte)(28)))));
            this.aboutToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
            this.aboutToolStripMenuItem.Text = "about";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // dockPanel
            // 
            this.dockPanel.AllowEndUserNestedDocking = false;
            this.dockPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dockPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.dockPanel.DocumentStyle = WeifenLuo.WinFormsUI.Docking.DocumentStyle.DockingWindow;
            this.dockPanel.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dockPanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            this.dockPanel.Location = new System.Drawing.Point(1, 96);
            this.dockPanel.Margin = new System.Windows.Forms.Padding(2);
            this.dockPanel.Name = "dockPanel";
            this.dockPanel.Size = new System.Drawing.Size(858, 380);
            this.dockPanel.TabIndex = 26;
            this.dockPanel.ActiveContentChanged += new System.EventHandler(this.dockPanel_ActiveContentChanged);
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.toolStrip1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BuildMenuRun,
            this.BuildMenuBuild,
            this.toolStripSeparator2,
            this.BuildMenuStop,
            this.BuildMenuPause,
            this.BuildMenuResume,
            this.BuildMenuResumeOne,
            this.BuildMenuRestart});
            this.toolStrip1.Location = new System.Drawing.Point(1, 57);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(858, 39);
            this.toolStrip1.TabIndex = 29;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // BuildMenuRun
            // 
            this.BuildMenuRun.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.BuildMenuRun.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BuildMenuRun.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            this.BuildMenuRun.Image = global::MAIDE.Properties.Resources.resume;
            this.BuildMenuRun.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BuildMenuRun.Name = "BuildMenuRun";
            this.BuildMenuRun.Size = new System.Drawing.Size(36, 36);
            this.BuildMenuRun.MouseDown += new System.Windows.Forms.MouseEventHandler(this.runToolStripMenuItem_Click);
            // 
            // BuildMenuBuild
            // 
            this.BuildMenuBuild.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.BuildMenuBuild.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BuildMenuBuild.Enabled = false;
            this.BuildMenuBuild.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            this.BuildMenuBuild.Image = global::MAIDE.Properties.Resources.build;
            this.BuildMenuBuild.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BuildMenuBuild.Name = "BuildMenuBuild";
            this.BuildMenuBuild.Size = new System.Drawing.Size(36, 36);
            this.BuildMenuBuild.Click += new System.EventHandler(this.BuildMenuBuild_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.toolStripSeparator2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(55)))));
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 39);
            // 
            // BuildMenuStop
            // 
            this.BuildMenuStop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.BuildMenuStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BuildMenuStop.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            this.BuildMenuStop.Image = global::MAIDE.Properties.Resources.cancel;
            this.BuildMenuStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BuildMenuStop.Name = "BuildMenuStop";
            this.BuildMenuStop.Size = new System.Drawing.Size(36, 36);
            this.BuildMenuStop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.stopToolStripMenuItem_Click);
            // 
            // BuildMenuPause
            // 
            this.BuildMenuPause.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.BuildMenuPause.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BuildMenuPause.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            this.BuildMenuPause.Image = global::MAIDE.Properties.Resources.pause;
            this.BuildMenuPause.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BuildMenuPause.Name = "BuildMenuPause";
            this.BuildMenuPause.Size = new System.Drawing.Size(36, 36);
            this.BuildMenuPause.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pauseToolStripMenuItem_Click);
            // 
            // BuildMenuResume
            // 
            this.BuildMenuResume.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.BuildMenuResume.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BuildMenuResume.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            this.BuildMenuResume.Image = global::MAIDE.Properties.Resources.resume;
            this.BuildMenuResume.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BuildMenuResume.Name = "BuildMenuResume";
            this.BuildMenuResume.Size = new System.Drawing.Size(36, 36);
            this.BuildMenuResume.MouseDown += new System.Windows.Forms.MouseEventHandler(this.resumeToolStripMenuItem_Click);
            // 
            // BuildMenuResumeOne
            // 
            this.BuildMenuResumeOne.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.BuildMenuResumeOne.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BuildMenuResumeOne.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            this.BuildMenuResumeOne.Image = global::MAIDE.Properties.Resources.resumeOne;
            this.BuildMenuResumeOne.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BuildMenuResumeOne.Name = "BuildMenuResumeOne";
            this.BuildMenuResumeOne.Size = new System.Drawing.Size(36, 36);
            this.BuildMenuResumeOne.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BuildMenuResumeOne_MouseDown);
            // 
            // BuildMenuRestart
            // 
            this.BuildMenuRestart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.BuildMenuRestart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BuildMenuRestart.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            this.BuildMenuRestart.Image = global::MAIDE.Properties.Resources.refresh;
            this.BuildMenuRestart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BuildMenuRestart.Name = "BuildMenuRestart";
            this.BuildMenuRestart.Size = new System.Drawing.Size(36, 36);
            this.BuildMenuRestart.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BuildMenuRestart_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.statusStrip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            this.statusStrip.GripMargin = new System.Windows.Forms.Padding(0);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.status});
            this.statusStrip.Location = new System.Drawing.Point(1, 476);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(858, 22);
            this.statusStrip.TabIndex = 32;
            // 
            // status
            // 
            this.status.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.status.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(0, 17);
            // 
            // BtnSetting
            // 
            this.BtnSetting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSetting.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.BtnSetting.ColorDown = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.BtnSetting.ColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.BtnSetting.ColorNormal = System.Drawing.Color.Empty;
            this.BtnSetting.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            this.BtnSetting.ImageDown = global::MAIDE.Properties.Resources.settings;
            this.BtnSetting.ImageHover = global::MAIDE.Properties.Resources.settings;
            this.BtnSetting.ImageNormal = global::MAIDE.Properties.Resources.settings;
            this.BtnSetting.Location = new System.Drawing.Point(712, 1);
            this.BtnSetting.Margin = new System.Windows.Forms.Padding(0);
            this.BtnSetting.Name = "BtnSetting";
            this.BtnSetting.Size = new System.Drawing.Size(34, 26);
            this.BtnSetting.TabIndex = 40;
            this.BtnSetting.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(860, 499);
            this.Controls.Add(this.BtnSetting);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.MainMenu);
            this.Controls.Add(this.dockPanel);
            this.Name = "MainForm";
            this.Shadow = new System.Windows.Forms.Padding(5);
            this.Text = "MAIDE";
            this.TitleIcon = global::MAIDE.Properties.Resources.Icon;
            this.TransparencyKey = System.Drawing.Color.Maroon;
            this.Controls.SetChildIndex(this.BtnRestore, 0);
            this.Controls.SetChildIndex(this.dockPanel, 0);
            this.Controls.SetChildIndex(this.MainMenu, 0);
            this.Controls.SetChildIndex(this.toolStrip1, 0);
            this.Controls.SetChildIndex(this.statusStrip, 0);
            this.Controls.SetChildIndex(this.BtnSetting, 0);
            this.Controls.SetChildIndex(this.BtnClose, 0);
            this.Controls.SetChildIndex(this.BtnMax, 0);
            this.Controls.SetChildIndex(this.BtnMin, 0);
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ToolStrip MainMenu;
        private WeifenLuo.WinFormsUI.Docking.DockPanel dockPanel;
        private System.Windows.Forms.ToolStripDropDownButton toolStripSplitFile;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MianMenuOpen;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem MianMenuSave;
        private System.Windows.Forms.ToolStripMenuItem MianMenuSaveAs;
        private System.Windows.Forms.ToolStripMenuItem MianMenuAll;
        private System.Windows.Forms.ToolStripMenuItem MianMenuExit;
        private System.Windows.Forms.ToolStripButton BuildMenuStop;
        private System.Windows.Forms.ToolStripButton BuildMenuPause;
        private System.Windows.Forms.ToolStripButton BuildMenuResume;
        private System.Windows.Forms.ToolStripButton BuildMenuRestart;
        private System.Windows.Forms.ToolStripDropDownButton ViewDropDown;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton BuildMenuRun;
        private System.Windows.Forms.ToolStripButton BuildMenuBuild;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel status;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton BuildMenuResumeOne;
        private UI.Button BtnSetting;
    }
}