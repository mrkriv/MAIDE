using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

namespace ASM.UI
{
    partial class CodeEditBox
    {
        public class RowReadonlyCollection : IEnumerable<RowReadonly>
        {
            IEnumerable<RowReadonly> ls;

            public RowReadonly this[int index]
            {
                get { return ls.ElementAt(index); }
            }

            internal RowReadonlyCollection(CodeEditBox owner)
            {
                ls = owner.rows.Select(e => new RowReadonly(e));
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return ls.GetEnumerator();
            }

            public IEnumerator<RowReadonly> GetEnumerator()
            {
                return ls.GetEnumerator();
            }
        }

        public class RowReadonly
        {
            private Row row;

            public int Index
            {
                get { return row.Index; }
            }

            public bool Flag
            {
                get { return row.Flag; }
            }

            public Symbol this[int index]
            {
                get { return row[index]; }
            }

            public RowReadonly(Row based)
            {
                row = based;
            }

            public int Length
            {
                get { return row.Length; }
            }

            public override string ToString()
            {
                return row.ToString();
            }
        }
    }
}
