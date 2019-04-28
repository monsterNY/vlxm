using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestionThree
{
  /// <summary>
  /// @desc : ThreeSum  
  /// @author : mons
  /// @create : 2019/4/28 9:48:34 
  /// @source : https://leetcode.com/problems/3sum/
  /// </summary>
  public class ThreeSum
  {
    //Time Limit
    public IList<IList<int>> Solution(int[] nums)
    {
      IList<IList<int>> res = new List<IList<int>>();

      if (nums.Length < 3) return res;

      Array.Sort(nums);

      Helper(nums, 0, new List<int>(), res, 0);

      return res;
    }

    public void Helper(int[] arr, int index, IList<int> builder, IList<IList<int>> res, int target)
    {
      if (builder.Count > 3 || builder.Count + arr.Length - index < 3) return;

      if (builder.Count == 3)
      {
        if (target == 0) res.Add(new List<int>(builder));

        return;
      }

      for (var i = index; index < arr.Length; index++)
      {
        if (arr[i] + target > 0 || (index > i && arr[index] == arr[index - 1])) continue;

        builder.Add(arr[index]);

        Helper(arr, index + 1, builder, res, target + arr[index]);

        builder.Remove(arr[index]);
      }
    }
  }
}