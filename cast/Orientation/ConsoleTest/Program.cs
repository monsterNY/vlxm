using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using ConsoleTest.Domain.StructModel;
using ConsoleTest.Entity;
using ConsoleTest.Game;
using ConsoleTest.Helper;
using ConsoleTest.LeetCode;
using ConsoleTest.MiddleQuestion;
using ConsoleTest.WeekTest;
using Newtonsoft.Json;
using Tools.RefTools;

namespace ConsoleTest
{
  class Program
  {
    #region Command

    public static void ShowConsole(Dictionary<string, object> dictionary)
    {
      Console.WriteLine($"\n-----------------S---------------------");

      foreach (var item in dictionary)
      {
        Console.WriteLine($"{item.Key}:{item.Value}");
      }

      Console.WriteLine($"-----------------E---------------------\n");
    }

    #endregion

    static void Main(string[] args)
    {
      var rand = new Random();
      CodeTimer timer = new CodeTimer();
      timer.Initialize();

      


      Console.WriteLine("Hello World");
      Console.ReadKey(true);
    }

    private static void TestDecodeString()
    {

      //      s = "3[a]2[bc]", return "aaabcbc".
      //        s = "3[a2[c]]", return "accaccacc".
      //        s = "2[abc]3[cd]ef", return "abcabccdcdcdef".


      DecodeString instance = new DecodeString();

      Console.WriteLine(instance.Solution("sd2[f2[e]g]i"));

      Console.WriteLine(instance.Solution("3[a]2[bc]"));
      Console.WriteLine(instance.Solution("3[a2[c]]"));
      Console.WriteLine(instance.Solution("2[abc]3[cd]ef"));
    }

    private static void TestWordSubsets()
    {
      WordSubsets instance = new WordSubsets();

      instance.Solution2(new[] { "amazon", "apple", "facebook", "google", "leetcode" }, new[] { "e", "o" });
    }

    private static void TestFindDuplicateSubtrees()
    {
      FindDuplicateSubtrees instance = new FindDuplicateSubtrees();

      instance.Imitation(new TreeNode(1, new TreeNode(2, 4, null), new TreeNode(3, new TreeNode(2, 4, null), 4)));
    }

    private static void TestFindDiagonalOrder()
    {
      FindDiagonalOrder instance = new FindDiagonalOrder();

      instance.Optimize(new[]
      {
        new[] {1, 2, 3},
        new[] {4, 5, 6},
        new[] {7, 8, 9}
      });
      instance.Optimize(new[] {new[] {2, 3}});

      instance.Solution(new[] {new[] {2, 3}});
    }

    private static void TestFindLength()
    {
      FindLength instance = new FindLength();

      Console.WriteLine(instance.OtherSolution(new[] {1, 0, 0, 0, 1}, new[] {1, 0, 0, 1, 1})); //3

      Console.WriteLine(instance.OtherSolution(new[] {1, 2, 3, 2, 1}, new[] {3, 2, 1, 4, 71})); //3

      Console.WriteLine(instance.Solution(new[] {1, 0, 0, 0, 1}, new[] {1, 0, 0, 1, 1})); //3

      Console.WriteLine(instance.Solution(new[] {0, 0, 0, 0, 1}, new[] {1, 0, 0, 0, 0})); //4
      Console.WriteLine(instance.Solution(new[] {1, 2, 3, 2, 1}, new[] {3, 2, 1, 4, 71})); //3
    }
  }
}