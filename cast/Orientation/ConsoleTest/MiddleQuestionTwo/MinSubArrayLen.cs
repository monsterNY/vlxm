using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestionTwo
{
  /// <summary>
  /// @desc : MinSubArrayLen  
  /// @author :mons
  /// @create : 2019/4/19 11:51:38 
  /// @source : https://leetcode.com/problems/minimum-size-subarray-sum/
  /// </summary>
  public class MinSubArrayLen
  {
    /**
     * Runtime: 100 ms, faster than 63.41% of C# online submissions for Minimum Size Subarray Sum.
     * Memory Usage: 23.4 MB, less than 54.54% of C# online submissions for Minimum Size Subarray Sum.
     *
     * Runtime: 96 ms, faster than 100.00% of C# online submissions for Minimum Size Subarray Sum.
     * Memory Usage: 23.6 MB, less than 9.09% of C# online submissions for Minimum Size Subarray Sum.
     *
     * test ....,
     *
     */
    public int Solution(int s, int[] nums)
    {
      int start = 0, num = 0, count = nums.Length;

      for (int i = 0; i < nums.Length; i++)
      {
        num += nums[i];
        if (num >= s)
        {
          for (; start < i; num -= nums[start++])
          {
            if (num - nums[start] < s)
            {
              break;
            }
          }

          if ((i - start) < count) count = i - start;
        }
      }

      return num >= s ? count + 1 : 0;
    }

    public int Optimize(int s, int[] nums)
    {
      int start = 0, num = 0, count = nums.Length;

      for (int i = 0; i < nums.Length; i++)
      {
        if ((num += nums[i]) >= s)
        {
          for (; start < i; num -= nums[start++])
            if (num - nums[start] < s)
              break;

          if ((i - start) < count) count = i - start;
        }
      }

      return num >= s ? count + 1 : 0;
    }

    //bug
    public int Try(int s, int[] nums)
    {
      Array.Sort(nums);

      int num = 0, count = 0;

      for (int i = nums.Length - 1; num < s && i >= 0; i--, count++)
      {
        num += nums[i];
      }

      return count;
    }
  }
}