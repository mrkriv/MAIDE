using System;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using ASM.VM;
using ASM.UI;

namespace ASM
{
    internal sealed partial class MainForm : Form
    {
        private const string newDocumentName = "NewProgram";
        private int newDocumentCount = 0;
        private string[] startArgs;
        private Thread runThread;
        public static MainForm Instance { get; private set; }
        public DocumentForm ActiveDocument;
        public Core core;

        public MainForm(string[] args)
        {
            startArgs = args;
            Instance = this;

            InitializeComponent();

            Icon = Properties.Resources.IconApplication;

            core = new Core();
            core.StateChanged += Core_StateChanged;
        }

        void setStyle(Control c, ToolStripRenderer render)
        {
            if (c is ToolStrip)
                ((ToolStrip)c).Renderer = render;

            foreach (Control ch in c.Controls)
                setStyle(ch, render);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ModuleAtribute.Init(dockPanel, ViewDropDown);
            setStyle(this, new MenuStripRenderer());

            foreach (ToolStripItem menu in MainMenu.Items)
            {
                if (menu is ToolStripDropDownButton)
                {
                    foreach (ToolStripItem item in ((ToolStripDropDownButton)menu).DropDownItems)
                        MenuStripRenderer.SetStyle(item);
                }
            }

            if (startArgs != null && startArgs.Length != 0)
            {
                FileInfo fi = new FileInfo(startArgs[0]);
                if (fi.Exists)
                    OpenFile(fi.FullName);
                else
                    NewDocument();
            }
            else
                NewDocument();

            status.Text = "Готово.";
        }

        private void OpenFile()
        {
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;

            foreach (string filePath in openFileDialog.FileNames)
            {
                bool isOpen = false;
                foreach (DocumentForm documentForm in dockPanel.Documents)
                {
                    if (filePath.Equals(documentForm.FilePath, StringComparison.OrdinalIgnoreCase))
                    {
                        documentForm.Select();
                        isOpen = true;
                        break;
                    }
                }

                if (!isOpen)
                    OpenFile(filePath);
            }
        }

        private DocumentForm OpenFile(string filePath)
        {
            DocumentForm doc = new DocumentForm();
            doc.LoadFile(filePath);
            doc.Show(dockPanel);
            Core_StateChanged(core, null);

            return doc;
        }

        private DocumentForm NewDocument()
        {
            DocumentForm doc = new DocumentForm();
            doc.Text = string.Format(CultureInfo.CurrentCulture, "{0}{1}", newDocumentName, ++newDocumentCount);
            doc.Show(dockPanel);
            Core_StateChanged(core, null);
            return doc;
        }

        private void dockPanel_ActiveContentChanged(object sender, EventArgs e)
        {
            Text = "ASM";

            DocumentForm doc = dockPanel.ActiveContent as DocumentForm;
            if (doc != null)
                ActiveDocument = doc;
            else if (!ActiveDocument.Created)
                ActiveDocument = null;

            if (ActiveDocument != null)
            {
                Text += " - " + ActiveDocument.Text;
            }
            Core_StateChanged(core, null);
        }
        
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewDocument();
        }
        
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void saveAllStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DocumentForm doc in dockPanel.Documents)
            {
                doc.Activate();
                doc.Save();
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveDocument != null)
                ActiveDocument.SaveAs();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveDocument != null)
                ActiveDocument.Save();
        }

        private void runToolStripMenuItem_Click(object sender, EventArgs _event)
        {
            ActiveDocument.Save();
            runThread = new Thread(run);
            runThread.Start();
        }

        private void ConsoleClosed(object sender, FormClosedEventArgs e)
        {
            core.Destroy();
            runThread.Abort();
            RegistersWindow.Binding.DataSource = null;
        }

        private void Core_StateChanged(object sender, EventArgs e)
        {
            bool isDoc = ActiveDocument != null;
            bool runOrLau = core.Status == Core.State.Pause || core.Status == Core.State.Launched;

            BuildMenuRun.Visible = isDoc && !runOrLau;
            BuildMenuStop.Visible = runOrLau || core.Status == Core.State.Finish;
            BuildMenuRestart.Visible = BuildMenuStop.Visible;
            BuildMenuPause.Visible = core.Status == Core.State.Launched;
            BuildMenuResume.Visible = core.Status == Core.State.Pause;
            BuildMenuBuild.Visible = BuildMenuRun.Visible;
        }

        private void pauseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            core.Pause();
            RegistersWindow.Instance.Refresh();
        }

        private void resumeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            core.Resume();
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Console.Destroy();
        }

        bool build()
        {
            if (!core.Build(ActiveDocument.CombineCode()))
            {
                status.Text = "В ходе сборки возникли ошибки.";
                ModuleAtribute.Show(typeof(ErrorWindow));
                return false;
            }
            status.Text = "Постоение успешно завершено.";
            return true;
        }

        private void run()
        {
            //ActiveDocument.Code.ReadOnly = true;
            if (!build())
                return;

            RegistersWindow.Binding.DataSource = core.Registers;

            BeginInvoke((Action)(() =>
            {
                Console.Create();
                Console.Instance.FormClosed += ConsoleClosed;
            }));

            core.Invoke();
            Console.Destroy();
        }

        private void BuildMenuBuild_Click(object sender, EventArgs e)
        {
            if (!build())
                return;

            CodeBuilder cb = new CodeBuilder(core, ActiveDocument.Text.Split('.')[0]);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (runThread != null)
                runThread.Abort();

            base.OnClosing(e);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            new Setting().Show();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutForm().ShowDialog();
        }

        private void BuildMenuRestart_Click(object sender, EventArgs e)
        {
            stopToolStripMenuItem_Click(sender, e);
            runToolStripMenuItem_Click(sender, e);
        }
    }
}