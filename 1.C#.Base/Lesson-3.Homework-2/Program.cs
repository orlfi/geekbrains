using System;

namespace Lesson_3.Homework_2
{
    class Program
    {
        static void Main(string[] args)
        {
            string[,] catalog = {
                {"Иванов И.И.", "+79108434578"},
                {"Сидоров С.С.", "sidor_ss@mail.ru"},
                {"Петров П.П.", "+79157856489"},
                {"Васечкин В.В.", "vasya@gmail.com"},
                {"Пупкин П.П.", "+79536897525"},
            };

            for (int i = 0; i < catalog.GetLength(0); i++)
            {
                Console.WriteLine($"{catalog[i, 0]}\t{catalog[i, 1]}");
            }

            Console.WriteLine("\r\nPress any key...");
            Console.ReadKey();
        }
    }
}
