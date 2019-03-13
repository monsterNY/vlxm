using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.LeetCode
{
  /// <summary>
  /// @desc : CheckPossibility  
  /// @author :mons
  /// @create : 2019/3/13 13:59:33 
  /// @source : https://leetcode.com/problems/non-decreasing-array/
  /// </summary>
  public class CheckPossibility
  {
    public bool Solution(int[] nums)
    {
      var arr = nums.Clone();

      var changeFlag = false;

      for (int i = 1; i < nums.Length; i++)
      {
        if (nums[i] < nums[i - 1])
        {
          if (changeFlag)
            return false;

          //          if (i + 1 < nums.Length && nums[i + 1] < nums[i - 1] && nums[i] <= nums[i + 1])
          //          {
          //            if (i > 1 && nums[i - 2] > nums[i])
          //            {
          //              return false;
          //            }
          //
          //            nums[i - 1] = nums[i];
          //          }
          //          else if (i + 1 < nums.Length && nums[i + 1] >= nums[i - 1])
          //          {
          //            nums[i] = nums[i - 1];
          //          }
          if (i + 1 < nums.Length)
          {
            if (i > 1 && nums[i - 2] > nums[i])
            {
              return false;
            }

            if (nums[i + 1] < nums[i - 1] && nums[i] <= nums[i + 1])
            {
              nums[i - 1] = nums[i];
            }
            else
              nums[i] = nums[i - 1];
          }
          else if (i == nums.Length - 1)
          {
            nums[i] = nums[i - 1];
          }

          changeFlag = true;
        }
      }

      var result = true;

      for (int i = 1; i < nums.Length; i++)
      {
        if (nums[i] < nums[i - 1])
          throw new Exception("error");
      }

      return true;
    }

    /**
     *
     * Runtime: 128 ms, faster than 86.36% of C# online submissions for Non-decreasing Array.
     * Memory Usage: 29.5 MB, less than 100.00% of C# online submissions for Non-decreasing Array.
     *
     * Runtime: 136 ms, faster than 85.23% of C# online submissions for Non-decreasing Array.
     * Memory Usage: 29.6 MB, less than 57.14% of C# online submissions for Non-decreasing Array.
     *
     * error 5 times .,
     * 有够难的。
     *
     */
    public bool Optimize(int[] nums)
    {
      if (nums.Length < 3)
        return true;

      var changeFlag = false;

      for (int i = 1; i < nums.Length - 1; i++)
      {
        if (nums[i] < nums[i - 1])
        {
          if (changeFlag)
            return false;

          if (nums[i + 1] < nums[i - 1] && nums[i] <= nums[i + 1])
          {
            if (i > 1 && nums[i - 2] > nums[i])
              return false;

            nums[i - 1] = nums[i];
          }
          else
            nums[i] = nums[i - 1];

          changeFlag = true;
        }
      }

      if (changeFlag && nums[nums.Length - 1] < nums[nums.Length - 2])
        return false;

      return true;

      #region bast

      //使用if else-if 比 if else 效率高？？？
      //--- 百思不得其解=-=
      //      var changeFlag = false;

//      for (int i = 1; i < nums.Length; i++)
//      {
//        if (nums[i] < nums[i - 1])
//        {
//          if (changeFlag)
//            return false;
//
//          if (i + 1 < nums.Length)
//          {
//            if (nums[i + 1] < nums[i - 1] && nums[i] <= nums[i + 1])
//            {
//              if (i > 1 && nums[i - 2] > nums[i])
//                return false;
//
//              nums[i - 1] = nums[i];
//            }
//            else if (i + 1 < nums.Length && nums[i + 1] >= nums[i - 1])
//            {
//              nums[i] = nums[i - 1];
//            }
//          }
//
//          changeFlag = true;
//        }
//      }
//
//      return true;

      #endregion
    }

    /**
     * nice flag =.
     */
    public bool OtherSolution(int[] nums)
    {
      int cnt = 0;                                                                    //the number of changes
      for (int i = 1; i < nums.Length && cnt <= 1; i++)
      {
        if (nums[i - 1] > nums[i])
        {
          cnt++;
          if (i - 2 < 0 || nums[i - 2] <= nums[i]) nums[i - 1] = nums[i];                    //modify nums[i-1] of a priority
          else nums[i] = nums[i - 1];                                                //have to modify nums[i]
        }
      }
      return cnt <= 1;
    }

  }
}