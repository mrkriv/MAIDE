﻿using System;
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
using MAIDE.Modules;

namespace MAIDE
{
    internal sealed partial class MainForm : DefaultForm
    {
        private const string newDocumentName = "NewProgram";
        private int newDocumentCount = 0;
        private string[] startArgs;
        public DocumentForm ActiveDocument;
        private Compiler compiler;
        private Thread runThread;
        private Setting setting;
        private MemoryStream ms;

        public static MainForm Instance { get; private set; }
        public Core Core { get; private set; }

        public MainForm(string[] args)
        {
            startArgs = args;
            Instance = this;

            InitializeComponent();

            Log.OnMessage += Log_OnMessage;

            Icon = Properties.Resources.IconApplication;

            Core = new Core();
            Core.StateChanged += Core_StateChanged;

            compiler = new Compiler(Core);
        }

        private void Log_OnMessage(string message)
        {
            status.Text = message;
        }

        protected override void OnLoad(EventArgs e)
        {
            ModuleAtribute.Init(dockPanel, ViewDropDown);

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

            Log.AddMessage(Language.Done);
            base.OnLoad(e);
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
            else if (ActiveDocument != null && !ActiveDocument.Created)
                ActiveDocument = null;

            if (ActiveDocument != null)
                Text += " - " + ActiveDocument.Text;

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
            ActiveDocument.CodeEditBox.ReadOnly = false;
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
            Log.AddMessage("Core paused");
            Core.Pause();
        }

        private void resumeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Log.AddMessage("Core resume");
            Core.Resume();
        }

        private void BuildMenuResumeOne_MouseDown(object sender, MouseEventArgs e)
        {
            Log.AddMessage("Core resume");
            Core.Resume(true);
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Log.AddMessage("Core destroy");
            Console.Destroy();
        }

        private bool build()
        {
            ms = new MemoryStream();

            if (!compiler.Build(ActiveDocument.GetCode(), ms, true))
            {
                Log.AddMessage(Language.BuildError);
                ModuleAtribute.Show(typeof(ErrorWindow));
                return false;
            }

            Log.AddMessage(Language.BuildDone);
            return true;
        }

        private void run()
        {
            ActiveDocument.CodeEditBox.ReadOnly = true;
            if (!build())
                return;
            
            BeginInvoke((Action)(() =>
            {
                Console.Create();
                Console.Instance.FormClosed += ConsoleClosed;
            }));

            Core.Invoke(ms, ActiveDocument.CodeEditBox.Rows);
            Console.Destroy();
        }

        private void BuildMenuBuild_Click(object sender, EventArgs e)
        {
            build();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (runThread != null)
                runThread.Abort();

            base.OnClosing(e);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (setting == null || setting.IsDisposed)
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
            runThread = new Thread(run);
            runThread.Start();
        }
    }
}   