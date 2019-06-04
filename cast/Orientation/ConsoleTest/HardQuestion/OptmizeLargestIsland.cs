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
  public class OptmizeLargestIsland
  {

    /**
     * Runtime: 100 ms, faster than 100.00% of C# online submissions for Making A Large Island.
     * Memory Usage: 25.8 MB, less than 30.77% of C# online submissions for Making A Large Island.
     *
     * ✨✨✨✨✨✨
     *
     */
    public int Solution(int[][] grid)
    {
      int max = 0, len = grid.Length, flagInt = 1;

      //存储每个点的value
      //itemArr[i].Length = 2
      //itemArr[i][0] = value
      //itemArr[i][1] = flag  
      //flag 用于辨别itemArr[i] 与 itemArr[i2]是否同源 - 即是否在相同的区间内

      var valueArr = new int[len][][];
      var visited = new bool[len][];

      for (int i = 0; i < len; i++)
      {
        valueArr[i] = new int[len][];
        visited[i] = new bool[len];
      }

      for (int i = 0; i < len; i++)
      for (int j = 0; j < len; j++)
      {
        if (grid[i][j] == 0 || visited[i][j]) continue;
        SetDpValue(grid, i, j, valueArr, GetValue(grid, i, j, visited), flagInt++);//设置每个点的value 和 flag
      }

      for (int i = 0; i < len; i++)
      for (int j = 0; j < len; j++)
        max = Math.Max(max, valueArr[i][j] == null ? GetSum(valueArr, i, j) : valueArr[i][j][0]);

      return max;
    }

    /// <summary>
    /// GET : node.sum = up + bottom + left + right
    /// </summary>
    /// <param name="dp"></param>
    /// <param name="i"></param>
    /// <param name="j"></param>
    /// <returns></returns>
    public int GetSum(int[][][] dp, int i, int j)
    {
      int sum = 0, len = dp.Length;

      var set = new HashSet<int>();

      if (i > 0 && dp[i - 1][j] != null)
      {
        sum += dp[i - 1][j][0];
        set.Add(dp[i - 1][j][1]);
      }

      if (i < len - 1 && dp[i + 1][j] != null && !set.Contains(dp[i + 1][j][1]))
      {
        sum += dp[i + 1][j][0];
        set.Add(dp[i + 1][j][1]);
      }

      if (j > 0 && dp[i][j - 1] != null && !set.Contains(dp[i][j - 1][1]))
      {
        sum += dp[i][j - 1][0];
        set.Add(dp[i][j - 1][1]);
      }

      if (j < len - 1 && dp[i][j + 1] != null && !set.Contains(dp[i][j + 1][1]))
        sum += dp[i][j + 1][0];

      return sum + 1;
    }

    /// <summary>
    /// 获取节点值
    /// </summary>
    /// <param name="grid"></param>
    /// <param name="i"></param>
    /// <param name="j"></param>
    /// <param name="visited"></param>
    /// <returns></returns>
    public int GetValue(int[][] grid, int i, int j, bool[][] visited)
    {
      if (i < 0 || i >= grid.Length || j < 0 || j >= grid.Length || grid[i][j] == 0 || visited[i][j]) return 0;

      visited[i][j] = true;

      //value = 1 + up + bottom + left + right
      var res = 1 +
                GetValue(grid, i - 1, j, visited) +
                GetValue(grid, i + 1, j, visited) +
                GetValue(grid, i, j - 1, visited) +
                GetValue(grid, i, j + 1, visited);

      return res;
    }

    /// <summary>
    /// 设置节点值
    /// </summary>
    /// <param name="grid"></param>
    /// <param name="i"></param>
    /// <param name="j"></param>
    /// <param name="dp"></param>
    /// <param name="value"></param>
    /// <param name="flagValue"></param>
    public void SetDpValue(int[][] grid, int i, int j, int[][][] dp, int value, int flagValue)
    {
      if (i < 0 || i >= grid.Length || j < 0 || j >= grid.Length || grid[i][j] == 0 || dp[i][j] != null) return;

      dp[i][j] = new[] {value, flagValue};

      //设置相同区块中的值
      SetDpValue(grid, i - 1, j, dp, value, flagValue);//set up
      SetDpValue(grid, i + 1, j, dp, value, flagValue);//set bottom
      SetDpValue(grid, i, j - 1, dp, value, flagValue);//set left
      SetDpValue(grid, i, j + 1, dp, value, flagValue);//set right
    }

  }
}