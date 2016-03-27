using System.Collections.Generic;
using System.Linq;

namespace ASM
{
    partial class CodeEditBox
    {
        public class Row
        {
            private List<Symbol> data = new List<Symbol>();
            public bool Flag = false;

            public readonly CodeEditBox Owner;
            public bool IsChange { get; internal set; }

            public int Length
            {
                get { return data.Count; }
            }

            public Symbol this[int index]
            {
                get { return data[index]; }
                set { data[index] = value; }
            }

            public Row(CodeEditBox owner)
            {
                Owner = owner;
            }

            public Row(CodeEditBox owner, IEnumerable<char> text) : this(owner)
            {
                data.AddRange(text.Select(e => (Symbol)e));
            }

            public void Remove(int offest)
            {
                Owner.startRecordHystory();
                Owner.AddToHistory(new HistoryRemoveChar(this, offest, data[offest]));
                data.RemoveAt(offest);
                Owner.commitHystory();
                IsChange = true;
            }

            public void Remove(int offest, int count)
            {
                if (count <= 0)
                    return;
                Owner.startRecordHystory();
                Owner.AddToHistory(new HistoryRemoveChars(this, offest, data.GetRange(offest, count).Select(s => (char)s)));
                data.RemoveRange(offest, count);
                Owner.commitHystory();
                IsChange = true;
            }

            public string GetRange(int offest, int count)
            {
                return string.Concat(data.GetRange(offest, count).Select(s => (char)s));
            }

            public IEnumerable<char> Cut(int offest, int count)
            {
                Owner.startRecordHystory();
                IEnumerable<char> txt = data.GetRange(offest, count).Select(s => (char)s);
                Remove(offest, count);
                Owner.commitHystory();
                return txt;
            }

            public void Write(char symbol, int offest = 0)
            {
                Owner.startRecordHystory();
                Owner.AddToHistory(new HistoryAddChar(this, offest, symbol));
                data.Insert(offest, symbol);
                IsChange = true;
                Owner.commitHystory();
            }

            public void Write(IEnumerable<char> text, int offest = 0)
            {
                Owner.startRecordHystory();
                Owner.AddToHistory(new HistoryAddChars(this, offest, text));
                data.InsertRange(offest, text.Select(c => (Symbol)c));
                IsChange = true;
                Owner.commitHystory();
            }

            public void Merger(Row line)
            {
                data.AddRange(line.data);
                IsChange = true;
            }

            public override string ToString()
            {
                return new string(data.Select(s => (char)s).ToArray());
            }
        }
    }
}