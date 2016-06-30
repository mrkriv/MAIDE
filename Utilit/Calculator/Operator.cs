using System.Collections.Generic;

namespace MAIDE.Utilit
{
    public partial class Calculator
    {
        private class Operator
        {
            public char C;
            public string Name;
            public BinarDelegate Binar;
            public UnarDelegate Unar;

            public Operator(int level, char c, BinarDelegate f, string name = null)
            {
                while (operation.Count <= level)
                    operation.Add(new List<Operator>());

                C = c;
                Binar = f;
                Name = name;

                operation[level].Add(this);
            }

            public Operator(int level, char c, UnarDelegate f, string name = null)
            {
                while (operation.Count <= level)
                    operation.Add(new List<Operator>());

                C = c;
                Unar = f;
                Name = name;

                operation[level].Add(this);
            }
        }
    }
}