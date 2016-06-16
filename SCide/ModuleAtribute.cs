using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace ASM
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ModuleAtribute : Attribute
    {
        private static Dictionary<Type, key> forms = new Dictionary<Type, key>();
        public DockState dock = DockState.DockBottom;
        public string dysplayName;
        public bool defaultShow = false;

        private struct key
        {
            public DockContent form;
            public DockState dock;
            public DockPanel panel;
        }

        public static void Init(DockPanel panel, ToolStripDropDownButton menu)
        {
            foreach (var type in typeof(ModuleAtribute).Assembly.GetTypes())
            {
                foreach (var a in type.GetCustomAttributes(typeof(ModuleAtribute), false))
                {
                    ModuleAtribute atrib = a as ModuleAtribute;
                    DockContent form = type.GetConstructor(new Type[] { }).Invoke(null) as DockContent;

                    ToolStripMenuItem btn = new ToolStripMenuItem(atrib.dysplayName);
                    menu.DropDownItems.Add(btn);
                    btn.Click += (s, e) =>
                    {
                        if (form.Visible)
                            form.Hide();
                        else
                            Show(type);
                    };

                    form.TabText = atrib.dysplayName;
                    form.FormClosing += (s, e) =>
                    {
                        e.Cancel = true;
                        form.Hide();
                    };
                    forms.Add(type, new key()
                    {
                        form = form,
                        dock = atrib.dock,
                        panel = panel
                    });

                    if (atrib.defaultShow)
                        Show(type);
                }
            }
        }

        public static DockContent Show(Type type)
        {
            key key = forms[type];

            if (!key.form.Visible)
            {
                if (Thread.CurrentThread.GetApartmentState() != ApartmentState.STA)
                {
                    Thread t = new Thread(() =>
                     key.form.Show(key.panel, key.dock));

                    t.SetApartmentState(ApartmentState.STA);
                    t.Start();
                }
                else
                    key.form.Show(key.panel, key.dock);
            }

            return key.form;
        }
    }
}