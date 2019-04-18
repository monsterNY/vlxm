using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleTest.Domain.StructModel;

namespace ConsoleTest.MiddleQuestionTwo
{
  /// <summary>
  /// @desc : Merge  
  /// @author :mons
  /// @create : 2019/4/18 16:57:47 
  /// @source : https://leetcode.com/problems/merge-intervals/
  /// </summary>
  public class Merge
  {
    /**
     * Runtime: 276 ms, faster than 99.43% of C# online submissions for Merge Intervals.
     * Memory Usage: 31.7 MB, less than 100.00% of C# online submissions for Merge Intervals.
     */
    public int[][] Solution(int[][] intervals)
    {
      List<int[]> res = new List<int[]>();

      int[] prev = null;

      foreach (var interval in intervals.OrderBy(u => u[0]))
      {
        if (prev != null && interval[0] <= prev[1])
        {
          if (prev[1] < interval[1])
            prev[1] = interval[1];
        }
        else
        {
          prev = interval;
          res.Add(prev);
        }
      }

      return res.ToArray();
    }
  }
}