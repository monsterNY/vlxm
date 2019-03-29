using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : Permute  
  /// @author :mons
  /// @create : 2019/3/29 13:51:47 
  /// @source : https://leetcode.com/problems/permutations/
  /// </summary>
  public class Permute
  {

    /**
     * Runtime: 252 ms, faster than 83.22% of C# online submissions for Permutations.
     * Memory Usage: 30.7 MB, less than 18.18% of C# online submissions for Permutations.
     *
     * ha , I've done this before.
     *
     */
    public IList<IList<int>> Solution(int[] nums)
    {
      IList<IList<int>> result = new List<IList<int>>();

      GetList(new List<int>(nums), new List<int>(), result);

      return result;
    }

    public void GetList(List<int> arr, List<int> build, IList<IList<int>> list)
    {
      if (arr.Count == 0)
      {
        list.Add(build);
      }

      for (int i = 0; i < arr.Count; i++)
      {
        var temp = new List<int>(arr);
        temp.RemoveAt(i);

        var item = new List<int>(build);
        item.Add(arr[i]);
        GetList(temp, item, list);
      }
    }
  }
}