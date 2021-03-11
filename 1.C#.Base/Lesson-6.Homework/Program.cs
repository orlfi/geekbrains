using System;

namespace Lesson_6.Homework
{
    class Program
    {
        static void Main(string[] args)
        {
            TaskManager.Command(args);
            while (true)
            {
                Console.Write("COMMAND>");
                string input = Console.ReadLine();
                if (input.ToLower() == "q" || input.ToLower() == "quit")
                    return;

                string[] commands = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                TaskManager.Command(commands);
            }
        }
    }
}
