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
        public Core core;
        public DocumentForm ActiveDocument;

        public MainForm(string[] args)
        {
            startArgs = args;
            Instance = this;

            InitializeComponent();
            
            MainMenu.Renderer = new MenuStripRenderer();
            Icon = Properties.Resources.IconApplication;

            core = new Core();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ModuleAtribute.Init(dockPanel, ViewDropDown);

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
            updateState();

            return doc;
        }

        private DocumentForm NewDocument()
        {
            DocumentForm doc = new DocumentForm();
            doc.Text = string.Format(CultureInfo.CurrentCulture, "{0}{1}", newDocumentName, ++newDocumentCount);
            doc.Show(dockPanel);
            updateState();
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

            updateState();
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

        private void replaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
           // ActiveDocument.Code.FindReplace.ShowReplace();
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
            updateState();
        }

        private void ConsoleClosed(object sender, FormClosedEventArgs e)
        {
            core.Destroy();
            runThread.Abort();
            RegistersWindow.Binding.DataSource = null;
            updateState();
        }

        void updateState()
        {
            bool isDoc = ActiveDocument != null;
            bool isStart = isDoc && core.IsReady;
            bool isFinished = isStart && core.IsFinished;
            bool isPaused = isStart && core.IsPaused;

            BuildMenuRun.Visible = isDoc && !isStart;
            BuildMenuStop.Visible = isStart;
            BuildMenuRestart.Visible = isStart;
            BuildMenuPause.Visible = isStart && !isFinished && !isPaused;
            BuildMenuResume.Visible = isStart && !isFinished && isPaused;
        }

        private void pauseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            core.Pause();
            updateState();
        }

        private void resumeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            core.Resume();
            updateState();
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Console.Destroy();
        }

        private void restartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stopToolStripMenuItem_Click(sender, e);
            runToolStripMenuItem_Click(sender, e);
            updateState();
        }

        bool build()
        {
            if (!core.Build(ActiveDocument.CombineCode()))
            {
                status.Text = "В ходе сборки возникли ошибки.";
                BeginInvoke((Action)updateState);
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

        private void restartToolStripMenuItem_Click(object sender, MouseEventArgs e)
        {
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutForm().ShowDialog();
        }
    }
}