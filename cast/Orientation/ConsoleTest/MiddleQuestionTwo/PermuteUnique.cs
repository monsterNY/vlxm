using System;
using System.Collections.Generic;
using System.Text;
using Tools.CusAttr;
using Tools.CusMenu;

namespace ConsoleTest.MiddleQuestionTwo
{
  /// <summary>
  /// @desc : PermuteUnique  
  /// @author :mons
  /// @create : 2019/4/12 16:34:13 
  /// @source : https://leetcode.com/problems/permutations-ii/
  /// </summary>
  [Obsolete]
  [Love(LoveTypes.Question)]
  public class PermuteUnique
  {

    public List<List<int>> permuteUnique(int[] nums)
    {
      List<List<int>> res = new List<List<int>>();
      if (nums == null || nums.Length == 0) return res;
      bool[] used = new bool[nums.Length];
      List<int> list = new List<int>();
      Array.Sort(nums);
      dfs(nums, used, list, res);
      return res;
    }

    public void dfs(int[] nums, bool[] used, List<int> list, List<List<int>> res)
    {
      if (list.Count == nums.Length)
      {
        res.Add(new List<int>(list));
        return;
      }

      for (int i = 0; i < nums.Length; i++)
      {
        if (used[i]) continue;
        if (i > 0 && nums[i - 1] == nums[i] && !used[i - 1]) continue;
        used[i] = true;
        list.Add(nums[i]);
        dfs(nums, used, list, res);
        used[i] = false;
        list.RemoveAt(list.Count - 1);
      }
    }

    public IList<IList<int>> Solution(int[] nums)
    {
      var dp = new bool[nums.Length][];
      for (int i = 0; i < dp.Length; i++)
      {
        dp[i] = new bool[nums.Length];
      }

      return GetList(new List<int>(nums), new List<int>(), new List<IList<int>>());
    }

    public List<IList<int>> GetList(List<int> arr, IList<int> build, List<IList<int>> result)
    {
      if (arr.Count == 0)
        result.Add(new List<int>(build));

      for (int i = 0; i < arr.Count; i++)
      {
        var temp = new List<int>(arr);
        temp.RemoveAt(i);

        build.Add(arr[i]);

        GetList(temp, build, result);

        build.RemoveAt(build.Count - 1);
      }

      return result;
    }
  }
}