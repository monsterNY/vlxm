using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.HardQuestion
{
  /// <summary>
  /// @desc : MinSwapsCouples  
  /// @author : mons
  /// @create : 2019/5/5 16:51:48 
  /// @source : https://leetcode.com/problems/couples-holding-hands/
  /// </summary>
  public class MinSwapsCouples
  {

    /**
     * Runtime: 88 ms, faster than 100.00% of C# online submissions for Couples Holding Hands.
     * Memory Usage: 21.7 MB, less than 100.00% of C# online submissions for Couples Holding Hands.
     *
     * nice,perfect~
     *
     */
    public int Solution(int[] row)
    {
      //target : 给row排序所需要的最小数量 -- no
      //target : 让相邻的元素排在一起

      var count = 0;
      for (int i = 0; i < row.Length; i += 2)
      {
        int changeIndex;
        if (row[i] % 2 == 0)
        {
          if (row[i] + 1 == row[i + 1]) continue;
          changeIndex = Array.IndexOf(row, row[i] + 1);
        }
        else
        {
          if (row[i] - 1 == row[i + 1]) continue;
          changeIndex = Array.IndexOf(row, row[i] - 1);
        }
        var temp = row[i + 1];
        row[i + 1] = row[changeIndex];
        row[changeIndex] = temp;
        count++;
      }

      return count;
    }
  }
}