using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.LeetCode
{
  /// <summary>
  /// @desc : RemoveElement  
  /// @author :mons
  /// @create : 2019/3/15 15:26:07 
  /// @source :https://leetcode.com/problems/remove-element/
  /// @same <see cref="RemoveDuplicates"/>
  /// </summary>
  public class RemoveElement
  {

    /**
     * Runtime: 244 ms, faster than 98.29% of C# online submissions for Remove Element.
     * Memory Usage: 28.3 MB, less than 65.82% of C# online submissions for Remove Element.
     *
     */
    public int Solution(int[] nums, int val)
    {
      var count = 0;
      for (int i = 0; i < nums.Length; i++)
      {
        if (nums[i] == val)
          count++;
        else
          nums[i - count] = nums[i];
      }

      return nums.Length - count;
    }
  }
}