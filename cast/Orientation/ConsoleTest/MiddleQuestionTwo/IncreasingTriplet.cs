using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestionTwo
{
  /// <summary>
  /// @desc : IncreasingTriplet  
  /// @author :mons
  /// @create : 2019/4/15 10:54:35 
  /// @source : https://leetcode.com/problems/increasing-triplet-subsequence/
  /// </summary>
  public class IncreasingTriplet
  {

    /**
     * Runtime: 92 ms, faster than 96.99% of C# online submissions for Increasing Triplet Subsequence.
     *
     * Memory Usage: 22.5 MB, less than 81.25% of C# online submissions for Increasing Triplet Subsequence.
     *
     */
    public bool increasingTriplet(int[] nums)
    {
      // start with two largest values, as soon as we find a number bigger than both, while both have been updated, return true.
      int small = int.MaxValue, big = int.MaxValue;
      foreach (int n in nums)
      {
        if (n <= small) { small = n; } // update small if n is smaller than bothz
        else if (n <= big) { big = n; } // update big only if greater than small but smaller than big
        else return true; // return if you find a number bigger than both
      }
      return false;
    }

    /**
     * Runtime: 204 ms, faster than 8.43% of C# online submissions for Increasing Triplet Subsequence.
     * Memory Usage: 22.6 MB, less than 43.75% of C# online submissions for Increasing Triplet Subsequence.
     *
     * ？？？
     */
    public bool Solution(int[] nums)
    {

      var dp = new int[nums.Length];

      Array.Fill(dp,1);

      for (int i = nums.Length - 2; i >= 0; i--)
      {
        for (int j = i; j < nums.Length; j++)
        {
          if (nums[i] < nums[j] && dp[i] < dp[j] + 1)
          {
            dp[i] = dp[j] + 1;
            if (dp[i] >= 3) return true;
          }
        }
      }

      return false;

    }

  }
}
