using System;

namespace HomeWork1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Имя пользователя: ");
            string userName = Console.ReadLine();
            Console.WriteLine($"Привет, {userName}, сегодня {DateTime.Today.ToString("dd MMMM yyyy")}");
            //Console.WriteLine(String.Format("Привет, {0}, сегодня {1}", userName, DateTime.Today.ToString("dd MMMM yyyy")));
        }
    }
}
