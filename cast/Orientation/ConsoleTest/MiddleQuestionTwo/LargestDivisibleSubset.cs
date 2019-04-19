using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestionTwo
{
  /// <summary>
  /// @desc : LargestDivisibleSubset  
  /// @author :mons
  /// @create : 2019/4/19 11:27:58 
  /// @source : https://leetcode.com/problems/largest-divisible-subset/
  /// </summary>
  public class LargestDivisibleSubset
  {

    /**
     * Runtime: 268 ms, faster than 82.61% of C# online submissions for Largest Divisible Subset.
     * Memory Usage: 28.8 MB, less than 75.00% of C# online submissions for Largest Divisible Subset.
     *
     *
     * Runtime: 264 ms, faster than 100.00% of C# online submissions for Largest Divisible Subset.
     * Memory Usage: 28.7 MB, less than 75.00% of C# online submissions for Largest Divisible Subset.
     *
     * nice!!!!!!!
     *
     */
    public IList<int> Solution(int[] nums)
    {
      IList<int> res = new List<int>();

      if (nums.Length == 0) return res;

      Array.Sort(nums);

      int[] dp = new int[nums.Length], prev = new int[nums.Length];

      var maxIndex = 0;

      Array.Fill(dp, 1);
      Array.Fill(prev, -1);

      for (int i = 1; i < nums.Length; i++)
      {
        for (int j = 0; j < i; j++)
        {
          if (nums[i] % nums[j] == 0 && dp[i] < dp[j] + 1)
          {
            dp[i] = dp[j] + 1;
            prev[i] = j;
            if (dp[i] > dp[maxIndex]) maxIndex = i;
          }
        }
      }

      do
      {
        res.Add(nums[maxIndex]);
      } while ((maxIndex = prev[maxIndex]) != -1);

      return res;
    }

    /**
     * Runtime: 264 ms, faster than 100.00% of C# online submissions for Largest Divisible Subset.
     * Memory Usage: 28.8 MB, less than 75.00% of C# online submissions for Largest Divisible Subset.
     */
    public IList<int> Solution2(int[] nums)
    {
      IList<int> res = new List<int>();

      if (nums.Length == 0) return res;

      Array.Sort(nums);

      int[] dp = new int[nums.Length], prev = new int[nums.Length];

      var maxIndex = 0;

      Array.Fill(dp, 1);

      for (int i = 1; i < nums.Length; i++)
      {
        for (int j = 0; j < i; j++)
        {
          if (nums[i] % nums[j] == 0 && dp[i] < dp[j] + 1)
          {
            dp[i] = dp[j] + 1;
            prev[i] = j + 1;
            if (dp[i] > dp[maxIndex]) maxIndex = i;
          }
        }
      }

      do
      {
        res.Add(nums[maxIndex]);
      } while ((maxIndex = prev[maxIndex] - 1) != 0);

      return res;
    }

  }
}