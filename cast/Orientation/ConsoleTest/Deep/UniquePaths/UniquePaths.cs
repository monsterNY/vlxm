using System;
using System.Collections.Generic;
using System.Text;
using Tools.CusAttr;
using Tools.CusMenu;

namespace ConsoleTest.Deep.UniquePaths
{
  /// <summary>
  /// @desc : One  
  /// @author : mons
  /// @create : 2019/5/5 14:05:09 
  /// @source : https://leetcode.com/problems/unique-paths/
  /// 难度：middle
  /// 
  /// 已参考
  /// 
  /// </summary>
  [Question(QuestionTypes.DP)]
  public class UniquePaths
  {

    /**
     * Runtime: 36 ms, faster than 100.00% of C# online submissions for Unique Paths.
     * Memory Usage: 13 MB, less than 7.14% of C# online submissions for Unique Paths.
     */
    public int Solution(int m, int n)
    {
      var dp = new int[m][];

      for (int i = 0; i < m; i++)
        dp[i] = new int[n];

      for (int i = 0; i < m; i++)
        dp[i][0] = 1;

      for (int i = 0; i < n; i++)
        dp[0][i] = 1;

      for (int i = 1; i < m; i++)
        for (int j = 1; j < n; j++)
          dp[i][j] = dp[i - 1][j] + dp[i][j - 1];

      return dp[m - 1][n - 1];
    }
  }
}