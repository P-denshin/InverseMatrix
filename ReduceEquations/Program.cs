using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReduceEquations {
    class Program {
        static void Main(string[] args) {
            Fraction a = new Fraction(5, 2);
            Fraction b = new Fraction(5, 2);
            Fraction c = (a - b).Irreducible() + (Fraction) 4;
            c = c.Reciprocal();
            Console.WriteLine(c.Numerator + " / " + c.Denominator);
            Console.ReadLine();
        }
    }
}
