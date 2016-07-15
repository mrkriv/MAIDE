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
        void AppyPalette();
    }

    public class Palette : Component
    {
        private static event PropertyChangedEventHandler propertyChanged;
        private static Color background;
        private static Color windowFrameActive;
        private static Color windowFrameDisable;
        private static Color fontTitle;
        private static Color fontMain;
        private static Color menuPressed;
        private static Color menuSelected;
        private static Color menuBorder;
        private static Color menuSeparator;
        private static Color textEditorBackground;
        private static Color textEditorSelected;
        private static Color rextEditorRowId;
        private static Color groupBorder;
        private static Color textEditorSelectLine;
        private static Color textEditorRunLine;
        private static Color dockingTabActive;
        private static Color dockingTabDisable;
        private static MenuPaletteRenderer menuStripRenderer;
        private static ThemeBase dockingTheme;
        private Control owner;
        
        [DefaultValue(typeof(Color), "45, 45, 48")]
        public static Color Background
        {
            get { return background; }
            set { setProperty(ref background, value); }
        }

        [DefaultValue(typeof(Color), "1, 122, 204")]
        public static Color WindowFrameActive
        {
            get { return windowFrameActive; }
            set { setProperty(ref windowFrameActive, value); }
        }

        [DefaultValue(typeof(Color), "66, 66, 66")]
        public static Color WindowFrameDisable
        {
            get { return windowFrameDisable; }
            set { setProperty(ref windowFrameDisable, value); }
        }

        [DefaultValue(typeof(Color), "111, 111, 112")]
        public static Color FontTitle
        {
            get { return fontTitle; }
            set { setProperty(ref fontTitle, value); }
        }

        [DefaultValue(typeof(Color), "241, 241, 241")]
        public static Color FontMain
        {
            get { return fontMain; }
            set { setProperty(ref fontMain, value); }
        }

        [DefaultValue(typeof(Color), "27, 27, 28")]
        public static Color MenuPressed
        {
            get { return menuPressed; }
            set { setProperty(ref menuPressed, value); }
        }

        [DefaultValue(typeof(Color), "62, 62, 64")]
        public static Color MenuSelected
        {
            get { return menuSelected; }
            set { setProperty(ref menuSelected, value); }
        }

        [DefaultValue(typeof(Color), "51, 51, 55")]
        public static Color MenuBorder
        {
            get { return menuBorder; }
            set { setProperty(ref menuBorder, value); }
        }

        [DefaultValue(typeof(Color), "51, 51, 55")]
        public static Color MenuSeparator
        {
            get { return menuSeparator; }
            set { setProperty(ref menuSeparator, value); }
        }

        [DefaultValue(typeof(Color), "30, 30, 30")]
        public static Color TextEditorBackground
        {
            get { return textEditorBackground; }
            set { setProperty(ref textEditorBackground, value); }
        }

        [DefaultValue(typeof(Color), "107, 140, 209, 255")]
        public static Color TextEditorSelected
        {
            get { return textEditorSelected; }
            set { setProperty(ref textEditorSelected, value); }
        }

        [DefaultValue(typeof(Color), "51, 51, 51")]
        public static Color TextEditorRowId
        {
            get { return rextEditorRowId; }
            set { setProperty(ref rextEditorRowId, value); }
        }

        [DefaultValue(typeof(Color), "15, 15, 15")]
        public static Color TextEditorSelectLine
        {
            get { return textEditorSelectLine; }
            set { setProperty(ref textEditorSelectLine, value); }
        }

        [DefaultValue(typeof(Color), "15, 50, 15")]
        public static Color TextEditorRunLine
        {
            get { return textEditorRunLine; }
            set { setProperty(ref textEditorRunLine, value); }
        }

        [DefaultValue(typeof(Color), "150, 150, 150")]
        public static Color GroupBorder
        {
            get { return groupBorder; }
            set { setProperty(ref groupBorder, value); }
        }

        [DefaultValue(typeof(Color), "1, 122, 204")]
        public static Color DockingTabActive
        {
            get { return dockingTabActive; }
            set { setProperty(ref dockingTabActive, value); }
        }

        [DefaultValue(typeof(Color), "45, 45, 48")]
        public static Color DockingTabDisable
        {
            get { return dockingTabDisable; }
            set { setProperty(ref dockingTabDisable, value); }
        }
        
        public static event PropertyChangedEventHandler PropertyChanged
        {
            add { propertyChanged += value; }
            remove { propertyChanged -= value; }
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

        static Palette()
        {
            propertyChanged = delegate { };
            Exep.LoadDefaultProperties(typeof(Palette));
            menuStripRenderer = new MenuPaletteRenderer();
            dockingTheme = new DockingTheme();
        }

        public Palette()
        {
            Enable = true;
            PropertyChanged += OnPaletteChange;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void setProperty<T>(ref T field, T newValue)
        {
            field = newValue;
            propertyChanged(null, null);
        }

        private void OnPaletteChange(object sender, PropertyChangedEventArgs e)
        {
            if (Enable && !DesignMode && Owner != null)
                set(Owner);
        }

        public void Set()
        {
            OnPaletteChange(null, null);
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


            if (control is IUsePalette)
                ((IUsePalette)control).AppyPalette();
            else
            {
                if (control is DockPanel)
                    ((DockPanel)control).Theme = dockingTheme;

                control.BackColor = Background;
                control.ForeColor = FontMain;
            }
        }

        private static void set(ToolStrip strip, int mode)
        {
            foreach (ToolStripItem c in strip.Items)
                set(c, mode);

            if (strip is StatusStrip)
                strip.BackColor = WindowFrameActive;
            else
                strip.BackColor = mode == 1 ? MenuPressed : Background;

            strip.ItemAdded += Strip_ItemAdded;
            strip.ForeColor = FontMain;
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
                item.BackColor = MenuPressed;
            else
                item.BackColor = Background;

            item.ForeColor = item is ToolStripSeparator ? MenuSeparator : FontMain;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                PropertyChanged -= OnPaletteChange;
            base.Dispose(disposing);
        }

        public static void JoinToObject(object obj)
        {
            Type type = typeof(Palette);

            foreach (PropertyInfo m in type.GetProperties(BindingFlags.Static | BindingFlags.Public))
            {
                if (m.PropertyType == typeof(Color))
                {
                    try
                    {
                        PropertyJoin.Create(type, m.Name, obj, m.Name);
                    }
                    catch { }
                }
            }
        }
    }
}