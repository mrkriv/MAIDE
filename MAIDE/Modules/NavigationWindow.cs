using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.ComponentModel;
using WeifenLuo.WinFormsUI.Docking;
using System;
using System.Drawing;
using System.Windows.Forms;
using ASM.UI;
using ASM.Utilit;

namespace ASM
{
    [ModuleAtribute(dysplayName = "Навигация", defaultShow = false, dock = DockState.DockLeft)]
    public partial class NavigationWindow : DockContent
    {
        private bool lockEvent = false;

        public NavigationWindow()
        {
            InitializeComponent();
            View.Nodes.Add("not section");
            View.ExpandAll();
        }

        private void updateTimer_Tick(object sender, EventArgs e)
        {
            if (MainForm.Instance.ActiveDocument == null || ContainsFocus)
                return;

            //CodeEditBox code = MainForm.Instance.ActiveDocument.Code;

            //lockEvent = true;
            //View.BeginUpdate();
            //View.Nodes.Clear();

            //TreeNode rootNode = new TreeNode();
            //rootNode.Text = "not section";
            //rootNode.Tag = 0;

            //for (int i = 0; i < code.Length; i++)
            //{
            //    string line = code[i].ToString();
            //    int n = line.IndexOf(':');
            //    int comment = line.IndexOf(';');

            //    if (n != -1 && (comment == -1 || n < comment))
            //    {
            //        int sect = line.IndexOf("csect");
            //        string name = line.Substring(0, n);

            //        if (sect != -1 && (comment == -1 || sect < comment))
            //        {
            //            View.Nodes.Add(rootNode);
            //            rootNode = new TreeNode();
            //            rootNode.Tag = i;
            //            rootNode.Text = name;
            //        }
            //        else
            //        {
            //            TreeNode node = new TreeNode();
            //            node.Text = name;
            //            node.Tag = i;
            //            rootNode.Nodes.Add(node);
            //        }
            //    }
            //}

            //View.Nodes.Add(rootNode);
            //View.ExpandAll();
            //View.EndUpdate();

            //lockEvent = false;
        }

        private void View_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (lockEvent)
                return;

            CodeEditBox code = null;// MainForm.GetActiveCodeBox();
            if (code == null)
                return;

            int line = (int)e.Node.Tag;
            code.GoTo(line + 15);
            code.GoTo(line);
            //code.EditBox.Select(code.EditBox.SelectionStart, e.Node.Text.Length);
            code.Focus();
        }
    }
}