using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : FindTargetSumWays  
  /// @author :mons
  /// @create : 2019/4/4 9:25:48 
  /// @source : https://leetcode.com/problems/target-sum/
  /// </summary>
  public class FindTargetSumWays
  {
    #region otherSolution

    /**
     * Runtime: 92 ms, faster than 99.41% of C# online submissions for Target Sum.
     * Memory Usage: 22.1 MB, less than 78.57% of C# online submissions for Target Sum.
     *
     * don't understand
     *
     */
    public int findTargetSumWays(int[] nums, int s)
    {
      int sum = 0;
      foreach (int n in nums)
        sum += n;
      return sum < s || (s + sum) % 2 > 0 ? 0 : subsetSum(nums, (s + sum) >> 1);
    }

    public int subsetSum(int[] nums, int s)
    {
      int[] dp = new int[s + 1];
      dp[0] = 1;
      foreach (int n in nums)
        for (int i = s; i >= n; i--)
          dp[i] += dp[i - n];
      return dp[s];
    }

    #endregion

    private int _count;

    /**
     * Runtime: 884 ms, faster than 39.05% of C# online submissions for Target Sum.
     * Memory Usage: 21.9 MB, less than 100.00% of C# online submissions for Target Sum.
     */
    public int Solution(int[] nums, int S)
    {
      Possible(nums, 0, 0, S);
      return _count;
    }

    public void Possible(int[] arr, int sum, int index, int targetSum)
    {
      if (index == arr.Length)
      {
        if (sum == targetSum) _count++;
        return;
      }

      Possible(arr, sum - arr[index], index + 1, targetSum);

      Possible(arr, sum + arr[index], index + 1, targetSum);
    }

    /**
     * Runtime: 604 ms, faster than 59.76% of C# online submissions for Target Sum.
     * Memory Usage: 22.4 MB, less than 57.14% of C# online submissions for Target Sum.
     *
     * Runtime: 600 ms, faster than 59.76% of C# online submissions for Target Sum.
     * Memory Usage: 21.9 MB, less than 100.00% of C# online submissions for Target Sum.
     *
     */
    public int Solution2(int[] nums, int S)
    {
//      var maxSum = nums.Sum();

      var maxSum = 0;
      for (int i = 0; i < nums.Length; i++)
      {
        maxSum += nums[i];
      }

      Possible2(nums, maxSum, 0, S, maxSum);
      return _count;
    }

    public void Possible2(int[] arr, int sum, int index, int targetSum, int maxSum)
    {
      if (sum < targetSum) return;
      if (index == arr.Length)
      {
        if (sum == targetSum) _count++;
        return;
      }

      Possible2(arr, sum - (2 * arr[index]), index + 1, targetSum, maxSum);

      Possible2(arr, sum, index + 1, targetSum, maxSum);
    }
  }
}