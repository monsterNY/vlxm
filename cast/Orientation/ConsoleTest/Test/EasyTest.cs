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

namespace ConsoleTest.Test
{
  /// <summary>
  /// @desc : EasyTest  
  /// @author :mons
  /// @create : 2019/3/20 14:28:56 
  /// @source : 
  /// </summary>
  public class EasyTest
  {


    private static void TestExclusiveTime()
    {
      ExclusiveTime instance = new ExclusiveTime();

      Console.WriteLine(instance.OtherSolution(2,
        new[] { "0:start:0", "0:start:2", "0:end:5", "1:start:7", "1:end:7", "0:end:8" }));
      Console.WriteLine(instance.OtherSolution(1,
        new[] { "0:start:0", "0:start:2", "0:end:5", "0:start:6", "0:end:6", "0:end:7" }));
      Console.WriteLine(instance.OtherSolution(2, new[] { "0:start:0", "1:start:2", "1:end:5", "0:end:6" }));
    }

    private static void TestOddEvenList()
    {
      Console.WriteLine(1.1 % 1);

      OddEvenList instance = new OddEvenList();

      //      instance.Optimize(new[] { 1, 2, 3, 4});
      instance.Solution(new[] { 1, 2, 3, 4, 5, 6, 7, 8 });
      instance.Solution(new ListNode(1, new ListNode(2, new ListNode(3, new ListNode(4, new ListNode(5))))));
    }

    private static void TestReorderedPowerOf2()
    {
      var num = 1;

      while (num > 0)
      {
        Console.WriteLine(num);
        num *= 2;
      }

      ReorderedPowerOf2 instance = new ReorderedPowerOf2();


      Console.WriteLine(instance.OtherSolution(218));

      Console.WriteLine(instance.Simple(218));
    }

    private static void TestKthSmallest()
    {
      KthSmallest instance = new KthSmallest();

      instance.kthSmallest(new TreeNode(3, new TreeNode(1, null, 2), 4), 1);
    }

    private static void TestNumRabbits(Random rand)
    {
      NumRabbits instance = new NumRabbits();

      for (int i = 0; i < 100; i++)
      {
        var len = rand.Next(5) + 2;
        var arr = new int[len];

        for (int j = 0; j < arr.Length; j++)
        {
          arr[j] = rand.Next(10) + 1;
        }

        var solution = instance.Solution(arr);

        ShowConsole(new Dictionary<string, object>()
        {
          {nameof(arr), JsonConvert.SerializeObject(arr)},
          {nameof(solution), solution}
        });
      }
    }

    private static void TestPrintTree()
    {
      PrintTree instance = new PrintTree();

      IList<IList<string>> result;

      //      result = instance.Solution(new TreeNode(3, new TreeNode(1, 0, new TreeNode(2, new TreeNode(7, 8, 9), 3)),
      //        new TreeNode(5, 4, 6)));
      //
      //      result.PrintList();

      result = instance.Solution(new TreeNode(3, new TreeNode(1, 0, new TreeNode(2, null, 3)), new TreeNode(5, 4, 6)));

      result.PrintList();

      result = instance.Solution(new TreeNode(1, 2, null));

      result.PrintList();

      result = instance.Solution(new TreeNode(1, new TreeNode(2, null, 4), 3));

      result.PrintList();

      result = instance.Solution(new TreeNode(1, new TreeNode(2, new TreeNode(3, 4, null), null), 5));

      result.PrintList();

      result = instance.Solution(new TreeNode(1, new TreeNode(2, 3, 4), new TreeNode(5, 6, 7)));

      result.PrintList();
    }

    private static void TestSubsets()
    {
      Subsets instance = new Subsets();

      var result = instance.Solution(new[] { 1, 2, 3 });

      Console.WriteLine(JsonConvert.SerializeObject(result));

      result = instance.Solution(new[] { 1, 2, 3, 4 });

      Console.WriteLine(JsonConvert.SerializeObject(result));
    }

    private static void TestLongestOnes()
    {
      LongestOnes instance = new LongestOnes();


      Console.WriteLine(instance.Solution(
        new[]
        {
          1, 0, 0, 1, 0, 0, 1, 0, 1, 1, 1, 1, 0, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 0, 0, 1, 1, 1, 0, 0, 1, 0,
          1, 0, 0, 1, 0, 0, 1, 1
        }, 9));

      Console.WriteLine(instance.Solution(new[] { 0, 0, 0, 1 }, 4));
    }

    private static void TestFindPoisonedDuration()
    {
      FindPoisonedDuration instance = new FindPoisonedDuration();

      Console.WriteLine(instance.Solution(new[] { 1, 4 }, 2));
      Console.WriteLine(instance.Solution(new[] { 1, 2 }, 2));
    }

    private static void TestSweepGame()
    {
      SweepGame instance = new SweepGame();

      Console.WriteLine("<<<欢迎来到xxx扫雷游戏>>>");

      Console.WriteLine("请输入炸弹个数:");

      var num = Convert.ToInt32(Console.ReadLine());

      var initBoard = instance.InitBoard(num);

      Console.WriteLine("初始化棋盘：");

      instance.ShowBoard(initBoard);

      int y, x;

      while (true)
      {
        //        Console.WriteLine(JsonConvert.SerializeObject(initBoard));

        Console.WriteLine("请输入翻开的列数:");
        x = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("请输入翻开的行数:");
        y = Convert.ToInt32(Console.ReadLine());

        if (y < 1 || y > initBoard.Length || x < 1 || x > initBoard[0].Length)
        {
          Console.WriteLine("输入位置不合理！");
        }
        else
        {
          initBoard = instance.GetStep(initBoard, y - 1, x - 1);

          Console.WriteLine("翻开结果：");

          instance.ShowBoard(initBoard);
          if (instance.IsOver)
          {
            break;
          }

          if (instance.IsWin(initBoard))
          {
            Console.WriteLine("恭喜获胜！！！！");
            break;
          }
        }
      }
    }

    private static void TestUpdateBoard()
    {
      UpdateBoard instance = new UpdateBoard();

      char[][] result = null;

      result = instance.Solution(new[]
      {
        new[] {'E', 'E', 'E', 'E', 'E', 'E', 'E', 'E'},
        new[] {'E', 'E', 'E', 'E', 'E', 'E', 'E', 'M'},
        new[] {'E', 'E', 'M', 'E', 'E', 'E', 'E', 'E'},
        new[] {'M', 'E', 'E', 'E', 'E', 'E', 'E', 'E'},
        new[] {'E', 'E', 'E', 'E', 'E', 'E', 'E', 'E'},
        new[] {'E', 'E', 'E', 'E', 'E', 'E', 'E', 'E'},
        new[] {'E', 'E', 'E', 'E', 'E', 'E', 'E', 'E'},
        new[] {'E', 'E', 'M', 'M', 'E', 'E', 'E', 'E'},
      }, new[] { 0, 0 });


      Console.WriteLine(JsonConvert.SerializeObject(result));

      var arr = instance.Solution(new[]
      {
        new[] {'E', 'E', 'E', 'E', 'E'},
        new[] {'E', 'E', 'M', 'E', 'E'},
        new[] {'E', 'E', 'E', 'E', 'E'},
        new[] {'E', 'E', 'E', 'E', 'E'},
      }, new[] { 3, 0 });

      Console.WriteLine(JsonConvert.SerializeObject(arr));
    }

    private static void TestNumEnclaves()
    {
      NumEnclaves instance = new NumEnclaves();

      Console.WriteLine(instance.Solution(new[]
      {
        new[] {0, 0, 1, 1, 1, 0, 1, 1, 1, 0, 1},
        new[] {1, 1, 1, 1, 0, 1, 0, 1, 1, 0, 0},
        new[] {0, 1, 0, 1, 1, 0, 0, 0, 0, 1, 0},
        new[] {1, 0, 1, 1, 1, 1, 1, 0, 0, 0, 1},
        new[] {0, 0, 1, 0, 1, 1, 0, 0, 1, 0, 0},
        new[] {1, 0, 0, 1, 1, 1, 0, 0, 0, 1, 1},
        new[] {0, 1, 0, 1, 1, 0, 0, 0, 1, 0, 0},
        new[] {0, 1, 1, 0, 1, 0, 1, 1, 1, 0, 0},
        new[] {1, 1, 0, 1, 1, 1, 0, 0, 0, 0, 0},
        new[] {1, 0, 1, 1, 0, 0, 0, 1, 0, 0, 1},
      })); //7

      Console.WriteLine(instance.Solution(new[]
      {
        new[] {0, 0, 0, 1, 1, 1, 0, 1, 0, 0},
        new[] {1, 1, 0, 0, 0, 1, 0, 1, 1, 1},
        new[] {0, 0, 0, 1, 1, 1, 0, 1, 0, 0},
        new[] {0, 1, 1, 0, 0, 0, 1, 0, 1, 0},
        new[] {0, 1, 1, 1, 1, 1, 0, 0, 1, 0},
        new[] {0, 0, 1, 0, 1, 1, 1, 1, 0, 1},
        new[] {0, 1, 1, 0, 0, 0, 1, 1, 1, 1},
        new[] {0, 0, 1, 0, 0, 1, 0, 1, 0, 1},
        new[] {1, 0, 1, 0, 1, 1, 0, 0, 0, 0},
        new[] {0, 0, 0, 0, 1, 1, 0, 0, 0, 1}
      })); //3
    }

