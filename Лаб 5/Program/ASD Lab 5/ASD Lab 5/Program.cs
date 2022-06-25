using System.Collections.Generic;
using System;
using static System.Console;

public class Program
{

    static int M;

    static public void OutDataARRAY(int[] ARRAY1, int[] ARRAY2)
    {
        Console.BackgroundColor = ConsoleColor.DarkGray;

        for (int i = 0; i < ARRAY1.Length; i++)
            Console.Write($"{ARRAY1[i],5}");

        Console.BackgroundColor = ConsoleColor.Black;

        Console.WriteLine();

        Console.BackgroundColor = ConsoleColor.DarkYellow;

        for (int i = 0; i < ARRAY2.Length; i++)
            Console.Write($"{ARRAY2[i],5}");

        Console.BackgroundColor = ConsoleColor.Black;

        Console.WriteLine("\n");
    }

    static public void OUTPUT_METHOD(int n, int[,] MARTIX)
    {
        Console.WriteLine();

        for (int I = 0; I < M; I++)
        {
            for (int J = 0; J < n; J++)
            {
                if (J * 1.0 % 2 != 0)

                    Console.BackgroundColor = ConsoleColor.DarkGray;

                else

                    Console.BackgroundColor = ConsoleColor.DarkYellow;

                Console.Write($"{MARTIX[I, J],5}");
            }
            Console.BackgroundColor = ConsoleColor.Black;

            Console.WriteLine();
        }

        Console.WriteLine();
    }

    static public void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("\nCHOOSE ACTIONS:");

            Console.WriteLine("A - GENERATE");

            Console.WriteLine("B - EXAMPLE\n");

