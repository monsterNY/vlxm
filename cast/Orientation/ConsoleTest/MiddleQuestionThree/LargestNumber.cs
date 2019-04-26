using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleTest.MiddleQuestionThree
{
  /// <summary>
  /// @desc : LargestNumber  
  /// @author : mons
  /// @create : 2019/4/26 14:16:39 
  /// @source : https://leetcode.com/problems/largest-number/
  /// </summary>
  [Obsolete]
  public class LargestNumber
  {
    public string Solution(int[] nums)
    {
      var res = nums.Select(u => u.ToString()).OrderBy((u => u), new CompareStr()).ToArray();

      StringBuilder builder = new StringBuilder();

      if (res[0] == "0") return "0";

      foreach (var str in res)
      {
        builder.Append(str);
      }

      return builder.ToString();
    }
  }

  //bug
  public class CompareStr : IComparer<string>
  {
    public int Compare(string x, string y)
    {

      //Runtime: 128 ms, faster than 72.94% of C# online submissions for Largest Number.
      //Memory Usage: 25 MB, less than 50.00 % of C# online submissions for Largest Number.

      //other Solution wc
      String s1 = x + y;
      String s2 = y + x;
      return s1.CompareTo(s2);

      //      var res = 0;
      //      for (int i = 0, j = 0;;)
      //      {
      //        res *= 10;
      //        if (i == j)
      //        {
      //          if (x[i] != y[i])
      //            res += x[i] - y[j];
      //        }
      //        else if (i > j)
      //        {
      //          res += x[i] == y[0] ? 1 : x[i] - y[0];
      //        }
      //        else
      //        {
      //          res += x[0] == y[j] ? -1 : x[0] - y[j];
      //        }
      //
      //        if (i == x.Length - 1 && j == y.Length - 1) break;
      //
      //        if (i < x.Length - 1) i++;
      //        if (j < y.Length - 1) j++;
      //      }
      //
      //      Console.WriteLine($"x:{x},y:{y},res:{res}");
      //
      //      return res;
    }
  }
}