using System;
using Library;

namespace TestBinnaryTree
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] values = { 16, 8, 4, 2, 1, 3, 6, 5, 7, 12, 10, 9, 11, 14, 13, 15, 24, 20, 18, 17, 19, 22, 21, 23, 28, 26, 25, 27, 30, 29, 31 };
            BinnaryTree tree = new BinnaryTree();

            foreach (var item in values)
            {
                tree.AddItem(item);
            }

            int heigth = tree.GetHeight();
            Console.WriteLine($"\nTree heigth: {heigth}");

            var node = tree.GetNodeByValue(28);
            if (node != null)
                Console.WriteLine($"\nFound an element with the value {node.Value}, Parent value: {node.Parent.Value}");

            tree.PrintTree();

            Console.WriteLine("\nRemove node (8), (20), (28)");
            tree.RemoveItem(8);
            tree.RemoveItem(20);
            tree.RemoveItem(28);
            tree.PrintTree();

            Console.WriteLine("\nRemove branch (6)");
            tree.RemoveBranch(6);
            Console.WriteLine();
            tree.PrintTree();
            Console.ReadKey();
        }
    }
}
