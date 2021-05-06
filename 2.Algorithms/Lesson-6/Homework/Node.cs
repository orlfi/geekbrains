using System;
using System.Collections.Generic;
using static System.Console;

namespace Homework
{
    class Node
    {
        public int Value { get; set; }
        public int X { get; set; }
        
        public int Y { get; set; }

        public List<Edge> Edges { get; set; } = new List<Edge>();

        public Node(int value, int x, int y)
        {
            Value = value;
            X = x;
            Y = y;
        }

        public void PrintGraph(Point position, ConsoleColor color)
        {
            ForegroundColor = color;
            SetCursorPosition(position.X + X, position.Y + Y);
            Write($"({Value})");
        }

        public void PrintMatrix(int index,Point position, ConsoleColor color)
        {
            SetCursorPosition(position.X + index * 2 + 2, position.Y);
            ForegroundColor = color;
            Write($"{Value}");

            SetCursorPosition(position.X, position.Y + index + 1);
            Write($"{Value}");
        }
    }
}
