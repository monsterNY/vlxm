using System;
using System.Collections.Generic;
using System.Text;
using Tools.CusExtension;

namespace ConsoleTest.HardQuestion
{
  /// <summary>
  /// @desc : LongestConsecutive  
  /// @author : mons
  /// @create : 2019/6/5 16:22:14 
  /// @source : 
  /// </summary>
  public class LongestConsecutive
  {

    public int OtherSolution(int[] num)
    {
      int res = 0;
      Dictionary<int, int> map = new Dictionary<int, int>();
      foreach (int n in num)
      {
        if (!map.ContainsKey(n))//仅处理未在区域内的值 即记录的值
        {
          int left = (map.ContainsKey(n - 1)) ? map.Get(n - 1) : 0;
          int right = (map.ContainsKey(n + 1)) ? map.Get(n + 1) : 0;
          // sum: length of the sequence n is in
          int sum = left + right + 1;
          map.Add(n, sum);

          // keep track of the max length 
          res = Math.Max(res, sum);

          // extend the length to the boundary(s)
          // of the sequence
          // will do nothing if n has no neighbors
          map.AddOrSet(n - left, sum);//设置左边界值
          map.AddOrSet(n + right, sum);//设置右边界值
        }
      }
      return res;
    }

    /**
     * Runtime: 96 ms, faster than 95.06% of C# online submissions for Longest Consecutive Sequence.
     * Memory Usage: 22.8 MB, less than 92.04% of C# online submissions for Longest Consecutive Sequence.
     *
     * ???
     *
     * 可以解决 但未达到O(n)的要求
     *
     */
    public int Simple(int[] nums)
    {
      if (nums.Length < 2) return nums.Length;

      int res = 1, len = nums.Length, diff = 1, item = 1;

      Array.Sort(nums);

      for (int i = 1; i < nums.Length; i++)
      {
        if (nums[i] == nums[i - diff])
        {
          diff++;
          continue;
        }

        if (nums[i] - nums[i - diff] == 1)
          item++;
        else item = 1;

        diff = 1;

        if (item > res) res = item;
      }

      return res;
    }

    public int Try(int[] nums)
    {
      int res = 0, len = nums.Length;

      //memory limit
      var flag = new int[int.MaxValue];

      for (int i = 0; i < len; i++)
        flag[nums[i]] = 1;

      for (int i = 0; i < int.MaxValue;)
      {
        var item = 0;
        while (flag[i++] == 1)
        {
          item++;
        }

        res = Math.Max(item, res);
      }

      return res;
    }
  }
}