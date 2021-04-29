using System.Collections.Generic;

namespace Library
{
    public interface ITree
    {
        TreeNode GetRoot();
        void AddItem(int value); // добавить узел
        void RemoveItem(int value); // удалить узел по значению
        TreeNode GetNodeByValue(int value); //получить узел дерева по значению
        Dictionary<TreeNode, Point> PrintTree(); //вывести дерево в консоль
    }
}