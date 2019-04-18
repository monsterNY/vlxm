using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestionTwo
{
  /// <summary>
  /// @desc : NumSubarrayProductLessThanK  
  /// @author :mons
  /// @create : 2019/4/18 14:43:23 
  /// @source : https://leetcode.com/problems/subarray-product-less-than-k/
  /// </summary>
  public class NumSubarrayProductLessThanK
  {
    /**
     * Runtime: 244 ms, faster than 87.38% of C# online submissions for Subarray Product Less Than K.
     * Memory Usage: 42.3 MB, less than 5.56% of C# online submissions for Subarray Product Less Than K.
     */
    public int Solution(int[] nums, int k)
    {
      int count = 0, prevNum = 1, startIndex = 0, prevCount = 0;

      for (int i = 0; i < nums.Length; i++, count += prevCount)
        for (prevNum *= nums[i], prevCount++; startIndex <= i && prevNum >= k; startIndex++, prevCount--)
          prevNum /= nums[startIndex];

      return count;
    }
  }
}