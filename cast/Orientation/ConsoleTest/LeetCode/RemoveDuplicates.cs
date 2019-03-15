using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.LeetCode
{
  /// <summary>
  /// @desc : RemoveDuplicates  
  /// @author :mons
  /// @create : 2019/3/15 14:17:05 
  /// @source : 
  /// </summary>
  public class RemoveDuplicates
  {

    /**
     * Runtime: 256 ms, faster than 96.02% of C# online submissions for Remove Duplicates from Sorted Array.
     * Memory Usage: 31.5 MB, less than 64.74% of C# online submissions for Remove Duplicates from Sorted Array.
     *
     * note:
     *  1.old replace new
     *  2.return new arr len , 还以为是重复数的个数。
     *
     */
    public int Solution(int[] nums)
    {
      var count = 0;
      for (int i = 0; i < nums.Length - 1; i++)
      {
        if (nums[i] == nums[i + 1])
          count++;
        else
          nums[i + 1 - count] = nums[i + 1];
      }

      return nums.Length - count;
      //      int count = 0, moveLen;
      //      for (int i = 1; i < nums.Length - count; i++)
      //      {
      //        if (nums[i] == nums[i - 1])
      //        {
      //          moveLen = 1;
      //
      //          if (i + 1 < nums.Length)
      //          {
      //            nums[i] = nums[i + 1];
      //          }
      //
      //          for (int j = i; j < nums.Length - moveLen - count; j++)
      //          {
      //            if (nums[j] == nums[j + 1])
      //            {
      //              moveLen++;
      //            }
      //
      //            nums[j] = nums[j + moveLen];
      //          }
      //
      //          count += moveLen;
      //        }
      //      }
      //
      //      return count;
    }
  }
}