using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MAIDE.Utilit;
using System.Reflection;
using System.Runtime.CompilerServices;
using WeifenLuo.WinFormsUI.Docking;

namespace MAIDE.UI
{
    public interface IUsePalette
    {
        bool UsePalette { get; set; }

        void AppyPalette();
    }

    public class Palette : Component
    {
        private static event PropertyChangedEventHandler paletteChanged;
        private static Dictionary<string, TableItem> table;
        private static MenuPaletteRenderer menuStripRenderer;
        private static ThemeBase dockingTheme;
        private static object settingObject;
        private static bool isLook;
        private Control owner;

        private class TableItem
        {
            public Color Color;
            public PropertyDescriptor Descriptor;

            public TableItem(Color color)
            {
                Color = color;
            }
        }

        public static event PropertyChangedEventHandler PaletteChanged
        {
            add { paletteChanged += value; }
            remove { paletteChanged -= value; }
        }

        [DefaultValue(true)]
        public bool Enable { get; set; }

        [DefaultValue(true)]
        public bool DesignSet
        {
            get { return true; }
            set
            {
                if (Owner != null)
                    set(Owner);
            }
        }

        public Color this[string name]
        {
            get { return GetColor(name); }
            set { SetColor(name, value); }
        }

        public Control Owner
        {
            get { return owner; }
            set
            {
                if (owner == value)
                    return;

                if (owner != null)
                {
                    owner.ControlAdded -= Owner_ControlAdded;
                    owner.ControlRemoved -= Owner_ControlRemoved;
                    if (owner is Form)
                        ((Form)owner).Load -= Palette_Load;
                }

                owner = value;

                if (DesignMode || owner == null)
                    return;

                owner.ControlAdded += Owner_ControlAdded;
                owner.ControlRemoved += Owner_ControlRemoved;
                if (owner is Form)
                    ((Form)owner).Load += Palette_Load;
                else
                    Set();
            }
        }

        static Palette()
        {
            Exep.LoadDefaultProperties(typeof(Palette));

            table = new Dictionary<string, TableItem>();
            table.Add("Background", new TableItem(Color.FromArgb(45, 45, 48)));
            table.Add("WindowFrameActive", new TableItem(Color.FromArgb(1, 122, 204)));
            table.Add("WindowFrameDisable", new TableItem(Color.FromArgb(66, 66, 66)));
            table.Add("FontTitle", new TableItem(Color.FromArgb(111, 111, 112)));
            table.Add("FontMain", new TableItem(Color.FromArgb(241, 241, 241)));
            table.Add("MenuPressed", new TableItem(Color.FromArgb(27, 27, 28)));
            table.Add("MenuSelected", new TableItem(Color.FromArgb(62, 62, 64)));
            table.Add("MenuBorder", new TableItem(Color.FromArgb(51, 51, 55)));
            table.Add("MenuSeparator", new TableItem(Color.FromArgb(51, 51, 55)));
            table.Add("TextEditorBackground", new TableItem(Color.FromArgb(30, 30, 30)));
            table.Add("TextEditorSelected", new TableItem(Color.FromArgb(107, 140, 209, 255)));
            table.Add("TextEditorRowId", new TableItem(Color.FromArgb(51, 51, 51)));
            table.Add("TextEditorSelectLine", new TableItem(Color.FromArgb(15, 15, 15)));
            table.Add("TextEditorRunLine", new TableItem(Color.FromArgb(15, 50, 15)));
            table.Add("GroupBorder", new TableItem(Color.FromArgb(150, 150, 150)));
            table.Add("DockingTabActive", new TableItem(Color.FromArgb(1, 122, 204)));
            table.Add("DockingTabDisable", new TableItem(Color.FromArgb(45, 45, 48)));
            table.Add("ButtonNormal", new TableItem(Color.FromArgb(45, 45, 48)));
            table.Add("ButtonHover", new TableItem(Color.FromArgb(62, 62, 64)));
            table.Add("ButtonDown", new TableItem(Color.FromArgb(51, 153, 255)));

            paletteChanged = new PropertyChangedEventHandler(onPaletteChange);
            menuStripRenderer = new MenuPaletteRenderer();
            dockingTheme = new DockingTheme();
        }

        public Palette()
        {
            Enable = true;
            PaletteChanged += (s, e) => Set();
        }

