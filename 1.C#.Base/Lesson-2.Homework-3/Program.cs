using System;

namespace Lesson_2.Homework_3
{
    class Program
    {
        /// <summary>
        /// Проверка на четность введенного числа
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
#if DEBUG

#endif
            Console.Write("Введите число: ");
            if (int.TryParse(Console.ReadLine(), out int userNumber))
                Console.Write(userNumber % 2 == 0 ? "четное" : "нечетное");
            else
                Console.Write("Необходимо число!");

            Console.ReadKey();
        }
    }
}
