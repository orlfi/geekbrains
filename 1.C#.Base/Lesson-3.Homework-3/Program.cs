using System;

namespace Lesson_3.Homework_3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите строку: ");
            string text = Console.ReadLine();
            Console.WriteLine($"Используя цикл: {ReverseUsingFor(text)}");
            Console.WriteLine($"Используя массив: {ReverseUsingArray(text)}");

            Console.WriteLine("\r\nPress any key...");
            Console.ReadKey();
        }

        static string ReverseUsingFor(string source)
        {
            string result = "";
            for (int i = (source.Length - 1); i >= 0; i--)
                result += source[i];
            return result;
        }

        static string ReverseUsingArray(string source)
        {
            char[] arr = source.ToCharArray();
            Array.Reverse<char>(arr);
            return new string(arr);
        }
    }
}
