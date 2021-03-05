using System;
using System.Collections.Generic;
using System.Text;

namespace Lesson_5.Homework_5
{
    public class ToDo
    {
        public string Title { get; set; }
        public bool IsDone { get; set; }

        public ToDo() { }
        public ToDo(string title, bool isDone = false)
        {
            Title = title;
            IsDone = isDone;
        }
    }
}
