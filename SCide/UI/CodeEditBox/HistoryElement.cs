using System;
using System.Collections.Generic;
using System.Linq;

namespace ASM.UI
{
    partial class CodeEditBox
    {
        public class HistoryElement
        {
            public virtual void Undo(CodeEditBox owner)
            {
                owner.textChanged(owner, new TextChangedEventArgs(this));
            }

            public virtual void Redo(CodeEditBox owner)
            {
                owner.textChanged(owner, new TextChangedEventArgs(this));
            }
        }

        public class HistoryAddChars : HistoryElement
        {
            protected IEnumerable<char> value;
            protected int offest;
            protected Row line;

            public HistoryAddChars(Row line, int offest, IEnumerable<char> value)
            {
                this.offest = offest;
                this.value = value;
                this.line = line;
            }

            public override void Redo(CodeEditBox owner)
            {
                line.Write(value, offest);
            }

            public override void Undo(CodeEditBox owner)
            {
                line.Remove(offest, value.Count());
            }
        }

        public class HistoryRemoveChars : HistoryAddChars
        {
            public HistoryRemoveChars(Row line, int offest, IEnumerable<char> value)
                : base(line, offest, value)
            {
            }

            public override void Undo(CodeEditBox owner)
            {
                base.Redo(owner);
            }

            public override void Redo(CodeEditBox owner)
            {
                base.Undo(owner);
            }
        }

        public class HistoryAddChar : HistoryElement
        {
            protected char value;
            protected int offest;
            protected Row line;

            public HistoryAddChar(Row line, int offest, char value)
            {
                this.offest = offest;
                this.value = value;
                this.line = line;
            }

            public override void Redo(CodeEditBox owner)
            {
                line.Write(value, offest);
            }

            public override void Undo(CodeEditBox owner)
            {
                line.Remove(offest);
            }
        }

        public class HistoryRemoveChar : HistoryAddChar
        {
            public HistoryRemoveChar(Row line, int offest, char value)
                : base(line, offest, value)
            {
            }

            public override void Undo(CodeEditBox owner)
            {
                base.Redo(owner);
            }

            public override void Redo(CodeEditBox owner)
            {
                base.Undo(owner);
            }
        }

        public class HistoryRemoveRow : HistoryElement
        {
            protected Row row;
            protected int index;

            public HistoryRemoveRow(Row line, int index)
            {
                this.row = line;
                this.index = index;
            }

            public override void Undo(CodeEditBox owner)
            {
                owner.InsertRow(index, row);
            }

            public override void Redo(CodeEditBox owner)
            {
                if (owner[index] == row)
                    owner.RemoveRow(index);
                else
                    throw new Exception("Undo/Redo system error, class HistoryRemoveRow");
            }
        }

        public class HistoryAddRow : HistoryRemoveRow
        {
            public HistoryAddRow(Row row, int index)
                : base(row, index)
            {
            }

            public override void Undo(CodeEditBox owner)
            {
                base.Redo(owner);
            }

            public override void Redo(CodeEditBox owner)
            {
                base.Undo(owner);
            }
        }

        public class HistoryRemoveRows : HistoryElement
        {
            protected IEnumerable<Row> rows;
            protected int index;

            public HistoryRemoveRows(IEnumerable<Row> rows, int index)
            {
                this.rows = rows;
                this.index = index;
            }

            public override void Undo(CodeEditBox owner)
            {
                owner.InsertRows(index, rows);
            }

            public override void Redo(CodeEditBox owner)
            {
                if (owner[index] == rows)
                    owner.RemoveRows(index, rows.Count());
                else
                    throw new Exception("Undo/Redo system error, class HistoryRemoveRow");
            }
        }

        public class HistoryAddRows : HistoryRemoveRows
        {
            public HistoryAddRows(IEnumerable<Row> rows, int index)
                : base(rows, index)
            {
            }

            public override void Undo(CodeEditBox owner)
            {
                base.Redo(owner);
            }

            public override void Redo(CodeEditBox owner)
            {
                base.Undo(owner);
            }
        }
    }
}
