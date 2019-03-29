using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : TopKFrequent  
  /// @author :mons
  /// @create : 2019/3/29 14:02:48 
  /// @source : https://leetcode.com/problems/top-k-frequent-elements/
  /// </summary>
  public class TopKFrequent
  {

    /**
     * Runtime: 264 ms, faster than 94.89% of C# online submissions for Top K Frequent Elements.
     * Memory Usage: 31.5 MB, less than 79.31% of C# online submissions for Top K Frequent Elements.
     *
     * are you kidding me???
     *
     */
    public IList<int> Simple(int[] nums, int k)
    {
      Dictionary<int, int> dictionary = new Dictionary<int, int>();

      for (int i = 0; i < nums.Length; i++)
      {
        if (dictionary.ContainsKey(nums[i]))
          dictionary[nums[i]]++;
        else
          dictionary.Add(nums[i], 1);
      }

      var list = new List<int>();

      foreach (var item in dictionary.OrderByDescending(u => u.Value))//主要是排序处理。。。
      {
        if (k == 0) break;
        list.Add(item.Key);
        k--;
      }

      return list;
    }
  }
}