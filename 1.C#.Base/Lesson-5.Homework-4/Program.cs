using System;
using System.Linq;
using System.IO;

namespace Lesson_5.Homework_4
{
    class Program
    {
        const string FILE_NAME = "dirinfo.txt";
        const string FILE_NAME_RECURSIVE = "dirinfor.txt";

        static void Main(string[] args)
        {
            string path = "";
            do
            {
                Console.Write("Введите путь каталога: ");
                path = Console.ReadLine();
                if (!Directory.Exists(path))
                    Console.WriteLine($"Путь '{path}' не найден.");
            }
            while (!Directory.Exists(path));

            try
            {
                using (StreamWriter sw = new StreamWriter(FILE_NAME_RECURSIVE))
                {
                    WriteDirectoryInfoRecursive(sw, path);
                }
                Console.WriteLine($"Список файлов через рекурсию сохранен в файл {AppDomain.CurrentDomain.BaseDirectory}{FILE_NAME_RECURSIVE}");

                using (StreamWriter sw = new StreamWriter(FILE_NAME))
                {
                    WriteDirectoryInfo(sw, path);
                }
                Console.WriteLine($"Список файлов без рекурсии сохранен в файл {AppDomain.CurrentDomain.BaseDirectory}{FILE_NAME}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void WriteDirectoryInfoRecursive(StreamWriter sw, string path, string parentIndent = "")
        {
            string[] directories = Directory.GetDirectories(path);
            string[] files = Directory.GetFiles(path);

            if (parentIndent == "")
            {
                WriteText(sw, $"[{path}]");
            }

            for (int i = 0; i < directories.Length; i++)
            {
                string currentIndent = "├─ ";
                string childIndent = "│  ";

                if (i == (directories.Length - 1) && files.Length == 0)
                {
                    currentIndent = "└─ ";
                    childIndent = "   ";
                }

                WriteText(sw, GetShortDirectoryName(directories[i], parentIndent + currentIndent));

                WriteDirectoryInfoRecursive(sw, directories[i], parentIndent + childIndent);
            }

            for (int i = 0; i < files.Length; i++)
            {
                string currentIndent = (i == files.Length - 1) ? "└─ " : "├─ ";
                WriteText(sw, $"{parentIndent}{currentIndent}{Path.GetFileName(files[i])}");
            }
        }

        static void WriteText(StreamWriter sw, string text)
        {
            Console.WriteLine(text);
            sw.WriteLine(text);
        }

        static void WriteDirectoryInfo(StreamWriter sw, string root)
        {
            string[] directories = Directory.GetDirectories(root, "*.*", System.IO.SearchOption.AllDirectories).OrderBy(item => item).ToArray<string>();
            foreach (var path in directories)
            {
                int level = path.Substring(root.Length).Count(item => item == '\\') - 1;
                string indent = new string('\t', level);
                sw.WriteLine(GetShortDirectoryName(path, indent));
                WriteFileInfo(sw, path, indent + '\t');
            }
            WriteFileInfo(sw, root);
        }
        
        static string GetShortDirectoryName(string path, string indent)
        {
            return $"{indent}[{path.Substring(path.LastIndexOf("\\") + 1)}]";
        }

        static void WriteFileInfo(StreamWriter sw, string path, string indent = "")
        {
            foreach (var file in Directory.GetFiles(path))
            {
                sw.WriteLine(indent + Path.GetFileName(file));
            }
        }
    }
}
