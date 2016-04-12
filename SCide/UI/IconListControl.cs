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
            public bool Visable;
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
                filter = value;
                if (items.Count() != 0)
                {
                    VisiableAny = false;
                    bool eq = false;

                    foreach (var i in items)
                    {
                        i.Visable = isValid(i.Value);
                        if (i.Visable)
                            VisiableAny = true;
                        if (!eq && i.Value == filter)
                        {
                            selectItem = i;
                            eq = true;
                        }
                    }
                    
                    if (!eq && (selectItem == null || !selectItem.Visable))
                        selectItem = items.FirstOrDefault(a => a.Visable);
                    
                    Invalidate(false);
                }
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

        public void SelectUp()
        {
            if (selectItem == null)
                return;

            var coll = items.Where(a => a.Visable).ToList();
            int i = coll.IndexOf(selectItem);

            if (i != 0)
            {
                selectItem = coll[i - 1];
                Invalidate(false);
            }
        }

        public void SelectDown()
        {
            if (selectItem == null)
                return;

            var coll = items.Where(a => a.Visable).ToList();
            int i = coll.IndexOf(selectItem);

            if (i + 1 < coll.Count())
            {
                selectItem = coll[i + 1];
                Invalidate(false);
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
                Visable = isValid(text)
            });
            modifyCollection = true;
            VisiableAny = items.Any(a => a.Visable);

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
            float dh2 = (ImageLib.ImageSize.Height - h) / -2;
            foreach (var i in items)
            {
                if (!i.Visable)
                    continue;

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
            foreach (var i in items)
            {
                if (i.Visable && i.Render_old_Y <= e.Y && i.Render_old_Y + h >= e.Y)
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