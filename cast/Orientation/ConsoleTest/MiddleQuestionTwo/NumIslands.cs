using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestionTwo
{
  /// <summary>
  /// @desc : NumIslands  
  /// @author :mons
  /// @create : 2019/4/11 15:58:35 
  /// @source : https://leetcode.com/problems/number-of-islands/
  /// </summary>
  public class NumIslands
  {

    /**
     * Runtime: 104 ms, faster than 100.00% of C# online submissions for Number of Islands.
     * Memory Usage: 25.7 MB, less than 100.00% of C# online submissions for Number of Islands.
     *
     * 记得之前弄过。。。
     *
     */
    public int Solution(char[][] grid)
    {
      var count = 0;
      for (int i = 0; i < grid.Length; i++)
      {
        for (int j = 0; j < grid[i].Length; j++)
        {
          if (grid[i][j] == '1')
          {
            count++;
            UpdateGrid(i, j, grid);
          }
        }
      }

      return count;
    }

    public void UpdateGrid(int i, int j, char[][] grid)
    {
      if (i < 0 || j < 0 || i == grid.Length || j == grid[0].Length || grid[i][j] == '0') return;

      grid[i][j] = '0';

      UpdateGrid(i - 1, j, grid); //top
      UpdateGrid(i + 1, j, grid); //bottom
      UpdateGrid(i, j - 1, grid); //left
      UpdateGrid(i, j + 1, grid); //right
    }
  }
}