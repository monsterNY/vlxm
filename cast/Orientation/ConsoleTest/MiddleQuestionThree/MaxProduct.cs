using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestionThree
{
  /// <summary>
  /// @desc : MaxProduct  
  /// @author : mons
  /// @create : 2019/4/25 16:41:46 
  /// @source : https://leetcode.com/problems/maximum-product-subarray/
  /// </summary>
  public class MaxProduct
  {
    /**
     * Runtime: 92 ms, faster than 97.20% of C# online submissions for Maximum Product Subarray.
     * Memory Usage: 22.6 MB, less than 8.33% of C# online submissions for Maximum Product Subarray.
     */
    public int Solution(int[] nums)
    {
      int max = int.MinValue;

      int[] maxArr = new int[nums.Length], minArr = new int[nums.Length];

      for (int i = 0; i < nums.Length; i++)
      {
        maxArr[i] = nums[i];
        minArr[i] = nums[i];

        if (i > 0)
        {
          maxArr[i] = Math.Max(maxArr[i], nums[i] * maxArr[i - 1]);
          maxArr[i] = Math.Max(maxArr[i], nums[i] * minArr[i - 1]);

          minArr[i] = Math.Min(minArr[i], nums[i] * maxArr[i - 1]);
          minArr[i] = Math.Min(minArr[i], nums[i] * minArr[i - 1]);
        }

        if (maxArr[i] > max)
        {
          max = maxArr[i];
        }
      }

      return max;
    }
  }
}