using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : MinDistance  
  /// @author :mons
  /// @create : 2019/4/4 13:48:51 
  /// @source : https://leetcode.com/problems/delete-operation-for-two-strings/
  /// <see cref="FindLength"/>
  /// </summary>
  [Obsolete("果然又是我不熟的 dp ")]
  public class MinDistance
  {

    /**
     * Runtime: 100 ms, faster than 67.92% of C# online submissions for Delete Operation for Two Strings.
     * Memory Usage: 23.6 MB, less than 100.00% of C# online submissions for Delete Operation for Two Strings.
     */
    public int OtherSolution(string word1, string word2)
    {
      int m = word1.Length, n = word2.Length;
      int[][] dp = new int[m + 1][];
      for (int i = 0; i < dp.Length; i++)
        dp[i] = new int[n + 1];
      for (int i = 0; i <= m; i++)
      for (int j = 0; j <= n; j++)
        if (i == 0 || j == 0)
          dp[i][j] = 0;
        else
          dp[i][j] = (word1[i - 1] == word2[j - 1] )? dp[i - 1][j - 1] + 1 : Math.Max(dp[i - 1][j], dp[i][j - 1]);
      return word1.Length + word2.Length - dp[m][n] - dp[m][n];
    }

    //Time Limit Exceeded

    public int Simple(string word1, string word2)
    {
      return FindMax(word1, word2, 0, 0, 0);
    }

    public int FindMax(string word1, string word2, int index1, int index2, int sameCount)
    {
      if (index1 == word1.Length && index2 == word2.Length) return sameCount;
      else if (index1 == word1.Length) return sameCount + word2.Length - index2;
      else if (index2 == word2.Length) return sameCount + word1.Length - index1;

      if (word1[index1] == word2[index2])
      {
        return Math.Min(
          Math.Min(FindMax(word1, word2, index1 + 1, index2 + 1, sameCount),
            FindMax(word1, word2, index1 + 1, index2, sameCount + 1))
          , FindMax(word1, word2, index1, index2 + 1, sameCount + 1));
      }
      else
      {
        return Math.Min(FindMax(word1, word2, index1 + 1, index2, sameCount + 1),
          FindMax(word1, word2, index1, index2 + 1, sameCount + 1));
      }
    }
  }
}