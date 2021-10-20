using System;
using static System.Console;
using static System.Math;

namespace Task_2
{
    class Program
    {
        static void Main(string[] args)
        {
            int n;
            n = Int32.Parse(ReadLine());

            for (int i = 1; i <= n; i++)
            {
                int CountNumber;
                CountNumber = 0;
                for (int y = (int)Pow(i, 2.0); y > 10; y /= 10)
                {
                    CountNumber++;
                }
                if (i == Pow(i, 2) % Pow(10, CountNumber))
                {
                    WriteLine(i);
                }

            }

        }
    }
}
