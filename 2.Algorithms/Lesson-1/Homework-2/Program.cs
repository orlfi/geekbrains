using System;

namespace Lesson_1.Homework_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        public static int StrangeSum(int[] inputArray)
        {
            int sum = 0;                                            // O(1)

            for (int i = 0; i < inputArray.Length; i++)             // O(inputArray.Length)
            {
                for (int j = 0; j < inputArray.Length; j++)         //// O(inputArray.Length)
                {
                    for (int k = 0; k < inputArray.Length; k++)     ////// O(inputArray.Length)
                    {
                        int y = 0;                                  ////// O(inputArray.Length)

                        if (j != 0)                                 ////// O(inputArray.Length)
                        {
                            y = k / j;                              // O(inputArray.Length)
                        }

                        sum += inputArray[i] + i + k + j + y;       ////// O(inputArray.Length)
                    }
                }
            }

            return sum;                                             // O(1)

            // производительность алгоритма составит:
            // O(2) + O(inputArray.Length*inputArray.Length*4*inputArray.Length) + O(inputArray.Length) =
            // O(4*Pow(inputArray.Length,3)) = O(Pow(inputArray.Length,3))
        }
    }
}
