using System;
using System.Linq;
using static System.Console;
using System.Threading;
using System.Collections.Generic;

namespace Homework
{
    class Program
    {
        static readonly Point MenuPosition = new Point(0, 32);
        static readonly Point GraphPosition = new Point(0, 2);
        static readonly Point MatrixPosition = new Point(75, 8);
        static readonly Point ProgressPosition = new Point(0, 29);
        const string GREEDY_BY_COLORING_NODES = "Greedy algorithm by coloring nodes";
        const string GREEDY_BY_VISITED_NODES = "Greedy algorithm by visited nodes";
        const string BFS = "Breadth First Search (BFS)";
        const string DFS = "Deep First Search (DFS)";
        const string DIJKSTRA = "Dijkstra's algorithm";

        static void Main(string[] args)
        {
            CursorVisible = false;
            //http://graphonline.ru/?graph=izBuHttzjSHpKNUs
            WindowWidth = 120;
            WindowHeight = 50;
            Graph graph = new Graph();
            graph.Initialize();
            graph.PrintGraph(GraphPosition);
            graph.PrintMatrix(MatrixPosition);
            PrintMenu();
            while (true)
            {
                CursorVisible = true;
                string input = ReadLine();
                if (int.TryParse(input, out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            string defaultNode = "0";
                            Write("Input root node number>");
                            Write($"{defaultNode}\b");
                            input = ReadLine();
                            CursorVisible = false;
                            if (int.TryParse(string.IsNullOrEmpty(input)? defaultNode : input, out int root))
                            {
                                var rootNode = graph.GetNode(root);
                                if (rootNode != null)
                                {
                                    graph.BreadthFirstSearch(rootNode, out List<Node> nodes, out List<Edge> edges);
                                    PrintSearch(graph, nodes, edges, BFS);
                                }
                            }
                            break;
                        case 2:
                            Write("Input root node number>");
                            input = ReadLine();
                            CursorVisible = false;
                            if (int.TryParse(input, out root))
                            {
                                var rootNode = graph.GetNode(root);
                                if (rootNode != null)
                                {
                                    graph.DeepFirstSearch(rootNode, out List<Node> nodes, out List<Edge> edges);
                                    PrintSearch(graph, nodes, edges, DFS);
                                }
                            }
                            break;
                        case 3:
                            CursorVisible = false;
                            var greedyEdges = graph.GreedyByColoringNodes();
                            PrintEdges(graph, greedyEdges, GREEDY_BY_COLORING_NODES);
                            break;
                        case 4:
                            CursorVisible = false;
                            greedyEdges = graph.GreedyByVisitedNodes();
                            PrintEdges(graph, greedyEdges, GREEDY_BY_VISITED_NODES);
                            break;
                        case 5:
                            string defaultFrom = "5";
                            string defaultTo = "3";
                            Write("Input source node number>");
                            Write($"{defaultFrom}\b");
                            input = ReadLine();
                            CursorVisible = false;
                            if (int.TryParse(string.IsNullOrEmpty(input) ? defaultFrom : input, out int from))
                            {
                                CursorVisible = true;
                                Write("Input destination node number>");
                                Write($"{defaultTo}\b");
                                input = ReadLine();
                                CursorVisible = false;
                                if (int.TryParse(string.IsNullOrEmpty(input) ? defaultTo : input, out int to))
                                {
                                    var edges = graph.Dijkstra(graph.GetNode(from), graph.GetNode(to));
                                    PrintEdges(graph, edges, DIJKSTRA);
                                }
                            }
                            break;
                        case 6:
                            return;
                        default:
                            break;
                    }
                }
                PrintMenu();
            }
        }

        static void PrintMenu()
        {
            ForegroundColor = ConsoleColor.White;
            SetCursorPosition(MenuPosition.X, MenuPosition.Y);
            WriteLine($"1 - {BFS}");
            WriteLine($"2 - {DFS}");
            WriteLine($"3 - {GREEDY_BY_COLORING_NODES}");
            WriteLine($"4 - {GREEDY_BY_VISITED_NODES}");
            WriteLine($"5 - {DIJKSTRA}");
            WriteLine("6 - Exit");
            WriteLine("Your choice>" + new string(' ', WindowWidth - 12));
            WriteLine(new string(' ', WindowWidth));
            WriteLine(new string(' ', WindowWidth));
            SetCursorPosition(MenuPosition.X + 12, MenuPosition.Y + 6);
        }

