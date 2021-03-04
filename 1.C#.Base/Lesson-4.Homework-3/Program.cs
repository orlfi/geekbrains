using System;
using System.Linq;

namespace Lesson_4.Homework_3
{
    class Program
    {
        enum Seasons
        {
            Winter, 
            Spring,
            Summer,
            Autumn
        }

        static void Main(string[] args)
        {
            if (GetMonth(out int monthNumber, out string errorMessage))
            {
                Console.WriteLine(GetSeasonName(GetSeason(monthNumber)));
            }
            else
                Console.WriteLine(errorMessage);

            Console.WriteLine("\n Press any key...");
            Console.ReadKey();
        }

        static bool GetMonth(out int monthNumber, out string errorMessage)
        {
            errorMessage = "";
            Console.WriteLine("Введите число от 1 до 12");
            if (int.TryParse(Console.ReadLine(), out monthNumber))
            {
                if (monthNumber >= 1 && monthNumber <= 12)
                    return true;
                else
                {
                    errorMessage = "Ошибка: введите число от 1 до 12";
                    return false;
                }
            }
            else
            {
                errorMessage = "Ошибка: Необходимо ввести число";
                return false;
            }

        }

        // Спасибо Gennady Kurbesov за предложенный вариант!
        // почитал про выражение switch, интересная фишка
        static Seasons GetSeason(int monthNumber)
        {
#pragma warning disable CS8846 // The switch expression does not handle all possible values of its input type (it is not exhaustive).
            Seasons season = monthNumber switch
#pragma warning restore CS8846 // The switch expression does not handle all possible values of its input type (it is not exhaustive).
            {
                _ when new[] { 12, 1, 2 }.Contains(monthNumber) => Seasons.Winter,
                _ when new[] { 3, 4, 5 }.Contains(monthNumber) => Seasons.Spring,
                _ when new[] { 6, 7, 8 }.Contains(monthNumber) => Seasons.Summer,
                _ when new[] { 9, 10, 11 }.Contains(monthNumber) => Seasons.Autumn,
            };
            return season;
        }

        static string GetSeasonName(Seasons season)
        {
#pragma warning disable CS8524 // The switch expression does not handle some values of its input type (it is not exhaustive) involving an unnamed enum value.
            return season switch
#pragma warning restore CS8524 // The switch expression does not handle some values of its input type (it is not exhaustive) involving an unnamed enum value.
            {
                Seasons.Winter => "Зима",
                Seasons.Spring => "Весна",
                Seasons.Summer => "Лето",
                Seasons.Autumn => "Осень"
            };
        }

    }
}
