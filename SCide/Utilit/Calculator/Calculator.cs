using System.Collections.Generic;

namespace ASM.Utilit
{
    public static partial class Calculator
    {
        private delegate double UnarDelegate(double a);
        private delegate double BinarDelegate(double a, double b);
        public static Dictionary<string, double> Constants = new Dictionary<string, double>();
        private static List<List<Operator>> operation = new List<List<Operator>>();

        static Calculator()
        {
            new Operator(0, '+', (a, b) => { return a + b; });
            new Operator(0, '-', (a, b) => { return a - b; });
            new Operator(1, '/', (a, b) => { return a / b; });
            new Operator(1, '*', (a, b) => { return a * b; });
            new Operator(2, '^', System.Math.Pow);
            new Operator(3, 'c', System.Math.Cos, "cos");
            new Operator(3, 's', System.Math.Sin, "sin");
            new Operator(3, 't', System.Math.Tan, "tg");
            new Operator(3, 'k', System.Math.Sqrt, "sqrt");

            Constants.Add("pi", 3.14159265359);
            Constants.Add("e", 2.71828182846);
        }

        public static double? Math(string exp)
        {
            try
            {
                return new Element(format(exp));
            }
            catch
            {
                return null;
            }
        }

        public static double? MathToDouble(this string self)
        {
            return Math(self);
        }

        public static string format(string str)
        {
            for (int level = 0; level < operation.Count; level++)
            {
                for (int i = 0; i < operation[level].Count; i++)
                {
                    if (!string.IsNullOrEmpty(operation[level][i].Name))
                        str = str.Replace(operation[level][i].Name, operation[level][i].C.ToString());
                }
            }
            return str;
        }
    }
}