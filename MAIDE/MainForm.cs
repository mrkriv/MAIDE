using System;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using MAIDE.VM;
using MAIDE.UI;
using MAIDE.Utilit;
using MAIDE.Modules;

namespace MAIDE
{
    internal sealed partial class MainForm : DefaultForm
    {
        private const string newDocumentName = "NewProgram";
        private int newDocumentCount = 0;
        private string[] startArgs;
        private Thread runThread;
        public static MainForm Instance { get; private set; }
        public DocumentForm ActiveDocument;
        public Core Core { get; private set; }
        private Setting setting;

        public MainForm(string[] args)
        {
            startArgs = args;
            Instance = this;

            InitializeComponent();

            Icon = Properties.Resources.IconApplication;

            Core = new Core();
            Core.StateChanged += Core_StateChanged;
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

            status.Text = Language.Done;
        }

        private void OpenFile()
        {
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;

            foreach (string filePath in openFileDialog.FileNames)
            {
                foreach (DocumentForm documentForm in dockPanel.Documents)
                {
                    if (filePath.Equals(documentForm.FileName, StringComparison.OrdinalIgnoreCase))
                    {
                        documentForm.Select();
                        return;
                    }
                }

                OpenFile(filePath);
            }
        }

        private DocumentForm OpenFile(string filePath)
        {
            DocumentForm doc = new DocumentForm();
            doc.LoadFile(filePath);
            doc.Show(dockPanel);
            Core_StateChanged(Core, null);
            
            return doc;
        }

        private DocumentForm NewDocument()
        {
            DocumentForm doc = new DocumentForm();
            doc.Text = string.Format(CultureInfo.CurrentCulture, "{0}{1}", newDocumentName, ++newDocumentCount);
            doc.Show(dockPanel);
            Core_StateChanged(Core, null);
            return doc;
        }

        private void dockPanel_ActiveContentChanged(object sender, EventArgs e)
        {
            Text = "MAIDE";

            DocumentForm doc = dockPanel.ActiveContent as DocumentForm;
            if (doc != null)
                ActiveDocument = doc;
            else if (!ActiveDocument.Created)
                ActiveDocument = null;

            if (ActiveDocument != null)
            {
                Text += " - " + ActiveDocument.Text;
            }
            Core_StateChanged(Core, null);
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
            Core.Stop();
            runThread.Abort();
        }

        private void Core_StateChanged(object sender, Core.StateChangedEventArgs e)
        {
            bool isDoc = ActiveDocument != null;
            bool runOrLau = Core.Status == Core.State.Pause || Core.Status == Core.State.Launched;

            BuildMenuRun.Visible = isDoc && !runOrLau;
            BuildMenuStop.Visible = runOrLau || Core.Status == Core.State.Finish;
            BuildMenuRestart.Visible = runOrLau;
            BuildMenuPause.Visible = Core.Status == Core.State.Launched;
            BuildMenuResume.Visible = Core.Status == Core.State.Pause;
            BuildMenuBuild.Visible = BuildMenuRun.Visible;
            BuildMenuResumeOne.Visible = BuildMenuResume.Visible;
        }

        private void pauseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Core.Pause();
        }

        private void resumeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Core.Resume();
        }

        private void BuildMenuResumeOne_MouseDown(object sender, MouseEventArgs e)
        {
            Core.Resume(true);
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Console.Destroy();
        }

        bool build()
        {
            if (!Core.Build(ActiveDocument.GetCode()))
            {
                status.Text = Language.BuildError;
                ModuleAtribute.Show(typeof(ErrorWindow));
                return false;
            }
            status.Text = Language.BuildDone;
            return true;
        }

        private void run()
        {
            //ActiveDocument.Code.ReadOnly = true;
            if (!build())
                return;
            
            BeginInvoke((Action)(() =>
            {
                Console.Create();
                Console.Instance.FormClosed += ConsoleClosed;
            }));

            Core.Invoke();
            Console.Destroy();
        }

        private void BuildMenuBuild_Click(object sender, EventArgs e)
        {
            if (!build())
                return;

            CodeBuilder cb = new CodeBuilder(Core, ActiveDocument.Text.Split('.')[0]);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (runThread != null)
                runThread.Abort();

            base.OnClosing(e);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (setting == null)
            {
                setting = new Setting();
                setting.Show();
            }
            else
                setting.BringToFront();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutForm().ShowDialog();
        }

        private void BuildMenuRestart_Click(object sender, EventArgs e)
        {
            Core.Stop();
            runThread.Abort();
            runThread = new Thread(Core.Invoke);
            runThread.Start();
        }
    }
}