using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.LeetCode
{
  /// <summary>
  /// @desc ：NumMagicSquaresInside  
  /// @author : mons
  /// @create : 2019/3/8 15:38:42 
  /// @source : https://leetcode.com/problems/magic-squares-in-grid/
  /// </summary>
  public class NumMagicSquaresInside
  {
    /**
     * error:
     *
     * 1.3*3网格
     * 2.网格由1-9组成 且不重复
     * 3.每条线的长度相同 即 总和为45(1+2+3+...9)
     *
     */
    public int Solution(int[][] grid)
    {
      if (grid.Length < 3 || grid[0].Length < 3)
        return 0;

      int sum = 15, count = 0;

      for (int i = 1; i < grid.Length - 1; i++)
      {
        for (int j = 1; j < grid[i].Length - 1; j++)
        {
          sum = grid[i][j] + grid[i][j - 1] + grid[i][j + 1];
          //左右
          if ((sum == grid[i][j] + grid[i][j - 1] + grid[i][j + 1])
              && (sum == grid[i - 1][j] + grid[i - 1][j - 1] + grid[i - 1][j + 1])
              && (sum == grid[i + 1][j] + grid[i + 1][j - 1] + grid[i + 1][j + 1])
              //垂直
              && (sum == grid[i][j] + grid[i - 1][j] + grid[i + 1][j])
              && (sum == grid[i][j - 1] + grid[i - 1][j - 1] + grid[i + 1][j - 1])
              && (sum == grid[i][j + 1] + grid[i - 1][j + 1] + grid[i + 1][j + 1])
              // 斜边
              && (sum == grid[i][j] + grid[i - 1][j - 1] + grid[i + 1][j + 1])
              && (sum == grid[i][j] + grid[i - 1][j + 1] + grid[i + 1][j - 1])
          )
            count++;
        }
      }

      return count;
    }
  }
}