            switch (Console.ReadLine())
            {
                case "A":
                    CREATION_METHOD();
                    break;
                case "B":
                    EXAMPLEMETHOD();
                    break;
                default:
                    Console.WriteLine("\nERROR");
                    break;
            }
        }
    }

    static int[] AddingALGORITHM(int low, int high, int[] MYARRAY)
    {
        int MYKAY = 0;

        for (int I = low; I < high; I++)
        {
            MYKAY = MYARRAY[I]; int J = I - 1;

            while (J >= 0 && MYARRAY[J] > MYKAY)
            {
                MYARRAY[J + 1] = MYARRAY[J];
                J -= 1;
            }
            MYARRAY[J + 1] = MYKAY;
        }

        return MYARRAY;
    }

    static public void EXAMPLEMETHOD()
    {
        M = 5;

        int[,] __MATRIX = new int[5, 5]
        {
            { -10, 11, 13, -17, -19 },
            { 22, -23, 25, -29, 40 },
            { 33, 44, -50, 18, 36 },
            { -21, 49, -37, 41, -1 },
            { 21, 14, -20, 38, 9 },
        };

        Console.WriteLine("READY MATRIX BEFORE SORTING:");

        OUTPUT_METHOD(5, __MATRIX);

        MAIN__FUNCTION(5, __MATRIX);

        Console.WriteLine("READY MATRIX AFTER SORTING:");

        OUTPUT_METHOD(5, __MATRIX);

        return;
    }


    static public int[,] MAIN__FUNCTION(int N, int[,] matrix)
    {
        int[] _ARRAY1 = new int[M * (N / 2)];

        int[] __ARRAY2 = new int[M * ((N + 1) / 2)];

        for (int J = 0; J < N; J++)
        {
            for (int I = 0; I < M; I++)
            {
                if (J * 1.000 % 2 != 0)
                    _ARRAY1[(J - 1) / 2 * M + I] = matrix[I, J];
                else
                    __ARRAY2[J / 2 * M + I] = matrix[I, J];
            }
        }

        int[] ___ARR = new int[__ARRAY2.Length];

        for (int I = 0; I < ___ARR.Length; I++)

            ___ARR[I] = Math.Abs(__ARRAY2[I]);

        Console.WriteLine("ARRAYS BEFORE SORTING:\n");

        OutDataARRAY(_ARRAY1, __ARRAY2);

        _ARRAY1 = TreeDIVIDING(_ARRAY1, 0, _ARRAY1.Length);

        Array.Reverse(_ARRAY1);

        ___ARR = TreeDIVIDING(___ARR, 0, __ARRAY2.Length);

        int[] newup = new int[__ARRAY2.Length];

        for (int I = 0; I < ___ARR.Length; I++)

            for (int J = 0; J < __ARRAY2.Length; J++)

                if (___ARR[I] == Math.Abs(__ARRAY2[J]))

                    newup[I] = __ARRAY2[J];

        Console.WriteLine("ARRAYS AFTER SORTING:\n");

        OutDataARRAY(_ARRAY1, newup);

        for (int I = 0; I < M; I++)
        {
            for (int J = 0; J < N; J++)
            {
                if (J * 1.0 % 2 != 0)
                    matrix[I, J] = _ARRAY1[(J - 1) / 2 * M + I];
                else
                    matrix[I, J] = newup[J / 2 * M + I];
            }
        }

        return matrix;
    }

    static public void CREATION_METHOD()
    {
        int N;
        Console.Write("\nENTER CUSTOM M:");

        try
        {
            M = int.Parse(Console.ReadLine());
        }

        catch
        {
            CREATION_METHOD();
            return;
        }

        Console.Write("ENTER CUSTOM N:");

        try
        {
            N = int.Parse(Console.ReadLine());
        }

        catch
        {
            CREATION_METHOD();
            return;
        }

        int[,] __MATRIX = new int[M, N];

        List<int> DEFLIST1 = new List<int>();

        Random RANDOM = new Random();

        for (int I = 0; I < M; I++)
        {
            for (int F = 0; F < N; F++)
            {
                while (true)
                {
                    int BRUH = RANDOM.Next(-50, 50);

                    if (!DEFLIST1.Contains(Math.Abs(BRUH)))
                    {
                        DEFLIST1.Add(Math.Abs(BRUH));

                        __MATRIX[I, F] = BRUH;

                        break;
                    }
                }
            }
        }

        Console.WriteLine("MATRIX BEFORE SORTING:");

        OUTPUT_METHOD(N, __MATRIX);

        MAIN__FUNCTION(N, __MATRIX);

        Console.WriteLine("MATRIX AFTER SORTING:");

        OUTPUT_METHOD(N, __MATRIX);

        return;
    }

    static int[] TreeDIVIDING(int[] MYARRAY, int __LOW, int _H)
    {
        int PIV;

        if (_H - __LOW < M)
            MYARRAY = AddingALGORITHM(__LOW, _H, MYARRAY);
        else
        {
            (MYARRAY, PIV) = FastSortMethod(__LOW, _H, MYARRAY);

            MYARRAY = TreeDIVIDING(MYARRAY, PIV + 1, _H);

            MYARRAY = TreeDIVIDING(MYARRAY, __LOW, PIV);
        }
        return MYARRAY;
    }
    static (int[], int) FastSortMethod(int __LOW, int _H, int[] MYARRAY)
    {
        int PIV = (__LOW + _H - 1) / 2;

        if (MYARRAY[_H - 1] > MYARRAY[__LOW])
        {
            if (MYARRAY[PIV] > MYARRAY[_H - 1])

                PIV = _H - 1;

            else if (MYARRAY[PIV] < MYARRAY[__LOW])

                PIV = __LOW;
        }
        else
        {
            if (MYARRAY[PIV] > MYARRAY[__LOW])

                PIV = __LOW;

            else if (MYARRAY[PIV] < MYARRAY[_H - 1])

                PIV = _H - 1;
        }
        (MYARRAY[_H - 1], MYARRAY[PIV]) = (MYARRAY[PIV], MYARRAY[_H - 1]);
        PIV = _H - 1;


        int I = __LOW;

        int J = PIV - 1;

        while (true)
        {
            if (I >= J)
            {
                if (I == J && MYARRAY[I] < MYARRAY[PIV])

                    I++;
                (MYARRAY[I], MYARRAY[PIV]) = (MYARRAY[PIV], MYARRAY[I]);

                PIV = I;

                break;
            }

            if (MYARRAY[I] < MYARRAY[PIV] && MYARRAY[J] > MYARRAY[PIV])
            {
                I++;
                J--;
            }

            else if (MYARRAY[J] > MYARRAY[PIV])
                J--;
            else if (MYARRAY[I] < MYARRAY[PIV])
                I++;

            else
            {
                (MYARRAY[I], MYARRAY[J]) = (MYARRAY[J], MYARRAY[I]);
                I++;
                J--;
            }
        }
        return (MYARRAY, PIV);
    }
}