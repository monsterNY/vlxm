using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.DP
{
  /// <summary>
  /// @desc : LengthOfLIS  
  /// @author :mons
  /// @create : 2019/4/12 9:34:35 
  /// @source : https://leetcode.com/problems/longest-increasing-subsequence/
  /// </summary>
  public class LengthOfLIS
  {
    /**
     * Runtime: 112 ms, faster than 67.51% of C# online submissions for Longest Increasing Subsequence.
     * Memory Usage: 21.8 MB, less than 10.00% of C# online submissions for Longest Increasing Subsequence.
     */
    public int Solution(int[] nums)
    {
      if (nums.Length < 2) return nums.Length;
      int[] dp = new int[nums.Length];

      var count = 1;
      Array.Fill(dp, 1);

      for (int i = nums.Length - 2; i >= 0; i--)
      {
        for (int j = i + 1; j < dp.Length; j++)
        {
          if (nums[i] < nums[j] && dp[i] < dp[j] + 1)
          {
            dp[i] = dp[j] + 1; //so cool
          }
        }

        if (dp[i] > count) count = dp[i];
      }

      return count;
    }

    //差不多。
    public int Solution2(int[] nums)
    {
      if (nums.Length < 2) return nums.Length;
      int[] dp = new int[nums.Length];

      var count = 1;
      Array.Fill(dp, 1);

      for (int i = nums.Length - 2; i >= 0; i--)
      {
        for (int j = i + 1; j < nums.Length; j++)
        {
          if (nums[i] < nums[j] && dp[i] < dp[j] + 1)
          {
            dp[i] = dp[j] + 1;
            if (dp[i] > count) count = dp[i];
          }
        }
      }

      return count;
    }

    //target : 返回最长的递增数组长度
    public int Explain(int[] nums)
    {
      if (nums.Length < 2) return nums.Length;

      int[] dp = new int[nums.Length]; // dp[i] --》 表示下标i的最长递增数组长度
      var count = 0;
      Array.Fill(dp, 1); //所有递增数组至少含有一个元素 

      for (int i = nums.Length - 2; i >= 0; i--)
      {
        for (int j = i + 1; j < dp.Length; j++)
        {
          if (nums[i] < nums[j] && dp[i] < dp[j] + 1) //若 nums[i] < nums[j] 则 i的递增长度为 1 + dp[j] 
          {
            dp[i] = dp[j] + 1;
          }
        }

        if (dp[i] > count) count = dp[i];
      }

      return count;
    }
  }
}