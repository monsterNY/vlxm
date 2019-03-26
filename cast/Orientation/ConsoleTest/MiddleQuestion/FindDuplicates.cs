using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : FindDuplicates  
  /// @author :mons
  /// @create : 2019/3/26 11:24:11 
  /// @source : https://leetcode.com/problems/find-all-duplicates-in-an-array/
  /// </summary>
  public class FindDuplicates
  {
    /**
     * Runtime: 328 ms, faster than 78.89% of C# online submissions for Find All Duplicates in an Array.
     * Memory Usage: 46.2 MB, less than 40.00% of C# online submissions for Find All Duplicates in an Array.
     */
    public IList<int> Simple(int[] nums)
    {
      IList<int> list = new List<int>();

      var dictionary = new Dictionary<int, int>();

      for (var i = 0; i < nums.Length; i++)
        if (dictionary.ContainsKey(nums[i]))
          dictionary[nums[i]]++;
        else
          dictionary.Add(nums[i], 1);

      foreach (var item in dictionary)
        if (item.Value == 2)
          list.Add(item.Key);

      return list;
    }

    /**
     * Runtime: 348 ms, faster than 70.85% of C# online submissions for Find All Duplicates in an Array.
     * Memory Usage: 42.3 MB, less than 70.00% of C# online submissions for Find All Duplicates in an Array.
     *
     * Runtime: 348 ms, faster than 70.85% of C# online submissions for Find All Duplicates in an Array.
     * Memory Usage: 42.2 MB, less than 100.00% of C# online submissions for Find All Duplicates in an Array.
     *
     * 错失了题目提示 。，
     *
     */
    public IList<int> Solution(int[] nums)
    {
      IList<int> list = new List<int>();

      Array.Sort(nums);

      for (var i = 1; i < nums.Length; i++)
        if (nums[i] == nums[i - 1])
          list.Add(nums[i++]);

      return list;
    }

    // when find a number i, flip the number at position i-1 to negative. 
    // if the number at position i-1 is already negative, i is the number that occurs twice.

    /**
     * Runtime: 308 ms, faster than 100.00% of C# online submissions for Find All Duplicates in an Array.
     * Memory Usage: 42.1 MB, less than 100.00% of C# online submissions for Find All Duplicates in an Array.
     */
    public IList<int> OtherSolution(int[] nums)
    {
      IList<int> list = new List<int>();
      for (int i = 0; i < nums.Length; ++i)
      {
        int index = Math.Abs(nums[i]) - 1;
        if (nums[index] < 0)
          list.Add(Math.Abs(index + 1));
        nums[index] = -nums[index];
      }

      return list;
    }
  }
}