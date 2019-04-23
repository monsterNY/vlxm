using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestionThree
{
  /// <summary>
  /// @desc : UniquePathsWithObstacles  
  /// @author :mons
  /// @create : 2019/4/23 10:30:47 
  /// @source : https://leetcode.com/problems/unique-paths-ii/
  /// @helper : https://leetcode.windliang.cc/leetCode-62-Unique-Paths.html#题目描述（中等难度）
  /// </summary>
  [Obsolete]
  public class UniquePathsWithObstacles
  {

    public int OtherSolution(int[][] obstacleGrid)
    {
      int width = obstacleGrid[0].Length;
      int[] dp = new int[width];
      dp[0] = 1;
      foreach (int[] row in obstacleGrid)
      {
        for (int j = 0; j < width; j++)
        {
          if (row[j] == 1)
            dp[j] = 0;
          else if (j > 0)
            dp[j] += dp[j - 1];
        }
      }
      return dp[width - 1];
    }

    public int Solution(int[][] obstacleGrid)
    {
      return 0;
    }

  }
}
