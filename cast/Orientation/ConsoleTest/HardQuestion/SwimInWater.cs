using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleTest.HardQuestion
{
  /// <summary>
  /// @desc : SwimInWater  
  /// @author : mons
  /// @create : 2019/5/6 13:57:40 
  /// @source : https://leetcode.com/problems/swim-in-rising-water/
  /// </summary>
  [Obsolete("bird.")]
  public class SwimInWater
  {
    public int Solution(int[][] grid)
    {
      int row = grid.Length, col = grid[0].Length, max = 0;

      var visited = new bool[row][];

      for (int i = 0; i < row; i++)
      {
        visited[i] = new bool[col];
      }

      var res = Helper(grid, row - 1, col - 1, visited);

      return res;
    }

    //Time Limit
    public int Helper(int[][] grid, int i, int j, bool[][] visited)
    {
      if (i == 0 && j == 0) return grid[i][j];
      if (visited[i][j]) return int.MaxValue;
      visited[i][j] = true;
      var min = int.MaxValue;
      if (i > 0)
      {
        min = Math.Min(min, Math.Max(grid[i][j], Helper(grid, i - 1, j, visited)));
      }

      if (i < grid.Length - 1)
      {
        min = Math.Min(min, Math.Max(grid[i][j], Helper(grid, i + 1, j, visited)));
      }

      if (j > 0)
      {
        min = Math.Min(min, Math.Max(grid[i][j], Helper(grid, i, j - 1, visited)));
      }

      if (j < grid[0].Length - 1)
      {
        min = Math.Min(min, Math.Max(grid[i][j], Helper(grid, i, j + 1, visited)));
      }

      visited[i][j] = false;

      return min;
    }
  }
}