    private static void TestFindCircleNum()
    {
      FindCircleNum instance = new FindCircleNum();
      Console.WriteLine(instance.Solution(new[]
      {
        new[] {1, 0, 0, 1},
        new[] {0, 1, 1, 0},
        new[] {0, 1, 1, 1},
        new[] {1, 0, 1, 1},
      })); //1

      Console.WriteLine(instance.Solution(new[]
      {
        new[] {1, 1, 1, 1, 1},
        new[] {1, 1, 1, 1, 1},
        new[] {1, 1, 1, 1, 1},
        new[] {1, 1, 1, 1, 1},
        new[] {1, 1, 1, 1, 1},
      })); //1

      Console.WriteLine(instance.Solution(new[]
      {
        new[] {1, 1, 0},
        new[] {1, 1, 0},
        new[] {0, 0, 1},
      })); //2

      Console.WriteLine(instance.Solution(new[]
      {
        new[] {1, 1, 0},
        new[] {1, 1, 1},
        new[] {0, 1, 1},
      })); //1
    }

    private static void TestMinimumDeleteSum()
    {
      MinimumDeleteSum instance = new MinimumDeleteSum();
      Console.WriteLine(instance.Solution("vsmfbgotim", "xibcpmzyikel")); //1965
      Console.WriteLine(instance.Solution("vwojt", "saqhgdrarwntji")); //1613
      Console.WriteLine(instance.Solution("ccaccjp", "fwosarcwge")); //1399
      Console.WriteLine(instance.Solution("delete", "leet")); //403
      Console.WriteLine(instance.Solution("sea", "eat")); //231
    }

    private static void TestGenerateParenthesis()
    {
      GenerateParenthesis instance = new GenerateParenthesis();

      //      Console.WriteLine(JsonConvert.SerializeObject(instance.Solution(3).Distinct()));
      for (int i = 0; i < 7; i++)
      {
        Console.WriteLine(i + "" + JsonConvert.SerializeObject(instance.Solution(i)));
      }
    }

    private static void TestPermute()
    {
      Permute instance = new Permute();
      instance.Solution(new[]
      {
        1, 2, 3
      });
    }

    private static void TestFindFrequentTreeSum()
    {
      FindFrequentTreeSum instance = new FindFrequentTreeSum();
      instance.Solution(new TreeNode(5, 2, -3));
      instance.Solution(new TreeNode(5, 2, -5));
      instance.Solution2(new TreeNode(5, 2, -3));
      instance.Solution2(new TreeNode(5, 2, -5));
    }

    private static void TestProductExceptSelf(Random rand)
    {
      ProductExceptSelf instance = new ProductExceptSelf();

      //      Console.WriteLine(JsonConvert.SerializeObject(instance.Solution(new[] {9, 0, -2})));
      //
      //      Console.WriteLine(instance.Solution(new[] {1, 2, 3, 4}));
      for (int i = 0; i < 100; i++)
      {
        var len = 4;

        var arr = new int[len];

        for (int j = 0; j < arr.Length; j++)
        {
          arr[j] = rand.Next(20) - 10;
        }

        Console.WriteLine("--------<<Test>>----------");

        Console.WriteLine($"source: {JsonConvert.SerializeObject(arr)}  ---------------- S");

        instance.Check(arr);

        Console.WriteLine($"source: {JsonConvert.SerializeObject(arr)}  ---------------- E");

        Console.WriteLine("--------Solution------- S---");

        Console.WriteLine(JsonConvert.SerializeObject(instance.Solution(arr)));


        Console.WriteLine("--------Solution------- E---");
      }
    }

    private static void TestFindDuplicate()
    {
      FindDuplicate instance = new FindDuplicate();
      instance.Solution(new[]
      {
        "root/a 1.txt(abcd) 2.txt(efgh)", "root/c 3.txt(abcd)", "root/c/d 4.txt(efgh)", "root 4.txt(efgh)"
      });
    }

    private static void TestEscapeGhosts()
    {
      EscapeGhosts instance = new EscapeGhosts();
      Console.WriteLine(instance.Solution(new int[][]
      {
        new[]
        {
          1, 9
        },
        new[]
        {
          2, -5
        },
        new[]
        {
          3, 8
        },
        new[]
        {
          9, 8
        },
        new[]
        {
          -1, 3
        }
      }, new[]
      {
        8, -10
      }));
    }

    private static void TestOptimalDivision(Random rand)
    {
      OptimalDivision instance = new OptimalDivision();
      Console.WriteLine(instance.Solution(new[]
      {
        1000, 100, 10, 2
      }));
      for (int i = 0; i < 10; i++)
      {
        var len = rand.Next(5) + 3;

        var arr = new int[len];
        for (int j = 0; j < arr.Length; j++)
        {
          arr[j] = rand.Next(8) * 10 + 20;
        }

        var solution = instance.Solution(arr);
        var result = instance.Try(arr);

        ShowConsole(new Dictionary<string, object>()
        {
          {nameof(arr), JsonConvert.SerializeObject(arr)},
          {nameof(solution), solution},
          {nameof(result), result}
        });
      }
    }

    private static void TestScoreOfParentheses()
    {
      ScoreOfParentheses instance = new ScoreOfParentheses();
      Console.WriteLine(instance.Solution("((()()))"));
      Console.WriteLine(instance.Solution("()"));
      Console.WriteLine(instance.Solution("(())"));
      Console.WriteLine(instance.Solution("()()"));
      Console.WriteLine(instance.Solution("(()(()))"));
    }

    private static void TestFrequencySort()
    {
      Console.WriteLine((int)'a');
      Console.WriteLine((int)'A');
      FrequencySort instance = new FrequencySort();
      Console.WriteLine((instance.Solution("tree")));
    }

    private static void TestCountSubstrings(CodeTimer timer)
    {
      CountSubstrings instance = new CountSubstrings();
      var codeTimerResult = timer.Time(1, (() => { Console.WriteLine(instance.Solution("abc")); }));
      Console.WriteLine(codeTimerResult);

      //      codeTimerResult = timer.Time(1, (() => { Console.WriteLine(instance.Optimize("abc")); }));
      //
      //      Console.WriteLine(codeTimerResult);
    }

    private static void TestSingleNumber()
    {
      SingleNumber instance = new SingleNumber();
      Console.WriteLine(instance.Solution(new[]
      {
        1, 2, 1, 3, 2, 5
      }));
    }

    private static void TestMincostTickets()
    {
      //      MincostTickets instance = new MincostTickets();

      //      Console.WriteLine(instance.Solution(new[]
      //      {
      //        1, 4, 6, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 20, 21, 22, 23, 27, 28
      //      }, new[] { 3, 13, 45 }));
      //
      //      Console.WriteLine(instance.Solution(new[]
      //      {
      //        1, 4, 6, 7, 8, 20
      //      }, new[] { 2, 7, 15 }));
    }

    private static void TestValidateStackSequences()
    {
      ValidateStackSequences instance = new ValidateStackSequences();
      Console.WriteLine(instance.Solution(new[]
      {
        1, 2, 3, 4, 5
      }, new[]
      {
        4, 5, 3, 2, 1
      }));

      Console.WriteLine(instance.Solution(new[]
      {
        1, 2, 3, 4, 5
      }, new[]
      {
        4, 3, 5, 1, 2
      }));
    }

