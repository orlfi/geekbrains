using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using static System.Console;

namespace Homework
{
    class Graph
    {
        public List<Node> Nodes = new List<Node>();

        public void Initialize()
        {
            AddNode(new Node(0, 60, 5));
            AddNode(new Node(1, 30, 0));
            AddNode(new Node(2, 45, 10));
            AddNode(new Node(3, 60, 15));
            AddNode(new Node(4, 30, 20));
            AddNode(new Node(5, 0, 5));
            AddNode(new Node(6, 0, 15));
            AddNode(new Node(7, 15, 10));
            AddNode(new Node(8, 15, 25));
            AddNode(new Node(9, 45, 25));
            AddEdge(Nodes[0], Nodes[1], 2, 47, 3);
            AddEdge(Nodes[0], Nodes[2], 5, 54, 7);
            AddEdge(Nodes[0], Nodes[3], 3, 61, 10);
            AddEdge(Nodes[1], Nodes[5], 3, 15, 2);
            AddEdge(Nodes[1], Nodes[7], 6, 22, 5);
            AddEdge(Nodes[1], Nodes[2], 3, 38, 5);
            AddEdge(Nodes[2], Nodes[7], 5, 31, 10);
            AddEdge(Nodes[2], Nodes[4], 6, 38, 15);
            AddEdge(Nodes[2], Nodes[3], 4, 53, 13);
            AddEdge(Nodes[3], Nodes[4], 8, 48, 17);
            AddEdge(Nodes[3], Nodes[9], 2, 52, 20);
            AddEdge(Nodes[4], Nodes[7], 4, 23, 15);
            AddEdge(Nodes[4], Nodes[6], 5, 16, 18);
            AddEdge(Nodes[5], Nodes[6], 4, 1, 10);
            AddEdge(Nodes[5], Nodes[7], 8, 8, 8);
            AddEdge(Nodes[6], Nodes[7], 7, 9, 12);
            AddEdge(Nodes[6], Nodes[8], 2, 9, 21);
            AddEdge(Nodes[4], Nodes[9], 4, 37, 22);
            AddEdge(Nodes[8], Nodes[9], 1, 30, 25);
        }

        public Node GetNode(int number)
        {
            return Nodes.SingleOrDefault(item => item.Value == number);
        }

        public int GetIndex(Node node)
        {
            int result = -1;
            for (int i = 0; i < Nodes.Count; i++)
                if (Nodes[i] == node)
                    return i;
            return result;
        }

        public void AddNode(Node node)
        {
            if (!Nodes.Contains(node))
                Nodes.Add(node);
        }

        public void AddEdge(Node from, Node to, int weight, int x = 0, int y = 0)
        {
            from.Edges.Add(new Edge(to, from, weight, x, y));
            to.Edges.Add(new Edge(from, to, weight, x, y));
        }

        public void BreadthFirstSearch(Node root, out List<Node> nodes, out List<Edge> edges)
        {
            bool[] visited = new bool[Nodes.Count];
            nodes = new List<Node>();
            edges = new List<Edge>();
            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(root);
            while (queue.Count != 0)
            {
                var current = queue.Dequeue();
                if (visited[current.Value])
                    continue;
                nodes.Add(current);
                visited[current.Value] = true;
                foreach (var edge in current.Edges)
                {
                    if (!visited[edge.To.Value] && !queue.Contains(edge.To))
                    {
                        queue.Enqueue(edge.To);
                        edges.Add(edge);
                    }
                }
            }
        }

        public List<Node> BreadthFirstSearchMatrix(Node root, int[,] matrix)
        {
            int index = GetIndex(root);
            int size = Nodes.Count;
            bool[] visited = new bool[size];
            List<Node> result = new List<Node>(size);
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(index);
            while (queue.Count != 0)
            {
                index = queue.Dequeue();
                visited[index] = true;
                result.Add(Nodes[index]);
                for (int i = 0; i < size; i++)
                {
                    if (matrix[index, i] != 0 && !visited[i])
                    {
                        visited[i] = true;
                        queue.Enqueue(i);
                    }
                }
            }
            return result;
        }

        public void DeepFirstSearch(Node root, out List<Node> nodes, out List<Edge> edges)
        {
            bool[] visited = new bool[Nodes.Count];
            nodes = new List<Node>();
            edges = new List<Edge>();
            Stack<Node> stack = new Stack<Node>();
            stack.Push(root);
            while (stack.Count != 0)
            {
                var current = stack.Pop();
                if (visited[current.Value])
                    continue;
                nodes.Add(current);
                visited[current.Value] = true;
                foreach (var edge in current.Edges)
                {
                    if (!visited[edge.To.Value] && !stack.Contains(edge.To))
                    {
                        stack.Push(edge.To);
                        edges.Add(edge);
                    }
                }
            }
        }

