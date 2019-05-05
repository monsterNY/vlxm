using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ConsoleTest.Deep.UniquePaths
{
  /// <summary>
  /// @desc : Two  
  /// @author : mons
  /// @create : 2019/5/5 14:52:56 
  /// @source : https://leetcode.com/problems/unique-paths-ii/
  ///
  /// 难度：middle
  /// 
  /// 已参考
  /// 
  /// </summary>
  public class UniquePathsII
  {
    /**
     * Runtime: 92 ms, faster than 99.39% of C# online submissions for Unique Paths II.
     * Memory Usage: 22.6 MB, less than 16.28% of C# online submissions for Unique Paths II.
     *
     * Runtime: 92 ms, faster than 99.39% of C# online submissions for Unique Paths II.
     * Memory Usage: 22.2 MB, less than 95.35% of C# online submissions for Unique Paths II.
     *
     */
    public int UniquePathsWithObstacles(int[][] obstacleGrid)
    {
      if (obstacleGrid.Length == 0) return 0;
      if (obstacleGrid[0][0] == 1) return 0;

      int row = obstacleGrid.Length, col = obstacleGrid[0].Length;

      bool swapped;
      var dp = new int[row];

      dp[0] = 1;

      for (int i = 0; i < col; i++)
      {
        swapped = true;
        for (int j = 0; j < row; j++)
        {
          if (obstacleGrid[j][i] == 1)
            dp[j] = 0;
          else if (j > 0) dp[j] += dp[j - 1];

          if (swapped && dp[j] > 0) swapped = false;
        }

        if (swapped) return 0;
        //Console.WriteLine(JsonConvert.SerializeObject(dp)); //test
      }

      return dp[row - 1];
    }
  }
}