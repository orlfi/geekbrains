using System;

namespace Lesson_1.AdditionalTask
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите пароль: ");
            string pass = Console.ReadLine();

            if (Check(pass))
                Console.WriteLine("верно");
            else
                Console.WriteLine("ошибка");
            
            Console.ReadKey();
        }
        static bool Check(string pass)
        {
            if (pass.Length < 4 || pass.Length > 6)
                return false;

            for(int i = 0; i < pass.Length; i++)
            {
                if ((pass[i] < '0' && pass[i] > '9') || (i > 0 && pass[i] == pass[i - 1]))
                    return false;
            }

            return true;
        }

    }
}