    private static void TestMinFallingPathSum(CodeTimer timer)
    {
      MinFallingPathSum instance = new MinFallingPathSum();

      var codeTimerResult = timer.Time(1, (() =>
      {
        Console.WriteLine(instance.Optimize(new[]
        {
          new int[] {-19, -1, -96, 48, -94, 36, 16, 55, -42, 37, -59, 6, -32, 96, 95, -58, 13, -34, 94, 85},
          new int[] {17, 44, 36, -29, 84, 80, -34, 50, -99, 64, 13, 91, -27, 25, -36, 57, 20, 98, -100, -72},
          new int[] {-92, -75, 86, 90, -4, 90, 64, 56, 50, -63, 10, -15, 90, -66, -66, 32, -69, -78, 1, 60},
          new int[] {21, 51, -47, -43, -14, 99, 44, 90, 8, 11, 99, -62, 57, 59, 69, 50, -69, 32, 85, 13},
          new int[] {-28, 90, 12, -18, 23, 61, -55, -97, 6, 89, 36, 26, 26, -1, 46, -50, 79, -45, 89, 86},
          new int[] {-85, -10, 49, -10, 2, 62, 41, 92, -67, 85, 86, 27, 89, -50, 77, 55, 22, -82, -94, -98},
          new int[] {-50, 53, -23, 55, 25, -22, 76, -93, -7, 66, -75, 42, -35, -96, -5, 4, -92, 13, -31, -100},
          new int[] {-62, -78, 8, -92, 86, 69, 90, -37, 81, 97, 53, -45, 34, 19, -19, -39, -88, -75, -74, -4},
          new int[] {29, 53, -91, 65, -92, 11, 49, 26, 90, -31, 17, -84, 12, 63, -60, -48, 40, -49, -48, 88},
          new int[] {100, -69, 80, 11, -93, 17, 28, -94, 52, 64, -86, 30, -9, -53, -8, -68, -33, 31, -5, 11},
          new int[] {9, 64, -31, 63, -84, -15, -30, -10, 67, 2, 98, 73, -77, -37, -96, 47, -97, 78, -62, -17},
          new int[] {-88, -38, -22, -90, 54, 42, -29, 67, -85, -90, -29, 81, 52, 35, 13, 61, -18, -94, 61, -62},
          new int[] {-23, -29, -76, -30, -65, 23, 31, -98, -9, 11, 75, -1, -84, -90, 73, 58, 72, -48, 30, -81},
          new int[] {66, -33, 91, -6, -94, 82, 25, -43, -93, -25, -69, 10, -71, -65, 85, 28, -52, 76, 25, 90},
          new int[] {-3, 78, 36, -92, -52, -44, -66, -53, -55, 76, -7, 76, -73, 13, -98, 86, -99, -22, 61, 100},
          new int[] {-97, 65, 2, -93, 56, -78, 22, 56, 35, -24, -95, -13, 83, -34, -51, -73, 2, 7, -86, -19},
          new int[] {32, 94, -14, -13, -6, -55, -21, 29, -21, 16, 67, 100, 77, -26, -96, 22, -5, -53, -92, -36},
          new int[] {60, 93, -79, 76, -91, 43, -95, -16, 74, -21, 85, 43, 21, -68, -32, -18, 18, 100, -43, 1},
          new int[] {87, -31, 26, 53, 26, 51, -61, 92, -65, 17, -41, 27, -42, -14, 37, -46, 46, -31, -74, 23},
          new int[] {-67, -14, -20, -85, 42, 36, 56, 9, 11, -66, -59, -55, 5, 64, -29, 77, 47, 44, -33, -77}
        }));
      }));

      Console.WriteLine(codeTimerResult);
      Console.WriteLine(instance.Solution(new[]
      {
        new int[] {-51, -35, 74},
        new int[] {-62, 14, -53},
        new int[] {94, 61, -10},
      }));

      Console.WriteLine(instance.Solution(new[]
      {
        new int[] {-80, -13, 22},
        new int[] {83, 94, -5},
        new int[] {73, -48, 61},
      }));

      Console.WriteLine(instance.Optimize(new[]
      {
        new int[] {-51, -35, 74},
        new int[] {-62, 14, -53},
        new int[] {94, 61, -10},
      }));

      Console.WriteLine(instance.Optimize(new[]
      {
        new int[] {-80, -13, 22},
        new int[] {83, 94, -5},
        new int[] {73, -48, 61},
      }));
    }

    private static void TestQueryString()
    {
      QueryString instance = new QueryString();
      Console.WriteLine(instance.Solution("0110", 3));
      Console.WriteLine(instance.Solution("0110", 4));
      Console.WriteLine(Convert.ToString(10, 2));
    }

    private static void MaxScoreSightseeingPairTest(CodeTimer timer)
    {
      MaxScoreSightseeingPair instance = new MaxScoreSightseeingPair();

      var arr = new[]
      {
        402, 224, 922, 720, 323, 714, 129, 303, 556, 532, 925, 824, 466, 169, 725, 83,
      };

      var resultNum = 0;
      var testCount = 1;
      CodeTimerResult codeTimerResult;
      Console.WriteLine(instance.Optimize2(new[]
      {
        1, 3, 5
      }));

      Console.WriteLine(instance.Optimize2(new[]
      {
        8, 1, 5, 2, 6
      }));

      //      var codeTimerResult = timer.Time(testCount, (() => { resultNum = instance.Solution(arr); }));
      //
      //      Console.WriteLine($"resultNum:{resultNum},time:{codeTimerResult}");
      codeTimerResult = timer.Time(testCount, (() => { resultNum = instance.Optimize(arr); }));
      Console.WriteLine($"resultNum:{resultNum},time:{codeTimerResult}");
      codeTimerResult = timer.Time(testCount, (() => { resultNum = instance.Optimize2(arr); }));
      Console.WriteLine($"resultNum:{resultNum},time:{codeTimerResult}");
    }

    private static void TestSmallestRepunitDivByK()
    {
      SmallestRepunitDivByK instance = new SmallestRepunitDivByK();

      long num = 1;

      //      while (num > 0)
      //      {
      //        instance.Test(num);
      //        num = num * 10 + 1;
      //      }
      //
      //      //Console.WriteLine(JsonConvert.SerializeObject(instance.list.Distinct().OrderBy(u => u)));
      //
      //      Console.WriteLine(JsonConvert.SerializeObject(instance.ArrList));

      //      instance.Solution(17);
      //
      //      Console.ReadKey(true);
      for (int i = 1; i < 1000; i++)
      {
        Console.WriteLine($"{i}---- result: {instance.Solution(i)}");
      }
    }

    private static StoneGame TestStoneGame()
    {
      StoneGame instance = new StoneGame();
      Console.WriteLine(instance.Solution(new[]
      {
        5, 3, 4, 5
      }));
      return instance;
    }

    private static void TestIntervalIntersection()
    {
      IntervalIntersection instance = new IntervalIntersection();

      var intervals = instance.Solution(new[]
      {
        new Interval(0, 2),
        new Interval(5, 10),
        new Interval(13, 23),
        new Interval(24, 25),
      }, new[]
      {
        new Interval(1, 5),
        new Interval(8, 12),
        new Interval(15, 24),
        new Interval(25, 26),
      });

      Console.WriteLine(JsonConvert.SerializeObject(intervals));
    }

    private static void InsertIntoMaxTreeTest()
    {
      InsertIntoMaxTree instance = new InsertIntoMaxTree();
      var treeNode = instance.Solution(new TreeNode(5, new TreeNode(2, null, 1), 4), 3);
      Console.WriteLine(treeNode);
    }

    private static void TestCanThreePartsEqualSum()
    {
      CanThreePartsEqualSum instance = new CanThreePartsEqualSum();
      Console.WriteLine(instance.Solution(new[]
      {
        0, 2, 1, -6, 6, 7, 9, -1, 2, 0, 1
      }));

      Console.WriteLine(instance.Solution(new[]
      {
        18, 12, -18, 18, -19, -1, 10, 10
      }));

      Console.WriteLine(instance.Solution(new[]
      {
        18, 12, -18, 18, -19, -1, 10, 10
      }));

      Console.WriteLine(instance.Solution(new[]
      {
        3, 3, 6, 5, -2, 2, 5, 1, -9, 4
      }));

      Console.WriteLine(instance.Solution(new[]
      {
        0, 2, 1, -6, 6, -7, 9, 1, 2, 0, 1
      }));
    }

    private static void TestReconstructQueue()
    {
      var arr = new[]
      {
        new[] {7, 0}, new[] {4, 4}, new[] {7, 1}, new[] {5, 0}, new[] {6, 1}, new[] {5, 2}
      };

      Array.Sort(arr, ((ints, ints1) => ints[0] - ints1[0]));
      Console.WriteLine(arr);
      ReconstructQueue instance = new ReconstructQueue();

      var solution = instance.Solution(new[]
      {
        new[] {7, 0}, new[] {4, 4}, new[] {7, 1}, new[] {5, 0}, new[] {6, 1}, new[] {5, 2}
      });

      Console.WriteLine(JsonConvert.SerializeObject(solution));
      solution = instance.Solution(new[]
      {
        new[] {2, 4}, new[] {3, 4}, new[] {9, 0}, new[] {0, 6}, new[] {7, 1}, new[] {6, 0}, new[] {7, 3},
        new[] {2, 5},
        new[] {1, 1}, new[] {8, 0}
      });

      Console.WriteLine(JsonConvert.SerializeObject(solution));

      var simple = instance.Simple(new[]
      {
        new[] {7, 0}, new[] {4, 4}, new[] {7, 1}, new[] {5, 0}, new[] {6, 1}, new[] {5, 2}
      });

      Console.WriteLine(JsonConvert.SerializeObject(simple));
      simple = instance.Simple(new[]
      {
        new[] {2, 4}, new[] {3, 4}, new[] {9, 0}, new[] {0, 6}, new[] {7, 1}, new[] {6, 0}, new[] {7, 3},
        new[] {2, 5},
        new[] {1, 1}, new[] {8, 0}
      });

      Console.WriteLine(JsonConvert.SerializeObject(simple));
    }

