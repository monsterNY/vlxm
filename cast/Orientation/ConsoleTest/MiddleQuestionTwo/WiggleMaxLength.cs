using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestionTwo
{
  /// <summary>
  /// @desc : WiggleMaxLength  
  /// @author :mons
  /// @create : 2019/4/17 14:22:02 
  /// @source : https://leetcode.com/problems/wiggle-subsequence/
  /// </summary>
  [Obsolete]

  public class WiggleMaxLength
  {
    public int Solution(int[] nums)
    {
      var dp = new int[nums.Length];

      int diff = 0, count = 0;

      for (int i = 1; i < nums.Length - 1; i++)
      {
        if (nums[i] < nums[i + 1] && nums[i] < nums[i - 1])
        {
          count++;
        }
      }

      return 0;
    }
  }
}