        const int SEARCH_SPEED = 300;
        static void PrintSearch(Graph graph, List<Node> nodes, List<Edge> edges, string header)
        {
            Clear(graph);
            PrintHeader(header);
            SetCursorPosition(ProgressPosition.X, ProgressPosition.Y);
            Write("Building tree...");
            for (int i = 0; i < nodes.Count; i++)
            {
                var node = nodes[i];
                node.PrintGraph(GraphPosition, ConsoleColor.Red);
                node.PrintMatrix(graph.GetIndex(node), MatrixPosition, ConsoleColor.Red);

                PrintSearchProgress(i, node);

                Thread.Sleep(SEARCH_SPEED);

                var nodeEdges = edges.Where(item => item.To == node).ToList();
                foreach (var edge in nodeEdges)
                {
                    edge.PrintGraph(GraphPosition, ConsoleColor.Green);
                    edge.PrintMatrix(graph.GetIndex(edge.From), graph.GetIndex(edge.To), MatrixPosition, ConsoleColor.Green);
                    Thread.Sleep(SEARCH_SPEED);
                }
            }
            SetCursorPosition(ProgressPosition.X + 16, ProgressPosition.Y);
            ForegroundColor = ConsoleColor.Green;
            Write("done!");
        }

        const int EDGES_SPEED = 500;
        static void PrintEdges(Graph graph, List<Edge> edges, string header)
        {
            Clear(graph);
            PrintHeader(header);
            SetCursorPosition(ProgressPosition.X, ProgressPosition.Y);
            Write("Building tree...");
            for (int i = 0; i < edges.Count; i++)
            {
                var edge = edges[i];
                edge.PrintGraph(GraphPosition, ConsoleColor.Green);
                edge.PrintMatrix(graph.GetIndex(edge.From), graph.GetIndex(edge.To), MatrixPosition, ConsoleColor.Green);
                edge.To.PrintMatrix(graph.GetIndex(edge.To), MatrixPosition, ConsoleColor.Red);
                edge.To.PrintGraph(GraphPosition, ConsoleColor.Red);
                edge.From.PrintMatrix(graph.GetIndex(edge.From), MatrixPosition, ConsoleColor.Red);
                edge.From.PrintGraph(GraphPosition, ConsoleColor.Red);
                PrintEdgesProgress(i, edge);
                Thread.Sleep(EDGES_SPEED);
            }
            SetCursorPosition(ProgressPosition.X+16, ProgressPosition.Y);
            ForegroundColor = ConsoleColor.Green;
            Write("done!");
        }

        static void PrintSearchProgress(int index, Node node)
        {
            SetCursorPosition(ProgressPosition.X + index * 4, ProgressPosition.Y + 1);
            Write($"({node.Value})");
        }

        static void PrintEdgesProgress(int index, Edge edge)
        {
            SetCursorPosition(ProgressPosition.X + index * 11, ProgressPosition.Y + 1);
            Write($"({edge.From.Value})-{edge.Weight}-({edge.To.Value})");
        }

        static void PrintHeader(string header)
        {
            SetCursorPosition(WindowWidth / 2 - header.Length / 2, 0);
            ForegroundColor = ConsoleColor.Cyan;
            Write(header);
        }

        static void Clear(Graph graph)
        {
            string clearRow = new string(' ', WindowWidth);

            SetCursorPosition(0, 0);
            Write(clearRow);

            SetCursorPosition(ProgressPosition.X, ProgressPosition.Y);
            Write(clearRow);

            SetCursorPosition(ProgressPosition.X, ProgressPosition.Y+1);
            Write(clearRow);

            graph.PrintGraph(GraphPosition);
            graph.PrintMatrix(MatrixPosition);
        }
    }
}
