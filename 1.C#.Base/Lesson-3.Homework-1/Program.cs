using System;

namespace Homework3_1
{

    class Program
    {
        const int MAX_MATRIX_SIZE = 9;
        static void Main(string[] args)
        {

            Console.Write($"Введите размер квадратной матрицы от 2 до {MAX_MATRIX_SIZE}: ");
            if (!int.TryParse(Console.ReadLine(), out int matrixSize) || matrixSize < 2 || matrixSize > MAX_MATRIX_SIZE)
            {
                Console.WriteLine($"Размер матрицы должен быть от 2 до {MAX_MATRIX_SIZE}");
                return;

            }

            Console.Write($"Введите номер диагонали от 1 до {matrixSize}: ");
            if (!int.TryParse(Console.ReadLine(), out int diagonalNumber) || diagonalNumber < 1 || diagonalNumber > matrixSize)
            {
                Console.WriteLine($"Номер диагонали должен быть от 1 до {matrixSize}");
                return;
            }

            int[,] arr = new int[matrixSize, matrixSize];

            InitArray(arr);

            PrintDiagonal(arr, diagonalNumber);

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\r\nPress any key...");
            Console.ReadKey();
        }

        static void PrintDiagonal(int[,] arr, int diagonalNumber)
        {
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    if (i == (j + diagonalNumber - 1) || j == (i + diagonalNumber - 1))
                        Console.ForegroundColor = ConsoleColor.Red;
                    else
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(arr[i, j].ToString("D2").PadLeft(3));
                }
                Console.WriteLine();
            }

        }

        static void InitArray(int[,] arr)
        {
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    arr[i, j] = (i + 1) * (j + 1);
                }
            }

        }
    }
}
