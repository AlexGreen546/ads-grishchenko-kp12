using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;


namespace Lab2
{
    class Program
    {
       
        
         
        static void Main(string[] args)
        {
            WriteLine("/random - створення рандомної матрицi;");
            WriteLine("/norandom - матриця вiд 0 до N*M;");
            WriteLine("/clear - очищення консолi;");
            WriteLine("/end - кiнець програми");
            while (true)
            {
                try
                {


                    string enterCommand = Console.ReadLine();
                    switch (enterCommand)
                    {


                        case "/random":

                            int N, M;
                            
                            Write("N = "); N = Int32.Parse(ReadLine());

                            if (N % 2 != 0)
                            {
                                WriteLine("Помилка!");
                                System.Environment.Exit(1);
                            }
                            Write("M = "); M = Int32.Parse(ReadLine());

                            Random rand = new Random();
                            int[,] matrix = new int[N, M];

                            for (int i = 0; i < N; i++)
                            {
                                for (int j = 0; j < M; j++)
                                {
                                    matrix[i, j] = rand.Next(1, 30);
                                    
                                }
                            }
                            Function(matrix, N, M);
                            break;

                        case "/norandom":

                           
                            Write("N = "); N = Int32.Parse(ReadLine());

                            if (N % 2 != 0)
                            {
                                Console.WriteLine("Помилка!");
                                System.Environment.Exit(1);
                            }
                            Write("M = "); M = Int32.Parse(ReadLine());

                            Random rand1 = new Random();
                            matrix = new int[N, M];

                            for (int i = 0; i < N; i++)
                            {
                                for (int j = 0; j < M; j++)
                                {
                                    matrix[i, j] = rand1.Next(0, N * M);
                                }
                            }
                            Function(matrix, N, M);
                            break;

                        case ("/end"):
                            System.Environment.Exit(1);
                            break;

                        case "/clear":
                            Console.Clear();
                            Console.WriteLine("/random - створення рандомної матрицi;");
                            Console.WriteLine("/norandom - матриця вiд 0 до N*M;");
                            Console.WriteLine("/clear - очищення консолi;");
                            Console.WriteLine("/end - кiнець програми");
                            break;

                        default:
                            Console.WriteLine("Невірна команда.");
                            break;
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine("Помилка!");
                    Console.WriteLine(e);
                    break;
                }
                



                static void Function(int[,] matrix, int N, int M)
                {

                    int min = Int32.MaxValue, max = Int32.MinValue;
                    int iMin = N, iMax = N, jMin = 1, jMax = 1;

                    int i;
                    int j;
                    for (i = 0; i < N; i++)
                    {
                        for (j = 0; j < M; j++)
                        {
                            Write(matrix[i, j] + " ");
                        }
                        WriteLine();
                    }
                    i = N - 1; j = 0;
                    WriteLine("-------------------------");
                    WriteLine($"{i}, {j}");
                    while (i >= N / 2)
                    {
                        if (i == N / 2)
                        {
                            j++;
                            WriteLine($"{i}, {j}");
                            if (min > matrix[i, j])
                            {
                                min = matrix[i, j];
                                iMin = i;
                                jMin = j;
                            }
                            else if (max < matrix[i, j])
                            {
                                max = matrix[i, j];
                                iMax = i;
                                jMax = j;
                            }
                        }
                        else if (j == 0 || j == M - 1)
                        {
                            i--;
                            WriteLine($"{i}, {j}");
                            if (min > matrix[i, j])
                            {
                                min = matrix[i, j];
                                iMin = i;
                                jMin = j;
                            }
                            else if (max < matrix[i, j])
                            {
                                max = matrix[i, j];
                                iMax = i;
                                jMax = j;
                            }
                        }


                        while (i < N - 1 && j < M - 1)
                        {
                            j++;
                            i++;
                            WriteLine($"{i}, {j}");
                            if (min > matrix[i, j])
                            {
                                min = matrix[i, j];
                                iMin = i;
                                jMin = j;
                            }
                            else if (max < matrix[i, j])
                            {
                                max = matrix[i, j];
                                iMax = i;
                                jMax = j;
                            }
                        }
                        if (i == N - 1)
                            j++;
                        else if (j == M - 1)
                            i--;
                        WriteLine($"{i}, {j}");
                        if (min > matrix[i, j])
                        {
                            min = matrix[i, j];
                            iMin = i;
                            jMin = j;
                        }
                        else if (max < matrix[i, j])
                        {
                            max = matrix[i, j];
                            iMax = i;
                            jMax = j;
                        }
                        while (j > 0 && i > N / 2)
                        {
                            i--;
                            j--;
                            WriteLine($"{i}, {j}");
                            if (min > matrix[i, j])
                            {
                                min = matrix[i, j];
                                iMin = i;
                                jMin = j;
                            }
                            else if (max < matrix[i, j])
                            {
                                max = matrix[i, j];
                                iMax = i;
                                jMax = j;
                            }
                        }
                    }
                    WriteLine("-------------------------");
                    int lastElementIndex;
                    if (M % 2 == 1)
                        lastElementIndex = 0;
                    else
                        lastElementIndex = N / 2 - 1;
                    while (!(j == 0 && i == lastElementIndex))
                    {
                        if (!(j == 0 && i == lastElementIndex))
                            while (i > 0)
                            {
                                i--;
                                WriteLine($"{i}, {j}");
                                if (min > matrix[i, j])
                                {
                                    min = matrix[i, j];
                                    iMin = i;
                                    jMin = j;
                                }
                                else if (max < matrix[i, j])
                                {
                                    max = matrix[i, j];
                                    iMax = i;
                                    jMax = j;
                                }
                            }
                        if (j != 0)
                        {
                            j--;
                            WriteLine($"{i}, {j}");
                            if (min > matrix[i, j])
                            {
                                min = matrix[i, j];
                                iMin = i;
                                jMin = j;
                            }
                            else if (max < matrix[i, j])
                            {
                                max = matrix[i, j];
                                iMax = i;
                                jMax = j;
                            }
                        }
                        if (!(j == 0 && i == lastElementIndex))
                            while (i < N / 2)
                            {
                                i++;
                                WriteLine($"{i}, {j}");
                                if (min > matrix[i, j])
                                {
                                    min = matrix[i, j];
                                    iMin = i;
                                    jMin = j;
                                }
                                else if (max < matrix[i, j])
                                {
                                    max = matrix[i, j];
                                    iMax = i;
                                    jMax = j;
                                }
                            }
                        if (j != 0)
                        {
                            j--;
                            WriteLine($"{i}, {j}");
                            if (min > matrix[i, j])
                            {
                                min = matrix[i, j];
                                iMin = i;
                                jMin = j;
                            }
                            else if (max < matrix[i, j])
                            {
                                max = matrix[i, j];
                                iMax = i;
                                jMax = j;
                            }
                        }
                    }
                    WriteLine($"max = {max}, indexMax = {iMax}, {jMax}");
                    WriteLine($"min = {min}, indexMin = {iMin}, {jMin}");
                    WriteLine("");
                    Main(null);
                }
            }
        }
    }
}