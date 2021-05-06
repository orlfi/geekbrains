using System;
using System.Collections.Generic;
using static System.Console;

namespace Homework
{
    class Edge
    {
        public int Weight { get; set; }
        public Node From { get; set; }

        public Node To { get; set; }

        public Edge(Node node, Node from, int weight, int x, int y)
        {
            To = node;
            From = from;
            Weight = weight;
            X = x;
            Y = y;
        }

        public int X { get; set; }

        public int Y { get; set; }

        public void PrintGraph(Point position, ConsoleColor color)
        {
            ForegroundColor = color;
            DrawLine(position, From, To);
            ForegroundColor = ConsoleColor.Yellow;
            SetCursorPosition(position.X + X, position.Y + Y);
            Write(Weight);
        }

        private void DrawLine(Point position, Node nodeFrom, Node nodeTo)
        {
            DrawLine(position.X + nodeFrom.X, position.Y + nodeFrom.Y, position.X + nodeTo.X, position.Y + nodeTo.Y);
        }

        private void DrawLine(int x1, int y1, int x2, int y2)
        {
            if (x2 == x1)
            {
                for (int y = Math.Min(y1, y2) + 1; y < Math.Max(y2, y1); y++)
                {
                    SetCursorPosition(x1 + 1, y);
                    Write('·');
                }
            }
            if (y2 == y1)
            {
                for (int x = Math.Min(x1, x2) + 3; x < Math.Max(x2, x1); x++)
                {

                    SetCursorPosition(x, y1);
                    Write('·');
                }
            }
            double a = (double)(y2 - y1) / (double)(x2 - x1);
            double b = y1 - a * x1;
            for (int x = Math.Min(x1, x2) + 3; x < Math.Max(x2, x1); x++)
            {
                int y = Convert.ToInt32(a * x + b);
                SetCursorPosition(x, y);
                Write('·');
            }
        }
        public void PrintMatrix(int fromIndex, int toIndex, Point position, ConsoleColor color)
        {
            ForegroundColor = Weight == 0 ? ConsoleColor.DarkGray : color;
            SetCursorPosition(position.X + fromIndex * 2 + 2, position.Y + toIndex + 1);
            Write($"{Weight}");
            SetCursorPosition(position.X + toIndex * 2 + 2, position.Y + fromIndex + 1);
            Write($"{Weight}");
        }

    }
}
