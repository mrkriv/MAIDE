using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using ASM.Utilit;

namespace ASM.UI
{
    public partial class IconListControl : UserControl
    {
        private List<item> items = new List<item>();
        private List<item> visiable_items = new List<item>();
        private item selectItem;
        private SolidBrush selectBrush;
        private string filter;
        private bool modifyCollection;

        private class item : IComparable<item>
        {
            public string Value;
            public int ImgIndex;
            public int Render_old_Y;

            public int CompareTo(item obj)
            {
                return Value.CompareTo(obj.Value);
            }
        }

        [Category("Appearance")]
        public ImageList ImageLib { get; set; }

        [Category("Appearance")]
        [DefaultValue(typeof(Color), "51, 153, 255, 255")]
        public Color SelectBrush
        {
            get { return selectBrush.Color; }
            set { selectBrush = new SolidBrush(value); }
        }

        [Category("Items")]
        [DefaultValue(true)]
        public bool Sorted { get; set; }

        [Category("Appearance")]
        [DefaultValue(1)]
        public int LeftPading { get; set; }

        [Category("Items")]
        public string SelectItem
        {
            get { return selectItem?.Value; }
            set
            {
                selectItem = items.Find(i => i.Value == value);
                Invalidate();
            }
        }
        
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public bool VisiableAny { get; private set; }

        [Category("Items")]
        public string Filter
        {
            get { return filter; }
            set
            {
                if (filter == value)
                    return;

                filter = value;
                filtre();
                Invalidate(false);
            }
        }

        public IconListControl()
        {
            InitializeComponent();

            DoubleBuffered = true;
            Filter = "";

            this.LoadDefaultProperties();
        }

        public void SelectUp()
        {
            if (selectItem == null)
                return;
            
            int i = visiable_items.IndexOf(selectItem);
            if (i != 0)
            {
                selectItem = visiable_items[i - 1];
                Invalidate(false);
            }
        }

        public void SelectDown()
        {
            if (selectItem == null)
                return;

            int i = visiable_items.IndexOf(selectItem);
            if (i + 1 < visiable_items.Count())
            {
                selectItem = visiable_items[i + 1];
                Invalidate(false);
            }
        }

        bool isValid(string value)
        {
            return filter.All(e => value.Contains(e));
        }

        void filtre()
        {
            visiable_items.Clear();
            
            foreach (var i in items)
            {
                if (isValid(i.Value))
                    visiable_items.Add(i);
            }

            selectItem = visiable_items.FirstOrDefault();
            VisiableAny = visiable_items.Count() != 0;
        }

        public void AddItem(string text, int img)
        {
            items.Add(new item() { Value = text, ImgIndex = img, });
            modifyCollection = true;
            Invalidate(false);
        }

        public void Clear()
        {
            items.Clear();
            visiable_items.Clear();
            modifyCollection = true;
            Invalidate(false);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            SolidBrush br = new SolidBrush(ForeColor);
            float h = Font.GetHeight(e.Graphics);

            if (modifyCollection)
            {
                filtre();
                modifyCollection = false;
            }

            float y = 0;
            float dh2 = (ImageLib.ImageSize.Height - h) / -2;
            foreach (var i in visiable_items)
            {
                if (y >= e.ClipRectangle.Y)
                {
                    if (selectItem == i)
                        e.Graphics.FillRectangle(selectBrush, 0, y, Width, h);

                    ImageLib.Draw(e.Graphics, LeftPading + (int)dh2, (int)(y + dh2), i.ImgIndex);
                    e.Graphics.DrawString(i.Value, Font, br, LeftPading + ImageLib.ImageSize.Width + 2, y);
                    i.Render_old_Y = (int)y;
                }
                y += h;

                if (y >= e.ClipRectangle.Y + e.ClipRectangle.Height)
                    break;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            float h = Font.GetHeight(Graphics.FromHwnd(Handle));
            foreach (var i in visiable_items)
            {
                if (i.Render_old_Y <= e.Y && i.Render_old_Y + h >= e.Y)
                {
                    if (i != selectItem)
                    {
                        selectItem = i;
                        Invalidate(false);
                    }
                    break;
                }
            }
        }
    }
}