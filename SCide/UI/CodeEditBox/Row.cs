using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ASM.UI
{
    partial class CodeEditBox
    {
        public enum RowFlag : byte
        {
            None = 0,
            Breakpoint = 1,
            Run = 2,
        };

        public class Row : IEnumerable<Symbol>
        {
            private List<Symbol> data = new List<Symbol>();
            
            public readonly CodeEditBox Owner;
            public bool IsChange { get; internal set; }
            public RowFlag Flag { get; set; }

            public int Index
            {
                get { return Owner.rows.IndexOf(this); }
            }

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
                Owner.StartRecordHystory();
                Owner.AddToHistory(new HistoryRemoveChar(this, offest, data[offest]));
                data.RemoveAt(offest);
                Owner.CommitHystory();
                IsChange = true;
            }

            public void Remove(int offest, int count)
            {
                if (count > 0)
                {
                    Owner.StartRecordHystory();
                    Owner.AddToHistory(new HistoryRemoveChars(this, offest, data.GetRange(offest, count).Select(s => (char)s)));
                    data.RemoveRange(offest, count);
                    Owner.CommitHystory();
                    IsChange = true;
                }
            }

            public IEnumerable<char> Cut(int offest, int count)
            {
                IEnumerable<char> txt = data.GetRange(offest, count).Select(s => (char)s);
                Remove(offest, count);
                return txt;
            }

            public void Write(char symbol, int offest = 0)
            {
                Owner.StartRecordHystory();
                Owner.AddToHistory(new HistoryAddChar(this, offest, symbol));
                data.Insert(offest, symbol);
                IsChange = true;
                Owner.CommitHystory();
            }

            public void Write(IEnumerable<char> text, int offest = 0)
            {
                Owner.StartRecordHystory();
                Owner.AddToHistory(new HistoryAddChars(this, offest, text));
                data.InsertRange(offest, text.Select(c => (Symbol)c));
                IsChange = true;
                Owner.CommitHystory();
            }
            
            public void Merger(Row line)
            {
                if (line.Length != 0)
                    Write(line.data.Select(e => (char)e), Length);
                Owner.RemoveRow(line.Index);
            }

            public string GetRange(int offest, int count)
            {
                return string.Concat(data.GetRange(offest, count).Select(s => (char)s));
            }

            public List<Word> GetWords(int offest, int end)
            {
                var result = new List<Word>();

                if (offest < data.Count())
                    result.Add(GetWord(offest));

                if (end >= data.Count())
                    end = data.Count() - 1;

                for (offest++; offest < end; offest++)
                {
                    if (wordSplitSymbols.Contains(data[offest - 1]))
                        result.Add(GetWord(offest));
                }
                return result;
            }

            public Word GetWord(int offest)
            {
                if (wordSplitSymbols.Contains(data[offest].Value))
                    return new Word(this, offest, 1);

                int s = offest;
                int e = offest;

                while (s > 0 && !wordSplitSymbols.Contains(data[s - 1].Value))
                    s--;

                while (e + 1 < data.Count && !wordSplitSymbols.Contains(data[e + 1].Value))
                    e++;

                if (wordSplitSymbols.Contains(data[e].Value))
                    e--;

                return new Word(this, s, e - s + 1);
            }

            public override string ToString()
            {
                return new string(data.Select(s => (char)s).ToArray());
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return data.GetEnumerator();
            }

            public IEnumerator<Symbol> GetEnumerator()
            {
                return data.GetEnumerator();
            }
        }
    }
}