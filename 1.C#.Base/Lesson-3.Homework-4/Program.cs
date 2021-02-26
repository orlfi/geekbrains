using System;

namespace Lesson_3.Homework_4
{
    /// <summary>
    /// Вывод карты расположения короблей "Морской бой"
    /// </summary>
    class Program
    {
        const string COL_NAMES = "   А Б В Г Д Е Ж З И К";
        const int BOARD_SIZE = 10;
        const char SHIP = 'X';
        const char EMPTY = 'O';
        //        const string EMPTY_CHAR = "\u2591\u2591";

        enum Directions
        {
            Horizontal,
            Vertical
        }

        static void Main(string[] args)
        {
            char[,] board = new char[BOARD_SIZE, BOARD_SIZE];
            int[] shipSizes = { 4, 3, 3, 2, 2, 2, 1, 1, 1, 1 };

            for (int i = 0; i < 4; i++)
            {
                FillBoard(board);
                PlaceShips(board, shipSizes);
                DrawBoard(board);
                Console.WriteLine();
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\r\nPress any key...");
            Console.ReadKey();
        }

        static void FillBoard(char[,] board)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    board[i, j] = EMPTY;
                }
            }

        }
        static void PlaceShips(char[,] board, int[] shipSizes)
        {
            for (int i = 0; i < shipSizes.Length; i++)
                PlaceShip(board, shipSizes[i]);
        }

        static void PlaceShip(char[,] board, int shipSize)
        {
            var rnd = new Random();
            int x;
            int y;
            int dir;
            do
            {
                dir = rnd.Next(0, 2);
                x = rnd.Next(0, 10);
                y = rnd.Next(0, 10);
            } while (!Check(x, y, (Directions)dir, shipSize, board));

            for (int i = 0; i < shipSize; i++)
            {
                if ((Directions)dir == Directions.Horizontal)
                {
                    board[x + i, y] = SHIP;
                }
                else
                {
                    board[x, y + i] = SHIP;
                }

            }
        }

        static bool Check(int x, int y, Directions dir, int shipSize, char[,] board)
        {
            int checkX;
            int checkY;

            if ((dir == Directions.Horizontal && (x + shipSize) > BOARD_SIZE) || (dir == Directions.Vertical && (y + shipSize) > BOARD_SIZE))
                return false;

            for (int i = 0; i < shipSize + 2; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (dir == Directions.Horizontal)
                    {
                        checkX = x + i - 1;
                        checkY = y + j - 1;
                    }
                    else
                    {
                        checkX = x + j - 1;
                        checkY = y + i - 1;
                    }

                    if (checkX < 0 || checkX >= BOARD_SIZE || checkY < 0 || checkY >= BOARD_SIZE)
                        continue;

                    if (board[checkX, checkY] == SHIP)
                        return false;
                }
            }
            return true;
        }


        static void DrawBoard(char[,] board)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(COL_NAMES);
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (j == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write($"{i.ToString().PadLeft(2)} ");
                    }

                    if (board[i, j] == SHIP)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }
                    Console.Write("\u2588\u2588");
                }
                Console.WriteLine();
            }
        }
    }
}