    private static void TestCountBits()
    {
      Console.WriteLine(1 >> 2);
      Console.WriteLine(1 << 2);
      CountBits instance = new CountBits();
      Console.WriteLine(JsonConvert.SerializeObject(instance.Solution(2)));

      var flag = 2;
      for (int i = 0; i < 100; i++)
      {
        if (i == flag * 2)
        {
          flag *= 2;
          Console.WriteLine("++++++++++++++++++");
        }

        var count = instance.Count(i);
        Console.WriteLine($"{i}------------{count}------------|{i - flag}");
      }
    }

    private static void TestComplexNumberMultiply()
    {
      ComplexNumberMultiply instance = new ComplexNumberMultiply();
      Console.WriteLine(instance.Solution("1+-1i", "1+-1i"));
    }

    private static void TestCountBattleships()
    {
      //[,]是二维数组 [][]是交叉数组 what???
      var array = new int[,] { { 1, 2 }, { 2, 3 }, { 2, 3 }, { 2, 3 } };
      array[0, 0] = 8;
      Console.WriteLine("数组的长度为{0}", array.Length);
      /*
      Console.WriteLine(array.GetLength(0));
      Console.WriteLine(array.GetLength(1));
      */
      for (int i = 0;
        i < array.GetLength(0);
        i++)
      {
        for (int j = 0; j < array.GetLength(1); j++)
        {
          Console.WriteLine(array[i, j]);
        }
      }

      Console.WriteLine("---------");
      CountBattleships instance = new CountBattleships();
      Console.WriteLine(
        instance.Solution(new char[,]
        {
          {
            'X', '.', '.', 'X'
          },
          {
            '.', '.', '.', 'X'
          },
          {
            '.', '.', '.', 'X'
          }
        }));
    }

    private static void DistributeCoinsTest()
    {
      DistributeCoins instance = new DistributeCoins();
      Console.WriteLine(instance.Solution(
        new TreeNode(4, null,
          new TreeNode(0, new TreeNode(0, null, new TreeNode(0, null, 2)), 0
          ))
      )); //6

      Console.WriteLine(instance.Solution(
        new TreeNode(0, new TreeNode(1, 3, 0), null)
      )); //4

      Console.WriteLine(instance.Solution(
        new TreeNode(1, new TreeNode(0, null, 3), 0)
      )); //4

      Console.WriteLine(instance.Solution(
        new TreeNode(4,
          new TreeNode(0, null,
            new TreeNode(0, null, 0)), null)
      )); //6

      Console.WriteLine(instance.Solution(
        new TreeNode(0,
          new TreeNode(1, 3, 0), null)
      )); //4

      Console.ReadKey(true);
      Console.WriteLine(instance.Solution(
        new TreeNode(3, 0, 0)
      ));

      Console.WriteLine(instance.Solution(
        new TreeNode(0, 3, 0)
      ));

      Console.WriteLine(instance.Solution(
        new TreeNode(1, 0, 2)
      ));

      Console.WriteLine(instance.Solution(
        new TreeNode(1, new TreeNode(0, null, 3), 0)
      ));
    }

    private static void TestMinAddToMakeValid()
    {
      MinAddToMakeValid instance = new MinAddToMakeValid();
      Console.WriteLine(instance.Solution("((())"));
    }

    private static void FindAndReplacePatternTest()
    {
      FindAndReplacePattern instance = new FindAndReplacePattern();

      var solution = instance.Solution(new[]
      {
        "abc", "deq", "mee", "aqq", "dkd", "ccc"
      }, "abb");

      Console.WriteLine(JsonConvert.SerializeObject(solution));
    }

    private static void TestDeckRevealedIncreasing(CodeTimer timer)
    {
      DeckRevealedIncreasing instance = new DeckRevealedIncreasing();
      instance.Solution(new[]
      {
        1, 2, 3, 4, 5, 6, 7
      });

      Console.ReadKey(true);

      List<int> list = new List<int>();
      for (int i = 1; i < 20; i++)
      {
        list.Add(i);

        int[] arr = null;

        var codeTimerResult = timer.Time(1, action: (() => { arr = instance.Simple(list.ToArray()); }));

        ShowConsole(new Dictionary<string, object>()
        {
          {nameof(list), JsonConvert.SerializeObject(list)},
          {nameof(arr), JsonConvert.SerializeObject(arr)},
          {nameof(codeTimerResult), codeTimerResult}
        });

        instance.Check(arr);
      }
    }

    [
      Obsolete]
    private static void TestInsertIntoBST()
    {
      InsertIntoBST instance = new InsertIntoBST();

      var treeNode = instance.Solution(new TreeNode(4,
        new TreeNode(2, 1, 3),
        7
      ), 5);

      Console.WriteLine(treeNode);
    }

    private static void ConstructMaximumBinaryTreeTest()
    {
      ConstructMaximumBinaryTree instance = new ConstructMaximumBinaryTree();
      var treeNode = instance.Solution(new[] { 3, 2, 1, 6, 0, 5 });
      Console.WriteLine(treeNode);
    }

    private static void RangeSumBSTTest()
    {
      RangeSumBST instance = new RangeSumBST();
      Console.WriteLine(instance.Solution(new TreeNode(10,
        new TreeNode(5,
          new TreeNode(3), new TreeNode(7)),
        new TreeNode(15,
          null, new TreeNode(18))
      ), 7, 15));

      Console.WriteLine(instance.Solution(new TreeNode(10,
        new TreeNode(5,
          new TreeNode(3, 1, null), new TreeNode(7, 6, null)),
        new TreeNode(15,
          new TreeNode(13), new TreeNode(18))
      ), 6, 10));
    }

    #region empty

    public void Empty()
    {
      //先进后出
      Stack<int> stack = new Stack<int>();
      stack.Push(1);
      stack.Push(2);
      stack.Push(3);
      stack.Push(4);

      //返回最后一个 不删除
      Console.WriteLine(stack.Peek());
      Console.WriteLine(stack.Peek());

      //返回最后一个 删除
      Console.WriteLine(stack.Pop());
      Console.WriteLine(stack.Pop());

      //先进先出
      Queue<int> queue = new Queue<int>();
      queue.Enqueue(1);
      queue.Enqueue(2);
      queue.Enqueue(3);
      queue.Enqueue(4);
      Console.WriteLine(queue.Dequeue());
      Console.WriteLine(queue.Dequeue());
      Console.WriteLine(queue.Peek());
      Console.WriteLine(queue.Peek());
      Console.WriteLine("Bitwise result: {0}", Convert.ToString(0xF8, 2));
      Console.WriteLine("Bitwise result: {0}", Convert.ToString(0x0 ^ 0xF8, 2));
      Console.WriteLine("Bitwise result: {0}", Convert.ToString(0x0 & 0xF8, 2));
      Console.WriteLine("Bitwise result: {0}", Convert.ToString(0x0 | 0xF8, 2));
      Console.WriteLine((int)'a');
      Console.WriteLine((int)'A');
      Console.WriteLine($"{{rand}}");
      var type = typeof(Types);
      var propertyInfos = type.GetProperties();
      var fieldInfos = type.GetFields(BindingFlags.Static | BindingFlags.Public);
      var descriptionAttribute = fieldInfos[1].GetCustomAttribute<DescriptionAttribute>();
      var strings = Enum.GetNames(type);
      var values = Enum.GetValues(type);
    }

    public static dynamic Build<T>(T instance, string[] properties)
    {
      dynamic obj = new System.Dynamic.ExpandoObject();

      var type = typeof(T);
      foreach (var prop in properties)
      {
        var propertyInfo = type.GetProperty(prop);

        if (propertyInfo != null)
        {
          ((IDictionary<string, object>)obj).Add(propertyInfo.Name, propertyInfo.GetValue(instance));
        }
      }

      return obj;
    }

    enum
      Types
    {
      [Description("正常")] Normal = 0,
      Special = 1,
    }

    private static void EmptyTest()
    {
      var stu = new Student()
      {
        Age = 18,
        ClassId = 1,
        Name = "xxx"
      };
      var result = Build(stu, new[] { nameof(Student.Age), nameof(Student.Name) });
      Console.WriteLine(result.Age);
      Console.WriteLine(result.Name);
    }

    #endregion
    private static void NumPairsDivisibleBy60Test(Random rand, CodeTimer timer)
    {
      NumPairsDivisibleBy60 instance = new NumPairsDivisibleBy60();

      for (int i = 0; i < 1000; i++)
      {
        var len = rand.Next(10);
        var arr = new int[len];
        for (int j = 0; j < len; j++)
        {
          arr[j] = (rand.Next(6) + 1) * 10;
        }

        int solution = 0, optimize = 0;

        var solutionTime = timer.Time(100, (() => { solution = instance.Solution(arr); }));
        var optimizeTime = timer.Time(100, (() => { optimize = instance.Optimize(arr); }));


        ShowConsole(new Dictionary<string, object>()
        {
          {nameof(arr), JsonConvert.SerializeObject(arr)},
          {nameof(solutionTime), solutionTime},
          {nameof(optimizeTime), optimizeTime},
          {"result", optimize}
        });

        if (solution != optimize)
          throw new Exception();
      }
    }

