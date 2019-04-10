using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : FindSubsequences  
  /// @author :mons
  /// @create : 2019/4/10 16:41:16 
  /// @source : https://leetcode.com/problems/increasing-subsequences/
  /// </summary>
  [Obsolete("bug:repetition")]
  public class FindSubsequences
  {
    public IList<IList<int>> Solution(int[] nums)
    {
      IList<IList<int>> res = new List<IList<int>>();

      GetResult(res, nums, 1, new List<int>() {nums[0]});

      return res;
    }

    public void GetResult(IList<IList<int>> res, int[] arr, int startIndex, IList<int> build)
    {
      if (build.Count > 1) res.Add(build);

      for (; startIndex < arr.Length; startIndex++)
      {
        if (build[build.Count - 1] < arr[startIndex])
        {
          var list = new List<int>(build);
          list.Add(arr[startIndex]);
          GetResult(res, arr, startIndex + 1, list);

          list = new List<int>(list);
          list.RemoveAt(0);
          GetResult(res,arr,startIndex+1,list);

        }
      }
    }
  }
}