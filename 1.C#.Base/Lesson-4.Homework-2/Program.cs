using System;

namespace Lesson_4.Homework_2
{
    class Program
    {
        static void Main(string[] args)
        {
            var tuple = GetStringSum(GetNumString());

            Console.WriteLine(string.IsNullOrEmpty(tuple.error) ? tuple.result.ToString() : tuple.error);

            Console.WriteLine("\n Press any key...");
            Console.ReadKey();
        }

        static string GetNumString()
        {
            Console.WriteLine("Введите набор чисел, разделенных запятой:");
            return Console.ReadLine().Replace("  ", " ");
        }

        static (int result, string error) GetStringSum(string text)
        {
            int result = 0;
            string[] arr = text.Split(" ");

            if (arr.Length < 2)
                return (result, "Необходимо задать более одного числа");

            for (int i = 0; i < arr.Length; i++)
            {
                if (int.TryParse(arr[i], out int number))
                    result += number;
                else
                    return (result, $"Ошибка преобразования элемента набора {arr[i]} в число");
            }
            
            return (result, "");
        }
    }
}
