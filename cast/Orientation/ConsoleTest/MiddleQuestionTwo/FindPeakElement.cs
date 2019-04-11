using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestionTwo
{
  /// <summary>
  /// @desc : FindPeakElement  
  /// @author :mons
  /// @create : 2019/4/11 15:18:30 
  /// @source : https://leetcode.com/problems/find-peak-element/
  /// </summary>
  public class FindPeakElement
  {
    /**
     * Runtime: 92 ms, faster than 94.76% of C# online submissions for Find Peak Element.
     * Memory Usage: 22.6 MB, less than 10.26% of C# online submissions for Find Peak Element.
     *
     * easy.,
     *
     */
    public int Solution(int[] nums)
    {
      for (int i = 0; i < nums.Length; i++)
      {
        if ((i == 0 || nums[i] > nums[i - 1]) && (i == nums.Length - 1 || nums[i] > nums[i + 1]))//clear~
          return i;
      }

      return -1;
    }
  }
}