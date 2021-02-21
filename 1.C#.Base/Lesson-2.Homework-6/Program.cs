using System;

namespace Lesson_2.Homework_6
{
    class Program
    {
        [Flags]
        public enum DaysOfWeek
        {
            Monday =    0b_0000001, 
            Tuesday =   0b_0000010,
            Wednesday = 0b_0000100,
            Thursday =  0b_0001000,
            Friday =    0b_0010000,
            Saturday =  0b_0100000,
            Sunday =    0b_1000000
        }

        static void Main(string[] args)
        {
            const DaysOfWeek firstOffice =  (DaysOfWeek)0b0111100;
            const DaysOfWeek secondOffice = (DaysOfWeek)0b1111111;
            const DaysOfWeek thirdOffice =  (DaysOfWeek)0b0101010;
            
            Console.WriteLine($"Первый офис работает: {firstOffice}");
            Console.WriteLine($"Второй офис работает: {secondOffice}");
            Console.WriteLine($"Третий офис работает: {thirdOffice}");

            // проверка работает ли третий оффис в понедельник и вторник
            CheckOffice(thirdOffice, DaysOfWeek.Monday | DaysOfWeek.Tuesday, "Третий");

            // проверка работает ли третий оффис в вторник и четверг
            CheckOffice(thirdOffice, DaysOfWeek.Tuesday | DaysOfWeek.Thursday, "Третий");
        }

        /// <summary>
        /// Проверка работы офиса
        /// </summary>
        /// <param name="office">Расписание работы офиса</param>
        /// <param name="days">проверочные дни</param>
        static void CheckOffice(DaysOfWeek office, DaysOfWeek days, string officeName)
        {
            if ((office & days) == days)
                Console.WriteLine($"Офис {officeName} работает в: {days}");
            else
                Console.WriteLine($"Офис {officeName} НЕ работает в: {days}");
        }
    }
}
