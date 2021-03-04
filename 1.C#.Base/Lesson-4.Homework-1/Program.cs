using System;

namespace Lesson_4.Homework_1
{
    class Program
    {
        static string[] firstNames = { "Иванов", "Петров", "Сидоров" };
        static string[] lastNames = { "Иван", "Петр", "Василий", "Михаил" };
        static string[] patronymics = { "Иванович", "Петрович", "Александрович", "Михайлович" };

        static void Main(string[] args)
        {

            for (int i = 0; i < 10; i++)
            {
                WriteRandomFullName();
            }

            Console.WriteLine("\n Press any key...");
            Console.ReadKey();
        }

        static string GetFullName(string firstName, string lastName, string patronymic)
        {
            return $"{firstName} {lastName} {patronymic}";
        }

        static void WriteRandomFullName()
        {
            Random rnd = new Random();
            int firstNameIdx = rnd.Next(0, firstNames.Length);
            int lastNameIdx = rnd.Next(0, lastNames.Length);
            int patronymicIdx = rnd.Next(0, patronymics.Length);
            Console.WriteLine(GetFullName(firstNames[firstNameIdx], lastNames[lastNameIdx], patronymics[patronymicIdx]));
        }
    }
}
