using System.Collections.Generic;

namespace MAIDE.Utilit
{
    public partial class Calculator
    {
        private class Element
        {
            private double value;
            private Operator op;
            private Element A, B;

            public Element(string data)
            {
                bool flag = true;

                int sp;
                while (flag && data[0] == '(' && data[data.Length - 1] == ')')
                {
                    sp = 0;
                    for (int i = 1; i < data.Length - 2; i++)
                    {
                        if (data[i] == ')')
                            sp--;
                        else if (data[i] == '(')
                            sp++;

                        if (sp == -1)
                        {
                            flag = false;
                            break;
                        }
                    }
                    if (flag)
                        data = data.Substring(1, data.Length - 2);
                }

                char c;
                foreach (List<Operator> ops in operation)
                {
                    sp = 0;
                    for (int i = data.Length - 1; i >= 0; i--)
                    {
                        c = data[i];
                        if (c == ')')
                            sp--;
                        else if (c == '(')
                            sp++;
                        else if (!char.IsNumber(c) && sp == 0)
                        {
                            foreach (Operator op in ops)
                            {
                                if (op.C == c)
                                {
                                    this.op = op;
                                    if (op.Binar != null)
                                        A = new Element(data.Substring(0, i));
                                    B = new Element(data.Substring(i + 1, data.Length - i - 1));
                                    return;
                                }
                            }
                        }
                    }
                }

                value = Constants.ContainsKey(data) ? Constants[data] : double.Parse(data);
            }

            public static implicit operator double(Element self)
            {
                if (self.op != null)
                {
                    if (self.op.Binar != null)
                        return self.op.Binar(self.A, self.B);
                    return self.op.Unar(self.B);
                }
                return self.value;
            }
        }
    }
}