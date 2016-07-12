using System;
using System.Resources;
using System.Windows.Forms;

namespace MAIDE.UI
{
    public class LocForm : Form
    {
        public static ResourceManager Resource;
        private bool needTranslate = true;

        public bool NeedTranslate
        {
            get { return needTranslate; }
            set
            {
                if (needTranslate == value)
                    return;
                needTranslate = value;
                Translate();
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Translate();
        }

        public void Translate()
        {
            if (needTranslate && !DesignMode && Resource != null)
                Translate(this);
        }

        public static void Translate(Control self)
        {
            foreach (Control c in self.Controls)
            {
                if (c is ToolStrip)
                    Translate(c as ToolStrip);
                else
                    Translate(c);
            }

            string text = Resource.GetString(self.Text);
            if (text != null)
                self.Text = text;
        }

        public static void Translate(ToolStrip self)
        {
            foreach (ToolStripItem c in self.Items)
                Translate(c);

            string text = Resource.GetString(self.Text);
            if (text != null)
                self.Text = text;
        }

        public static void Translate(ToolStripItem self)
        {
            if (self is ToolStripDropDownButton)
            {
                foreach (ToolStripItem c in ((ToolStripDropDownButton)self).DropDownItems)
                    Translate(c);
            }

            string text = Resource.GetString(self.Text);
            if (text != null)
                self.Text = text;
        }
    }
}
