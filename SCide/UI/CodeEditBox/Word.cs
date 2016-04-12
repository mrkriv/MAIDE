using System.Collections;
using System.Drawing;

namespace ASM.UI
{
    partial class CodeEditBox
    {
        public class Word : IEnumerable
        {
            private Enumerator enumerator;
            public readonly Row Owner;
            public readonly int Offest;
            public int Length { get; private set; }
            public int End { get; private set; }

            public Symbol this[int index]
            {
                get { return Owner[Offest + index]; }
                set { Owner[Offest + index] = value; }
            }

            public Word(Row owner, int offest, int length)
            {
                Owner = owner;
                Offest = offest;
                Length = length;
                End = Offest + Length;
            }

            public void SetColor(Color color)
            {
                foreach (Symbol s in this)
                    s.Color = color;
            }

            public void Set(string value)
            {
                Owner.Owner.StartRecordHystory();
                Owner.Remove(Offest, Length);
                if (!string.IsNullOrEmpty(value))
                    Owner.Write(value, Offest);
                Owner.Owner.CommitHystory();

                Length = value.Length;
                End = Offest + Length;
                enumerator = null;
            }

            public static bool operator ==(Word a, string b)
            {
                if (b == null || a.Length != b.Length)
                    return false;
                for (int i = 0; i < b.Length; i++)
                {
                    if (a.Owner[a.Offest + i] != b[i])
                        return false;
                }
                return true;
            }

            public static bool operator !=(Word a, string b)
            {
                if (b == null || a.Length != b.Length)
                    return true;
                for (int i = 0; i < b.Length; i++)
                {
                    if (a.Owner[a.Offest + i] != b[i])
                        return true;
                }
                return false;
            }

            public override bool Equals(object obj)
            {
                if (obj == null)
                    return false;

                return ToString() == obj.ToString();
            }

            public bool Equals(Word word)
            {
                if (word == null)
                    return false;

                return ToString() == word.ToString();
            }

            public override int GetHashCode()
            {
                return Offest ^ Length;
            }

            public override string ToString()
            {
                return Owner.GetRange(Offest, Length);
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public Enumerator GetEnumerator()
            {
                if (enumerator == null)
                    enumerator = new Enumerator(Owner, Offest, Length);
                return enumerator;
            }

            public class Enumerator : IEnumerator
            {
                private readonly int offest;
                private readonly int end;
                private readonly Row row;
                private int position;

                public Enumerator(Row row, int offest, int length)
                {
                    this.row = row;
                    this.offest = offest - 1;
                    position = this.offest;
                    end = offest + length;
                }

                public bool MoveNext()
                {
                    return ++position < end;
                }

                public void Reset()
                {
                    position = offest;
                }

                public Symbol Current
                {
                    get { return row[position]; }
                }

                object IEnumerator.Current
                {
                    get { return Current; }
                }
            }
        }
    }
}