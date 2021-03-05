using System;
using System.IO;

namespace Lesson_5.Homework_2
{
    class Program
    {
        static void Main(string[] args)
        {
            File.AppendAllLines("log.txt", new string[] { $"{DateTime.Now}\t\tСтарт программы." });
        }
    }
}
