using System;
using System.Linq;
using System.Diagnostics;

namespace Lesson_6.Homework
{
    public class TaskManager
    {
        private static int NAME_LENGTH = 35;
        private static int ID_LENGTH = 6;
        private static int SESSION_ID_LENGTH = 7;
        private static int THREADS_LENGTH = 7;
        private static int SIZE_LENGTH = long.MaxValue.ToString().Length-3;
        private static char PLACEHOLDER = '=';
        private static int HELP_DESCRIPTION_MARGIN = 15;

        public static void Command(params string[] commands)
        {
            if (commands.Length == 0)
            {
               Console.WriteLine("Error: You must specify the command!"); 
               Help();
               return;
            }

            switch(commands[0].ToLower())
            {
                case "list":
                    List(commands.Length>1?commands[1]:"");
                    break;
                case "kill":
                    if (commands.Length>1)
                    {
                    if (int.TryParse(commands[1], out int id))
                        KillById(id);
                    else
                        KillByName(commands[1]);
                    }
                    else
                    {
                        Console.WriteLine("Error: To terminate tasks you must specify an ID or a name"); 
                        Help();
                    }
                    break;
                case "help":
                    Help();
                    break;
            }
        }

        private static void Help()
        {
            Console.WriteLine();
            Console.WriteLine("TASKMANAGER [list [filter]] [kill id] [kill name] [help]\r\n");
            Console.WriteLine("Description:");
            Console.WriteLine("\tThis tool displays a list of running processes on local machine and");
            Console.WriteLine("\tis used to terminate tasks by ID or name\r\n");
            Console.WriteLine("Parameters list:");
            Console.WriteLine("\t{0}{1}","list".PadRight(HELP_DESCRIPTION_MARGIN), "list of running processes");
            Console.WriteLine("\t{0}{1}", "list filter".PadRight(HELP_DESCRIPTION_MARGIN), "list of running processes filtered by name");
            Console.WriteLine("\t{0}{1}","kill id".PadRight(HELP_DESCRIPTION_MARGIN), "terminate tasks by ID");
            Console.WriteLine("\t{0}{1}","kill name".PadRight(HELP_DESCRIPTION_MARGIN), "terminate tasks by name");
            Console.WriteLine("\t{0}{1}","help".PadRight(HELP_DESCRIPTION_MARGIN), "displays help");
            Console.WriteLine("\t{0}{1}","q or quit".PadRight(HELP_DESCRIPTION_MARGIN), "exit the program");
            Console.WriteLine();
        }

        private static void List(string filter)
        {
            Console.WriteLine();
            PrintHeader();
            var processes = Process.GetProcesses();
            if (!string.IsNullOrEmpty(filter))
                processes = processes.Where(item => item.ProcessName.Contains(filter)).ToArray();

            foreach(var process in  processes)
                Console.WriteLine(GetFormatedString(process.ProcessName, process.Id.ToString(), process.SessionId.ToString(), process.Threads.Count.ToString(), (process.WorkingSet64/1024).ToString("N0")+"K"));
            
            Console.WriteLine();
        }
        private static void PrintHeader()
        {
            Console.WriteLine(GetFormatedString("Name", "PID", "Session", "Threads", "Mem Usage"));
            Console.WriteLine(GetFormatedString(
                new string(PLACEHOLDER, NAME_LENGTH), 
                new string(PLACEHOLDER, ID_LENGTH), 
                new string(PLACEHOLDER, SESSION_ID_LENGTH), 
                new string(PLACEHOLDER, THREADS_LENGTH), 
                new string(PLACEHOLDER, SIZE_LENGTH))
            );
        }

        private static string GetFormatedString(string s1, string s2, string s3, string s4, string s5)
        {
            return string.Format("{0} {1} {2} {3} {4}", s1.PadRight(NAME_LENGTH), s2.PadLeft(ID_LENGTH), s3.PadLeft(SESSION_ID_LENGTH), s4.PadLeft(THREADS_LENGTH), s5.PadLeft(SIZE_LENGTH));
        }

        private static void KillById(int id)
        {
            var process = Process.GetProcessById(id);
                try
                {
                    process.Kill();
                    Console.WriteLine($"The Process ID {id} was killed.\r\n");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
        }

        private static void KillByName(string name)
        {
            var processes = Process.GetProcessesByName(name);
            foreach (var process in processes)
            {
                try
                {
                    process.Kill();
                    Console.WriteLine($"The processes '{name}' were killed.\r\n");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

    }
}