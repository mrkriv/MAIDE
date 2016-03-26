using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASM
{
    partial class CodeEditBox
    {
        public class Row
        {
            private List<Symbol> data = new List<Symbol>();

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
                Owner.AddToHistory(new HistoryRemoveChar(this, offest, data[offest]));
                data.RemoveAt(offest);
                IsChange = true;
            }

            public void Remove(int offest, int count)
            {
                if (count <= 0)
                    return;
                Owner.AddToHistory(new HistoryRemoveChars(this, offest, data.GetRange(offest, count).Select(s => (char)s)));
                data.RemoveRange(offest, count);
                IsChange = true;
            }

            public IEnumerable<char> Cut(int offest, int count)
            {
                IEnumerable<char> txt = data.GetRange(offest, count).Select(s => (char)s);
                Remove(offest, count);
                return txt;
            }

            public void Write(char symbol, int offest = 0)
            {
                Owner.AddToHistory(new HistoryAddChar(this, offest, symbol));
                data.Insert(offest, symbol);
                IsChange = true;
            }

            public void Write(IEnumerable<char> text, int offest = 0)
            {
                Owner.AddToHistory(new HistoryAddChars(this, offest, text));
                data.InsertRange(offest, text.Select(c => (Symbol)c));
                IsChange = true;
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