    private static void RemoveElementsTest()
    {
      RemoveElements instance = new RemoveElements();

      var listNode = instance.Solution(new ListNode(1,
        new ListNode(2, new ListNode(2, new ListNode(1)))
      ), 2);
    }

    private static void ReverseBitsTest()
    {
      var num = 100;

      StringBuilder builder = new StringBuilder();

      while (num > 0)
      {
        builder.Append(num % 2);
        num /= 2;
      }

      ReverseBits instance = new ReverseBits();
      Console.WriteLine(instance.Solution(43261596));

      for (uint i = 100; i < 1000; i++)
      {
        var solution = instance.Solution(i);

        instance.Check(i, solution);
      }
    }

    private static void TrailingZeroesTest()
    {
      TrailingZeroes instance = new TrailingZeroes();

      Console.WriteLine(instance.Solution(300));
      Console.WriteLine(instance.Solution(15));
      Console.WriteLine(instance.GetSum(30));
    }

    private static void ConvertToTitleTest()
    {
      ConvertToTitle instance = new ConvertToTitle();

      for (int i = 1; i < 1000; i++)
      {
        Console.WriteLine($"{i}-------------------{instance.Solution(i)}");
      }
    }

    private static void GetIntersectionNodeTest()
    {
      GetIntersectionNode instance = new GetIntersectionNode();

      var commandNode = new ListNode(8,
        new ListNode(4, new ListNode(5))
      );

      instance.OtherSolution(
        new ListNode(
          4, new ListNode(1, commandNode)
        ), new ListNode(
          5, new ListNode(0, new ListNode(1, commandNode))
        ));
    }

    private static void StrIsPalindromeTest()
    {
      Console.WriteLine((int)'0');
      Console.WriteLine((int)'a');
      Console.WriteLine((int)'A');
      Console.WriteLine((int)'P');

      StrIsPalindrome instance = new StrIsPalindrome();
      Console.WriteLine(instance.IsPalindrome("A man, a plan, a canal: Panama"));
      Console.WriteLine(instance.IsPalindrome("race a car"));
      Console.WriteLine(instance.IsPalindrome(".,"));
      Console.WriteLine(instance.IsPalindrome("0P"));
    }

    private static void HasPathSumTest()
    {
      HasPathSum instance = new HasPathSum();

      Console.WriteLine(instance.Solution(new TreeNode(5,
        new TreeNode(4,
          new TreeNode(11, new TreeNode(7), new TreeNode(2)), null),
        new TreeNode(8,
          new TreeNode(13), new TreeNode(4,
            new TreeNode(1), null))
      ), 10));
    }

    private static void MinDepthTest()
    {
      MinDepth instance = new MinDepth();

      var tree = new TreeNode(3, new TreeNode(9), new TreeNode(20, new TreeNode(15), new TreeNode(7)));

      Console.WriteLine(instance.Solution(tree));

      Console.WriteLine(instance.Solution(new TreeNode(1, new TreeNode(2), null)));
      Console.WriteLine(instance.Solution(new TreeNode(1, new TreeNode(2, new TreeNode(4), null),
        new TreeNode(3, null, new TreeNode(5)))));
    }

    private static void MergeSortedArrTest(Random rand, CodeTimer timer)
    {
      MergeSortedArr instance = new MergeSortedArr();

      //      instance.Optimize(new[] {5, 8, 9, 10, 0, 0, 0, 0, 0, 0}, 4, new int[] {7, 7, 9, 10, 10, 11}, 6);
      //
      //      Console.ReadKey(true);

      var arr = new[] { 1, 2, 3, 0, 0, 0 };
      var arrB = new[] { 2, 5, 6 };

      instance.Merge(arr, 3, arrB, 3);

      for (int i = 0; i < 1000; i++)
      {
        var lenA = rand.Next(6) + 4;
        var lenB = rand.Next(3) + 4;

        var nums = new int[lenA + lenB];
        var nums2 = new int[lenB];
        var sortArr = new int[lenA + lenB];

        for (int j = 0; j < lenA; j++)
        {
          if (j > 0)
          {
            nums[j] = nums[j - 1] + rand.Next(4);
          }
          else
            nums[j] = rand.Next(8) + 2;
        }

        for (int j = 0; j < lenB; j++)
        {
          if (j > 0)
          {
            nums2[j] = nums2[j - 1] + rand.Next(4);
          }
          else
            nums2[j] = rand.Next(8) + 2;
        }

        var sortArr2 = new int[lenA + lenB];
        var nums3 = new int[lenB];
        Array.Copy(nums, sortArr2, lenA);
        Array.Copy(nums, sortArr, lenA);
        Array.Copy(nums2, nums3, lenB);

        var codeTimerResult = timer.Time(1, (() => instance.Optimize(sortArr, lenA, nums2, lenB)));


        var codeTimerResult2 = timer.Time(1, (() => instance.Optimize2(sortArr2, lenA, nums3, lenB)));

        ShowConsole(new Dictionary<string, object>()
        {
          {nameof(lenA), lenA},
          {nameof(lenB), lenB},
          {
            nameof(nums), JsonConvert.SerializeObject(nums)
          },
          {
            nameof(nums2), JsonConvert.SerializeObject(nums2)
          },
          {
            nameof(sortArr), JsonConvert.SerializeObject(sortArr)
          },
          {
            nameof(codeTimerResult), codeTimerResult
          },
          {
            nameof(sortArr2), JsonConvert.SerializeObject(sortArr2)
          },
          {
            nameof(codeTimerResult2), codeTimerResult2
          }
        });
        instance.CheckResult(sortArr, nums, nums2);
      }
    }

    private static void MySqrtTest(CodeTimer timer)
    {
      MySqrt instance = new MySqrt();

      var solution1 = instance.Solution(2147395599);
      var optimize1 = instance.Optimize(2147395599);

      Console.ReadKey(true);

      var arr = new bool[100000];

      var parallelQuery = arr.AsParallel().Select(((b, i) =>
      {
        if (i < 3)
          return 0;

        var solution = 0;

        var optimize = 0;

        var codeTimerResult = timer.Time(100, () => { solution = instance.Solution(i); });

        var optimizeTimerResult = timer.Time(100, (() => { optimize = instance.Optimize(i); }));

        if (solution != optimize)
        {
          throw new Exception("error");
        }

        ShowConsole(new Dictionary<string, object>()
        {
          {nameof(i), i},
          {nameof(solution), solution},
          {nameof(codeTimerResult), codeTimerResult},
          {nameof(optimize), optimize},
          {nameof(optimizeTimerResult), optimizeTimerResult},
        });

        return 0;
      })).ToList();

      Console.WriteLine((int)'0');
    }

    private static void LengthOfLastWordTest()
    {
      LengthOfLastWord instance = new LengthOfLastWord();

      Console.WriteLine(instance.Solution("Hello World"));
      Console.WriteLine(instance.Solution("b   a    "));
    }

    private static void StrIndexOfTest(CodeTimer timer)
    {
      StrIndexOf instance = new StrIndexOf();

      Console.WriteLine(instance.StrStr("hello", "ll"));

      var codeTimerResult = timer.Time(1000, () => { instance.StrStr("hello", "ll"); });


      var result = timer.Time(1000, () => { "hello".IndexOf("ll"); });

      Console.WriteLine($"use span : {codeTimerResult}");

      Console.WriteLine($"indexOf : {result}");
    }

    private static void RemoveDuplicatesTest()
    {
      RemoveDuplicates instance = new RemoveDuplicates();

      Console.WriteLine(instance.Solution(new[] { 1, 1, 2 }));
      Console.WriteLine(instance.Solution(new[] { 0, 0, 1, 1, 1, 2, 2, 3, 3, 4 }));
    }

    private static void LongestCommonPrefixTest()
    {
      LongestCommonPrefix instance = new LongestCommonPrefix();

      Console.WriteLine(instance.Solution(new string[] { "flower", "flow", "flight" }));
      Console.WriteLine(instance.Solution(new string[] { "dog", "racecar", "car" }));

      Console.WriteLine("" == string.Empty);
    }

    private static void IsIsomorphicTest()
    {
      IsIsomorphic instance = new IsIsomorphic();

      Console.WriteLine(instance.Solution("egg", "add"));
      Console.WriteLine(instance.Solution("foo", "bar"));
      Console.WriteLine(instance.Solution("paper", "title"));
    }

    private static void CountPrimesTest()
    {
      CountPrimes instance = new CountPrimes();

      var result = instance.OtherSolution(100);

      Console.WriteLine(result);
    }

    private static void IsPalindromeTest()
    {
      IsPalindrome instance = new IsPalindrome();

      Console.WriteLine(instance.Solution(new ListNode(1)));
    }

    private static void FirstBadVersionTest()
    {
      FirstBadVersion instance = new FirstBadVersion();

      Console.WriteLine(instance.Solution(5, 1));
      //      instance.CheckResult(5, 1);

      for (int i = 5; i < 100000; i++)
      {
        Console.WriteLine("-------Solution----------");
        var result = instance.Solution(i);
        Console.WriteLine("--------Optimize---------");
        result = instance.Optimize(i);

        instance.CheckResult(i, result);
      }
    }

