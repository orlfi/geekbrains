using System;

namespace Lesson_1.Homework_3
{
    class Program
    {
        public class TestCase
        {
            public int Number { get; set; }
            public int Expected { get; set; }
            public Exception ExpectedException { get; set; }
        }

        static void Test(TestCase testCase)
        {
            try
            {
                int result = FibRec(testCase.Number);
                if (result == testCase.Expected)
                {
                    Console.WriteLine("VALID TEST");
                }
                else
                {
                    Console.WriteLine("NOT VALID TEST");
                }
            }
            catch (Exception ex)
            {
                if (testCase.ExpectedException != null)
                {
                    Console.WriteLine("VALID TEST");
                }
                else
                {
                    Console.WriteLine("NOT VALID TEST");
                }

            }

        }

        static void Main(string[] args)
        {
            Console.WriteLine("Fibonacci recursion tests:");
            TestCase testFirbRec1 = new TestCase()
            {
                Number = 5,
                Expected = 5,
                ExpectedException = null
            };

            TestCase testFirbRec2 = new TestCase()
            {
                Number = 1,
                Expected = 1,
                ExpectedException = null
            };

            TestCase testFirbRec3 = new TestCase()
            {
                Number = -10,
                Expected = 0,
                ExpectedException = new Exception()
            };

            TestCase testFirbRec4 = new TestCase()
            {
                Number = 10,
                Expected = 15,
                ExpectedException = null
            };


            TestCase testFirbRec5 = new TestCase()
            {
                Number = 8,
                Expected = 0,
                ExpectedException = new Exception()
            };

            Test(testFirbRec1);
            Test(testFirbRec2);
            Test(testFirbRec3);
            Test(testFirbRec4);
            Test(testFirbRec5);
            
            Console.WriteLine("\nFibonacci for tests:");

            TestCase testFibFor1 = new TestCase()
            {
                Number = 6,
                Expected = 8,
                ExpectedException = null
            };

            TestCase testFibFor2 = new TestCase()
            {
                Number = 2,
                Expected = 1,
                ExpectedException = null
            };

            TestCase testFibFor3 = new TestCase()
            {
                Number = -7,
                Expected = 0,
                ExpectedException = new Exception()
            };

            TestCase testFibFor4 = new TestCase()
            {
                Number = -10,
                Expected = 0,
                ExpectedException = null
            };
            TestCase testFibFor5 = new TestCase()
            {
                Number = 12,
                Expected = 30,
                ExpectedException = null
            };

            Test(testFibFor1);
            Test(testFibFor2);
            Test(testFibFor3);
            Test(testFibFor4);
            Test(testFibFor5);
        }

        static int FibRec(int n)
        {
            if (n < 0)
                throw new ArgumentException("n должно быть больше 0", "n");

            if (n == 0)
                return 0;

            if (n == 1)
                return 1;

            return FibRec(n - 1) + FibRec(n - 2);
        }

        static int FibFor(int n)
        {
            if (n < 0)
                throw new ArgumentException("n должно быть больше 0", "n");

            if (n == 0)
                return 0;
            else if (n == 1)
                return 1;

            int result = 0;
            int previous1 = 1;
            int previous2 = 0;

            for (int i = 2; i <= (n); i++)
            {
                result = previous1 + previous2;
                previous2 = previous1;
                previous1 = result;
                
            }

            return result;
        }
    }
}
