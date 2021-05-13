using System;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Generic;

namespace Bubble
{
    class Program
    {
        const int BLOCK_SIZE = 100_000;
        const int BLOCKS = 10;

        const string RANDOM_FILE_NAME = "random.txt";
        const string SORTED_FILE_NAME = "sorted.txt";
        const string SORTED_BLOCK_FILE_NAME_TEMPLATE = "sorted_{0}.txt";
        static void Main(string[] args)
        {

            int[] arr = { 4, 2, 8, 5, 7, 3, 9, 6, 1 };
            Console.WriteLine($"Source array: {string.Join(',', arr)}");
            Block(arr, 3);
            Console.WriteLine($"Block sorted: {string.Join(',', arr)}");

            if (!File.Exists(RANDOM_FILE_NAME))
                File.Delete(RANDOM_FILE_NAME);
            
            GenerateFile(RANDOM_FILE_NAME);

            Console.WriteLine($"Random file: {Path.GetFullPath(RANDOM_FILE_NAME)}");
            ExternalSort(RANDOM_FILE_NAME, SORTED_FILE_NAME, BLOCK_SIZE);
            Console.WriteLine($"Sorted file: {Path.GetFullPath(SORTED_FILE_NAME)}");
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }

        static void ExternalSort(string source, string destination, int blockSize)
        {
            int blockCount = 0;
            List<int> block = null;
            using (StreamReader sr = File.OpenText(source))
            {
                while (true)
                {
                    block = ReadBlock(sr, blockSize);
                    if (block != null)
                    {
                        QuickSort(block);
                        WriteBlock(block, blockCount);
                        blockCount++;
                    }
                    else
                        break;
                }
            }

            string rightBlockPath = "";
            for (int i = 0; i < blockCount - 1; i++)
            {
                string leftBlockPath = string.Format(SORTED_BLOCK_FILE_NAME_TEMPLATE, i);
                rightBlockPath = string.Format(SORTED_BLOCK_FILE_NAME_TEMPLATE, i + 1);
                MergeFiles(leftBlockPath, rightBlockPath);
            }

            if (File.Exists(SORTED_FILE_NAME))
                File.Delete(SORTED_FILE_NAME);
            File.Move(rightBlockPath, SORTED_FILE_NAME);
        }

        static void MergeFiles(string leftBlockPath, string rightBlockPath)
        {
            string tmpFileName = string.Format(SORTED_BLOCK_FILE_NAME_TEMPLATE, "tmp");
            using (StreamReader leftReader = File.OpenText(leftBlockPath),
                rightReader = File.OpenText(rightBlockPath))
            {
                using (StreamWriter sw = new StreamWriter(tmpFileName))
                {
                    string left = leftReader.ReadLine();
                    string right = rightReader.ReadLine();
                    while (left != null && right != null)
                    {
                        if (int.TryParse(left, out int leftValue) && int.TryParse(right, out int rightValue))
                        {
                            if (leftValue < rightValue)
                            {
                                sw.WriteLine(left);
                                left = leftReader.ReadLine();
                            }
                            else
                            {
                                sw.WriteLine(right);
                                right = rightReader.ReadLine();
                            }
                        }
                    }

                    if (left == null)
                    {
                        while (right != null)
                        {
                            sw.WriteLine(right);
                            right = rightReader.ReadLine();
                        }
                    }
                    else if (right == null)
                    {
                        while (left != null)
                        {
                            sw.WriteLine(left);
                            left = leftReader.ReadLine();
                        }
                    }
                }
            }

            File.Delete(leftBlockPath);
            File.Delete(rightBlockPath);
            File.Move(tmpFileName, rightBlockPath);
        }

        static List<int> ReadBlock(StreamReader sr, int blockSize)
        {
            List<int> block = new List<int>(blockSize);
            for (int j = 0; j < blockSize; j++)
            {
                string text = sr.ReadLine();
                if (!string.IsNullOrEmpty(text))
                {
                    if (int.TryParse(text, out int number))
                        block.Add(number);
                }
                else
                    return null;
            }
            return block;
        }

        static void WriteBlock(List<int> block, int blockNumber)
        {
            File.WriteAllLines(string.Format(SORTED_BLOCK_FILE_NAME_TEMPLATE, blockNumber), block.Select(item => item.ToString()).ToArray());
        }

        static void GenerateFile(string fileName)
        {
            using (StreamWriter sw = File.CreateText(fileName))
            {
                for (int i = 0; i < BLOCKS; i++)
                {
                    int[] arr = GetRandomArray(BLOCK_SIZE);
                    for (int j = 0; j < BLOCK_SIZE; j++)
                    {
                        sw.WriteLine(arr[j].ToString());
                    }
                }
            }
        }

        static int[] GetRandomArray(int count)
        {
            Random rnd = new Random();
            int[] array = new int[count];
            for (int i = 0; i < count; i++)
                array[i] = rnd.Next(BLOCK_SIZE * BLOCKS);
            return array;
        }

        static void Bubble(int[] arr)
        {
            int size = arr.Length;

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size - 1; j++)
                {
                    if (arr[j] > arr[j + 1])
                    {
                        int tmp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = tmp;
                    }
                }
            }
        }

        static void QuickSort<T>(T[] arr, int first = -1, int last = -1)
        {
            first = (first == -1 ? 0 : first);
            last = (last == -1 ? (arr.Length - 1) : last);
            T reference = arr[(last + first) / 2];
            int left = first;
            int right = last;
            var comparer = Comparer<T>.Default;
            do
            {

                while (comparer.Compare(arr[left], reference) < 0)
                    left++;

                while (comparer.Compare(arr[right], reference) > 0)
                    right--;

                if (left <= right)
                {

                    if (comparer.Compare(arr[left], arr[right]) > 0)
                    {
                        T tmp = arr[left];
                        arr[left] = arr[right];
                        arr[right] = tmp;
                    }
                    left++;
                    right--;
                }
            } while (left <= right);

            if (first < right)
                QuickSort(arr, first, right);
            if (left < last)
                QuickSort(arr, left, last);
        }

        static void QuickSort<T>(List<T> arr, int first = -1, int last = -1)
        {
            first = (first == -1 ? 0 : first);
            last = (last == -1 ? (arr.Count - 1) : last);
            T reference = arr[(last + first) / 2];
            int left = first;
            int right = last;
            var comparer = Comparer<T>.Default;
            do
            {

                while (comparer.Compare(arr[left], reference) < 0)
                    left++;

                while (comparer.Compare(arr[right], reference) > 0)
                    right--;

                if (left <= right)
                {

                    if (comparer.Compare(arr[left], arr[right]) > 0)
                    {
                        T tmp = arr[left];
                        arr[left] = arr[right];
                        arr[right] = tmp;
                    }
                    left++;
                    right--;
                }
            } while (left <= right);

            if (first < right)
                QuickSort(arr, first, right);
            if (left < last)
                QuickSort(arr, left, last);
        }

        static void Block(int[] arr, int n)
        {
            List<int>[] blocks = new List<int>[n + 1];
            for (int i = 0; i <= n; i++)
                blocks[i] = new List<int>();

            int min = arr[0];
            int max = arr[0];

            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] < min)
                    min = arr[i];

                if (arr[i] > max)
                    max = arr[i];
            }

            for (int i = 0; i < arr.Length; i++)
                blocks[n * (arr[i] - min) / (max - min)].Add(arr[i]);

            int index = 0;
            for (int i = 0; i <= n; i++)
            {
                QuickSort(blocks[i]);
                foreach (var item in blocks[i])
                    arr[index++] = item;
            }
        }
    }
}