    private static void IsPerfectSquareTest()
    {
      IsPerfectSquare instance = new IsPerfectSquare();

      Console.WriteLine(instance.Solution(16641));

      Console.WriteLine(Math.Sqrt(16641));
      Console.WriteLine(Math.Sqrt(16641) % 1 == 0);

      Console.ReadKey(true);

      //      var codeTimerResult = timer.Time(1, (() => { instance.Solution(2147483647); }));
      //
      //      Console.WriteLine(codeTimerResult);
      //
      //      Console.ReadKey(true);

      for (int i = 2; i < 1000000; i++)
      {
        var num = Math.Sqrt(i);
        if (instance.Solution(i))
        {
          Console.WriteLine(i);

          if (num % 1 != 0)
          {
            throw new Exception("error");
          }
        }
        else
        {
          if (num % 1 == 0)
          {
            throw new Exception("error");
          }
        }
      }
    }

    private static void FindPairsTest()
    {
      FindPairs instance = new FindPairs();

      Console.WriteLine(instance.Solution(new int[] { 3, 1, 4, 1, 5 }, 2));
    }

    private static void CheckPerfectNumberTest()
    {
      CheckPerfectNumber instance = new CheckPerfectNumber();

      Console.WriteLine(instance.Solution(28));

      for (int i = 26111368; i < int.MaxValue; i++)
      {
        if (instance.OtherSolution(i))
        {
          Console.WriteLine(i);
        }
      }
    }

    private static void CountSegmentsTest()
    {
      CountSegments instance = new CountSegments();

      Console.WriteLine(instance.Solution("Hello, my name is John"));

      Console.WriteLine(instance.Solution("a, b, c"));
    }

    private static void RepeatedSubstringPatternTest(Random rand, CodeTimer timer)
    {
      RepeatedSubstringPattern instance = new RepeatedSubstringPattern();

      Console.WriteLine(instance.Optimize("debdeb"));
      Console.ReadKey(true);

      Console.WriteLine(instance.Solution("abacababacab"));
      Console.WriteLine(instance.Solution("eeabaeabac"));
      Console.ReadKey(true);

      Console.WriteLine(instance.Solution("abab"));
      Console.WriteLine(instance.Solution("aba"));
      Console.WriteLine(instance.Solution("abcabcabcabc"));
      Console.ReadKey(true);

      for (int i = 0; ; i++)
      {
        var len = rand.Next(5) + 5;
        var arr = new char[len];
        for (int j = 0; j < arr.Length; j++)
        {
          arr[j] = (char)(rand.Next(5) + 'a');
        }

        var str = new string(arr);

        var optimize = instance.Optimize(str);
        var otherSolution = instance.OtherSolution(str);

        if (optimize != otherSolution)
          throw new Exception(str);

        bool result = false;
        var codeTimerResult = timer.Time(1, (() => { result = instance.Solution(str); }));

        ShowConsole(new Dictionary<string, object>()
        {
          {nameof(arr), JsonConvert.SerializeObject(arr)},
          {nameof(result), result},
          {nameof(codeTimerResult), codeTimerResult}
        });
      }
    }

    private static void ThirdMaxTest(Random rand, CodeTimer timer)
    {
      ThirdMax instance = new ThirdMax();

      Console.WriteLine(instance.Solution(new[] { 1, 2, 2, 5, 3, 5 }));
      //    Console.WriteLine(instance.Solution(new[] { 2, 5, 4, 3, 0, 5, 2, 7 }));
      Console.ReadKey(true);

      Console.WriteLine(instance.Solution(new[] { 3, 2, 1 }));
      Console.WriteLine(instance.Solution(new[] { 1, 2 }));
      Console.WriteLine(instance.Solution(new[] { 2, 2, 3, 1 }));

      for (int i = 0; i < 10000; i++)
      {
        var len = 8;
        var arr = new int[len];

        for (int j = 0; j < arr.Length; j++)
        {
          arr[j] = rand.Next(10);
        }

        int result = 0;
        var codeTimerResult = timer.Time(1, (() => { result = instance.Solution(arr); }));

        ShowConsole(new Dictionary<string, object>()
        {
          {nameof(arr), JsonConvert.SerializeObject(arr)},
          {nameof(result), result},
          {nameof(codeTimerResult), codeTimerResult}
        });
      }
    }

    private static void FindUnsortedSubarrayTest(Random rand, CodeTimer timer)
    {
      FindUnsortedSubarray instance = new FindUnsortedSubarray();

      Console.WriteLine(instance.Solution(new[] { 1, 4, 6, 9, 15, 16, 17, 17 }));

      Console.ReadKey(true);
      //
      //      Console.WriteLine(instance.Solution(new[] {2, 6, 4, 8, 10, 9, 15}));
      //      Console.WriteLine(instance.Solution(new[] { 18, 14, 5, 2, 20, 4, 18, 12 }));

      for (int i = 0; i < 10000; i++)
      {
        var len = 8;
        var arr = new int[len];

        for (int j = 0; j < arr.Length; j++)
        {
          arr[j] = rand.Next(20) + 1;
        }

        int result = 0;

        var codeTimerResult = timer.Time(1, (() => { result = instance.Solution((int[])arr.Clone()); }));

        ShowConsole(new Dictionary<string, object>()
        {
          {nameof(arr), JsonConvert.SerializeObject(arr)},
          {nameof(result), result},
          {nameof(codeTimerResult), codeTimerResult}
        });
      }
    }

    private static void CheckPossibilityTest(Random rand, CodeTimer timer)
    {
      CheckPossibility instance = new CheckPossibility();

      Console.WriteLine(instance.Solution(new[] { 4, 8, 4, 8 }));
      Console.WriteLine(instance.Solution(new[] { 4, 2, 3 }));
      //
      //      Console.ReadKey(true);

      //      Console.WriteLine(instance.Solution(new int[] {4, 2, 3}));
      //      Console.WriteLine(instance.Solution(new int[] {4, 2, 1}));

      for (int i = 0; i < 10000; i++)
      {
        var len = 6;
        var arr = new int[len];

        for (int j = 0; j < arr.Length; j++)
        {
          arr[j] = rand.Next(10) + 1;
        }

        bool result = false;

        var codeTimerResult = timer.Time(1, (() => { result = instance.Solution((int[])arr.Clone()); }));

        ShowConsole(new Dictionary<string, object>()
        {
          {nameof(arr), JsonConvert.SerializeObject(arr)},
          {nameof(result), result},
          {nameof(codeTimerResult), codeTimerResult}
        });
      }
    }

    private static void MyLinkedListTest(CodeTimer timer)
    {
      MyLinkedList instance = new MyLinkedList();
      instance.AddAtIndex(1, 2);
      instance.AddAtIndex(0, 1);

      //      instance.AddAtHead(1);//1
      //      instance.AddAtTail(3);//3
      //      instance.AddAtIndex(1,2);//123
      //      instance.Get(1);//2
      //      instance.DeleteAtIndex(1);//13
      //      instance.Get(1);//3
      instance.Show();

      //        ["MyLinkedList","addAtHead","addAtTail","addAtIndex","get","deleteAtIndex","get"]
      //        [[],[1],[3],[1,2],[1],[1],[1]]

      Console.ReadKey(true);

      var codeTimerResult = timer.Time(10, (() =>
      {
        MyLinkedList linkedList = new MyLinkedList();
        linkedList.AddAtHead(1);
        linkedList.Show();
        linkedList.AddAtTail(3);
        linkedList.Show();
        linkedList.AddAtIndex(1, 2); // linked list becomes 1->2->3
        linkedList.Show();
        var result = linkedList.Get(1);
        Console.WriteLine($"get : {result}");
        linkedList.Show();
        linkedList.DeleteAtIndex(1); // now the linked list is 1->3
        linkedList.Show();
        result = linkedList.Get(1); // returns 3
        Console.WriteLine($"get : {result}");
        linkedList.Show();
      }));
      Console.WriteLine(codeTimerResult);
    }

