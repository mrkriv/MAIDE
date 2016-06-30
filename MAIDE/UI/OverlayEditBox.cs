using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ASM.Utilit;

namespace ASM.UI
{
    public partial class OverlayEditBox : Form
    {
        private object obj;
        private PropertyDescriptor prop;

        public OverlayEditBox(Control control, string propName)
            : this(control, propName, control.PointToScreen(new Point(0, 0)))
        {
            editor.Size = control.Size;
            editor.Font = control.Font;
        }

        public static DialogResult Show(Control control, string propName)
        {
            return new OverlayEditBox(control, propName).ShowDialog();
        } 

        public OverlayEditBox(object Obj, string propName, Point position)
        {
            InitializeComponent();
            obj = Obj;
            prop = TypeDescriptor.GetProperties(obj).Find(propName, true);
            Location = position;
            editor.Text = prop.GetValue(obj).ToString();
            InputHook.MouseDown += OnGlobalMouseDown;
        }

        private void OverlayEditBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DialogResult = DialogResult.OK;
                Control c = obj as Control;
                if (c != null)
                    c.Invoke((Action)(() => setValue()));
                else
                    setValue();

                Close();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }
        }

        private void setValue()
        {
            prop.SetValue(obj, prop.Converter.ConvertTo(editor.Text, prop.PropertyType));
        }

        private void OnGlobalMouseDown(object sender, MouseEventArgs e)
        {
            if (!Bounds.Contains(e.Location))
                Close();
        }

        private void OverlayEditBox_Leave(object sender, EventArgs e)
        {
            Close();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            InputHook.MouseDown -= OnGlobalMouseDown;
            base.OnClosing(e);
        }
    }
}