using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : SubarraySum  
  /// @author :mons
  /// @create : 2019/4/10 10:51:56 
  /// @source : https://leetcode.com/problems/subarray-sum-equals-k/
  /// </summary>
  public class SubarraySum
  {

    /**
     *
     * Runtime: 108 ms, faster than 100.00% of C# online submissions for Subarray Sum Equals K.
     * Memory Usage: 28.5 MB, less than 56.25% of C# online submissions for Subarray Sum Equals K.
     *
     * so cool
     *
     */
    public int OtherSolution(int[] nums, int k)
    {
      int res = 0, preSum = 0;

      Dictionary<int, int> dictionary = new Dictionary<int, int>() {{0, 1}};

      foreach (var item in nums)
      {
        preSum += item;
        if (dictionary.ContainsKey(preSum - k))
        {
          res += dictionary[preSum - k];
        }
        if(dictionary.ContainsKey(preSum))
          dictionary[preSum]++;
        else
          dictionary.Add(preSum,1);
      }

      return res;

    }

    public int Optimize(int[] nums, int k)
    {
      int res = 0, preSum = 0;

      Dictionary<int, int> dictionary = new Dictionary<int, int>() { { 0, 1 } };

      foreach (var item in nums)
      {
        preSum += item;
        if (dictionary.ContainsKey(preSum - k))
        {
          res += dictionary[preSum - k];
        }
        if (dictionary.ContainsKey(preSum))
          dictionary[preSum]++;
        else
          dictionary.Add(preSum, 1);
      }

      return res;

    }

    //bug not % is equal
    public int Solution(int[] nums, int k)
    {
      var count = 0;

      var dp = new int[nums.Length + 1];

      for (int i = nums.Length - 1; i >= 0; i--)
      {
        var item = 0;
        for (int j = i; j < nums.Length; j++)
        {
          item += nums[j];
          if (item == k)
          {
            dp[i] = dp[i + 1] + 1;
            count += dp[i];
            break;
          }
        }
      }

      return count;
    }

    /**
     * Runtime: 600 ms, faster than 39.12% of C# online submissions for Subarray Sum Equals K.
     * Memory Usage: 25.1 MB, less than 87.50% of C# online submissions for Subarray Sum Equals K.
     */
    public int Simple(int[] nums, int k)
    {
      var count = 0;
      for (int i = 0; i < nums.Length; i++)
      {
        if (nums[i] == k)
          count++;

        var item = nums[i];
        for (int j = i + 1; j < nums.Length; j++)
        {
          item += nums[j];
          if (item == k)
            count++;
        }
      }

      return count;
    }
  }
}