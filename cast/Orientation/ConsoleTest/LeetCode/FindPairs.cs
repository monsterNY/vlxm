using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Tools.RefTools;

namespace ConsoleTest.LeetCode
{
  /// <summary>
  /// @desc : FindPairs  
  /// @author :mons
  /// @create : 2019/3/14 15:43:19 
  /// @source : https://leetcode.com/problems/k-diff-pairs-in-an-array/
  /// </summary>
  public class FindPairs
  {
    /// <summary>
    ///
    /// 看似简单，实则复杂的题目呀。，
    ///
    /// Runtime: 2036 ms, faster than 5.59% of C# online submissions for K-diff Pairs in an Array.
    /// Memory Usage: 28.1 MB, less than 62.50% of C# online submissions for K-diff Pairs in an Array.
    ///
    /// twice low~~~
    /// 
    /// </summary>
    /// <param name="nums"></param>
    /// <param name="k"></param>
    /// <returns></returns>
    public int Solution(int[] nums, int k)
    {
      int count = 0;

      var map = new Dictionary<int, int>();

      int max, min;

      for (var i = 0; i < nums.Length; i++)
      {
        for (var j = i + 1; j < nums.Length; j++)
        {
          if (nums[i] > nums[j])
          {
            max = nums[i];
            min = nums[j];
          }
          else
          {
            max = nums[j];
            min = nums[i];
          }

          if ((max - min) == k && !map.ContainsKey(min))
          {
            count++;
            map.Add(min, max);
          }
        }
      }

      return count;
    }

    public int Optimize(int[] nums, int k)
    {
      int count = 0;

      List<int> list = new List<int>();

      int max, min;

      for (var i = 0; i < nums.Length; i++)
      {
        for (var j = i + 1; j < nums.Length; j++)
        {
          if (nums[i] > nums[j])
          {
            max = nums[i];
            min = nums[j];
          }
          else
          {
            max = nums[j];
            min = nums[i];
          }

          if ((max - min) == k && !list.Contains(min))
          {
            count++;
            list.Add(min);
          }
        }
      }

      return count;
    }

    /**
     * Runtime: 120 ms, faster than 90.68% of C# online submissions for K-diff Pairs in an Array.
     * Memory Usage: 28.8 MB, less than 25.00% of C# online submissions for K-diff Pairs in an Array.
     *
     * so cool~
     *
     */
    public int OtherSolution(int[] nums, int k)
    {
      if (nums == null || nums.Length == 0 || k < 0) return 0;

      Dictionary<int, int> map = new Dictionary<int, int>();
      int count = 0;
      foreach (int i in nums)
      {
        if (!map.ContainsKey(i))
          map.Add(i, 1);
        else
          map[i]++;
      }

      foreach (var item in map)
      {
        if (k == 0)
        {
          //count how many elements in the array that appear more than twice.
          if (item.Value >= 2)
          {
            count++;
          }
        }
        else
        {
          if (map.ContainsKey(item.Key + k))
          {
            count++;
          }
        }
      }

      return count;
    }
  }
}