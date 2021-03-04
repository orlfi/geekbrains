using System;

namespace Lesson_4.Homework_4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите количество чисел в ряду Фибоначчи: ");
            if (int.TryParse(Console.ReadLine(), out int count))
            {
                Console.WriteLine($"Число Фибоначчи для числа {count}: {Fib(count)}");
            }
            else
                Console.WriteLine ("Необходимо ввести число ");

            Console.WriteLine("\n Press any key...");
            Console.ReadKey();
        }

        static int Fib(int cnt)
        {
            return FibCalc(cnt);
        }


        static int FibCalc(int iter, int prev1 = 1, int prev2 = 0)
        {
            if (iter == 0)
            {
                Console.WriteLine("");
                return prev2;
            }

            if (prev2 == 0)
            {
                prev1 = 1;
                Console.Write($"Ряд: {prev2},{prev1}");
            }
            else
                Console.Write($",{prev1}");

            return FibCalc(iter - 1, prev1 + prev2, prev1);
        }
    }
}
