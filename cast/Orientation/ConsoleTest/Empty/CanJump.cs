using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.Empty
{
  /// <summary>
  /// @desc : CanJump  
  /// @author :monster_yj
  /// @create : 2019/4/24 19:37:24 
  /// @source : 
  /// </summary>
  public class CanJump
  {
    public bool Solution(int[] nums)
    {
      if (nums.Length == 1) return true;
      if (nums[0] == 0) return false;

      int needStep = 1;
      for (int i = nums.Length - 2; i > 0; i--)
        if (nums[i] < needStep)
          needStep++;
        else
          needStep = 1;

      return nums[0] >= needStep;
    }

    public bool Solution2(int[] nums)
    {
      if (nums.Length == 1) return true;
      if (nums[0] == 0) return false;

      int needStep = 1;
      var i = nums.Length - 2;
      while (i > 0)
      {

        if (nums[i] > i) ;

      }

      return nums[0] >= needStep;
    }
  }
}