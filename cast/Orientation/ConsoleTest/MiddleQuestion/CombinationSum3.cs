using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : CombinationSum3  
  /// @author :mons
  /// @create : 2019/4/2 14:17:08 
  /// @source : https://leetcode.com/problems/combination-sum-iii/
  /// </summary>
  public class CombinationSum3
  {
    /**
     * Runtime: 220 ms, faster than 60.36% of C# online submissions for Combination Sum III.
     * Memory Usage: 23.5 MB, less than 33.33% of C# online submissions for Combination Sum III.
     *
     * Runtime: 212 ms, faster than 100.00% of C# online submissions for Combination Sum III.
     * Memory Usage: 23.5 MB, less than 33.33% of C# online submissions for Combination Sum III. -- 测试有毒
     *
     */
    public IList<IList<int>> Solution(int k, int n)
    {
      IList<IList<int>> result = new List<IList<int>>();

      GetAllList(0, k, n, new List<int>(), result, 1);

      return result;
    }

    public void GetAllList(int num, int limit, int target, IList<int> list, IList<IList<int>> result, int startIndex)
    {
      if (num > target) return;
      if (list.Count == limit)
      {
        if (num == target) result.Add(list);
        return;
      }

      for (; startIndex < 9; startIndex++)
      {
        var item = new List<int>(list);
        item.Add(startIndex);
        GetAllList(num + startIndex, limit, target, item, result, startIndex + 1);
      }
    }

  }
}