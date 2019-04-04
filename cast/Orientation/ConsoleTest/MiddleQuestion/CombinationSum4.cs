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
  public class CombinationSum4
  {
    protected int Count = 0;

    public int Solution(int[] nums, int target)
    {
      for (int i = 0; i < nums.Length; i++)
      {
        GetPossible(target, 0, i, nums);
      }

      return 0;
    }

    //time limit
    public void GetList(int[] arr, int build, int target)
    {
      if (build > target) return;
      for (int i = 0; i < arr.Length; i++)
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
      if (sum == target || target - sum % arr[index] == 0)
      {
        Count++;
      }

      if (index == arr.Length) return;

      if (target - sum % arr[index] == 0)
      {
        Count++;
      }

      GetPossible(target, sum + arr[index], index + 1, arr);
    }
  }
}