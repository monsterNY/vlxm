using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using ConsoleTest.Domain;
using ConsoleTest.Domain.StructModel;
using ConsoleTest.DP;
using ConsoleTest.Entity;
using ConsoleTest.Game;
using ConsoleTest.Helper;
using ConsoleTest.LeetCode;
using ConsoleTest.MiddleQuestion;
using ConsoleTest.MiddleQuestionTwo;
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

      LetterCombinations instance = new LetterCombinations();

      Console.WriteLine(instance.Solution("23"));

      Console.WriteLine("Hello World");
      Console.ReadKey(true);
    }

    private static void TestTwoFlatten()
    {
      TwoFlatten instance = new TwoFlatten();

      var root = new Node(1);

      var node = root;

      for (int i = 2; i < 7; i++)
      {
        node.next = i;
        node.next.prev = node;
        node = node.next;
      }

      node = root.next.next;
      node.child = 7;
      node.child.prev = node;
      node = node.child;

      var flag = node;

      for (int i = 8; i < 11; i++)
      {
        node.next = i;
        node.next.prev = node;
        node = node.next;
      }

      node = flag.next;

      node.child = 11;
      node.child.prev = node;
      node = node.child;

      node.next = 12;
      node.next.prev = node;

      instance.Solution(root);
    }

    private static void TetsReorganizeString()
    {
      ReorganizeString instance = new ReorganizeString();

      Console.WriteLine(instance.Solution("vvvlo"));
      Console.WriteLine(instance.Solution("aab"));
      Console.WriteLine(instance.Solution("aaab"));
    }

    private static void TestMonotoneIncreasingDigits()
    {
      MonotoneIncreasingDigits instance = new MonotoneIncreasingDigits();

      Console.WriteLine(instance.Solution(10));
    }

    private static void TestSubarraySum()
    {
      SubarraySum instance = new SubarraySum();

      Console.WriteLine(instance.Solution(new[] {1, 1, 1}, 2));
    }

    private static void TestNumSubarrayBoundedMax()
    {
      NumSubarrayBoundedMax instance = new NumSubarrayBoundedMax();

      //       [73,55,36,5,55,14,9,7,72,52]
      //      32
      //      69

      Console.WriteLine(instance.Solution(new[] {73, 55, 36, 5, 55, 14, 9, 7, 72, 52}, 32, 69));

      Console.WriteLine(instance.Solution(new[] {2, 9, 2, 5, 6}, 2, 8)); //7
    }

    private static void TestPushDominoes()
    {
      PushDominoes instance = new PushDominoes();

      Console.WriteLine(instance.Solution(".L.R...LR..L.."));
    }

    private static void TestNumRescueBoats()
    {
      NumRescueBoats instance = new NumRescueBoats();

      Console.WriteLine(instance.Solution(new[] {3, 1, 7}, 7));

      Console.WriteLine(instance.Solution(
        new[]
        {
          68, 88, 50, 92, 33, 50, 50, 68, 21, 61, 22, 35, 97, 90, 82, 4, 15, 26, 79, 85, 59, 72, 81, 7, 9, 87, 4, 23, 5,
          2, 85, 34, 17, 15, 66, 97, 51, 91, 51, 58, 68, 81, 76, 100, 75, 91, 21, 54, 60, 83
        }, 100));

      instance.Solution(new[] {4, 2}, 5);
    }

    private static void TestLastRemaining()
    {
      LastRemaining instance = new LastRemaining();

      for (int i = 1; i < 100; i++)
      {
        Console.WriteLine($"i:{i},result:{instance.Solution(i)}");
      }
    }

    private static void TestMaxProfit()
    {
      MaxProfit instance = new MaxProfit();

      Console.WriteLine(instance.Solution(new[] {3, 2, 6, 5, 0, 3}));

      Console.WriteLine(instance.Solution(new[] {1, 2, 3, 0, 2})); //3
    }

    private static void TestGuidedMissileSystem(Random rand, CodeTimer timer)
    {
      GuidedMissileSystem instance = new GuidedMissileSystem();

      Console.WriteLine(instance.Solution(new[] {389, 207, 155, 300, 299, 170, 158, 65}));

      Console.ReadKey(true);

      for (int i = 0; i < 1000; i++)
      {
        var len = rand.Next(8) + 3;

        var arr = new int[len];

        for (int j = 0; j < arr.Length; j++)
        {
          arr[j] = rand.Next(500);
        }

        int solution = -1;

        var codeTimerResult = timer.Time(10, () => { solution = instance.Solution(arr); });

        ShowConsole(new Dictionary<string, object>()
        {
          {nameof(arr), JsonConvert.SerializeObject(arr)},
          {nameof(solution), solution},
          {nameof(codeTimerResult), codeTimerResult}
        });
      }
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

      instance.Solution2(new[] {"amazon", "apple", "facebook", "google", "leetcode"}, new[] {"e", "o"});
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