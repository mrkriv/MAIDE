namespace ASM.UI
{
    partial class CodeEditBox
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CodeEditBox));
            this.vScrollBar = new System.Windows.Forms.VScrollBar();
            this.hScrollBar = new System.Windows.Forms.HScrollBar();
            this.CaredIndicator = new System.Windows.Forms.Timer(this.components);
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmCut = new System.Windows.Forms.ToolStripMenuItem();
            this.cmCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.cmPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.cmDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imgList = new System.Windows.Forms.ImageList(this.components);
            this.autoCompiler = new ASM.UI.IconListControl();
            this.contextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // vScrollBar
            // 
            this.vScrollBar.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.vScrollBar.Dock = System.Windows.Forms.DockStyle.Right;
            this.vScrollBar.Location = new System.Drawing.Point(542, 0);
            this.vScrollBar.Name = "vScrollBar";
            this.vScrollBar.Size = new System.Drawing.Size(16, 358);
            this.vScrollBar.TabIndex = 1;
            this.vScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar_Scroll);
            // 
            // hScrollBar
            // 
            this.hScrollBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.hScrollBar.Location = new System.Drawing.Point(0, 358);
            this.hScrollBar.Name = "hScrollBar";
            this.hScrollBar.Size = new System.Drawing.Size(558, 16);
            this.hScrollBar.TabIndex = 2;
            this.hScrollBar.Visible = false;
            // 
            // CaredIndicator
            // 
            this.CaredIndicator.Enabled = true;
            this.CaredIndicator.Interval = 650;
            this.CaredIndicator.Tick += new System.EventHandler(this.CaredIndicator_Tick);
            // 
            // ContextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmCut,
            this.cmCopy,
            this.cmPaste,
            this.cmDelete,
            this.toolStripSeparator1,
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem});
            this.contextMenu.Name = "ContextMenu";
            this.contextMenu.ShowImageMargin = false;
            this.contextMenu.Size = new System.Drawing.Size(157, 142);
            this.contextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenu_Opening);
            // 
            // cmCut
            // 
            this.cmCut.Name = "cmCut";
            this.cmCut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.cmCut.Size = new System.Drawing.Size(156, 22);
            this.cmCut.Text = "Вырезать";
            this.cmCut.Click += new System.EventHandler(this.cmCut_Click);
            // 
            // cmCopy
            // 
            this.cmCopy.Name = "cmCopy";
            this.cmCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.cmCopy.Size = new System.Drawing.Size(156, 22);
            this.cmCopy.Text = "Копировать";
            this.cmCopy.Click += new System.EventHandler(this.cmCopy_Click);
            // 
            // cmPaste
            // 
            this.cmPaste.Name = "cmPaste";
            this.cmPaste.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.cmPaste.Size = new System.Drawing.Size(156, 22);
            this.cmPaste.Text = "Вставить";
            this.cmPaste.Click += new System.EventHandler(this.cmPaste_Click);
            // 
            // cmDelete
            // 
            this.cmDelete.Name = "cmDelete";
            this.cmDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.cmDelete.Size = new System.Drawing.Size(156, 22);
            this.cmDelete.Text = "Удалить";
            this.cmDelete.Click += new System.EventHandler(this.cmDelete_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(153, 6);
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.undoToolStripMenuItem.Text = "Undo";
            this.undoToolStripMenuItem.Click += new System.EventHandler(this.undoToolStripMenuItem_Click);
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.redoToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.redoToolStripMenuItem.Text = "Redo";
            this.redoToolStripMenuItem.Click += new System.EventHandler(this.redoToolStripMenuItem_Click);
            // 
            // imgList
            // 
            this.imgList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgList.ImageStream")));
            this.imgList.TransparentColor = System.Drawing.Color.Transparent;
            this.imgList.Images.SetKeyName(0, "component.png");
            this.imgList.Images.SetKeyName(1, "link.png");
            this.imgList.Images.SetKeyName(2, "rg.png");
            // 
            // autoCompiler
            // 
            this.autoCompiler.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.autoCompiler.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.autoCompiler.CausesValidation = false;
            this.autoCompiler.ForeColor = System.Drawing.Color.Gainsboro;
            this.autoCompiler.ImageLib = this.imgList;
            this.autoCompiler.LeftPading = 1;
            this.autoCompiler.Location = new System.Drawing.Point(80, 96);
            this.autoCompiler.Name = "autoCompiler";
            this.autoCompiler.Size = new System.Drawing.Size(252, 138);
            this.autoCompiler.TabIndex = 4;
            // 
            // CodeEditBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ContextMenuStrip = this.contextMenu;
            this.Controls.Add(this.autoCompiler);
            this.Controls.Add(this.vScrollBar);
            this.Controls.Add(this.hScrollBar);
            this.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ForeColor = System.Drawing.Color.Gainsboro;
            this.Name = "CodeEditBox";
            this.Size = new System.Drawing.Size(558, 374);
            this.contextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.VScrollBar vScrollBar;
        private System.Windows.Forms.HScrollBar hScrollBar;
        private System.Windows.Forms.Timer CaredIndicator;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem cmCut;
        private System.Windows.Forms.ToolStripMenuItem cmCopy;
        private System.Windows.Forms.ToolStripMenuItem cmPaste;
        private System.Windows.Forms.ToolStripMenuItem cmDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
        private System.Windows.Forms.ImageList imgList;
        private IconListControl autoCompiler;
    }
}
