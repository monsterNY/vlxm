using System;
using Skills.Tools;

namespace Skills
{
  class Program
  {
    static void Main(string[] args)
    {

//      Console.BackgroundColor = ConsoleColor.Blue;

//      Console.ForegroundColor = ConsoleColor.Black;

      var stringBuilder = ConsoleTools.Ask("what are you doing?");

      Console.WriteLine(stringBuilder);

//      Console.WriteLine(Console.ForegroundColor);
//
//      var str = Console.ReadLine();
//
//      Console.WriteLine(str);

      Console.WriteLine("Hello World!");

      Console.ReadKey(true);
    }
  }
}