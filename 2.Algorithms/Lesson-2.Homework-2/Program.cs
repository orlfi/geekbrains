using System;

namespace Lesson_2.Homework_2
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = 1_000_000;
            int[] arr = new int[n];
            Random rnd = new Random();
            for (int i = 0; i < arr.Length; i++)
                arr[i] = rnd.Next(1, n);
            
            Array.Sort(arr);

            int maxIterations = 0;

            for (int i = 0; i < 20; i++)
            {
                int searchValue = rnd.Next(1, n);
                (int index, int iterations) result = BinarySearch(arr, searchValue);
                maxIterations = Math.Max(maxIterations, result.iterations);
                if (result.index == -1)
                    Console.WriteLine($"({i}) значение {searchValue} не найдено итераций {result.iterations}");
                else
                    Console.WriteLine($"({i}) значение {searchValue} найдено под индексом {result.index} итераций {result.iterations}");
            }

            Console.WriteLine($"Максимальное количество интераций {maxIterations} для {n} элементов. Асимптотическая сложность O(log{Math.Round(Math.Pow(n,1.0/maxIterations),1, MidpointRounding.AwayFromZero)}({n}))");
        }

        static (int index, int iterations) BinarySearch(int[] arr, int searchValue)
        {
            int counter = 0;

            int min = 0;
            int max = arr.Length-1;

            while (min <= max)
            {
                int mid = (max+min)/2;
                if (arr[mid] == searchValue)
                    return (mid, counter); ;

                if (arr[mid] > searchValue)
                        max = mid-1;
                else if (arr[mid] < searchValue)
                    min = mid+1;
                counter++;
            }
            return (-1, counter);
        }


    }
}
