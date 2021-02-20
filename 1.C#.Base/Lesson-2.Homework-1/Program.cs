using System;

namespace Lesson_2.Homework_1
{
    class Program
    {
        /// <summary>
        /// Расчет средней температуры
        /// Переписал без обработчиков ошибок
        /// Не так красиво, но сокращает накладные расходы при обработке исключений, что в данном примере не критично
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.Write("Минимальная температура: ");
            if (int.TryParse(Console.ReadLine(), out int min))
            {

                Console.Write("Максимальная температура: ");
                if (int.TryParse(Console.ReadLine(), out int max))
                {
                    Console.WriteLine($"Средняя температура: {(min + max) / 2}");
                }
                else
                    Console.WriteLine("Необходимо ввести число");
            }
            else
                Console.WriteLine("Необходимо ввести число");

            //try
            //{
            //    Console.Write("Минимальная температура: ");
            //    int min = Convert.ToInt32(Console.ReadLine());
            //    if (min < -273)
            //        throw new Exception("Минимальная температура не может быть меньше абсолютного нуля!");

            //    Console.Write("Максимальная температура: ");
            //    int max = Convert.ToInt32(Console.ReadLine());

            //    if (min > max)
            //        throw new Exception($"Минимальная температура {min} больше максимальной температуры {max}");

            //    Console.WriteLine($"Средняя температура: {(min + max) / 2}");
            //}
            //catch (OverflowException)
            //{
            //    Console.WriteLine("Введенное значение находится за пределами диапазона типа Int32.");
            //}
            //catch (FormatException)
            //{
            //    Console.WriteLine("Необходимо ввести число");
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}

            Console.ReadKey();
        }
    }
}
