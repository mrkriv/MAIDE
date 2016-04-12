using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ASM.UI
{
    public partial class DragDropPanel : UserControl
    {
        private bool mouseDown;
        Point offestPosition;

        [Category("Appearance")]
        public Control Content
        {
            get
            {
                return splitContainer1.Panel2.Controls.Count == 0 ? null : splitContainer1.Panel2.Controls[0];
            }
            set
            {
                splitContainer1.Panel2.Controls.Clear();
                splitContainer1.Panel2.Controls.Add(value);
                value.Dock = DockStyle.Fill;
            }
        }

        [Category("Appearance")]
        public Control RightSlot
        {
            get
            {
                return rightSlot.Controls.Count == 0 ? null : rightSlot.Controls[0];
            }
            set
            {
                rightSlot.Controls.Clear();
                rightSlot.Controls.Add(value);
                value.Dock = DockStyle.Fill;
            }
        }

        [Category("Appearance")]
        public string Caption
        {
            get { return caption.Text; }
            set { caption.Text = value; }
        }

        [Category("Appearance")]
        public bool DragDropEnable
        {
            get; set;
        }

        public DragDropPanel()
        {
            InitializeComponent();
        }

        private void splitContainer1_Panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseDown = true;
                offestPosition = new Point(e.X, e.Y);
            }
        }

        private void splitContainer1_Panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown && DragDropEnable)
                Location = Location.Add(new Point(e.X, e.Y).Substract(offestPosition));
        }

        private void splitContainer1_Panel1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                mouseDown = false;
        }
    }
}
