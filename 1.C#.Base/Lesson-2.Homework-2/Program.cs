using System;

namespace Lesson_2.Homework_2
{
    class Program
    {
        /// <summary>
        /// Запрос порядкового номера ТЕКУЩЕГО месяца и вывод его наименования
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.Write("Введите порядковый номер текущего месяца: ");
            if (byte.TryParse(Console.ReadLine(), out byte userMonth) && userMonth == DateTime.Today.Month)
                Console.Write($"Текущий месяц: {DateTime.Today.ToString("MMMM")}");
            else
                Console.Write("Необходимо ввести корректный номер текущего месяца");

            Console.ReadKey();
        }
    }
}
