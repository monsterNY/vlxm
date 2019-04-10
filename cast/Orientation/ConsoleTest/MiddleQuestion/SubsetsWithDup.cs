using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : SubsetsWithDup  
  /// @author :mons
  /// @create : 2019/4/10 14:43:52 
  /// @source : https://leetcode.com/problems/subsets-ii/
  /// </summary>
  [Obsolete("no imagination")]
  public class SubsetsWithDup
  {
    public IList<IList<int>> Solution(int[] nums)
    {
      Dictionary<int, int> dictionary = new Dictionary<int, int>();

      foreach (var num in nums)
      {
        if (dictionary.ContainsKey(num))
          dictionary[num]++;
        else dictionary.Add(num, 1);
      }

      IList<IList<int>> list = new List<IList<int>>();
      var keys = dictionary.Keys.ToArray();
      GetAllList(keys, 0, new List<int>(), list);
      
      return list;
    }

    public void GetAllList(int[] arr, int startIndex, List<int> build, IList<IList<int>> res)
    {
      res.Add(build);

      for (; startIndex < arr.Length; startIndex++)
      {
        var list = new List<int>(build);
        list.Add(arr[startIndex]);
        GetAllList(arr, startIndex + 1, list, res);
      }
    }

  }
}