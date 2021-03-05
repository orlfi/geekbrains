using System;
using System.IO;

namespace Lesson_5.Homework_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите текст для вывода в файл:");
            string text = Console.ReadLine();
            File.WriteAllText("file.txt", text);
        }
    }
}
