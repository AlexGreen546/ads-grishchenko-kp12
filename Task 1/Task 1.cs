using System;
using static System.Math;
using static System.Console;

namespace Task_1
{
    class Program
    {
        static void Main()
        {
            double x, y, z;
            WriteLine("input x: ");
            x = double.Parse(ReadLine());
            WriteLine("input y: ");
            y = double.Parse(ReadLine());
            WriteLine("input z: ");
            z = double.Parse(ReadLine());

            double a, f, g;

            g = Pow(x, 3) + x;
            if (g == 0)
            {
                WriteLine("Incorect");
                ReadKey();
            }
            else
            {
                f = Pow(Abs(y) + Pow(z, 3), 1.0 / 3.0);
                a = x + f / g;
                WriteLine("a = " + a.ToString());

                double b, j;
                j = x - y;
                if ((x - y) < 0 || z == 0 || a == 0)
                {
                    WriteLine("Incorect");
                    ReadKey();
                }
                else
                {
                    b = (Sqrt(j) / z) + (1.0 / Pow(a, 2));
                    WriteLine("b = " + b.ToString());
                    ReadKey();
                }
                ReadKey();
            }
        }
    }
}