        public static Color GetColor(string name)
        {
            if (table.ContainsKey(name))
                return table[name].Color;
            return Color.Empty;
        }

        public static void SetColor(string name, Color value)
        {
            if (table.ContainsKey(name))
                table[name].Color = value;
            else
            {
                table.Add(name, new TableItem(value));
                if (settingObject != null)
                    createSettingJoin(TypeDescriptor.GetProperties(settingObject).Find(name, false));
            }

            paletteChanged(null, new PropertyChangedEventArgs(name));
        }

        private void Owner_ControlAdded(object sender, ControlEventArgs e)
        {
            e.Control.ControlAdded += Owner_ControlAdded;
            e.Control.ControlRemoved += Owner_ControlRemoved;
            set(e.Control);
        }

        private void Owner_ControlRemoved(object sender, ControlEventArgs e)
        {
            e.Control.ControlAdded -= Owner_ControlAdded;
            e.Control.ControlRemoved -= Owner_ControlRemoved;
        }

        private void Palette_Load(object sender, EventArgs e)
        {
            Set();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void setProperty<T>(ref T field, T newValue)
        {
            field = newValue;
            paletteChanged(null, null);
        }

        private static void onPaletteChange(object sender, PropertyChangedEventArgs e)
        {
            isLook = true;
            TableItem item = table[e.PropertyName];

            if (settingObject != null && item.Descriptor != null)
                item.Descriptor.SetValue(settingObject, item.Color);

            isLook = false;
        }

        public void Set()
        {
            if (Enable && !DesignMode && Owner != null)
                set(Owner);
        }

        private static void set(Control control)
        {
            foreach (Control c in control.Controls)
            {
                if (c is ToolStrip)
                    set(c as ToolStrip, 0);
                else
                    set(c);
            }

            if (control.ContextMenuStrip != null)
                set(control.ContextMenuStrip, 1);

            IUsePalette iControl = control as IUsePalette;

            if (iControl != null)
            {
                if (iControl.UsePalette)
                    iControl.AppyPalette();
            }
            else
            {
                if (control is DockPanel)
                    ((DockPanel)control).Theme = dockingTheme;

                control.BackColor = GetColor("Background");
                control.ForeColor = GetColor("FontMain");
            }
        }

        private static void set(ToolStrip strip, int mode)
        {
            foreach (ToolStripItem c in strip.Items)
                set(c, mode);

            if (strip is StatusStrip)
                strip.BackColor = GetColor("WindowFrameActive");
            else
                strip.BackColor = GetColor(mode == 1 ? "MenuPressed" : "Background");

            strip.ItemAdded += Strip_ItemAdded;
            strip.ForeColor = GetColor("FontMain");
            strip.Renderer = menuStripRenderer;
        }

        private static void Strip_ItemAdded(object sender, ToolStripItemEventArgs e)
        {
            set(e.Item, 0);
        }

        private static void set(ToolStripItem item, int mode)
        {
            if (item is ToolStripDropDownButton)
            {
                foreach (ToolStripItem c in ((ToolStripDropDownButton)item).DropDownItems)
                    set(c, mode);
            }

            if (item.OwnerItem != null || mode == 1)
                item.BackColor = GetColor("MenuPressed");
            else
                item.BackColor = GetColor("Background");

            item.ForeColor = GetColor(item is ToolStripSeparator ? "MenuSeparator" : "FontMain");
        }

        public static void JoinSetting(object obj)
        {
            if (obj == null)
                throw new ArgumentNullException("obj");
            
            settingObject = obj;

            foreach (PropertyDescriptor desc in TypeDescriptor.GetProperties(settingObject))
            {
                if (desc.PropertyType == typeof(Color) && table.ContainsKey(desc.Name))
                {
                    createSettingJoin(desc);
                    table[desc.Name].Color = (Color)desc.GetValue(obj);
                }
            }
        }

        private static void createSettingJoin(PropertyDescriptor desc)
        {
            if (desc.PropertyType != typeof(Color))
                return;

            table[desc.Name].Descriptor = desc;
            desc.AddValueChanged(settingObject, (s, e) =>
            {
                if (!isLook)
                    SetColor(desc.Name, (Color)desc.GetValue(settingObject));
            });
        }
    }
}