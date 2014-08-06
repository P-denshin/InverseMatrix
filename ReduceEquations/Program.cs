using System;

namespace ReduceEquations {
    class Program {
        static void Main(string[] args) {
            Matrix a = new Matrix(2, 2,     1, 2,
                                                        4, 7);
            a = a.Inverse();
            a.Show();
            Console.ReadLine();
        }
    }
}