    private static void LargestTimeFromDigitsTest(Random rand, CodeTimer timer)
    {
      var instance = new LargestTimeFromDigits();

      //      instance.Combination(new List<int>(){1,2,3,4}, 0);

      Console.WriteLine(instance.Solution(new[] { 1, 2, 3, 4 }));
      Console.WriteLine(instance.Solution(new[] { 5, 5, 5, 5 }));

      for (int i = 0; i < 100; i++)
      {
        var arr = new int[4];
        for (int j = 0; j < arr.Length; j++)
        {
          arr[j] = rand.Next(10);
        }

        string result = null;
        var codeTimerResult = timer.Time(1, (() => { result = instance.Solution(arr); }));

        ShowConsole(new Dictionary<string, object>()
        {
          {nameof(arr), JsonConvert.SerializeObject(arr)},
          {nameof(result), result},
          {nameof(codeTimerResult), codeTimerResult}
        });
      }
    }
    public static void ShowConsole(Dictionary<string, object> dictionary)
    {
      Console.WriteLine($"\n-----------------S---------------------");

      foreach (var item in dictionary)
      {
        Console.WriteLine($"{item.Key}:{item.Value}");
      }

      Console.WriteLine($"-----------------E---------------------\n");
    }
    private static void LargestSumAfterKNegationsTest(Random rand, CodeTimer timer)
    {
      LargestSumAfterKNegations instacne = new LargestSumAfterKNegations();

      //basic success
      Console.WriteLine(instacne.Solution(new[] { 4, 2, 3 }, 1));
      Console.WriteLine(instacne.Solution(new[] { 3, -1, 0, 2 }, 3));
      Console.WriteLine(instacne.Solution(new[] { 2, -3, -1, 5, -4 }, 2));

      for (int i = 0; i < 100; i++)
      {
        var arrLen = rand.Next(10000) + 1;
        var k = rand.Next(10000) + 1;
        var arr = new int[arrLen];

        for (int j = 0; j < arrLen; j++)
        {
          arr[j] = rand.Next(201) - 100;
        }

        int result = 0;

        var codeTimerResult = timer.Time(10, () => { result = instacne.Solution(arr, k); });

        ShowConsole(new Dictionary<string, object>()
        {
          {"arr", JsonConvert.SerializeObject(arr)},
          {"k", k},
          {"result", result},
          {nameof(codeTimerResult), codeTimerResult}
        });
      }
    }

    private static void ReachNumberTest()
    {
      var instance = new ReachNumber();

      Console.WriteLine(instance.Solution(-3));
      Console.ReadKey(true);

      for (int i = 1; i < 10; i++)
      {
        var result = instance.Solution(i);

        ShowConsole(new Dictionary<string, object>()
        {
          {nameof(i), i},
          {nameof(result), result}
        });
      }
    }

    private static void NumMagicSquaresInsideTest()
    {
      var instance = new NumMagicSquaresInside();

      Console.WriteLine(instance.Solution(new[]
      {
        new int[] {5, 5, 5},
        new int[] {5, 5, 5},
        new int[] {5, 5, 5},
      }));
    }

