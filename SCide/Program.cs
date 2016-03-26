using System;
using System.Threading;
using System.Reflection;
using System.Security.Permissions;
using System.Windows.Forms;
using System.Diagnostics;

namespace ASM
{
    internal static class Program
    {
        public static bool Debug = false;

        [STAThread]
        [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlAppDomain)]
        private static void Main(string[] args)
        {
            foreach (string arg in args)
            {
                switch (arg)
                {
                    case "-debug":
                        Debug = true;
                        break;
                }
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (!Debug)
            {
                Application.ThreadException += (s, e) => FatalException(e.Exception);
                AppDomain.CurrentDomain.UnhandledException += (s, e) => FatalException(e.ExceptionObject as Exception);
                try
                {
                    Application.Run(new MainForm(args));
                }
                catch (Exception e)
                {
                    FatalException(e);
                }
            }
            else
                Application.Run(new MainForm(args));
        }

        private static void FatalException(Exception e)
        {
            MainForm.Instance.Hide();
            var result = new ExceptionForm(e).ShowDialog();

            switch(result)
            {
                case DialogResult.Ignore:
                    MainForm.Instance.Show();
                    break;
                case DialogResult.Abort:
                    MainForm.Instance.Close();
                    break;
                case DialogResult.Retry:
                    
                    break;
                default:
                    MainForm.Instance.Close();
                    break;
            }
        }
    }
}