﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Reflection;

namespace ASM
{
    public partial class CodeEditBox : UserControl
    {
        private readonly string linesNumFormat = "{0,3:D1}";
        private readonly float defaultFontSize = 10.0f;
        private readonly float[] charSizes = new float[char.MaxValue - char.MinValue];

        private List<HistoryElement> recordStack;
        private Stack<List<HistoryElement>> undoStack = new Stack<List<HistoryElement>>();
        private Stack<List<HistoryElement>> redoStack = new Stack<List<HistoryElement>>();
        private List<Row> rows = new List<Row>();

        private SolidBrush selectBrush;
        private SolidBrush linesNumBrush;
        private SolidBrush selectLineBrush;
        private SolidBrush textBaseBrush;
        private Pen selectLinePen;
        private float lineHeight;
        private float offestX;
        private float zoom;
        private bool caretVisible;
        private bool leftMouseDown;
        private bool recordHystory;
        private Point selectStart = new Point();
        private Point selectEnd = new Point();
        private EventHandler<TextChangedEventArgs> textChanged;

        [DefaultValue(false)]
        [Category("Behavior")]
        public bool Modified { get; set; }

        [DefaultValue(false)]
        [Category("Behavior")]
        public bool ReadOnly { get; set; }

        [DefaultValue(3)]
        [Category("Appearance")]
        public int LeftOffest { get; set; }

