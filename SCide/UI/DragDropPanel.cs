using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ASM.Utilit;

namespace ASM.UI
{
    public partial class DragDropPanel : UserControl
    {
        private EventHandler<DragEventArgs> drag;
        private bool mouseDown;
        private Point offestPosition;

        public class DragEventArgs : EventArgs
        {
            public Point Old { get; set; }
            public Point New { get; set; }

            public DragEventArgs(Point old, Point @new)
            {
                Old = old;
                New = @new;
            }
        }

        public event EventHandler<DragEventArgs> Drag
        {
            add
            {
                lock (this) { drag += value; }
            }
            remove
            {
                lock (this) { drag -= value; }
            }
        }

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
            get { return title.Text; }
            set { title.Text = value; }
        }

        [Category("Appearance")]
        public bool DragDropEnable
        {
            get; set;
        }

        public DragDropPanel()
        {
            InitializeComponent();
            drag = new EventHandler<DragEventArgs>(OnDrag);
        }

        private void splitContainer1_Panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseDown = true;
                offestPosition = new Point(e.X, e.Y);
            }
        }

        protected void OnDrag(object sender, DragEventArgs e)
        {
            Location = e.New;
        }

        private void splitContainer1_Panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown && DragDropEnable)
                drag(this, new DragEventArgs(Location, Location.Add(new Point(e.X, e.Y).Substract(offestPosition))));
        }

        private void splitContainer1_Panel1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                mouseDown = false;
        }
    }
}