        public List<Edge> GreedyByColoringNodes()
        {
            int size = Nodes.Count;
            var matrix = GetMatrix();
            int[] a = new int[size];
            List<Edge> edges = new List<Edge>(size);

            //the nodes are painted in different colors
            for (int i = 0; i < size; i++)
                a[i] = i;

            int iMin = 0;
            int jMin = 0;
            for (int k = 0; k < size - 1; k++)
            {
                int min = Int32.MaxValue;
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        if (matrix[i, j] > 0 && a[i] != a[j] && matrix[i, j] < min)
                        {
                            iMin = i;
                            jMin = j;
                            min = matrix[i, j];
                        }
                    }
                }

                edges.Add(Nodes[iMin].Edges.Single(item => item.To == Nodes[jMin]));
                int jM = a[jMin], iM = a[iMin];
                for (int i = 0; i < size; i++)
                {
                    if (a[i] == jM)
                        a[i] = iM;
                }
            }
            return edges;
        }

        public List<Edge> Dijkstra(Node from, Node to)
        {
            int size = Nodes.Count;
            int[] weights = new int[size];
            int current = GetIndex(from);
            Queue<int> queue = new Queue<int>();

            // init array with "infinite" values
            for (int i = 0; i < size; i++)
                weights[i] = Int32.MaxValue;
            weights[current] = 0;

            queue.Enqueue(current);
            var matrix = GetMatrix();

            // calculate weights
            while (queue.Count > 0)
            {
                current = queue.Dequeue();
                for (int i = 0; i < size; i++)
                {
                    if (matrix[current, i] > 0)
                        if (weights[current] + matrix[current, i] < weights[i])
                        {
                            weights[i] = matrix[current, i] + weights[current];
                            queue.Enqueue(i);
                        }
                }
            }

            // get path
            current = GetIndex(to);
            List<Edge> result = new List<Edge>();
            while (weights[current] != 0)
            {
                for (int i = 0; i < size; i++)
                {
                    if (matrix[current, i] > 0)
                        if (weights[current] - matrix[current, i] == weights[i])
                        {
                            result.Add(Nodes[current].Edges.Single(item => item.To == Nodes[i]));
                            current = i;
                        }
                }
            }

            result.Reverse();
            return result;
        }

        public List<Edge> GreedyByVisitedNodes()
        {
            int size = Nodes.Count;
            var matrix = GetMatrix();
            List<Edge> edges = new List<Edge>(size);
            bool[] visited = new bool[size];
            int count = 0;

            int iMin = 0;
            int jMin = 0;
            int min = int.MaxValue;

            // min in matrix
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (matrix[i, j] > 0 && matrix[i, j] < min)
                    {
                        min = matrix[i, j];
                        iMin = i;
                        jMin = j;
                    }
                }
            }
            visited[iMin] = true;
            visited[jMin] = true;
            edges.Add(Nodes[iMin].Edges.Single(item => item.To == Nodes[jMin]));
            count++;

            while (count < Nodes.Count)
            {
                min = Int32.MaxValue;
                for (int i = 0; i < size; i++)
                {
                    if (!visited[i])
                        continue;

                    for (int j = 0; j < size; j++)
                    {
                        // min in visited nodes
                        if (!visited[j] && matrix[i, j] > 0 && matrix[i, j] < min)
                        {
                            min = matrix[i, j];
                            iMin = i;
                            jMin = j;
                        }
                    }
                }

                visited[iMin] = true;
                visited[jMin] = true;
                edges.Add(Nodes[iMin].Edges.Single(item => item.To == Nodes[jMin]));
                count++;
            }
            return edges;
        }

        public int[,] GetMatrix()
        {
            int[,] result = new int[Nodes.Count, Nodes.Count];
            foreach (var node in Nodes)
            {
                foreach (var edge in node.Edges)
                    result[node.Value, edge.To.Value] = edge.Weight;
            }
            return result;
        }

        public void PrintGraph(Point position)
        {
            foreach (var node in Nodes)
            {
                node.PrintGraph(position, ConsoleColor.Blue);
                foreach (var edge in node.Edges)
                {
                    edge.PrintGraph(position, ConsoleColor.DarkGray);
                }
            }
        }

        public void PrintMatrix(Point position)
        {
            for (int i = 0; i < Nodes.Count; i++)
                Nodes[i].PrintMatrix(i, position, ConsoleColor.Blue);

            var matrix = GetMatrix();

            for (int i = 0; i < Nodes.Count; i++)
            {
                for (int j = 0; j < Nodes.Count; j++)
                {
                    ForegroundColor = matrix[i, j] == 0 ? ConsoleColor.DarkGray : ConsoleColor.White;
                    SetCursorPosition(position.X + j * 2 + 2, position.Y + i + 1);
                    Write($"{matrix[i, j]}");
                }
            }
        }
    }
}
