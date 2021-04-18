using System;
using System.Collections.Generic;
using System.Text;

namespace Lesson_2.Library
{
    public class Node
    {
        public int Value { get; set; }
        public Node NextNode { get; set; }
        public Node PrevNode { get; set; }

        public Node() { }

        public Node(int value) => Value = value;
        
        internal Node(BidirectionalList list,  int value): this(value)
        {
            this.list = list;
        }
        /// <summary>
        /// Для исключения доступа к данному полю, необходимо Node и LinkedList вынести в отдельную библиотеку
        /// </summary>
        internal BidirectionalList list;
    }
}
