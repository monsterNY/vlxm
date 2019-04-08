using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : CombinationSum4  
  /// @author :mons
  /// @create : 2019/4/4 17:26:48 
  /// @source : https://leetcode.com/problems/combination-sum-iv/
  /// </summary>
  [Obsolete(
    "DP,source:https://leetcode.com/problems/combination-sum-iv/discuss/85036/1ms-Java-DP-Solution-with-Detailed-Explanation")]
  public class CombinationSum4
  {
    #region otherSolution

    //source:https://leetcode.com/problems/combination-sum-iv/discuss/85036/1ms-Java-DP-Solution-with-Detailed-Explanation
    // time limit
    public int combinationSum4(int[] nums, int target)
    {
      if (target == 0) return 1;

      var res = 0;
      for (var i = 0; i < nums.Length; i++)
        if (target >= nums[i])
          res += combinationSum4(nums, target - nums[i]);

      return res;
    }

    public int OtherSolution(int[] nums, int target)
    {
      int[] comb = new int[target + 1];
      comb[0] = 1;
      for (int i = 1; i < comb.Length; i++)
      {
        for (int j = 0; j < nums.Length; j++)
        {
          if (i - nums[j] >= 0)
          {
            comb[i] += comb[i - nums[j]];
          }
        }
      }
      return comb[target];
    }

    #endregion


    protected int Count = 0;

    public int Try(int[] nums, int target)
    {
      for (var i = 0; i < nums.Length; i++) GetPossible(target, 0, i, nums);

      return 0;
    }

    //time limit
    public void GetList(int[] arr, int build, int target)
    {
      if (build > target) return;
      for (var i = 0; i < arr.Length; i++)
      {
        if (build + arr[i] == target)
        {
          Count++;
          continue;
        }

        if (target - build % arr[i] == 0)
        {
          Count++;
          continue;
        }

        GetList(arr, build + arr[i], target);
      }
    }

    //no
    public void GetPossible(int target, int sum, int index, int[] arr)
    {
      if (sum > target) return;
      if (sum == target || target - sum % arr[index] == 0) Count++;

      if (index == arr.Length) return;

      if (target - sum % arr[index] == 0) Count++;

      GetPossible(target, sum + arr[index], index + 1, arr);
    }
  }
}