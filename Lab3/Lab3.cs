using System;
using static System.Console;

namespace Lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();
            WriteLine("Введите число N: "); int n = Int32.Parse(ReadLine());
            WriteLine("Масив: ");
            int[] a = new int[n];
            int[] b = new int[a.Length];
            for (int i = 0; i < n; i++)
            {
                a[i] = rnd.Next(0, 50);
                WriteLine("{0} ", a[i]);

                if (a[i] == 0)
                {
                    WriteLine("Ввод нуля недопустим. ");
                    break;
                }
            }
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] % 2 == 0)
                {
                    WriteLine("Парне: ");
                    b[i] = a[i];
                    WriteLine(b[i] + "\t");
                }
                if (a[i] % 2 != 0)
                {
                    WriteLine("Непарне: ");
                    b[i] = a[i];
                    WriteLine(b[i] + "\t");
                }
            }
        }
    }
}