    private static void PowerfulIntegersTest(Random rand, CodeTimer timer)
    {
      PowerfulIntegers instance = new PowerfulIntegers();

      //basic success
      //      Console.WriteLine(JsonConvert.SerializeObject(instance.Solution(2, 3, 10)));
      //      Console.WriteLine(JsonConvert.SerializeObject(instance.Solution(3, 5, 15)));

      //      Console.WriteLine(JsonConvert.SerializeObject(instance.Solution(6,8,67)));
      //
      //      Console.ReadKey(true);

      for (int i = 0; i < 1000; i++)
      {
        Console.WriteLine("\n-----------------S------------------");
        var x = rand.Next(10) + 1;
        var y = rand.Next(10) + 1;
        var bound = rand.Next(100) + 10;

        Console.WriteLine($"x:{x}");
        Console.WriteLine($"y:{y}");
        Console.WriteLine($"bound:{bound}");

        IList<int> result = null;

        var codeTimerResult = timer.Time(100, (() => { result = instance.Solution(x, y, bound); }));

        Console.WriteLine($"result:{JsonConvert.SerializeObject(result)}");

        Console.WriteLine($"codeTimerResult:{codeTimerResult}");

        Console.WriteLine("-----------------E------------------\n");
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="rand"></param>
    /// <param name="timer"></param>
    private static void HasGroupsSizeXTest(Random rand, CodeTimer timer)
    {
      var instance = new HasGroupsSizeX();

      //      Console.WriteLine(instance.Solution(new[] {1, 2, 3, 4, 4, 3, 2, 1}));
      //      Console.WriteLine(instance.Solution(new[] {1, 1, 1, 2, 2, 2, 3, 3}));
      //      Console.WriteLine(instance.Solution(new[] {1}));
      //      Console.WriteLine(instance.Solution(new[] {1, 1}));
      //      Console.WriteLine(instance.Solution(new[] {1, 1, 2, 2, 2, 2}));

      //      Console.WriteLine(instance.Solution(new []{1, 1, 1, 1, 2, 2, 2, 2, 2, 2}));

      //      Console.ReadKey(true);

      for (int i = 0; i < 100; i++)
      {
        var arrLen = rand.Next(10000) + 1;

        var arr = new int[arrLen];

        for (int j = 0; j < arr.Length; j++)
        {
          arr[j] = rand.Next(2) + 1;
        }

        bool result = false;

        Console.WriteLine("\n------------S---------------");
        var codeTimerResult = timer.Time(1, (() => { result = instance.Solution(arr); }));

        //        Console.WriteLine($"arr:{JsonConvert.SerializeObject(arr)}");
        Console.WriteLine($"result:{result}");
        Console.WriteLine($"time:{codeTimerResult}");
        Console.WriteLine("\n------------E---------------");
      }
    }

    private static void ValidMountainArrayTest()
    {
      ValidMountainArray instance = new ValidMountainArray();

      Console.WriteLine(instance.Solution(new[] { 2, 1 }));
      Console.WriteLine(instance.Solution(new[] { 3, 5, 5 }));
      Console.WriteLine(instance.Solution(new[] { 0, 3, 2, 1 }));
      Console.WriteLine(instance.Solution(new[]
      {
        14, 82, 89, 84, 79, 70, 70, 68, 67, 66, 63, 60, 58, 54, 44, 43, 32, 28, 26, 25, 22, 15, 13, 12, 10, 8, 7, 5, 4,
        3
      }));
    }

    private static void AddToArrayFormOfIntegerTest(CodeTimer timer)
    {
      AddToArrayFormOfInteger instance = new AddToArrayFormOfInteger();

      //      Console.WriteLine(JsonConvert.SerializeObject(instance.SimpleDeal(new[] {3, 0, 6, 7, 6, 1, 0, 5, 7, 6}, 6525)));
      //
      //      Console.ReadKey(true);

      //basic test -- success
      //      var result = instance.AddToArrayForm(new[] {1, 2, 0, 0}, 34);
      //
      //      Console.WriteLine(JsonConvert.SerializeObject(result));
      //
      //      result = instance.AddToArrayForm(new[] { 2, 7, 4 }, 181);
      //
      //      Console.WriteLine(JsonConvert.SerializeObject(result));
      //
      //      result = instance.AddToArrayForm(new[] { 2, 1, 5 }, 806);
      //
      //      Console.WriteLine(JsonConvert.SerializeObject(result));
      //
      //      result = instance.AddToArrayForm(new[] { 9, 9, 9, 9, 9, 9, 9, 9, 9, 9 }, 1);
      //
      //      Console.WriteLine(JsonConvert.SerializeObject(result));

      var rand = new Random();

      for (int i = 0; i < 100; i++)
      {
        var arrLen = rand.Next(10) + 1;
        var arr = new int[arrLen];
        IList<int> result = null;
        var randNum = rand.Next(10000);
        for (int j = 0; j < arr.Length; j++)
        {
          arr[j] = rand.Next(10);
        }

        var codeTimerResult = timer.Time(1, () => { result = instance.AddToArrayForm(arr, randNum); });

        Console.WriteLine("------------S-------------------");

        Console.WriteLine($@"
arr:{JsonConvert.SerializeObject(arr)}
randNum:{randNum}
result:{JsonConvert.SerializeObject(result)}
time:{codeTimerResult}
");

        //        IList<int> realResult = null;
        //        var realCodeTimerResult = timer.Time(1, () => { realResult = instance.SimpleDeal(arr, randNum); });
        //
        //        Console.WriteLine($@"
        //realResult:{JsonConvert.SerializeObject(realResult)}
        //realCodeTimerResult:{realCodeTimerResult}
        //");

        Console.WriteLine("------------E-------------------");
      }
    }

    private static void StringWithoutAaaOrBbbTest()
    {
      StringWithoutAaaOrBbb instance = new StringWithoutAaaOrBbb();

      Console.WriteLine(instance.StrWithout3a3b(1, 2));
      Console.WriteLine(instance.StrWithout3a3b(4, 1));
    }

    private static void FindTheTownJudgeTest()
    {
      FindTheTownJudge instance = new FindTheTownJudge();

      var result = instance.FindJudge(2, new[]
      {
        new[] {1, 2}
      });

      var optimize = instance.Optimize(2, new[]
      {
        new[] {1, 2}
      });

      Console.WriteLine(result);
      Console.WriteLine($"optimize:{optimize}");

      result = instance.FindJudge(3, new[]
      {
        new[] {1, 3},
        new[] {2, 3}
      });

      optimize = instance.Optimize(3, new[]
      {
        new[] {1, 3},
        new[] {2, 3}
      });

      Console.WriteLine(result);
      Console.WriteLine($"optimize:{optimize}");

      result = instance.FindJudge(3, new[]
      {
        new[] {1, 3},
        new[] {2, 3},
        new[] {3, 1},
      });

      optimize = instance.Optimize(3, new[]
      {
        new[] {1, 3},
        new[] {2, 3},
        new[] {3, 1},
      });

      Console.WriteLine(result);
      Console.WriteLine($"optimize:{optimize}");

      result = instance.FindJudge(3, new[]
      {
        new[] {1, 2},
        new[] {2, 3},
      });

      optimize = instance.Optimize(3, new[]
      {
        new[] {1, 2},
        new[] {2, 3},
      });

      Console.WriteLine(result);
      Console.WriteLine($"optimize:{optimize}");

      result = instance.FindJudge(4, new[]
      {
        new[] {1, 3},
        new[] {1, 4},
        new[] {2, 3},
        new[] {2, 4},
        new[] {4, 3},
      });

      optimize = instance.Optimize(4, new[]
      {
        new[] {1, 3},
        new[] {1, 4},
        new[] {2, 3},
        new[] {2, 4},
        new[] {4, 3},
      });

      Console.WriteLine(result);
      Console.WriteLine($"optimize:{optimize}");

      Console.WriteLine();
    }

    private static void FindCommonCharactersTest(CodeTimer timer)
    {
      var instance = new FindCommonCharacters();

      var commonChars = instance.CommonChars(new string[]
      {
        "bella", "label", "roller"
      });

      Console.WriteLine(JsonConvert.SerializeObject(commonChars));

      commonChars = instance.CommonChars(new string[]
      {
        "cool", "lock", "cook"
      });

      Console.WriteLine(JsonConvert.SerializeObject(commonChars));

      var rand = new Random();

      for (int i = 0; i < 100; i++)
      {
        var randArrNum = rand.Next(4) + 100;
        var strNum = rand.Next(5) + 100;
        IList<string> result = null;
        IList<string> rival = null;
        string[] arr = new string[randArrNum];

        for (int j = 0; j < randArrNum; j++)
        {
          StringBuilder builder = new StringBuilder();
          for (int k = 0; k < strNum; k++)
          {
            builder.Append((char)(rand.Next(26) + 97));
          }

          arr[j] = builder.ToString();
        }

        var spendTime = timer.Time(1, (() => { result = instance.CommonChars(arr); }));
        var spendTime2 = timer.Time(1, (() => { rival = instance.OtherSolution(arr); }));

        //        arr: { JsonConvert.SerializeObject(arr)}

        Console.WriteLine($@"
{spendTime}
result:{JsonConvert.SerializeObject(result)}

<rival>
{spendTime2}
result:{JsonConvert.SerializeObject(rival)}

-------------------------------------------------------------

");
      }
    }

    private static void AddBinaryTest()
    {
      AddBinary instance = new AddBinary();
      instance.OtherSolution("1010", "1011");
      instance.OtherSolution("11", "0");
    }

    private static void IntegerToRomanTest()
    {
      var instance = new IntegerToRoman();

      var testInstance = new RomanToInteger();

      var rand = new Random();

      for (int i = 0; i < 1000; i++)
      {
        var num = rand.Next(10000);
        var solution = instance.Solution(num);

        var realNum = testInstance.Optimize(solution);

        Console.WriteLine($"roman:{solution},num:{num},realNum:{realNum},success:{num == realNum}");
      }
    }

    private static void SimpleDateDiffTest()
    {
      var startDate = new DateTime(2018, 7, 1, 0, 0, 0);
      var endDate = new DateTime(2019, 2, 1, 0, 0, 0);

      var list = new List<string>();

      for (int i = startDate.Year; i <= endDate.Year; i++)
      {
        for (int j = i > startDate.Year ? 1 : startDate.Month; j <= (i == endDate.Year ? endDate.Month : 12); j++)
        {
          list.Add($"{i}{(j < 10 ? "0" : "")}{j}");
        }
      }

      Console.WriteLine(JsonConvert.SerializeObject(list));
    }

    private static void ConvertANumberToHexadecimalTest(CodeTimer timer)
    {
      //      Console.WriteLine(0xffff);
      //      Console.WriteLine(int.MaxValue);
      //
      //      Console.WriteLine(Convert.ToString(0, 16));
      //      Console.WriteLine(Convert.ToString(1, 16));
      //      Console.WriteLine(Convert.ToString(-1, 16));
      //      Console.WriteLine(Convert.ToString(-2, 16));
      //      Console.WriteLine(Convert.ToString(-3, 16));
      //      Console.WriteLine(Convert.ToString(0xffffffff, 16));

      ConvertANumberToHexadecimal instance = new ConvertANumberToHexadecimal();

      Console.WriteLine(instance.ToHex(26));

      Console.WriteLine(instance.ToHex(-1));

      var rand = new Random();

      var successCount = 0;

      for (int i = 0; i < 1000; i++)
      {
        var num = rand.Next(int.MaxValue);

        num *= rand.Next(2) == 0 ? 1 : -1;

        Console.WriteLine($"\n\n传入num:{num}");

        var result = timer.Time(1, (() => { Console.WriteLine(instance.ToHex(num)); }));

        Console.WriteLine($"耗费时间：{result}");

        var result2 = timer.Time(1, (() => { Console.WriteLine($"正确答案：{Convert.ToString(num, 16)}"); })); //使用原有帮助类处理

        Console.WriteLine($"帮助类-耗费时间：{result}");

        if (result.TimeElapsed <= result2.TimeElapsed)
        {
          successCount++;
        }
      }

      Console.WriteLine($"成功次数：{successCount}");
    }

    private static void RomanToIntegerTest(CodeTimer timer)
    {
      var instance = new RomanToInteger();

      //success
      //      Console.WriteLine(instance.RomanToInt("III"));
      //      Console.WriteLine(instance.RomanToInt("IV"));
      //      Console.WriteLine(instance.RomanToInt("IX"));
      //      Console.WriteLine(instance.RomanToInt("LVIII"));
      //      Console.WriteLine(instance.RomanToInt("MCMXCIV"));
      //
      //      Console.WriteLine(instance.RomanToInt("CXCXL"));
      //
      //      Console.ReadKey(true);

      var rand = new Random();
      var charArr = new[]
      {
        'I',
        'V',
        'X',
        'L',
        'C',
        'D',
        'M'
      };

      var randCount = 10;
      var fixCount = 4;
      for (int i = 0; i < 10; i++)
      {
        var len = rand.Next(randCount) + fixCount;

        var arr = new char[len];

        for (int j = 0; j < len; j++)
        {
          arr[j] = charArr[rand.Next(charArr.Length)];
        }

        var str = new string(arr);

        Console.WriteLine($"\n随机生成字符串：{str}\n");

        var result = timer.Time(1, (() => { Console.WriteLine($"[RomanToInt]计算结果：{instance.RomanToInt(str)}"); }));

        Console.WriteLine(result);

        Console.WriteLine("--------------------------");

        result = timer.Time(1, (() => { Console.WriteLine($"[Optimize]计算结果：{instance.Optimize(str)}"); }));

        Console.WriteLine(result);

        Console.WriteLine("--------------------------");
      }
    }

    private static void GroupCount()
    {
      var testCount = 10;

      var arr = new List<dynamic>();

      var rand = new Random();

      for (int i = 0; i < 1000000; i++)
      {
        arr.Add(new { sex = rand.Next(2), age = rand.Next(100) });
      }

      var timer = new CodeTimer();

      var result = timer.Time(testCount, (() =>
      {
        //        Console.WriteLine("-------------------------linq-----------------------");
        var enumerable = arr.GroupBy(u => u.sex).Select(u => new { u.Key, count = u.Count() });
        foreach (var item in enumerable)
        {
          //          Console.WriteLine($"性别：{(item.Key == 0 ? "女" : "男")}，数量：{item.count}");
        }

        //
        //        Console.WriteLine();
      }));

      Console.WriteLine("-------------------------linq-----------------------"); //效率最低。。。
      Console.WriteLine(result);

      result = timer.Time(testCount, (() =>
      {
        var countArr = new int[2];

        foreach (var item in arr)
        {
          countArr[item.sex == 0 ? 0 : 1]++;
        }

        foreach (var item in countArr)
        {
        }

        //        Console.WriteLine("-------------------------arr-----------------------");
        //        Console.WriteLine($"性别男，数量：{countArr[1]}");
        //        Console.WriteLine($"性别女，数量：{countArr[0]}");
        //        Console.WriteLine();
      }));

      Console.WriteLine("-------------------------arr-----------------------"); //arr效率最高
      Console.WriteLine(result);

      result = timer.Time(testCount, (() =>
      {
        //        Console.WriteLine("-------------------------dictionary-----------------------");
        var countDic = new Dictionary<int, int>();

        foreach (var item in arr)
        {
          if (countDic.ContainsKey(item.sex))
          {
            countDic[item.sex]++;
          }
          else
          {
            countDic.Add(item.sex, 1);
          }
        }

        foreach (var item in countDic)
        {
          //          Console.WriteLine($"性别：{(item.Key == 0 ? "女" : "男")}，数量：{item.Value}");
        }

        //
        //        Console.WriteLine();
      }));

      Console.WriteLine("-------------------------dictionary-----------------------"); //字典效率第二
      Console.WriteLine(result);
    }

  }
}
