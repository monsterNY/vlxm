using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestionTwo
{
  /// <summary>
  /// @desc : RemoveDuplicates  
  /// @author :mons
  /// @create : 2019/4/12 16:05:12 
  /// @source : https://leetcode.com/problems/remove-duplicates-from-sorted-array-ii/
  /// </summary>
  public class RemoveDuplicates
  {
    /**
     * Runtime: 252 ms, faster than 74.66% of C# online submissions for Remove Duplicates from Sorted Array II.
     * Memory Usage: 30.4 MB, less than 72.73% of C# online submissions for Remove Duplicates from Sorted Array II.
     */
    public int Solution(int[] nums)
    {
      int moveLen = 0, len = nums.Length, i = 1;

      for (; i < len; i++)
      {
        if (i < len - 1 && nums[i - 1] == nums[i] && nums[i] == nums[i + 1])
          moveLen++;
        else if (moveLen > 0)
        {
          i = i - moveLen + 1;
          for (int j = i; j < nums.Length - moveLen; j++)
            nums[j] = nums[j + moveLen];

          moveLen = 0;
        }
      }
      
      return len;
    }

    public int Try(int[] nums)
    {
      int moveLen = 0, len = nums.Length, i = 1;

      for (; i < len - 1; i++)
      {
        if (nums[i - 1] == nums[i] && nums[i] == nums[i + 1])
        {
          moveLen++;
        }
        else if (moveLen > 0)
        {
          len -= moveLen;
          i = i - moveLen + 1;
          for (int j = i; j < nums.Length - moveLen; j++)
          {
            nums[j] = nums[j + moveLen];
          }

          moveLen = 0;
        }
      }

      if (moveLen > 0)
      {
        len -= moveLen;
        for (int j = i - moveLen + 1; j < nums.Length - moveLen; j++)
        {
          nums[j] = nums[j + moveLen];
        }
      }

      return len;
    }

  }
}