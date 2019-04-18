using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestionTwo
{
  /// <summary>
  /// @desc : Makesquare  
  /// @author :mons
  /// @create : 2019/4/18 15:50:09 
  /// @source : https://leetcode.com/problems/matchsticks-to-square/
  /// </summary>
  [Obsolete("麻烦。")]
  public class Makesquare
  {
    public bool Solution(int[] nums)
    {
      if (nums.Length == 0) return true;
      var sum = 0;
      foreach (var num in nums)
      {
        sum += num;
      }

      if (sum % 4 != 0) return false;

      var avg = sum / 4;

      Array.Sort(nums);

      var count = 4;
      while (count > 0)
      {
        sum = 0;
        for (int i = nums.Length - 1; i >= 0; i--)
        {
          if (nums[i] == 0) continue;
          if (sum + nums[i] <= avg)
          {
            sum += nums[i];
            nums[i] = 0;
          }
        }

        if (sum == avg) count--;
        else return false;
      }

      return true;
    }
  }
}