using System;

namespace Lesson_2.Homework_4
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.Write("Минимальная температура: ");
                int min = int.Parse(Console.ReadLine());
                if (min < -273)
                    throw new Exception("Минимальная температура не может быть меньше абсолютного нуля!");

                Console.Write("Максимальная температура: ");
                int max = int.Parse(Console.ReadLine());

                if (min > max)
                    throw new Exception($"Минимальная температура {min} больше максимальной температуры {max}");

                Console.WriteLine($"Средняя температура: {(min + max) / 2}");

                Console.Write("Введите порядковый номер текущего месяца: ");
                DateTime today =DateTime.Today;
                byte userMonth = byte.Parse(Console.ReadLine());
                if (userMonth == today.Month)
                    Console.WriteLine($"Текущий месяц: {today.ToString("MMMM")}");
                else
                    throw new Exception("Необходимо ввести корректный номер текущего месяца");

                if ((userMonth <= 2 || userMonth == 12) && (min + max) / 2 > 0)
                    Console.WriteLine("Дождливая зима");
            }
            catch (OverflowException)
            {
                Console.WriteLine("Введенное значение находится за пределами диапазона типа Int32.");
            }
            catch (FormatException)
            {
                Console.WriteLine("Необходимо ввести число");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();

        }
    }
}
