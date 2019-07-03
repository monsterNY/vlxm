using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.HardQuestion
{
  /// <summary>
  /// @desc : KthSmallestPrimeFraction  
  /// @author : mons
  /// @create : 2019/7/2 15:50:02 
  /// @source :
  /// @otherSolution : https://leetcode.com/problems/k-th-smallest-prime-fraction/discuss/115819/Summary-of-solutions-for-problems-%22reducible%22-to-LeetCode-378
  /// </summary>
  [Obsolete]
  public class KthSmallestPrimeFraction
  {
    public int[] Solution(int[] A, int K)
    {
      var len = A.Length;

      if (K == 1) return new[] {A[0], A[len - 1]};

      Dictionary<int, int> dictionary = new Dictionary<int, int>();

      int i = 0, j = len - 1;

      while (K-- > 0)
      {
        if (!dictionary.ContainsKey(j)) dictionary.Add(j, 0);
        else i = dictionary[j];

        for (int k = j + 1; k < len; k++)
        {
          if (A[i] / (double) A[j] > A[dictionary[k]] / (double) A[k])
          {
            j = k;
            i = dictionary[k];
          }
        }

        Console.Write($"i:{A[i]},j:{A[j]}\t");
        dictionary[j]++;
        if (j == 1) j = len - 1;
        else j--;
      }

      return new[] {A[i], A[j]};
    }

    // time limit
    public int[] Try(int[] A, int K)
    {
      var len = A.Length;

      if (K == 1) return new[] {A[0], A[len - 1]};

      int[][] minArr = new int[len - 1][];

      for (int i = 0; i < len - 1; i++)
      {
        minArr[i] = new[] {0, len - i - 1};
      }

      int[] min = minArr[0];

      while (K-- > 0)
      {
        var minNum = 1d;
        for (int i = 0; i < len - 1; i++)
        {
          var num = A[minArr[i][0]] / (double) A[minArr[i][1]];
          if (num < minNum)
          {
            minNum = num;
            min = minArr[i];
          }
        }

        Console.WriteLine($"p:{min[0]},q:{min[1]}");

        min[0]++;
      }

      min[0]--;
      return new[] {A[min[0]], A[min[1]]};
    }
  }
}