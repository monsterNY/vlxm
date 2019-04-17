using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestionTwo
{
  /// <summary>
  /// @desc : NumsSameConsecDiff  
  /// @author :mons
  /// @create : 2019/4/17 17:43:00 
  /// @source : https://leetcode.com/problems/numbers-with-same-consecutive-differences/
  /// </summary>
  public class NumsSameConsecDiff
  {

    /**
     * Runtime: 212 ms, faster than 100.00% of C# online submissions for Numbers With Same Consecutive Differences.
     * Memory Usage: 24 MB, less than 66.67% of C# online submissions for Numbers With Same Consecutive Differences.
     *
     * nice.
     *
     */
    public int[] Solution(int N, int K)
    {

      if (N == 1) return new[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};

      List<int> list = new List<int>();

      for (int i = 1; i < 10; i++)
      {
        Dfs(i, K, list, N - 1);
      }

      return list.ToArray();
    }

    public void Dfs(int num, int k, List<int> res, int n)
    {
      if (n == 0)
      {
        res.Add(num);
        return;
      }

      int prev = num % 10, diff1 = prev + k, diff2 = prev - k;

      if (diff1 == diff2)
      {
        if (diff1 < 10 && diff1 >= 0)
        {
          Dfs(num * 10 + diff1, k, res, n - 1);
        }
      }
      else
      {
        if (diff1 < 10)
        {
          Dfs(num * 10 + diff1, k, res, n - 1);
        }

        if (diff2 >= 0)
        {
          Dfs(num * 10 + diff2, k, res, n - 1);
        }
      }
    }
  }
}