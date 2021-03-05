using System;
using System.IO;

namespace Lesson_5.Homework_3
{
    class Program
    {
        const int COUNT = 5;
        const string FILE_NAME = "numbers.bin";
        static void Main(string[] args)
        {
            File.WriteAllBytes(FILE_NAME, InputBytes(COUNT));
            PrintBytes(File.ReadAllBytes(FILE_NAME));
        }

        static byte[] InputBytes(int count)
        {
            byte[] result = new byte[count];
            int cnt = 0;
            while (cnt < count)
            {
                Console.Write($"Введите число от 0 до 255. Число N{cnt + 1} из {COUNT}: ");
                if (!byte.TryParse(Console.ReadLine(), out result[cnt]))
                {
                    Console.WriteLine($"Ошибка: необходимо ввести число от 0 до 255.");
                    continue;
                }
                cnt++;
            }
            return result;
        }

        static void PrintBytes(byte[] bytes)
        {
            foreach (var item in bytes)
                Console.WriteLine(item);
        }
    }
}
