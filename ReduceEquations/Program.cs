using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReduceEquations {
    class Program {
        static void Main(string[] args) {
            Matrix a = new Matrix(2, 2,     1, 2,
                                                        3, 4);
            a = a.Inverse();
            a.Show();
            Console.Read();
        }
    }
}
