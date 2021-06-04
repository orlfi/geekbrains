using System;
using static System.Console;

namespace Homework
{
    class Program
    {
        static void Main(string[] args)
        {
            Write("Enter the number of rows (an integer from 2 to 9): ");
            string text = ReadLine();
            if (int.TryParse(text, out int m) && m >=2 && m <= 9)
            {
                Write("Enter the number of columns (an integer from 2 to 9): ");
                text = ReadLine();
                if (int.TryParse(text, out int n) && n >= 2 && n <= 9)
                {
                    int result = CalcRecursive(m, n, 4);
                    SetCursorPosition(0, 3);
                    Console.WriteLine($"Result (RECURSIVE): {result }");

                    result = CalcFor(m, n, m+7);
                    SetCursorPosition(0, m+6);
                    Console.WriteLine($"Result (FOR): {result }");
                }
                else
                    WriteLine("Wrong number!");
            }
            else
                WriteLine("Wrong number!");

            ReadKey();
        }



        static int CalcRecursive(int m, int n, int offsetY)
        {
            int result;
            if (m == 1 || n == 1)
                result = 1;
            else
                result = CalcRecursive(m, n - 1, offsetY) + CalcRecursive(m - 1, n, offsetY);
            SetCursorPosition((n-1)*8, (m+ offsetY));
            Write(result);
            return result;
        }

        static int CalcFor(int m, int n, int offsetY)
        {
            int[,] matrix = new int[m, n];

            for (int i = 0; i < m; i++)
                for (int j = 0; j < n; j++)
                {
                    if (i == 0 && j == 0)
                        matrix[i, j] = 0;
                    else if (i == 0 || j == 0)
                        matrix[i, j] = 1;
                    else
                        matrix[i, j] = matrix[i-1, j]+ matrix[i, j-1];

                    SetCursorPosition((j) * 8, (i + offsetY));
                    Write(matrix[i, j]);
                }
            return matrix[m-1, n-1];
        }

    }
}

