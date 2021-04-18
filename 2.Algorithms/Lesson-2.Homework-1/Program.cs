using System;
using Lesson_2.Library;

namespace Lesson_2.Homework_1
{
    class Program
    {
        static void Main(string[] args)
        {
            BidirectionalList list = new BidirectionalList();
            Console.WriteLine($"Print empty list:");
            Console.WriteLine(list.ToString());

            Console.WriteLine($"Add 2 nodes:");
            list.AddNode(10);
            list.AddNode(20);
            Console.WriteLine(list.ToString());

            Console.WriteLine($"Remove node idx=1:");
            list.RemoveNode(1);
            Console.WriteLine(list.ToString());

            Console.WriteLine($"Remove node idx=0:");
            list.RemoveNode(0);
            Console.WriteLine(list.ToString());

            Console.WriteLine($"Remove node idx=0:");
            list.RemoveNode(0);
            Console.WriteLine(list.ToString());

            Console.WriteLine($"Add 6 nodes:");
            list.AddNode(1);
            list.AddNode(2);
            list.AddNode(3);
            list.AddNode(4);
            list.AddNode(5);
            list.AddNodeAfter(null, 6);
            Console.WriteLine(list.ToString());

            Console.WriteLine($"Find wrong value:");
            var node = list.FindNode(103);
            Console.WriteLine(node == null? "not found": node.Value.ToString());
            Console.WriteLine();

            Console.WriteLine($"Add value 30 after value 3:");
            node = list.FindNode(3);
            list.AddNodeAfter(node, 30);
            Console.WriteLine(list.ToString());

            Console.WriteLine($"Add value 50 after value 5:");
            node = list.FindNode(5);
            list.AddNodeAfter(node, 50);
            Console.WriteLine(list.ToString());

            Console.WriteLine($"Remove index 5:");
            list.RemoveNode(5);
            Console.WriteLine(list.ToString());

            Console.WriteLine($"Remove first node:");
            list.RemoveNode(list.First);
            Console.WriteLine(list.ToString());

            Console.WriteLine($"Remove last node:");
            list.RemoveNode(list.Last);
            Console.WriteLine(list.ToString());

            Console.WriteLine($"Attempt to delete a Node that does not belong to the current list:");
            try
            {
                node = new Node(2);
                list.RemoveNode(node);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }
    }
}
