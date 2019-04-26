using System;
using System.Collections.Generic;
using System.Text;
using Tools.CusAttr;
using Tools.CusMenu;

namespace ConsoleTest.MiddleQuestionThree
{
  /// <summary>
  /// @desc : CheckSubarraySum  
  /// @author : mons
  /// @create : 2019/4/26 16:02:47 
  /// @source : https://leetcode.com/problems/continuous-subarray-sum/
  /// </summary>
  [Love(LoveTypes.Question,LoveTypes.Fix)]
  public class CheckSubarraySum
  {


    public bool Solution(int[] nums, int k)
    {

      var len = nums.Length;

      var dp = new int[len][];

      for (int i = 0; i < len; i++)
      {
        dp[i] = new int[len];
      }

      var sum = 0;

      for (int i = 0; i < len; i++)
      {
        sum += nums[i];
        dp[0][i] = sum;

        if (k == 0)
        {
          if (sum == 0) return true;
        }
        else if (sum % k == 0) return true;

      }

      for (int i = 1; i < len; i++)
      {
        
        // i -> j = sum - 0 -> i - j + 1 -> len - 1

      }

      return false;

    }

    /**
     * Runtime: 140 ms, faster than 62.18% of C# online submissions for Continuous Subarray Sum.
     * Memory Usage: 29.5 MB, less than 100.00% of C# online submissions for Continuous Subarray Sum.
     */
    public bool Simple(int[] nums, int k)
    {
      for (int i = 0; i < nums.Length - 1; i++)
      {
        var sum = nums[i];
        for (int j = i + 1; j < nums.Length; j++)
        {
          sum += nums[j];
          if (k == 0)
          {
            if (sum == 0) return true;
          }
          else if (sum % k == 0) return true;
        }
      }

      return false;
    }

    //otherSolution:
    //We iterate through the input array exactly once, keeping track of the running sum mod k of the elements in the process. If we find that a running sum value at index j has been previously seen before in some earlier index i in the array, then we know that the sub-array (i,j] contains a desired sum.

    //保存 sum % k 当我们发现一个数在之前出现过  则 中间肯定包含一组和满足条件

  }
}