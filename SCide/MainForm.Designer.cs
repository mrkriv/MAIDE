namespace ASM
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
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
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
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
            this.BuildMenuRestart = new System.Windows.Forms.ToolStripButton();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.status = new System.Windows.Forms.ToolStripStatusLabel();
            this.MainMenu.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "Assembler|*.asm|All files|*.*";
            // 
            // MainMenu
            // 
            this.MainMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.MainMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSplitFile,
            this.ViewDropDown,
            this.toolStripButton1,
            this.toolStripDropDownButton1});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(840, 25);
            this.MainMenu.TabIndex = 25;
            // 
            // toolStripSplitFile
            // 
            this.toolStripSplitFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.toolStripSplitFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripSplitFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.MianMenuOpen,
            this.toolStripSeparator1,
            this.MianMenuSave,
            this.MianMenuSaveAs,
            this.MianMenuAll,
            this.MianMenuExit});
            this.toolStripSplitFile.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitFile.Image")));
            this.toolStripSplitFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitFile.Name = "toolStripSplitFile";
            this.toolStripSplitFile.ShowDropDownArrow = false;
            this.toolStripSplitFile.Size = new System.Drawing.Size(40, 22);
            this.toolStripSplitFile.Text = "Файл";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.newToolStripMenuItem.Text = "Создать новый";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // MianMenuOpen
            // 
            this.MianMenuOpen.Name = "MianMenuOpen";
            this.MianMenuOpen.ShortcutKeyDisplayString = "";
            this.MianMenuOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.MianMenuOpen.Size = new System.Drawing.Size(225, 22);
            this.MianMenuOpen.Text = "Открыть";
            this.MianMenuOpen.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(222, 6);
            // 
            // MianMenuSave
            // 
            this.MianMenuSave.Name = "MianMenuSave";
            this.MianMenuSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.MianMenuSave.Size = new System.Drawing.Size(225, 22);
            this.MianMenuSave.Text = "Сохранить";
            this.MianMenuSave.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // MianMenuSaveAs
            // 
            this.MianMenuSaveAs.Name = "MianMenuSaveAs";
            this.MianMenuSaveAs.Size = new System.Drawing.Size(225, 22);
            this.MianMenuSaveAs.Text = "Сохранить как";
            this.MianMenuSaveAs.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // MianMenuAll
            // 
            this.MianMenuAll.Name = "MianMenuAll";
            this.MianMenuAll.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.MianMenuAll.Size = new System.Drawing.Size(225, 22);
            this.MianMenuAll.Text = "Сохранить все";
            this.MianMenuAll.Click += new System.EventHandler(this.saveAllStripMenuItem_Click);
            // 
            // MianMenuExit
            // 
            this.MianMenuExit.Name = "MianMenuExit";
            this.MianMenuExit.Size = new System.Drawing.Size(225, 22);
            this.MianMenuExit.Text = "Выход";
            this.MianMenuExit.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // ViewDropDown
            // 
            this.ViewDropDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.ViewDropDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ViewDropDown.Image = ((System.Drawing.Image)(resources.GetObject("ViewDropDown.Image")));
            this.ViewDropDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ViewDropDown.Name = "ViewDropDown";
            this.ViewDropDown.ShowDropDownArrow = false;
            this.ViewDropDown.Size = new System.Drawing.Size(39, 22);
            this.ViewDropDown.Text = "Окна";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::ASM.Properties.Resources.settings;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.ShowDropDownArrow = false;
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(57, 22);
            this.toolStripDropDownButton1.Text = "Справка";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.aboutToolStripMenuItem.Text = "О программе";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // dockPanel
            // 
            this.dockPanel.ActiveAutoHideContent = null;
            this.dockPanel.AllowEndUserNestedDocking = false;
            this.dockPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dockPanel.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dockPanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            this.dockPanel.Location = new System.Drawing.Point(0, 66);
            this.dockPanel.Margin = new System.Windows.Forms.Padding(2);
            this.dockPanel.Name = "dockPanel";
            this.dockPanel.Size = new System.Drawing.Size(840, 379);
            this.dockPanel.TabIndex = 26;
            this.dockPanel.ActiveContentChanged += new System.EventHandler(this.dockPanel_ActiveContentChanged);
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BuildMenuRun,
            this.BuildMenuBuild,
            this.toolStripSeparator2,
            this.BuildMenuStop,
            this.BuildMenuPause,
            this.BuildMenuResume,
            this.BuildMenuRestart});
            this.toolStrip1.Location = new System.Drawing.Point(0, 25);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(840, 39);
            this.toolStrip1.TabIndex = 29;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // BuildMenuRun
            // 
            this.BuildMenuRun.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BuildMenuRun.Image = global::ASM.Properties.Resources.resume;
            this.BuildMenuRun.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BuildMenuRun.Name = "BuildMenuRun";
            this.BuildMenuRun.Size = new System.Drawing.Size(36, 36);
            this.BuildMenuRun.MouseDown += new System.Windows.Forms.MouseEventHandler(this.runToolStripMenuItem_Click);
            // 
            // BuildMenuBuild
            // 
            this.BuildMenuBuild.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BuildMenuBuild.Enabled = false;
            this.BuildMenuBuild.Image = global::ASM.Properties.Resources.build;
            this.BuildMenuBuild.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BuildMenuBuild.Name = "BuildMenuBuild";
            this.BuildMenuBuild.Size = new System.Drawing.Size(36, 36);
            this.BuildMenuBuild.Click += new System.EventHandler(this.BuildMenuBuild_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 39);
            // 
            // BuildMenuStop
            // 
            this.BuildMenuStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BuildMenuStop.Image = global::ASM.Properties.Resources.cancel;
            this.BuildMenuStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BuildMenuStop.Name = "BuildMenuStop";
            this.BuildMenuStop.Size = new System.Drawing.Size(36, 36);
            this.BuildMenuStop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.stopToolStripMenuItem_Click);
            // 
            // BuildMenuPause
            // 
            this.BuildMenuPause.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BuildMenuPause.Image = global::ASM.Properties.Resources.pause;
            this.BuildMenuPause.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BuildMenuPause.Name = "BuildMenuPause";
            this.BuildMenuPause.Size = new System.Drawing.Size(36, 36);
            this.BuildMenuPause.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pauseToolStripMenuItem_Click);
            // 
            // BuildMenuResume
            // 
            this.BuildMenuResume.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BuildMenuResume.Image = global::ASM.Properties.Resources.resume;
            this.BuildMenuResume.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BuildMenuResume.Name = "BuildMenuResume";
            this.BuildMenuResume.Size = new System.Drawing.Size(36, 36);
            this.BuildMenuResume.MouseDown += new System.Windows.Forms.MouseEventHandler(this.resumeToolStripMenuItem_Click);
            // 
            // BuildMenuRestart
            // 
            this.BuildMenuRestart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BuildMenuRestart.Image = global::ASM.Properties.Resources.refresh;
            this.BuildMenuRestart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BuildMenuRestart.Name = "BuildMenuRestart";
            this.BuildMenuRestart.Size = new System.Drawing.Size(36, 36);
            this.BuildMenuRestart.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BuildMenuRestart_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.status});
            this.statusStrip.Location = new System.Drawing.Point(0, 447);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(840, 22);
            this.statusStrip.SizingGrip = false;
            this.statusStrip.TabIndex = 32;
            // 
            // status
            // 
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(0, 17);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(840, 469);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.MainMenu);
            this.Controls.Add(this.dockPanel);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "MainForm";
            this.Text = "ASM";
            this.Load += new System.EventHandler(this.MainForm_Load);
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
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton BuildMenuBuild;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel status;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
    }
}