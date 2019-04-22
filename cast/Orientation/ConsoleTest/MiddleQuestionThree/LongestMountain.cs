using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestionThree
{
  /// <summary>
  /// @desc : LongestMountain  
  /// @author :mons
  /// @create : 2019/4/22 11:57:04 
  /// @source : https://leetcode.com/problems/longest-mountain-in-array/
  /// </summary>
  [Obsolete]
  public class LongestMountain
  {

    //don't understand
    public int Solution(int[] A)
    {
      if (A.Length < 3) return 0;

      var maxLen = 0;
      var dp = new int[A.Length];
      var containTop = new bool[A.Length];
      Array.Fill(dp, 1);

      for (int i = 1; i < A.Length - 1; i++)
      {
        if (dp[i] == dp[i - 1])
          dp[i] = 1;

        dp[i] = dp[i - 1] + 1;

        if (containTop[i - 1] && A[i] > A[i - 1])
        {
          if (dp[i - 1] >= 3 && dp[i - 1] > maxLen) maxLen = dp[i] - 1;
          dp[i] = 1;
          containTop[i] = false;
        }else if (containTop[i - 1] || (A[i] > A[i - 1] && A[i] > A[i + 1]))
        {
          containTop[i] = true;
        }
      }

      if (containTop[A.Length - 2] && A[A.Length - 1] < A[A.Length - 2] && dp[A.Length - 2] + 1 > maxLen)
      {
        maxLen = dp[A.Length - 2] + 1;
      }

      return maxLen;
    }
  }
}