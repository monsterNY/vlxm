using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.Mock
{
  /// <summary>
  /// @desc : CombinationSum  
  /// @author : mons
  /// @create : 2019/5/28 14:10:27 
  /// @source : 
  /// </summary>
  public class CombinationSum
  {

    /**
     * Runtime: 256 ms, faster than 83.35% of C# online submissions for Combination Sum.
     * Memory Usage: 29.9 MB, less than 94.37% of C# online submissions for Combination Sum.
     */
    public IList<IList<int>> Solution(int[] candidates, int target)
    {
      IList<IList<int>> res = new List<IList<int>>();

      Helper(candidates, 0, target, new List<int>(), res);

      return res;
    }

    public void Helper(int[] arr, int index, int target, List<int> builder, IList<IList<int>> res)
    {
      if (target == 0)
        res.Add(new List<int>(builder));

      for (int i = index; i < arr.Length; i++)
      {
        if (target - arr[i] >= 0)
        {
          builder.Add(arr[i]);
          Helper(arr, i, target - arr[i], builder, res);
          builder.Remove(arr[i]);
        }
      }
    }
  }
}