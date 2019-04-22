using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;
using ConsoleTest.Domain;
using ConsoleTest.Domain.StructModel;
using ConsoleTest.DP;
using ConsoleTest.MiddleQuestion;
using ConsoleTest.MiddleQuestionThree;
using ConsoleTest.MiddleQuestionTwo;
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

    public static void ShowConsole(Action action)
    {
      Console.WriteLine($"\n-----------------S---------------------");

      action.Invoke();

      Console.WriteLine($"-----------------E---------------------\n");
    }

    #endregion

    static void Main(string[] args)
    {
      var rand = new Random();
      CodeTimer timer = new CodeTimer();
      timer.Initialize();

      KSmallestPairs instance = new KSmallestPairs();

      Console.WriteLine(instance.Solution(new[] {1, 2, 4, 5, 6}, new[] {3, 5, 7, 9}, 20));

      Console.WriteLine(instance.Solution(new[] {1, 2, 4, 5, 6}, new[] {3, 5, 7, 9}, 3));

      Console.WriteLine(instance.Solution(new[] {1, 1, 2}, new[] {1, 2, 3}, 10));

      Console.WriteLine("Hello World");
      Console.ReadKey(true);
    }

    private static void TestLongestMountain()
    {
      LongestMountain instance = new LongestMountain();

      Console.WriteLine(instance.Solution(new[] {0, 0, 1, 0, 0, 1, 1, 1, 1, 1}));

      Console.WriteLine(instance.Solution(new[] {0, 1, 2, 3, 4, 5, 4, 3, 2, 1, 0}));

      Console.WriteLine(instance.Solution(new[] {2, 1, 4, 7, 3, 2, 5}));
    }

    private static void TestCanReorderDoubled()
    {
      CanReorderDoubled instance = new CanReorderDoubled();

      Console.WriteLine(instance.Solution3(new[] {0, 0}));

      Console.WriteLine(instance.Solution(new[] {-6, 2, -6, 4, -3, 8, 3, 2, -2, 6, 1, -3, -4, -4, -8, 4}));

      Console.WriteLine(instance.Solution(new[] {4, -2, 2, -4}));
    }

    private static void TestFindCheapestPrice()
    {
      FindCheapestPrice instance = new FindCheapestPrice();

      Console.WriteLine(instance.Solution(10, new[]
      {
        new[] {3, 4, 4},
        new[] {2, 5, 6},
        new[] {4, 7, 10},
        new[] {9, 6, 5},
        new[] {7, 4, 4},
        new[] {6, 2, 10},
        new[] {6, 8, 6},
        new[] {7, 9, 4},
        new[] {1, 5, 4},
        new[] {1, 0, 4},
        new[] {9, 7, 3},
        new[] {7, 0, 5},
        new[] {6, 5, 8},
        new[] {1, 7, 6},
        new[] {4, 0, 9},
        new[] {5, 9, 1},
        new[] {8, 7, 3},
        new[] {1, 2, 6},
        new[] {4, 1, 5},
        new[] {5, 2, 4},
        new[] {1, 9, 1},
        new[] {7, 8, 10},
        new[] {0, 4, 2},
        new[] {7, 2, 8},
      }, 6, 0, 1));

      Console.WriteLine(instance.Solution(3, new[]
      {
        new[] {0, 1, 100},
        new[] {1, 2, 100},
        new[] {0, 2, 500},
      }, 0, 2, 1));

      Console.WriteLine(instance.Solution(3, new[]
      {
        new[] {0, 1, 100},
        new[] {1, 2, 100},
        new[] {0, 2, 500},
      }, 0, 2, 0));
    }

    private static void TestReverseBetween()
    {
      ReverseBetween instance = new ReverseBetween();

      Console.WriteLine(instance.Solution(new[] {1, 2, 3, 4, 5}, 1, 4));
      Console.WriteLine(instance.Solution(new[] {1, 2, 3, 4}, 1, 4));
    }

    private static void TestMinSubArrayLen()
    {
      MinSubArrayLen instance = new MinSubArrayLen();

      Console.WriteLine(instance.Solution(7, new[] {2, 3, 1, 2, 4, 3}));
    }

    private static void TestLargestDivisibleSubset()
    {
      LargestDivisibleSubset instance = new LargestDivisibleSubset();

      Console.WriteLine(instance.Solution(new[] {1, 2, 3,}));
    }

    private static void TestMaxRotateFunction()
    {
      MaxRotateFunction instance = new MaxRotateFunction();


      //      Console.WriteLine(instance.Solution(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }));
      //      Console.WriteLine(instance.Solution(new []{ 4, 3, 2, 6 }));

      var list = new List<int>();

      for (int i = 1; i < 11; i++)
      {
        list.Add(i);

        ShowConsole(() =>
        {
          for (int j = 0; j < list.Count; j++)
          {
            Console.WriteLine(JsonConvert.SerializeObject(list));

            list.Insert(0, list[list.Count - 1]);
            list.RemoveAt(list.Count - 1);
          }
        });
      }
    }

    private static void TestNumSubarrayProductLessThanK()
    {
      NumSubarrayProductLessThanK instance = new NumSubarrayProductLessThanK();

      Console.WriteLine(instance.Solution(new[] {10, 5, 2, 6}, 100));
    }

    private static void TestNumsSameConsecDiff()
    {
      NumsSameConsecDiff instnace = new NumsSameConsecDiff();

      Console.WriteLine(instnace.Solution(2, 1));
      Console.WriteLine(instnace.Solution(3, 7));
    }

    private static void TestInsertionSortList()
    {
      InsertionSortList instance = new InsertionSortList();

      Console.WriteLine(instance.Solution(new[] {3, 2, 4}));
      Console.WriteLine(instance.Solution(new[] {4, 2, 1, 3}));
    }

    private static void TestPacificAtlantic()
    {
      PacificAtlantic instance = new PacificAtlantic();

      //      Console.WriteLine(instance.Solution(new []
      //      {
      //        new []{1,1},
      //        new []{1,1}
      //      }));


      //      Console.WriteLine(instance.Solution(new[]
      //      {
      //        new[] {1, 2, 3, 4, 5},
      //        new[] {16, 17, 18, 19, 6},
      //        new[] {15, 24, 25, 20, 7},
      //        new[] {14, 23, 22, 21, 8},
      //        new[] {13, 12, 11, 10, 9},
      //      }));

      //      Console.WriteLine(instance.Solution(new[]
      //      {
      //        new[] {3,3,3},
      //        new[] {3,1,3},
      //        new[] {0,2,4},
      //      }));

      //      Console.WriteLine(instance.Solution(new[]
      //      {
      //        new[] {1, 2, 3, 4},
      //        new[] {12, 13, 14, 5},
      //        new[] {11, 16, 15, 6},
      //        new[] {10, 9, 8, 7},
      //      }));

      //      Console.WriteLine(instance.Solution(new[]
      //      {
      //        new[] {10, 10, 10},
      //        new[] {10, 1, 10},
      //        new[] {10, 10, 10},
      //      }));

      Console.WriteLine(instance.Solution(new[]
      {
        new[] {1, 2, 2, 3, 5},
        new[] {3, 2, 3, 4, 4},
        new[] {2, 4, 5, 3, 1},
        new[] {6, 7, 1, 4, 5},
        new[] {5, 1, 1, 2, 4}
      }));
    }

    private static void TestNumSubarraysWithSum()
    {
      NumSubarraysWithSum instance = new NumSubarraysWithSum();

      Console.WriteLine(instance.Solution(new[] {1, 0, 1, 0, 1}, 2));
    }

    private static void TestAsteroidCollision()
    {
      AsteroidCollision instance = new AsteroidCollision();
      Console.WriteLine(instance.Solution3(new[] {-2, 1, -2, -2}));
      Console.WriteLine(instance.Solution3(new[] {5, 10, -5}));
    }

    private static void TestMinimumTotal()
    {
      MinimumTotal instance = new MinimumTotal();

      Console.WriteLine(instance.Solution(new List<IList<int>>()
      {
        new List<int>() {2},
        new List<int>() {3, 4},
        new List<int>() {6, 5, 7},
        new List<int>() {4, 1, 8, 3},
      })); //-1

      Console.WriteLine(instance.Solution(new List<IList<int>>()
      {
        new List<int>() {-1},
        new List<int>() {2, 3},
        new List<int>() {1, -1, -3},
      })); //-1
    }

    private static void TestGetHint()
    {
      GetHint instance = new GetHint();

      Console.WriteLine(instance.Solution("1122", "1222"));
    }

    private static void TestCarFleet()
    {
      CarFleet instance = new CarFleet();

      Console.WriteLine(instance.Solution(10, new[] {6, 8}, new[] {3, 2})); //2
      Console.WriteLine(instance.Solution(12, new[] {10, 8, 0, 5, 3}, new[] {2, 4, 1, 1, 3})); //3
    }

    private static void TestEquationsPossible()
    {
      EquationsPossible instance = new EquationsPossible();

      Console.WriteLine(instance.Solution(new[] {"b!=f", "c!=e", "f==f", "d==f", "b==f", "a==f"}));

      Console.WriteLine(instance.Solution(new[] {"a==b", "e==c", "b==c", "a!=e"})); //false

      Console.WriteLine(instance.Solution(new[] {"b==b", "b==e", "e==c", "d!=e"})); //true

      Console.WriteLine(instance.Solution(new[] {"a==b", "b==c", "a==c"})); //true
    }

    private static void TestBrokenCalc()
    {
      BrokenCalc instance = new BrokenCalc();

      Console.WriteLine(instance.OtherSolution(1, 1000000000));
      Console.WriteLine(instance.Solution(1, 119));
      Console.WriteLine(instance.Solution(2, 3));

      var num = 1;

      var num2 = 1000000000;
    }

    private static void TestAccountsMerge()
    {
      AccountsMerge instance = new AccountsMerge();

      instance.Solution(new List<IList<string>>()
      {
        new List<string>() {"Alex", "Alex5@m.co", "Alex4@m.co", "Alex0@m.co"},
        new List<string>() {"Ethan", "Ethan3@m.co", "Ethan3@m.co", "Ethan0@m.co"},
        new List<string>() {"Kevin", "Kevin4@m.co", "Kevin2@m.co", "Kevin2@m.co"},
        new List<string>() {"Gabe", "Gabe0@m.co", "Gabe3@m.co", "Gabe2@m.co"},
        new List<string>() {"Gabe", "Gabe3@m.co", "Gabe4@m.co", "Gabe2@m.co"},
      });
    }

    private static void TestRemoveDuplicates()
    {
      RemoveDuplicates instance = new RemoveDuplicates();

      Console.WriteLine(instance.Solution(new[] {1, 1, 1, 1}));
    }

    private static void TestPathSum()
    {
      PathSum instance = new PathSum();

      Console.WriteLine(instance.Solution(new TreeNode(1, new TreeNode(2, 3, 5), 4), 6));
    }

    private static void TestShiftingLetters()
    {
      ShiftingLetters instance = new ShiftingLetters();

      Console.WriteLine(instance.Solution("mkgfzkkuxownxvfvxasy",
        new[]
        {
          505870226, 437526072, 266740649, 224336793, 532917782, 311122363, 567754492, 595798950, 81520022, 684110326,
          137742843, 275267355, 856903962, 148291585, 919054234, 467541837, 622939912, 116899933, 983296461, 536563513
        }));
    }

    private static void TestCanPartition()
    {
      CanPartition instance = new CanPartition();

      Console.WriteLine(instance.Solution(new[]
      {
        1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
        1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
        1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 100
      }));

      instance.Solution(new[] {3, 3, 3, 4, 5});
    }

    private static void TestLengthOfLIS()
    {
      LengthOfLIS instance = new LengthOfLIS();

      Console.WriteLine(instance.Solution(new[] {1, 3, 6, 7, 9, 4, 10, 5, 6}));
    }

    private static void TestLetterCombinations()
    {
      LetterCombinations instance = new LetterCombinations();

      Console.WriteLine(instance.Solution("23"));
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