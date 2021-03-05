using System;
using System.Linq;
using System.IO;
using System.Text.Json;

namespace Lesson_5.Homework_5
{
    class Program
    {
        const string FILE_NAME = "tasks.json";
        static void Main(string[] args)
        {

            ToDo[] tasks;
            if (!File.Exists(FILE_NAME))
                tasks = FillTasks();
            else
                tasks = JsonSerializer.Deserialize<ToDo[]>(File.ReadAllText(FILE_NAME));

            PrintTasks(tasks);
            bool change = false;
            while(true)
            {
                if (tasks.Count(item => item.IsDone) == tasks.Length)
                {
                    Console.Write("Поздравляю! Все задачи выполнены.");
                    break;
                }


                Console.Write("Введите номер выполненной задачи (q для выхода): ");
                string text = Console.ReadLine();
                if (text == "q")
                    break;

                if (int.TryParse(text, out int number) && number > 0 && number <= tasks.Length)
                {
                    if (SetDone(tasks, number, out string error))
                    {
                        PrintTasks(tasks);
                        change = true;
                    }
                    else
                    {
                        Console.WriteLine(error);
                    }
                }
                else
                {
                    Console.WriteLine($"Необходимо ввести число от 1 до {tasks.Length}");
                }
            }

            if (change)
            {
                string json = JsonSerializer.Serialize<ToDo[]>(tasks);
                File.WriteAllText(FILE_NAME, json);
            }
        }

        static bool SetDone(ToDo[] tasks, int index, out string error)
        {
            error = "";
            if (tasks[index-1].IsDone)
            {
                error = $"Задача {index} уже выполнена";
                return false;
            }

            tasks[index - 1].IsDone = true;
            return true;
        }

        static ToDo[] FillTasks()
        {
            return new ToDo[] { 
                new ToDo("Создать ветку GIT с домашним заданием"), 
                new ToDo("Выполнить ДЗ"), 
                new ToDo("Создать PULL REQUEST"), 
                new ToDo("Отправить ДЗ на проверку"), 
                new ToDo("Закоммитить ДЗ в ветку main") 
            };
        }
        static void PrintTasks(ToDo[] tasks)
        {
            for (int i = 0; i < tasks.Length; i++)
            {
                Console.WriteLine((tasks[i].IsDone ? "[x] " : "    ") + $"{i+1}.{tasks[i].Title}");
            }
        }


    }
}
