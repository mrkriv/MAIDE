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

namespace ASM.UI
{
    public partial class IconListControl : UserControl
    {
        private List<item> items = new List<item>();
        private item selectItem;
        private SolidBrush selectBrush;
        private string filter;
        private bool modifyCollection;

        private class item : IComparable<item>
        {
            public string Value;
            public bool visable;
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

        [Category("Items")]
        public string Filter
        {
            get { return filter; }
            set
            {
                filter = value;
                foreach (var i in items)
                    i.visable = isValid(i.Value);

                if (!items.Any(a => a.visable))
                    Visible = false;
                Invalidate(false);
            }
        }

        public IconListControl()
        {
            InitializeComponent();

            DoubleBuffered = true;
            Filter = "";

            foreach (var inf in typeof(IconListControl).GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var atrs = inf.GetCustomAttributes(typeof(DefaultValueAttribute), true);
                if (atrs.Count() != 0)
                    inf.SetValue(this, ((DefaultValueAttribute)atrs.First()).Value, BindingFlags.SetProperty, null, null, null);
            }
        }

        bool isValid(string value)
        {
            return filter.All(e => value.Contains(e));
        }

        public void AddItem(string text, int img)
        {
            items.Add(new item()
            {
                Value = text,
                ImgIndex = img,
                visable = isValid(text)
            });
            modifyCollection = true;

            if (!items.Any(a => a.visable))
                Visible = false;

            Invalidate(false);
        }

        public void Clear()
        {
            items.Clear();
            modifyCollection = true;
            Invalidate(false);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            SolidBrush br = new SolidBrush(ForeColor);
            float h = Font.GetHeight(e.Graphics);

            if (modifyCollection)
            {
                if (Sorted)
                    items.Sort();

                modifyCollection = false;
            }

            float y = 0;
            float ddy = (ImageLib.ImageSize.Height - h) / -2;
            foreach (var i in items)
            {
                if (!i.visable)
                    continue;

                if (y >= e.ClipRectangle.Y)
                {
                    if (selectItem == i)
                        e.Graphics.FillRectangle(selectBrush, 0, y, Width, h);

                    ImageLib.Draw(e.Graphics, LeftPading + (int)ddy, (int)(y + ddy), i.ImgIndex);
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
            item newTarget = null;

            foreach (var i in items)
            {
                if (i.Render_old_Y <= e.Y && i.Render_old_Y + h >= e.Y)
                {
                    newTarget = i;
                    break;
                }
            }
            if (newTarget != selectItem)
            {
                selectItem = newTarget;
                Invalidate(false);
            }
        }
    }
}