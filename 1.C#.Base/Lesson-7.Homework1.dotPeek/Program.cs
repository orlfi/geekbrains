// Decompiled with JetBrains decompiler
// Type: Lesson_7.Homework1.Source.Program
// Assembly: Lesson-7.Homework1.Source, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7C8E5703-6738-4190-8488-08E858925152
// Assembly location: C:\Projects\GeekBrains\1.C#.Base\Lesson-7.Homework1.Source\bin\Debug\netcoreapp3.1\Lesson-7.Homework1.Source.dll

using System;

namespace Lesson_7.Homework1.Source
{
  internal class Program
  {
    private static void Main(string[] args)
    {
      Console.Write("Input number: ");
      int result;
      if (int.TryParse(Console.ReadLine(), out result))
      {
        if (result % 2 == 0)
          Console.WriteLine("the number is even!");
        else
          Console.WriteLine("the number is odd!");

        if (result>0)
          Console.WriteLine("the number is positive!");
        else
          Console.WriteLine("the number is negative!");
      }
      else
        Console.WriteLine("Error: you must enter a number!");
      Console.ReadKey();
    }
  }
}
