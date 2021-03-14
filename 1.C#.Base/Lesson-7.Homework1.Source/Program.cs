using System;

namespace Lesson_7.Homework1.Source
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Input number: ");
            if (int.TryParse(Console.ReadLine(), out int number))
            {
                if (number % 2 == 0)
                    Console.WriteLine("the number is even!");
                else
                    Console.WriteLine("the number is odd!");
            }
            else
                Console.WriteLine("Error: you must enter a number!");

            Console.ReadKey();
        }
    }
}