        [DefaultValue(5)]
        [Category("Appearance")]
        public int RightOffest { get; set; }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public Point SelectStart
        {
            get { return selectStart; }
            set
            {
                selectStart = value;
                selectEnd = selectStart;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public Point SelectEnd
        {
            get { return selectEnd; }
            set
            {
                selectEnd = value;

            }
        }

        [DefaultValue(1)]
        [Category("Appearance")]
        public float Zoom
        {
            get { return zoom; }
            set
            {
                zoom = Math.Max(0.01f, Math.Min(value, 10f));
                Font = new Font(Font.FontFamily, defaultFontSize * zoom, Font.Style);
                Invalidate(false);
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public int Length
        {
            get { return rows.Count; }
        }

        public new string Text
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                foreach (var line in rows)
                    sb.AppendLine(line.ToString());

                return sb.ToString();
            }
            set
            {
                string[] split = value.Split('\n');
                List<Row> buff = new List<Row>(split.Count());

                foreach (var s in split)
                    buff.Add(new Row(this, s.Replace("\r", "")));

                RemoveRows(0, rows.Count);
                InsertRows(0, buff);
            }
        }
        
        [Category("Appearance")]
        [DefaultValue(typeof(Color), "51, 51, 51")]
        public Color LinesNumBrush
        {
            get { return linesNumBrush.Color; }
            set { linesNumBrush = new SolidBrush(value); }
        }

        [Category("Appearance")]
        [DefaultValue(typeof(Color), "15, 15, 15")]
        public Color SelectLineBrush
        {
            get { return selectLineBrush.Color; }
            set
            {
                selectLineBrush = new SolidBrush(value);
                selectLinePen = new Pen(Color.FromArgb(value.R + 64, value.G + 64, value.B + 64));
            }
        }

        [Category("Appearance")]
        [DefaultValue(typeof(Color), "107, 140, 209, 255")]
        public Color SelectBrush
        {
            get { return selectBrush.Color; }
            set { selectBrush = new SolidBrush(value); }
        }

        [Category("Appearance")]
        [DefaultValue(typeof(Color), "220, 220, 220")]
        public Color TextBaseBrush
        {
            get { return textBaseBrush.Color;  }
            set { textBaseBrush = new SolidBrush(value);  }
        }

        public new event EventHandler<TextChangedEventArgs> TextChanged
        {
            add
            {
                lock (this) { textChanged += value; }
            }
            remove
            {
                lock (this) { textChanged -= value; }
            }
        }

        public class TextChangedEventArgs : EventArgs
        {
            public HistoryElement Action { get; set; }
            public TextChangedEventArgs(HistoryElement action)
            {
                Action = action;
            }
        }

        public Row this[int row]
        {
            get { return rows[row]; }
        }

        public CodeEditBox()
        {
            InitializeComponent();

            textChanged = (s, e) =>
            {
                CodeEditBox sender = s as CodeEditBox;
                sender.Invalidate(false);
                sender.Modified = true;
                sender.updateSizes();
            };

            SetStyle(ControlStyles.Selectable, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);

            ContextMenu.Renderer = new MenuStripRenderer();
            foreach (ToolStripItem item in ContextMenu.Items)
                MenuStripRenderer.SetStyle(item);
            
            foreach (var inf in typeof(CodeEditBox).GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var atrs = inf.GetCustomAttributes(typeof(DefaultValueAttribute), true); //.Where(e => e is DefaultValueAttribute);
                if (atrs.Count() != 0)
                    inf.SetValue(this, ((DefaultValueAttribute)atrs.First()).Value, BindingFlags.SetProperty, null, null, null);
            }

            AddRow(new Row(this));
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);

            for (int i = 20; i < 127; i++)
                charSizes[i] = getCharWidth((char)i);

            for (int i = 1024; i < 1279; i++)
                charSizes[i] = getCharWidth((char)i);

            charSizes['\t'] = charSizes[' '] * 4;
            lineHeight = Font.GetHeight(Graphics.FromHwnd(Handle));

            vScrollBar.SmallChange = (int)lineHeight;

            updateSizes();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            updateSizes();
        }

        private void updateSizes()
        {
            vScrollBar.LargeChange = Math.Max(1, Height);
            vScrollBar.Maximum = (int)(lineHeight * rows.Count);
            vScrollBar.Visible = vScrollBar.Maximum > Height;
            offestX = TextRenderer.MeasureText(string.Format(linesNumFormat, rows.Count + 1), Font).Width + LeftOffest + RightOffest;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(BackColor);

            e.Graphics.FillRectangle(linesNumBrush, 0, 0, offestX, Height);
            e.Graphics.DrawLine(Pens.Black, offestX, 0, offestX, Height);

            int start = vScrollBar.Value == 0 ? 0 : (int)(vScrollBar.Value / lineHeight) + 1;
            int end = Math.Min(rows.Count, start + (int)(Width / lineHeight));

            float py = 0;
            for (int y = start; y < end; y++)
            {
                float px = offestX;

                if (SelectStart.Y == y && SelectEnd.Y == y)
                {
                    e.Graphics.FillRectangle(selectLineBrush, 0, py, offestX, lineHeight);
                    e.Graphics.DrawRectangle(selectLinePen, offestX + 1, py, Width - offestX - 1, lineHeight);
                    e.Graphics.FillRectangle(selectLineBrush, offestX + 2, py + 1, Width - offestX - 2, lineHeight - 2);
                }

                e.Graphics.DrawString(string.Format(linesNumFormat, y + 1), Font, textBaseBrush, LeftOffest, py, StringFormat.GenericDefault);

                for (int x = 0; x < rows[y].Length; x++)
                {
                    Symbol sb = rows[y][x];

                    e.Graphics.DrawString(sb.Value.ToString(), Font, new SolidBrush(sb.Color), px, py, StringFormat.GenericDefault);
                    sb.Render_old_X = (int)px;
                    sb.Render_old_Width = (int)charSizes[sb.Value];

                    if (caretVisible && SelectStart.Y == y && SelectStart.X == x)
                        e.Graphics.DrawLine(new Pen(ForeColor), px + 3, py, px + 3, py + lineHeight);

                    px += charSizes[sb.Value];
                }

                if (caretVisible && SelectStart.Y == y && SelectStart.X == rows[y].Length)
                    e.Graphics.DrawLine(new Pen(ForeColor), px + 2, py, px + 2, py + lineHeight);

                if (SelectStart != SelectEnd && rows[y].Length != 0)
                {
                    if (SelectStart.Y <= y && SelectEnd.Y >= y)
                    {
                        Symbol sStart = GetSymbolByPoint(SelectStart.X, SelectStart.Y);
                        Symbol sEnd = GetSymbolByPoint(SelectEnd.X, SelectEnd.Y);

                        int x1 = SelectStart.Y == y && sStart != null ? sStart.Render_old_X : (int)offestX;
                        int x2 = SelectEnd.Y == y && sEnd != null ? sEnd.Render_old_X + 5 : rows[y][rows[y].Length - 1].Render_old_X + 10;

                        e.Graphics.FillRectangle(selectBrush, x1, py, x2 - x1, lineHeight);
                    }
                }

                py += lineHeight;
            }

            base.OnPaint(e);
        }

        public void AddRow(Row newRow)
        {
            InsertRow(rows.Count, newRow);
        }

        public void AddRows(IEnumerable<Row> newRows)
        {
            InsertRows(rows.Count, newRows);
        }

        public void RemoveRow(int index)
        {
            AddToHistory(new HistoryRemoveRow(rows[index], index));
            rows.RemoveAt(index);
        }

        public void InsertRow(int index, Row newRow)
        {
            AddToHistory(new HistoryAddRow(newRow, index));
            rows.Insert(index, newRow);
        }

        public void InsertRows(int index, IEnumerable<Row> newRows)
        {
            AddToHistory(new HistoryAddRows(newRows, index));
            rows.InsertRange(index, newRows);
        }

        public void RemoveRows(int index, int count)
        {
            AddToHistory(new HistoryRemoveRows(rows.GetRange(index, count), index));
            rows.RemoveRange(index, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Symbol GetSymbolByPoint(Point p)
        {
            return GetSymbolByPoint(p.X, p.Y);
        }

        public Symbol GetSymbolByPoint(int x, int y)
        {
            if (y >= 0 && x >= 0 && rows.Count > y && rows[y].Length > x)
                return rows[y][x];
            return null;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            switch(e.Button)
            {
                case MouseButtons.Left:
                    SelectStart = GetPointByLocation(e.Location);
                    SelectEnd = SelectStart;
                    Invalidate(false);
                    leftMouseDown = true;
                    break;
                case MouseButtons.Right:
                    ContextMenu.Show(PointToScreen(new Point(e.X, e.Y)));
                    break;
            }
            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            Cursor = e.X < offestX ? Cursors.Default : Cursors.IBeam;

            if (leftMouseDown)
            {
                Point newPos = GetPointByLocation(e.Location);
                if (newPos != SelectEnd)
                {
                    SelectEnd = newPos;
                    Invalidate(false);
                }
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                leftMouseDown = false;
            base.OnMouseUp(e);
        }

        public Point GetPointByLocation(Point pos)
        {
            int y = Math.Max(Math.Min(rows.Count - 1, (int)((vScrollBar.Value + pos.Y) / lineHeight)), 0);

            if (rows[y].Length == 0)
                return new Point(0, y);

            int x = 0;
            while (x < rows[y].Length && 
                pos.X > rows[y][x].Render_old_X + rows[y][x].Render_old_Width / 2)
                x++;

            //return new Point(Math.Min(rows[y].Length - 1, x), y);
            return new Point(x, y);
        }

        public void ResetSelect()
        {
            caretVisible = true;
            SelectEnd = SelectStart;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            bool needUpdate = true;
            switch (e.KeyCode)
            {
                case Keys.Up:
                    if (SelectStart.Y > 0)
                    {
                        selectStart.Y--;
                        int len = rows[SelectStart.Y].Length;
                        if (selectStart.X > len)
                            selectStart.X = len;

                        if (e.Modifiers == Keys.Shift)
                            selectEnd.Y--;
                        else
                            ResetSelect();
                    }
                    break;
                case Keys.Down:
                    if (SelectStart.Y + 1 < rows.Count)
                    {
                        selectStart.Y++;
                        int len = rows[SelectStart.Y].Length;
                        if (SelectStart.X > len)
                            selectStart.X = len;

                        if (e.Modifiers == Keys.Shift)
                            selectEnd.Y++;
                        else
                            ResetSelect();
                    }
                    break;
                case Keys.Left:
                    if (SelectStart.X > 0)
                    {
                        selectStart.X--;
                        if (e.Modifiers == Keys.Shift)
                            selectEnd.X--;
                        else
                            ResetSelect();
                    }
                    else if (SelectStart.Y > 0)
                    {
                        selectStart.X = rows[--selectStart.Y].Length;
                        if (e.Modifiers == Keys.Shift)
                        {
                            selectEnd.Y--;
                            selectEnd.X = SelectStart.X;
                        }
                        else
                            ResetSelect();
                    }
                    else
                        needUpdate = false;
                    break;
                case Keys.Right:
                    if (SelectStart.X < rows[SelectStart.Y].Length)
                    {
                        selectStart.X++;
                        if (e.Modifiers == Keys.Shift)
                            selectEnd.X++;
                        else
                            ResetSelect();
                    }
                    else if (SelectStart.Y + 1 < rows.Count)
                    {
                        selectStart.X = 0;
                        selectStart.Y++;
                        ResetSelect();
                    }
                    break;
                case Keys.Tab:
                    RemoveSelected();
                    rows[SelectStart.Y].Write('\t', SelectStart.X);
                    selectStart.X++;
                    break;
                default:
                    needUpdate = false;
                    break;
            }

            if (needUpdate)
                Invalidate(false);

            base.OnKeyDown(e);
        }

        public void AddToHistory(HistoryElement element)
        {
            if (!recordHystory)
                return;

            recordStack.Add(element);

            if (redoStack.Count != 0)
                redoStack.Clear();
        }

        public void commitHystory()
        {
            pushHystory();
            recordStack = new List<HistoryElement>();
            recordHystory = true;
        }

        public void pushHystory()
        {
            if (!recordHystory)
                return;

            recordHystory = false;
            undoStack.Push(recordStack);
        }

        public void Undo()
        {
            if (undoStack.Count == 0)
                return;

            ResetSelect();

            redoStack.Push(undoStack.Pop());
            redoStack.Last().Reverse();
            foreach (var i in redoStack.Last())
                i.Undo(this);

            Invalidate(true);
        }

        public void Redo()
        {
            if (redoStack.Count == 0)
                return;

            ResetSelect();

            undoStack.Push(redoStack.Pop());
            undoStack.Last().Reverse();
            foreach (var i in undoStack.Last())
                i.Redo(this);
            Invalidate(true);
        }

        public void SelectAll()
        {
            SelectStart = new Point();
            selectEnd.Y = rows.Count - 1;
            selectEnd.X = rows[SelectEnd.Y].Length - 1;
            Invalidate(true);
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case (char)8:
                    if (GetSelectLen() != 0)
                        RemoveSelected();
                    else
                    {
                        if (SelectStart.X > 0)
                            rows[SelectStart.Y].Remove(--selectStart.X);
                        else if (SelectStart.Y != 0)
                        {
                            selectStart.X = rows[SelectStart.Y - 1].Length;
                            rows[SelectStart.Y - 1].Merger(rows[SelectStart.Y]);
                            RemoveRow(selectStart.Y--);
                        }
                    }
                    break;
                case (char)13:
                    RemoveSelected();
                    if (rows[SelectStart.Y].Length != SelectStart.X)
                        InsertRow(SelectStart.Y + 1, new Row(this, rows[SelectStart.Y].Cut(SelectStart.X, rows[SelectStart.Y].Length - SelectStart.X)));
                    else
                        InsertRow(SelectStart.Y + 1, new Row(this));
                    selectStart.Y++;
                    selectStart.X = 0;
                    break;
                case (char)11:
                    break;
                default:
                    RemoveSelected();
                    rows[SelectStart.Y].Write(e.KeyChar, SelectStart.X);
                    selectStart.X++;
                    break;
            }

            ResetSelect();
            Invalidate(false);

            base.OnKeyPress(e);
        }

        float getCharWidth(char c)
        {
            SizeF p1 = TextRenderer.MeasureText("<" + c + ">", Font);
            SizeF p2 = TextRenderer.MeasureText("<>", Font);
            return p1.Width - p2.Width;
        }

        protected override bool IsInputKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Right | Keys.Shift:
                case Keys.Left | Keys.Shift:
                case Keys.Up | Keys.Shift:
                case Keys.Down | Keys.Shift:
                case Keys.Right:
                case Keys.Left:
                case Keys.Up:
                case Keys.Down:
                case Keys.Tab:
                    return true;
            }
            return base.IsInputKey(keyData);
        }

        public void GoTo(Point p)
        {
            ResetSelect();
            SelectStart = p;
            Invalidate(false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GoTo(int line)
        {
            GoTo(new Point(0, line));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GoTo(int x, int y)
        {
            GoTo(new Point(x, y));
        }

        private void CaredIndicator_Tick(object sender, EventArgs e)
        {
            if (Focused)
            {
                caretVisible = !caretVisible;
                Invalidate(false);
            }
        }

        private void ContextMenu_Opening(object sender, CancelEventArgs e)
        {
            cmCopy.Enabled = GetSelectLen() != 0;
            cmCut.Enabled = cmCopy.Enabled;
            cmDelete.Enabled = cmCopy.Enabled;
        }

        public int GetSelectLen()
        {
            if (SelectStart.Y == SelectEnd.Y)
                return SelectEnd.X - SelectStart.X;

            int len = rows[SelectStart.Y].Length - SelectStart.X + SelectEnd.X;
            int y = SelectStart.Y + 1;

            while (y < SelectEnd.Y)
            {
                len += rows[y].Length;
                y++;
            }

            return len;
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            if (ModifierKeys != Keys.Control)
            {
                if (vScrollBar.Visible)
                {
                    int newValue = (int)(vScrollBar.Value - Math.Sign(e.Delta) * Height * 0.1f);
                    if (vScrollBar.Minimum <= newValue && vScrollBar.Maximum >= newValue)
                    {
                        vScrollBar.Value = newValue;
                        vScrollBar_Scroll(vScrollBar, new ScrollEventArgs(ScrollEventType.ThumbPosition, newValue));
                    }
                }
            }
            else
                Zoom += e.Delta / 3000.0f;
        }

        private void vScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.OldValue != e.NewValue)
                Invalidate(false);
        }

        private void cmPaste_Click(object sender, EventArgs e)
        {
            if (!Clipboard.ContainsText(TextDataFormat.Text))
                return;

            RemoveSelected();
            
            var tLines = Clipboard.GetText(TextDataFormat.Text).Split('\n');

            rows[SelectStart.Y].Write(tLines[0], SelectStart.X);

            if (tLines.Count() > 1)
            {
                int len = SelectStart.X + tLines[0].Length;
                var temp = rows[SelectStart.Y].Cut(len, rows[SelectStart.Y].Length - len);
                var buff = new List<Row>();

                for (int i = 1; i < tLines.Length; i++)
                    buff.Add(new Row(this, tLines[i]));

                InsertRows(SelectStart.Y + 1, buff);
                rows[SelectStart.Y + buff.Count].Write(temp, buff.Last().Length);
            }
            
            SelectStart = new Point(selectStart.X + tLines.Last().Length, selectStart.Y + tLines.Length - 1);
        }

        public void RemoveSelected()
        {
            if (SelectStart == SelectEnd)
                return;

            if (SelectStart.Y == SelectEnd.Y)
                rows[SelectStart.Y].Remove(SelectStart.X, SelectEnd.X - SelectStart.X);
            else
            {
                rows[SelectStart.Y].Remove(SelectStart.X, rows[SelectStart.Y].Length - SelectStart.X);
                if (SelectEnd.Y - SelectStart.Y > 2)
                {
                    RemoveRows(SelectStart.Y + 1, SelectEnd.Y - SelectStart.Y - 1);
                }
                if (SelectEnd.X != rows[SelectStart.Y + 1].Length)
                {
                    var txt = rows[SelectStart.Y + 1].Cut(SelectEnd.X, rows[SelectStart.Y + 1].Length - SelectEnd.X);
                    rows[SelectStart.Y].Write(txt, SelectStart.X);
                }
                RemoveRow(SelectStart.Y + 1);
            }
            ResetSelect();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Redo();
        }
    }
}