using System;
using System.Collections.Generic;
using System.Linq;

namespace ASM.UI
{
    partial class CodeEditBox
    {
        public abstract class HistoryElement
        {
            public int Offest { get; protected set; }
            public Row Row { get; protected set; }

            public HistoryElement(Row row, int index)
            {
                Row = row;
                Offest = index;
            }

            public abstract void Undo(CodeEditBox owner);
            public abstract void Redo(CodeEditBox owner);
            public abstract void InvokeEvent(CodeEditBox owner);
        }

        public class HistoryAddChars : HistoryElement
        {
            public IEnumerable<char> Value { get; protected set; }

            public HistoryAddChars(Row line, int offest, IEnumerable<char> value) :
                base(line, offest)
            {
                Value = value;
            }

            public override void Redo(CodeEditBox owner)
            {
                Row.Write(Value, Offest);
            }

            public override void Undo(CodeEditBox owner)
            {
                Row.Remove(Offest, Value.Count());
            }

            public override void InvokeEvent(CodeEditBox owner)
            {
                owner.textChanged(owner, new TextChangedEventArgs(Row, Offest, Value.Count()));
            }
        }

        public class HistoryRemoveChars : HistoryAddChars
        {
            public HistoryRemoveChars(Row line, int offest, IEnumerable<char> value)
                : base(line, offest, value)
            { }

            public override void Undo(CodeEditBox owner)
            {
                base.Redo(owner);
            }

            public override void Redo(CodeEditBox owner)
            {
                base.Undo(owner);
            }

            public override void InvokeEvent(CodeEditBox owner)
            {
                if (Offest != 0)
                    owner.textChanged(owner, new TextChangedEventArgs(Row, Offest - 1, 1));
            }
        }

        public class HistoryAddChar : HistoryElement
        {
            public char Value { get; protected set; }

            public HistoryAddChar(Row line, int offest, char value) :
                base(line, offest)
            {
                Value = value;
            }

            public override void Redo(CodeEditBox owner)
            {
                Row.Write(Value, Offest);
            }

            public override void Undo(CodeEditBox owner)
            {
                Row.Remove(Offest);
            }

            public override void InvokeEvent(CodeEditBox owner)
            {
                owner.textChanged(owner, new TextChangedEventArgs(Row, Offest, 1));
            }
        }

        public class HistoryRemoveChar : HistoryAddChar
        {
            public HistoryRemoveChar(Row line, int offest, char value)
                : base(line, offest, value)
            { }

            public override void Undo(CodeEditBox owner)
            {
                base.Redo(owner);
            }

            public override void Redo(CodeEditBox owner)
            {
                base.Undo(owner);
            }

            public override void InvokeEvent(CodeEditBox owner)
            {
                if (Offest != 0)
                    owner.textChanged(owner, new TextChangedEventArgs(Row, Offest - 1, 1));
            }
        }

        public class HistoryAddRow : HistoryElement
        {
            public HistoryAddRow(Row row, int index)
                : base(row, index)
            { }

            public override void Undo(CodeEditBox owner)
            {
                owner.RemoveRow(Offest);
            }

            public override void Redo(CodeEditBox owner)
            {
                owner.InsertRow(Offest, Row);
            }

            public override void InvokeEvent(CodeEditBox owner)
            {
                if (Row.Length != 0)
                    owner.textChanged(owner, new TextChangedEventArgs(Row));
            }
        }

        public class HistoryRemoveRow : HistoryAddRow
        {
            public HistoryRemoveRow(Row line, int index) :
                base(line, index)
            { }

            public override void Undo(CodeEditBox owner)
            {
                base.Redo(owner);
            }

            public override void Redo(CodeEditBox owner)
            {
                base.Undo(owner);
            }

            public override void InvokeEvent(CodeEditBox owner) { }
        }

        public class HistoryAddRows : HistoryElement
        {
            public IEnumerable<Row> Rows { get; protected set; }

            public HistoryAddRows(IEnumerable<Row> rows, int index)
                : base(rows.First(), index)
            {
                Rows = rows;
            }

            public override void Undo(CodeEditBox owner)
            {
                owner.RemoveRows(Offest, Rows.Count());
            }

            public override void Redo(CodeEditBox owner)
            {
                owner.InsertRows(Offest, Rows);
            }

            public override void InvokeEvent(CodeEditBox owner)
            {
                foreach (var r in Rows)
                {
                    if (r.Length != 0)
                        owner.textChanged(owner, new TextChangedEventArgs(r, Offest));
                }
            }
        }

        public class HistoryRemoveRows : HistoryAddRows
        {
            public HistoryRemoveRows(IEnumerable<Row> rows, int index) :
                base(rows, index)
            { }

            public override void Undo(CodeEditBox owner)
            {
                base.Redo(owner);
            }

            public override void Redo(CodeEditBox owner)
            {
                base.Undo(owner);
            }

            public override void InvokeEvent(CodeEditBox owner) { }
        }
    }
}