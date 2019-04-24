using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestionThree
{
  /// <summary>
  /// @desc : CanJump  
  /// @author : mons
  /// @create : 2019/4/24 17:42:40 
  /// @source : https://leetcode.com/problems/jump-game/
  /// </summary>
  public class CanJump
  {
    /**
     * Runtime: 116 ms, faster than 59.83% of C# online submissions for Jump Game.
     * Memory Usage: 24 MB, less than 89.19% of C# online submissions for Jump Game.
     */
    public bool Solution(int[] nums)
    {
      if (nums.Length == 1) return true;
      if (nums[0] == 0) return false;

      int needStep = 1;
      for (int i = nums.Length - 2; i > 0; i--)
      {
        if (nums[i] < needStep)
          needStep++;
        else
          needStep = 1;
      }

      return nums[0] >= needStep;
    }

    public bool Solution2(int[] nums)
    {
      return Helper(nums, 0);
    }

    //Time Limit
    public bool Helper(int[] nums, int index)
    {
      if (index + nums[index] >= nums.Length - 1) return true;
      if (nums[index] == 0) return false;

      for (int i = index + 1; i <= index + nums[index]; i++)
      {
        if (Helper(nums, i)) return true;
      }

      return false;
    }

    public bool Solution3(int[] nums)
    {
      bool[] flag = new bool[nums.Length];
      return Helper2(nums, 0, flag);
    }

    public bool Helper2(int[] nums, int index, bool[] flag)
    {
      if (index + nums[index] >= nums.Length - 1) return true;
      if (nums[index] == 0 || flag[index])
      {
        flag[index] = true;
        return false;
      }

      for (int i = index + 1; i <= index + nums[index]; i++)
      {
        if (Helper(nums, i)) return true;
      }

      flag[index] = true;

      return false;
    }
  }
}