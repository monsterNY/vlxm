using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.LeetCode
{
  /// <summary>
  /// @desc : LargestSumAfterKNegations  
  /// @author :mons
  /// @create : 2019/3/12 16:49:23 
  /// @source : https://leetcode.com/problems/maximize-sum-of-array-after-k-negations/
  /// </summary>
  public class LargestSumAfterKNegations
  {
    public int Solution(int[] A, int K)
    {
      var sum = 0;

      var positiveArr = new List<int>();

      var negativeArr = new List<int>();

      int min = int.MaxValue;

      foreach (var item in A)
      {
        if (item >= 0)
        {
          positiveArr.Add(item);
          if (item < min)
            min = item;
        }
        else
          negativeArr.Add(item);

        sum += item;
      }

      negativeArr.Sort();

      for (int i = 0; i < negativeArr.Count && i < K; i++)
      {
        var value = -negativeArr[i];
        sum += 2 * value;
        if (value < min)
          min = value;
      }

      if (K > negativeArr.Count && (K - negativeArr.Count) % 2 == 1)
        sum -= 2 * min;

      return sum;
    }

    /**
     * Runtime: 96 ms, faster than 91.51% of C# online submissions for Maximize Sum Of Array After K Negations.
     * Memory Usage: 22.8 MB, less than 100.00% of C# online submissions for Maximize Sum Of Array After K Negations.
     *
     * 这么简单的。。。？？？
     *
     * Runtime: 92 ms, faster than 100.00% of C# online submissions for Maximize Sum Of Array After K Negations.
     * Memory Usage: 22.7 MB, less than 100.00% of C# online submissions for Maximize Sum Of Array After K Negations.
     *
     * what??? 还好没想复杂
     *
     */
    public int Optimize(int[] A, int K)
    {
//      var positiveArr = new List<int>();

      var negativeArr = new List<int>();

      int min = int.MaxValue, value, sum = 0;

      foreach (var item in A)
      {
        if (item >= 0)
        {
//          positiveArr.Add(item);
          if (item < min)
            min = item;
        }
        else
          negativeArr.Add(item);

        sum += item;
      }

      if (K < negativeArr.Count)
        negativeArr.Sort();

      for (int i = 0; i < negativeArr.Count && i < K; i++)
      {
        value = -negativeArr[i];
        sum += 2 * value;
        if (value < min)
          min = value;
      }

      if (min > 0 && K > negativeArr.Count && (K - negativeArr.Count) % 2 == 1)
        sum -= 2 * min;

      return sum;
    }
  }
}