using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestionTwo
{
  /// <summary>
  /// @desc : CanPartition  
  /// @author :mons
  /// @create : 2019/4/12 14:12:08 
  /// @source : https://leetcode.com/problems/partition-equal-subset-sum/
  /// </summary>
  public class CanPartition
  {

    /**
     * Runtime: 92 ms, faster than 98.68% of C# online submissions for Partition Equal Subset Sum.
     * Memory Usage: 22.3 MB, less than 83.33% of C# online submissions for Partition Equal Subset Sum.
     *
     * .,.,.
     *
     */
    public bool Solution(int[] nums)
    {
      var sum = 0;
      var max = 0;
      for (int i = 0; i < nums.Length; i++)
      {
        sum += nums[i];
        if (nums[i] > max) max = nums[i];
      }

      if (sum % 2 != 0) return false;

      var target = sum / 2;

      if (max > target) return false;

      Array.Sort(nums);

      return GetResult(nums, target, 0);
    }

    //Time Limit
    public bool GetResult(int[] arr, int target, int startIndex)
    {
      if (startIndex == -1) return false;
      if (0 == target) return true;

      for (; startIndex >= 0; startIndex--)
      {
        if (target >= arr[startIndex])
        {
          if (GetResult(arr, target - arr[startIndex], startIndex - 1)) return true;
        }
      }

      return false;
    }
  }
}