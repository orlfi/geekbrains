using System;
using System.Collections.Generic;
using static System.Console;

namespace Library
{
    public class BinnaryTree : ITree
    {
        private const int HORIZONTAL_SCALE = 3;

        private TreeNode _root;

        public TreeNode GetRoot()
        {
            return _root;
        }

        public void AddItem(int value)
        {
            TreeNode tmp = _root;
            if (tmp == null)
            {
                _root = new TreeNode(null, value, this);
                return;
            }

            if (value == tmp.Value)
                return;

            while (value != tmp.Value)
            {
                if (value < tmp.Value)
                {
                    if (tmp.LeftChild != null)
                    {
                        tmp = tmp.LeftChild;
                        continue;
                    }
                    else
                    {
                        tmp.leftChild = new TreeNode(tmp, value, this);
                        return;
                    }
                }
                else if (value > tmp.Value)
                {
                    if (tmp.RightChild != null)
                    {
                        tmp = tmp.RightChild;
                        continue;
                    }
                    else
                    {
                        tmp.rightChild = new TreeNode(tmp, value, this);
                        return;
                    }
                }
            }
        }

        public void RemoveBranch(int value)
        {
            ref TreeNode tmp = ref _root;

            while (tmp != null)
            {
                if (value == tmp.Value)
                {
                    tmp = null;
                    return;
                }
                else if (value < tmp.Value)
                    tmp = ref tmp.leftChild;
                else
                    tmp = ref tmp.rightChild;
            }
        }

        public void RemoveItem(int value)
        {
            TreeNode current = _root;

            while (current != null && current.Value != value)
            {
                if (value < current.Value)
                    current = current.leftChild;
                else
                    current = current.rightChild;
            }

            if (current == null)
                return;

            TreeNode parent = current.Parent;

            if (current.rightChild == null)
            {
                if (parent.leftChild == current)
                    parent.leftChild = current.leftChild;
                if (parent.rightChild == current)
                    parent.rightChild = current.leftChild;
                return;
            }
            else if (current.leftChild == null)
            {
                if (parent.leftChild == current)
                    parent.leftChild = current.rightChild;
                if (parent.rightChild == current)
                    parent.rightChild = current.rightChild;
                return;
            }

            var replace = current.rightChild;
            while (replace.leftChild != null)
                replace = replace.leftChild;
            current.Value = replace.Value;
            replace.Parent.leftChild = null;
        }

        public TreeNode GetNodeByValue(int value)
        {
            TreeNode current = _root;

            while (current != null && value != current.Value)
            {
                if (value < current.Value)
                    current = current.LeftChild;
                else
                    current = current.RightChild;
            }
            return current;
        }

        public int GetHeight()
        {
            Queue<TreeNode> queue = new Queue<TreeNode>();
            List<TreeNode> items = new List<TreeNode>();
            int result = 0;

            queue.Enqueue(_root);
            while (queue.Count != 0)
            {
                var current = queue.Dequeue();
                items.Add(current);
                var depth = current.depth + 1;
                result = Math.Max(result, depth);

                if (current.LeftChild != null)
                {
                    current.LeftChild.depth = depth;
                    queue.Enqueue(current.LeftChild);
                }

                if (current.RightChild != null)
                {
                    current.RightChild.depth = depth;
                    queue.Enqueue(current.RightChild);
                }
            }

            return result;
        }

        public List<TreeNode> BreadthFirstSearch()
        {
            List<TreeNode> result = new List<TreeNode>();
            Queue<TreeNode> queue = new Queue<TreeNode>();
            queue.Enqueue(_root);
            while(queue.Count !=0)
            {
                var current = queue.Dequeue();
                result.Add(current);

                if (current.LeftChild != null)
                    queue.Enqueue(current.LeftChild);

                if (current.RightChild != null)
                    queue.Enqueue(current.RightChild);
            }
            return result;
        }

        public List<TreeNode> DeepFirstSearch()
        {
            List<TreeNode> result = new List<TreeNode>();
            Stack<TreeNode> stack = new Stack<TreeNode>();
            stack.Push(_root);
            while(stack.Count !=0)
            {
                var current = stack.Pop();
                result.Add(current);

                if (current.RightChild != null)
                    stack.Push(current.RightChild);

                if (current.LeftChild != null)
                    stack.Push(current.LeftChild);
            }
            return result;
        }

        public Dictionary<TreeNode, Point> PrintTree()
        {
            Dictionary<TreeNode, Point> nodeInfo = new Dictionary<TreeNode, Point>();
            PrintBranch(_root, WindowWidth / 2, CursorTop, GetHeight(), 0, nodeInfo);
            return nodeInfo;
        }

        private void PrintBranch(TreeNode node, int x, int y, int height, int level, Dictionary<TreeNode, Point> nodeInfo)
        {
            if (node == null)
                return;

            nodeInfo.Add(node, new Point(x, y + level * 4));
            SetCursorPosition(x, y + level * 4);
            WriteLine($"({node.Value})");

            int itemWidth = (1 << height - 1);

            if (node.LeftChild != null)
            {
                level++;
                int branchX = x - itemWidth / (1 << level) * HORIZONTAL_SCALE;
                int textWidth = node.LeftChild.Value.ToString().Length + 2;

                // If it does not fit, we will print (..)
                if (branchX < 0)
                {
                    SetCursorPosition(x + textWidth / 2, y + (level - 1) * 4 + 1);
                    WriteLine("|");
                    SetCursorPosition(x, y + (level - 1) * 4 + 2);
                    WriteLine("(..)");
                    return;
                }

                SetCursorPosition(x, y + (level - 1) * 4 + 1);
                WriteLine("/");


                int width = x - branchX - 2;
                SetCursorPosition(branchX + 2 - (width < 1 ? (width * -1 + 1) : 0), y + (level - 1) * 4 + 2);
                WriteLine(width > 1 ? "".PadRight(width, '-') : "/");

                SetCursorPosition(branchX + 1 - (width < 1 ? (width * -1 + 1) : 0), y + (level - 1) * 4 + 3);
                WriteLine("/");

                PrintBranch(node.LeftChild, branchX, y, height, level, nodeInfo);
                level--;
            }

            if (node.RightChild != null)
            {
                level++;
                int branchX = x + itemWidth / (1 << level) * HORIZONTAL_SCALE;
                int textWidth = node.RightChild.Value.ToString().Length + 2;

                // If it does not fit, we will print (..)
                if (branchX + textWidth > WindowWidth)
                {
                    SetCursorPosition(x + textWidth / 2, y + (level - 1) * 4 + 1);
                    WriteLine("|");
                    SetCursorPosition(x, y + (level - 1) * 4 + 2);
                    WriteLine("(..)");
                    return;
                }

                SetCursorPosition(x + textWidth - 1, y + (level - 1) * 4 + 1);
                WriteLine("\\");

                SetCursorPosition(x + textWidth, y + (level - 1) * 4 + 2);
                int width = branchX - x - textWidth;
                WriteLine(width > 1 ? "".PadRight(width, '-') : "\\");

                SetCursorPosition(branchX + (width < 1 ? (width * -1 + 1) : 0), y + (level - 1) * 4 + 3);
                WriteLine("\\");

                PrintBranch(node.RightChild, branchX, y, height, level, nodeInfo);
                level--;
            }
        }
    }
}