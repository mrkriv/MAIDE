﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.CompilerServices;
using System.Reflection;

namespace ASM.UI
{
    public partial class CodeEditBox : UserControl
    {
        private readonly string linesNumFormat = "{0,3:D1}";
        private readonly float defaultFontSize = 10.0f;
        private readonly float[] charSizes = new float[char.MaxValue - char.MinValue];

        private readonly Stack<List<HistoryElement>> undoStack = new Stack<List<HistoryElement>>();
        private readonly Stack<List<HistoryElement>> redoStack = new Stack<List<HistoryElement>>();
        private readonly List<Keys> keyboardState = new List<Keys>();
        private List<HistoryElement> recordStack;
        private readonly List<Row> rows = new List<Row>();
        private int recordMissCount = 0;

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
                normalizeSelect();
                Invalidate(false);
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public Point SelectEnd
        {
            get { return selectEnd; }
            set
            {
                selectEnd = value;
                normalizeSelect();
                Invalidate(false);
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
                startRecordHystory();
                string[] split = value.Split('\n');
                List<Row> buff = new List<Row>(split.Count());

                foreach (var s in split)
                    buff.Add(new Row(this, s.Replace("\r", "")));

                RemoveRows(0, rows.Count);
                InsertRows(0, buff);
                commitHystory();
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
            get { return textBaseBrush.Color; }
            set { textBaseBrush = new SolidBrush(value); }
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
                var atrs = inf.GetCustomAttributes(typeof(DefaultValueAttribute), true);
                if (atrs.Count() != 0)
                    inf.SetValue(this, ((DefaultValueAttribute)atrs.First()).Value, BindingFlags.SetProperty, null, null, null);
            }

            AddRow(new Row(this));
            undoStack.Clear();
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

            Point selectS, selectE;
            if ((selectStart.Y == selectEnd.Y && selectStart.X > selectEnd.X) || selectStart.Y > selectEnd.Y)
            {
                selectS = selectEnd;
                selectE = selectStart;
            }
            else
            {
                selectS = selectStart;
                selectE = selectEnd;
            }

            float py = 0;
            for (int y = start; y < end; y++)
            {
                float px = offestX;

                if (selectS.Y == y && selectE.Y == y)
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

                    if (caretVisible && selectS.Y == y && selectS.X == x)
                        e.Graphics.DrawLine(new Pen(ForeColor), px + 3, py, px + 3, py + lineHeight);

                    px += charSizes[sb.Value];
                }

                if (caretVisible && selectS.Y == y && selectS.X == rows[y].Length)
                    e.Graphics.DrawLine(new Pen(ForeColor), px + 2, py, px + 2, py + lineHeight);

                if (selectS != selectE)
                {
                    if (selectS.Y <= y && selectE.Y >= y)
                    {
                        if (rows[y].Length != 0)
                        {
                            if (selectS.Y == y && rows[y].Length == selectS.X)
                            {
                                int x1 = rows[y][selectS.X - 1].Render_old_X + rows[y][selectS.X - 1].Render_old_Width;
                                e.Graphics.FillRectangle(selectBrush, x1, py, 10, lineHeight);
                            }
                            else
                            {
                                Symbol sStart = GetSymbolByPoint(selectS.X, selectS.Y);
                                Symbol sEnd = GetSymbolByPoint(selectE.X, selectE.Y);

                                int x1 = selectS.Y == y && sStart != null ? sStart.Render_old_X : (int)offestX;
                                int x2 = selectE.Y == y && sEnd != null ? sEnd.Render_old_X + 5 : rows[y][rows[y].Length - 1].Render_old_X + 10;

                                e.Graphics.FillRectangle(selectBrush, x1, py, x2 - x1, lineHeight);
                            }
                        }
                        else
                            e.Graphics.FillRectangle(selectBrush, offestX, py, 10, lineHeight);
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
            startRecordHystory();
            AddToHistory(new HistoryRemoveRow(rows[index], index));
            rows.RemoveAt(index);
            commitHystory();
        }

        public void InsertRow(int index, Row newRow)
        {
            startRecordHystory();
            AddToHistory(new HistoryAddRow(newRow, index));
            rows.Insert(index, newRow);
            commitHystory();
        }

        public void InsertRows(int index, IEnumerable<Row> newRows)
        {
            startRecordHystory();
            AddToHistory(new HistoryAddRows(newRows, index));
            rows.InsertRange(index, newRows);
            commitHystory();
        }

        public void RemoveRows(int index, int count)
        {
            startRecordHystory();
            AddToHistory(new HistoryRemoveRows(rows.GetRange(index, count), index));
            rows.RemoveRange(index, count);
            commitHystory();
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
            switch (e.Button)
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
                if (newPos != selectEnd)
                {
                    selectEnd = newPos;
                    Invalidate(false);
                }
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                leftMouseDown = false;
            normalizeSelect();
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

            return new Point(x, y);
        }

        public void ResetSelect()
        {
            caretVisible = true;
            SelectEnd = SelectStart;
        }
        
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            foreach (ToolStripItem m in ContextMenu.Items)
            {
                if (m is ToolStripMenuItem)
                {
                    Keys k = ((ToolStripMenuItem)m).ShortcutKeys;
                    Keys k2 = k & ~(Keys.Control | Keys.Shift | Keys.Alt);
                    Keys k3 = k & ~k2;

                    if (keyboardState.Contains(k2) && (ModifierKeys & k3) != 0)
                    {
                        m.PerformClick();
                        break;
                    }
                }
            }

            if (ModifierKeys != 0 && (ModifierKeys& Keys.Shift) == 0)
                return;

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
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            keyboardState.Remove(e.KeyCode);
            base.OnKeyUp(e);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            bool needUpdate = true;
            keyboardState.Add(e.KeyCode);

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

        public void startRecordHystory()
        {
            if (recordHystory)
                recordMissCount++;
            else
            {
                recordMissCount = 0;
                recordStack = new List<HistoryElement>();
                recordHystory = true;
            }
        }

        public void commitHystory()
        {
            if (!recordHystory)
                return;

            if (recordMissCount != 0)
                recordMissCount--;
            else
            {
                recordHystory = false;
                if (recordStack.Count != 0)
                    undoStack.Push(recordStack);
            }
        }

        public void Undo()
        {
            if (undoStack.Count == 0)
                return;

            ResetSelect();

            var elem = undoStack.Pop().ToList();

            startRecordHystory();
            foreach (var i in elem)
                i.Undo(this);

            redoStack.Push(elem);
            commitHystory();
            undoStack.Pop();

            Invalidate(true);
        }

        public void Redo()
        {
            if (redoStack.Count == 0)
                return;

            ResetSelect();

            var elem = redoStack.Pop().ToList();

            startRecordHystory();
            foreach (var i in elem)
                i.Redo(this);

            undoStack.Push(elem);
            commitHystory();
            undoStack.Pop();

            Invalidate(true);
        }

        public void SelectAll()
        {
            SelectStart = new Point();
            selectEnd.Y = rows.Count - 1;
            selectEnd.X = rows[SelectEnd.Y].Length - 1;
            Invalidate(true);
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
                case Keys.Control:
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
            if (!((ToolStripMenuItem)sender).Enabled || !Clipboard.ContainsText(TextDataFormat.Text))
                return;

            Paste(Clipboard.GetText(TextDataFormat.Text));
        }

        private void cmCut_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(GetSelect());
            RemoveSelected();
        }

        public string GetSelect()
        {
            StringBuilder sb = new StringBuilder();
            if (SelectStart.Y == SelectEnd.Y)
                sb.Append(rows[SelectStart.Y].GetRange(SelectStart.X, SelectEnd.X - SelectStart.X));
            else
            {
                sb.AppendLine(rows[SelectStart.Y].GetRange(SelectStart.X, rows[SelectStart.Y].Length - SelectStart.X));

                for (int i = SelectStart.Y + 1; i < SelectEnd.Y; i++)
                    sb.AppendLine(rows[i].ToString());
                sb.AppendLine(rows[SelectEnd.Y].GetRange(0, SelectEnd.X));
            }
            return sb.ToString();
        }

        public void Paste(string text)
        {
            startRecordHystory();

            var lines = text.Split('\n');
            RemoveSelected();

            rows[SelectStart.Y].Write(lines[0], SelectStart.X);

            if (lines.Count() > 1)
            {
                int len = SelectStart.X + lines[0].Length;
                var temp = rows[SelectStart.Y].Cut(len, rows[SelectStart.Y].Length - len);
                var buff = new List<Row>();

                for (int i = 1; i < lines.Length; i++)
                    buff.Add(new Row(this, lines[i]));

                InsertRows(SelectStart.Y + 1, buff);
                rows[SelectStart.Y + buff.Count].Write(temp, buff.Last().Length);
            }

            int nx = (lines.Length == 1 ? selectStart.X : 0) + lines.Last().Length;
            SelectStart = new Point(nx, selectStart.Y + lines.Length - 1);
            Invalidate(false);
            commitHystory();
        }

        public void RemoveSelected()
        {
            if (SelectStart == SelectEnd)
                return;

            startRecordHystory();
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
            commitHystory();
        }

        void normalizeSelect()
        {
            if ((selectStart.Y == selectEnd.Y && selectStart.X > selectEnd.X) || selectStart.Y > selectEnd.Y)
            {
                Point p = selectStart;
                selectStart = selectEnd;
                selectEnd = p;
            }

            if (rows.Count <= selectStart.Y)
            {
                selectStart.Y = rows.Count - 1;
                selectEnd = selectStart;
            }

            Row r = rows[selectStart.Y];
            if (r.Length < selectStart.X)
            {
                selectStart.X = r.Length;
                selectEnd = selectStart;
            }
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (((ToolStripMenuItem)sender).Enabled)
                Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (((ToolStripMenuItem)sender).Enabled)
                Redo();
        }

        private void cmDelete_Click(object sender, EventArgs e)
        {
            RemoveSelected();
        }

        private void cmCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(GetSelect());
        }
    }
}