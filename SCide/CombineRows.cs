using System;
using System.Collections.Generic;
using System.Linq;
using ASM.UI;

namespace ASM
{
    public class CombineRows
    {
        List<CodeEditBox.RowReadonlyCollection> ls = new List<CodeEditBox.RowReadonlyCollection>();

        public int Length { get; private set; }

        public CodeEditBox.RowReadonly this[int index]
        {
            get
            {
                int offest = 0;
                foreach (var rc in ls)
                {
                    int _offest = offest + rc.Count();
                    if (_offest > index)
                        return rc[index - offest];
                    offest = _offest;
                }
                throw new ArgumentOutOfRangeException();
            }
        }

        public void Add(CodeEditBox.RowReadonlyCollection v)
        {
            ls.Add(v);
            Length += v.Count();
        }
    }
}