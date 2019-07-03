using System;
using System.Collections.Generic;
using System.Text;
using ConsoleTest.Domain;
using ConsoleTest.Domain.StructModel;
using ConsoleTest.DP;
using ConsoleTest.Funny;
using ConsoleTest.HardQuestion;
using ConsoleTest.MiddleQuestion;
using ConsoleTest.MiddleQuestionThree;
using ConsoleTest.MiddleQuestionTwo;
using Newtonsoft.Json;
using Tools.CusTools;
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

      Queue<int> queue = new Queue<int>();

      KthSmallestPrimeFraction instance = new KthSmallestPrimeFraction();

      Console.WriteLine(instance.Solution(new[] {1, 2, 3, 5}, 6));

      int[] result = null;

      var codeTimerResult = timer.Time(1, (() => { result = instance.Solution(new int[]
      {
          29761, 29789, 29819, 29833, 29863, 29867, 29879, 29881, 29917, 29921, 29927, 29959, 29983

      }, 453785); }));

      ShowConsole(new Dictionary<string, object>()
      {
        {nameof(codeTimerResult), codeTimerResult},
        {nameof(result), JsonConvert.SerializeObject(result)}
      });

      Console.WriteLine("Hello World");
      Console.ReadKey(true);
    }

    private static void TestLongestIncreasingPath()
    {
      LongestIncreasingPath instance = new LongestIncreasingPath();

      Console.WriteLine(instance.Solution(new[]
      {
        new[] {9, 9, 4},
        new[] {6, 6, 8},
        new[] {2, 1, 1},
      })); //4

      Console.WriteLine(instance.Solution(new[]
      {
        new[] {3, 4, 5},
        new[] {3, 2, 6},
        new[] {2, 2, 1}
      })); //4
    }

    private static void TestCodec()
    {
      Codec instance = new Codec();

      var treeNode = instance.deserialize("[1,2,3,null,null,4,5]");

      treeNode = instance.deserialize("[1,2,3,null,null,4,5,1,2,3,4]");

      Console.WriteLine(treeNode);

      Console.WriteLine(instance.serialize(treeNode));
    }

    private static void TestLongestConsecutive()
    {
      LongestConsecutive instance = new LongestConsecutive();

      Console.WriteLine(instance.OtherSolution(new[] {1, 2, 0, 1}));
    }

    private static void TestFindKthNumber(Random rand, CodeTimer timer)
    {
      FindKthNumber findKthNumber = new FindKthNumber();

      Console.WriteLine(findKthNumber.OtherSolution(5, 9, 4));
      Console.WriteLine(findKthNumber.Solution(9, 5, 14));
      //      Console.WriteLine(findKthNumber.Solution(3, 10, 23));

      Console.ReadKey(true);

      for (int i = 0; i < 100; i++)
      {
        var row = rand.Next(10) + 1;
        var col = rand.Next(10) + 1;
        var index = rand.Next(row * col) + 1;
        int res = 0;
        int realRes = findKthNumber.Simple(row, col, index);

        var codeTimerResult = timer.Time(1, (() => { res = findKthNumber.Solution(row, col, index); }));

        ShowConsole(new Dictionary<string, object>()
        {
          {nameof(row), row},
          {nameof(col), col},
          {nameof(index), index},
          {nameof(res), res},
          {nameof(realRes), realRes},
          {nameof(codeTimerResult), codeTimerResult},
        });

        if (res != realRes)
          throw new Exception("result not compare!");
      }
    }

    private static void TestLargestIsland()
    {
      LargestIsland instance = new LargestIsland();

      Console.WriteLine(instance.Solution(new[]
      {
        new[] {0, 1},
        new[] {0, 1}
      }));

      Console.WriteLine(instance.Solution(new[]
      {
        new[] {0, 0, 0, 0, 0, 0, 0},
        new[] {0, 1, 1, 1, 1, 0, 0},
        new[] {0, 1, 0, 0, 1, 0, 0},
        new[] {1, 0, 1, 0, 1, 0, 0},
        new[] {0, 1, 0, 0, 1, 0, 0},
        new[] {0, 1, 0, 0, 1, 0, 0},
        new[] {0, 1, 1, 1, 1, 0, 0}
      })); //18

      Console.WriteLine(instance.Solution(new[]
      {
        new[] {1, 1},
        new[] {1, 0}
      })); //4

      Console.WriteLine(instance.Solution(new[]
      {
        new[] {0, 1, 0},
        new[] {1, 0, 1},
        new[] {1, 0, 0}
      })); //5

      Console.WriteLine(instance.Solution(new[]
      {
        new[] {1, 0},
        new[] {0, 1}
      })); //3

      Console.WriteLine(instance.Solution(new[]
      {
        new[] {1, 0, 1},
        new[] {0, 1, 1},
        new[] {0, 1, 1}
      })); //7
    }

    private static void TestFindSecretWord()
    {
      var instance = new FindSecretWord();

      instance.OptimizeRand(
        new[]
        {
          "wichbx", "oahwep", "tpulot", "eqznzs", "vvmplb", "eywinm", "dqefpt", "kmjmxr", "ihkovg", "trbzyb", "xqulhc",
          "bcsbfw", "rwzslk", "abpjhw", "mpubps", "viyzbc", "kodlta", "ckfzjh", "phuepp", "rokoro", "nxcwmo", "awvqlr",
          "uooeon", "hhfuzz", "sajxgr", "oxgaix", "fnugyu", "lkxwru", "mhtrvb", "xxonmg", "tqxlbr", "euxtzg", "tjwvad",
          "uslult", "rtjosi", "hsygda", "vyuica", "mbnagm", "uinqur", "pikenp", "szgupv", "qpxmsw", "vunxdn", "jahhfn",
          "kmbeok", "biywow", "yvgwho", "hwzodo", "loffxk", "xavzqd", "vwzpfe", "uairjw", "itufkt", "kaklud", "jjinfa",
          "kqbttl", "zocgux", "ucwjig", "meesxb", "uysfyc", "kdfvtw", "vizxrv", "rpbdjh", "wynohw", "lhqxvx", "kaadty",
          "dxxwut", "vjtskm", "yrdswc", "byzjxm", "jeomdc", "saevda", "himevi", "ydltnu", "wrrpoc", "khuopg", "ooxarg",
          "vcvfry", "thaawc", "bssybb", "ccoyyo", "ajcwbj", "arwfnl", "nafmtm", "xoaumd", "vbejda", "kaefne", "swcrkh",
          "reeyhj", "vmcwaf", "chxitv", "qkwjna", "vklpkp", "xfnayl", "ktgmfn", "xrmzzm", "fgtuki", "zcffuv", "srxuus",
          "pydgmq"
        }, new Master("ccoyyo"));
    }

    private static void TestCountOfAtoms()
    {
      CountOfAtoms instance = new CountOfAtoms();

      Console.WriteLine(instance.Solution("Mg(OH)2"));
      Console.WriteLine(instance.Solution("K4(ON(SO3)2)2"));
    }

    private static void TestMaxChunksToSorted()
    {
      MaxChunksToSorted instance = new MaxChunksToSorted();

      var res = instance.OtherSolution(new[] {3, 1, 2, 4, 4, 8, 4, 9, 3, 7, 1, 5, 6, 2, 7, 12}); //2

      Console.WriteLine(res);

      Console.WriteLine(instance.OtherSolution(new[] {5, 4, 3, 2, 1})); //1
      Console.WriteLine(instance.OtherSolution(new[] {2, 1, 3, 4, 4})); //4

      Console.WriteLine(instance.OtherSolution(new[] {0, 0, 1, 1, 1})); //5

      Console.WriteLine(instance.OtherSolution(new[] {1, 0, 1, 3, 2})); //3

      Console.WriteLine(instance.OtherSolution(new[] {0, 3, 0, 3, 2})); //2

      Console.WriteLine(instance.OtherSolution(new[] {0, 2, 1, 4, 3})); //3
    }

    private static void TestMinSwapsCouples()
    {
      MinSwapsCouples instance = new MinSwapsCouples();

      Console.WriteLine(instance.Solution(new[] {5, 6, 4, 0, 2, 1, 9, 3, 8, 7, 11, 10}));
    }

    private static void TestFreqStack()
    {
      FreqStack instance = new FreqStack();

      instance.Push(5);
      instance.Push(7);
      instance.Push(5);
      instance.Push(7);
      instance.Push(4);
      instance.Push(5);

      Console.WriteLine(instance.Pop());
      Console.WriteLine(instance.Pop());
      Console.WriteLine(instance.Pop());
      Console.WriteLine(instance.Pop());
    }

    private static void TestFibonacci()
    {
      for (int i = 0; i < 10; i++)
      {
        Console.WriteLine(Fibonacci.GetResult(i));
      }
    }

    private static void TestRecoverFromPreorder()
    {
      RecoverFromPreorder instance = new RecoverFromPreorder();

      Console.WriteLine(instance.Solution("1-401--349---90--88")); //success
      Console.WriteLine(instance.Solution("1-2--3--4-5--6--7")); //success
    }

    private static void TestFractionToDecimal()
    {
      FractionToDecimal instance = new FractionToDecimal();

      //      Console.WriteLine(instance.Solution(1, 17));
      //      Console.WriteLine(instance.Solution(22, 7));
      Console.WriteLine(instance.Solution(4, 333));
      Console.WriteLine(instance.Solution(100, 3));
      Console.WriteLine(instance.Solution(1, 2));
    }

    private static void TestNumDecodings()
    {
      NumDecodings instance = new NumDecodings();

      Console.WriteLine(instance.Solution("1211221"));
    }

    private static void TestSolve()
    {
      Solve instance = new Solve();

      instance.Solution(new[]
      {
        new[] {'O', 'X', 'O', 'O', 'O', 'X'},
        new[] {'O', 'O', 'X', 'X', 'X', 'O'},
        new[] {'X', 'X', 'X', 'X', 'X', 'O'},
        new[] {'O', 'O', 'O', 'O', 'X', 'X'},
        new[] {'X', 'X', 'O', 'O', 'X', 'O'},
        new[] {'O', 'O', 'X', 'X', 'X', 'X'},
      });

      instance.Solution(new[]
      {
        new[] {'X', 'X', 'X', 'X'},
        new[] {'X', 'O', 'O', 'X'},
        new[] {'X', 'X', 'O', 'X'},
        new[] {'X', 'O', 'X', 'X'}
      });
    }

    private static void TestDecodeAtIndex()
    {
      DecodeAtIndex instance = new DecodeAtIndex();

      //      var str = instance.ShowStr("cpmxv8ewnfk3xxcilcmm68d2ygc88daomywc3imncfjgtwj8nrxjtwhiem5nzqnicxzo248g52y72v3yujqpvqcssrofd99lkovg", 0);
      //
      //      Console.WriteLine(str);
      //
      //      Console.WriteLine(str[480551547 - 1]);

      Console.WriteLine(instance.Optimize3("a23", 6));

      Console.WriteLine(instance.Optimize3(
        "gc8hoa2l4lyc7cx6grev7o2qgmolppnwwgexaur2v8paml69syh2tavusb4jthoqelszpmkq2l3jem2aezlhy5c8uaibvyowbjb2",
        874960845)); //o

      Console.WriteLine(instance.Optimize3(
        "cpmxv8ewnfk3xxcilcmm68d2ygc88daomywc3imncfjgtwj8nrxjtwhiem5nzqnicxzo248g52y72v3yujqpvqcssrofd99lkovg",
        480551547)); //x

      Console.WriteLine(instance.Optimize3("vzpp636m8y", 2920)); //z

      Console.WriteLine(instance.Optimize3("a2b3c4d5e6f7g8h9", 3)); //b

      Console.WriteLine(instance.Optimize3("leet2code3", 10)); //o
      Console.WriteLine(instance.Optimize3("ha22", 5)); //h
      Console.WriteLine(instance.Optimize3("a2345678999999999999999", 1)); //a

      Console.WriteLine("------------------------------");

      Console.WriteLine(instance.Solution(
        "gc8hoa2l4lyc7cx6grev7o2qgmolppnwwgexaur2v8paml69syh2tavusb4jthoqelszpmkq2l3jem2aezlhy5c8uaibvyowbjb2",
        874960845)); //o

      Console.WriteLine(instance.Solution(
        "cpmxv8ewnfk3xxcilcmm68d2ygc88daomywc3imncfjgtwj8nrxjtwhiem5nzqnicxzo248g52y72v3yujqpvqcssrofd99lkovg",
        480551547)); //x

      Console.WriteLine(instance.Solution("vzpp636m8y", 2920)); //z

      Console.WriteLine(instance.Solution("a2b3c4d5e6f7g8h9", 3)); //b

      Console.WriteLine(instance.Solution("leet2code3", 10)); //o
      Console.WriteLine(instance.Solution("ha22", 5)); //h
      Console.WriteLine(instance.Solution("a2345678999999999999999", 1)); //a
    }

    private static void TestSmallestRangeII()
    {
      SmallestRangeII instance = new SmallestRangeII();

      Console.WriteLine(instance.OtherSolution(new[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10}, 1));
    }

    private static void TestLadderLength()
    {
      LadderLength instance = new LadderLength();

      Console.WriteLine(instance.Simple("hit", "cog", new List<string>()
      {
        "hot", "dot", "dog", "lot", "log", "cog"
      }));
    }

    private static void TestLargestNumber()
    {
      var str1 =
        "98909827968595339456944893859149094902689398937839883538183810810780707982784676057536747174237321720571007032685668066758674466986636554651163276306626562416221603859725909578457125682552954605422520849804812479847044453428339323905384638363699366436503636357535673516346233993298316330843021297028227452732697246523622362231322281216213206020001921763154815181495141713801147114310901048";

      var str2 =
        "98909827968595339456944893859149094902689398937839883538183810810780707982784676057536747174237321720571007032685668066758674466986636554651163276306626562416221603859725909578457125682552954605422520849804812479847044453428339323905384638363699366436503636357535673516346233993298316330843021297028227452732697246523622362231322812216213206020001921763154815181495141713801147114310901048";

      Console.WriteLine(str1.Equals(str2));

      bool flag = false;
      for (int i = 0; i < str1.Length; i++)
      {
        if (str1[i] != str2[i])
        {
          Console.WriteLine(i);
          flag = true;
        }

        if (flag) Console.WriteLine($"c:{str1[i]},c2:{str2[i]}");
      }


      LargestNumber instance = new LargestNumber();

      Console.WriteLine(instance.Solution(new[] {121, 12}));

      //      Console.WriteLine(instance.Solution(new []{ 1, 20 }));
      //
      //      Console.WriteLine(instance.Solution(new[]
      //      {
      //        2331, 9511, 5618, 6542, 2387, 7224, 5955, 9267, 8454, 4178, 4399, 183, 3314, 6191, 3993, 9334, 5608, 5610, 5123,
      //        3649, 6003, 8321, 6744, 5974, 1849, 7250, 6533, 7960, 7968, 9034, 1568, 9209, 1358, 3920, 5422, 203, 468, 7441,
      //        435, 9277, 1984, 7326, 9044, 3398, 1501, 2139, 1991, 6612, 2039, 184, 2666, 4648, 7896, 5698, 352, 7319, 7546,
      //        5654, 440, 4755, 4365, 6702, 8568, 9949, 49, 5235, 7306, 1870, 4667, 6787, 9866, 6188, 9836, 1970, 8424, 473,
      //        1345, 275, 8634, 7553, 9971, 2085, 9910, 9602, 8960, 5917, 6795, 3822, 883, 224, 5012, 8353, 8529, 8423, 9679,
      //        6096, 5501, 6116, 1245, 3923
      //      }));
      //
      //      Console.WriteLine(instance.Solution(new[] {3, 30, 34, 5, 9}));
    }

    private static void TestLongestPalindrome()
    {
      LongestPalindrome instance = new LongestPalindrome();

      Console.WriteLine(instance.Solution("a"));
    }

    private static void TestFind132pattern()
    {
      Find132pattern instance = new Find132pattern();

      Console.WriteLine(instance.Solution3(new[] {1, 2, 3, 4}));

      Console.WriteLine(instance.Solution(new[] {3, 1, 4, 2}));
    }

    private static void TestWiggleSort()
    {
      WiggleSort instance = new WiggleSort();

      instance.Solution(new[] {1, 5, 1, 1, 6, 4});
    }

    private static void TestSpiralOrder()
    {
      SpiralOrder instance = new SpiralOrder();

      Console.WriteLine(instance.Solution(new[]
      {
        new[] {1, 2, 3, 4},
        new[] {5, 6, 7, 8},
        new[] {9, 10, 11, 12},
      }));
    }

    private static void TestReorderList()
    {
      ReorderList instance = new ReorderList();

      instance.Simple(new[] {1, 2, 3, 4});
    }

    private static void TestExist()
    {
      Exist instance = new Exist();

      Console.WriteLine(instance.Solution(new[]
      {
        new[] {'A', 'B', 'C', 'E'},
        new[] {'S', 'F', 'C', 'S'},
        new[] {'A', 'D', 'E', 'E'},
      }, "ABCCED"));
    }

    private static void TestIntegerReplacement()
    {
      IntegerReplacement instance = new IntegerReplacement();

      Console.WriteLine(instance.Solution(65535));
    }

    private static void TestEvalRPN()
    {
      EvalRPN instance = new EvalRPN();

      Console.WriteLine(instance.Solution2(new[]
        {"10", "6", "9", "3", "+", "-11", "*", "/", "*", "17", "+", "5", "+"}));
    }

    private static void TestVerticalTraversal()
    {
      var list = new List<int>();
      list.Insert(0, 1);

      VerticalTraversal instance = new VerticalTraversal();

      Console.WriteLine(instance.Solution(new TreeNode(0,
        new TreeNode(2, new TreeNode(3, new TreeNode(4, null, 7), new TreeNode(5, 6, null)), null),
        1)));

      Console.WriteLine(instance.Solution(new TreeNode(3, 9, new TreeNode(20, 15, 7))));
    }

    private static void TestCheckValidString(Random rand, CodeTimer timer)
    {
      CheckValidString instance = new CheckValidString();

      for (int i = 0; i < 100; i++)
      {
        var len = rand.Next(8);

        var build = new StringBuilder();

        for (int j = 0; j < len; j++)
        {
          switch (rand.Next(3))
          {
            case 0:
              build.Append('(');
              break;
            case 1:
              build.Append('*');
              break;
            case 2:
              build.Append(')');
              break;
          }

          bool result = false;

          var codeTimerResult = timer.Time(1, () => { result = instance.Solution(build.ToString()); });

          ShowConsole(new Dictionary<string, object>()
          {
            {nameof(build), build},
            {nameof(result), result},
            {nameof(codeTimerResult), codeTimerResult}
          });
        }
      }
    }

    private static void TestDeleteDuplicates()
    {
      DeleteDuplicates instance = new DeleteDuplicates();

      Console.WriteLine(instance.Simple(new[] {1, 2, 3, 3, 4, 4, 5}));
    }

    private static void TestMatrixGame()
    {
      MatrixGame game = new MatrixGame();

      var arr = new int[3][];

      for (int i = 0; i < arr.Length; i++)
      {
        arr[i] = new int[] {1, 2, 3};
      }

      game.Run(arr);
    }

    private static void TestCalculate()
    {
      Calculate instance = new Calculate();

      Console.WriteLine(instance.Clear(" 3+5 / 2 "));
      Console.WriteLine(instance.Solution("3+2*2"));
    }

    private static void TestCanTransform()
    {
      CanTransform instance = new CanTransform();

      Console.WriteLine(instance.Solution("RL", "LR"));
    }

    private static void TestFindNumberOfLIS()
    {
      FindNumberOfLIS instance = new FindNumberOfLIS();

      Console.WriteLine(instance.Solution(new[] {1, 2, 4, 3, 5, 4, 7, 2}));
    }

    private static void TestSearchRange()
    {
      SearchRange instance = new SearchRange();
      Console.WriteLine(instance.Solution2(new[] {5, 7, 7, 8, 8, 10}, 8));
      Console.WriteLine(instance.Solution2(new[] {5, 7, 7, 8, 8, 10}, 6));
    }

    private static void TestKSmallestPairs()
    {
      KSmallestPairs instance = new KSmallestPairs();

      Console.WriteLine(instance.Solution(new[] {1, 2, 4, 5, 6}, new[] {3, 5, 7, 9}, 20));

      Console.WriteLine(instance.Solution(new[] {1, 2, 4, 5, 6}, new[] {3, 5, 7, 9}, 3));

      Console.WriteLine(instance.Solution(new[] {1, 1, 2}, new[] {1, 2, 3}, 10));
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