using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ConsoleTest.HardQuestion
{
  /// <summary>
  /// @desc : LargestIsland  
  /// @author : mons
  /// @create : 2019/6/3 16:28:43 
  /// @source : https://leetcode.com/problems/making-a-large-island/
  /// </summary>
  public class LargestIsland
  {

    /**
     * Runtime: 104 ms, faster than 90.91% of C# online submissions for Making A Large Island.
     * Memory Usage: 25.6 MB, less than 84.62% of C# online submissions for Making A Large Island.
     *
     * omg...
     *
     */
    public int Solution(int[][] grid)
    {
      int max = 0, len = grid.Length, flagInt = 1;

      int[][] dp = new int[len][], flag = new int[len][];
      var visited = new bool[len][];

      for (int i = 0; i < len; i++)
      {
        dp[i] = new int[len];
        flag[i] = new int[len];
        visited[i] = new bool[len];
      }

      Console.WriteLine(JsonConvert.SerializeObject(grid));

      for (int i = 0; i < len; i++)
      {
        for (int j = 0; j < len; j++)
        {
          if (grid[i][j] == 0 || visited[i][j]) continue;

          SetDpValue(grid, i, j, dp, Helper(grid, i, j, dp, visited), flag, flagInt++);
        }
      }

      Console.WriteLine(JsonConvert.SerializeObject(dp));
      Console.WriteLine(JsonConvert.SerializeObject(flag));

      for (int i = 0; i < len; i++)
      {
        for (int j = 0; j < len; j++)
        {
          if (dp[i][j] == 0)
            max = Math.Max(max, GetMax(dp, i, j, flag));
          else
            max = Math.Max(max, dp[i][j]);
        }
      }

      return max;
    }

    public int GetMax(int[][] dp, int i, int j, int[][] flag)
    {
      int sum = 0, len = dp.Length;

      var set = new HashSet<int>();

      if (i > 0)
      {
        sum += dp[i - 1][j];
        set.Add(flag[i - 1][j]);
      }

      if (i < len - 1 && !set.Contains(flag[i + 1][j]))
      {
        sum += dp[i + 1][j];
        set.Add(flag[i + 1][j]);
      }

      if (j > 0 && !set.Contains(flag[i][j - 1]))
      {
        sum += dp[i][j - 1];
        set.Add(flag[i][j - 1]);
      }

      if (j < len - 1 && !set.Contains(flag[i][j + 1]))
      {
        sum += dp[i][j + 1];
        set.Add(flag[i][j + 1]);
      }

      return sum + 1;

    }

    public int Helper(int[][] grid, int i, int j, int[][] dp, bool[][] visited)
    {
      if (i < 0 || i >= grid.Length || j < 0 || j >= grid.Length || grid[i][j] == 0 || visited[i][j]) return 0;

      visited[i][j] = true;

      var res = 1 +
                Helper(grid, i - 1, j, dp, visited) +
                Helper(grid, i + 1, j, dp, visited) +
                Helper(grid, i, j - 1, dp, visited) +
                Helper(grid, i, j + 1, dp, visited);

      return res;
    }

    public void SetDpValue(int[][] grid, int i, int j, int[][] dp, int value, int[][] flag, int flagValue)
    {
      if (i < 0 || i >= grid.Length || j < 0 || j >= grid.Length || grid[i][j] == 0 || dp[i][j] > 0) return;

      dp[i][j] = value;
      flag[i][j] = flagValue;

      SetDpValue(grid, i - 1, j, dp, value, flag, flagValue);
      SetDpValue(grid, i + 1, j, dp, value, flag, flagValue);
      SetDpValue(grid, i, j - 1, dp, value, flag, flagValue);
      SetDpValue(grid, i, j + 1, dp, value, flag, flagValue);
    }



  }
}