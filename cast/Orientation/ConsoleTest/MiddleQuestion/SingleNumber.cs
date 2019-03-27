
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : SingleNumber  
  /// @author :mons
  /// @create : 2019/3/27 17:33:54 
  /// @source : https://leetcode.com/problems/single-number-iii/
  /// </summary>
  public class SingleNumber
  {

    /**
     * Runtime: 252 ms, faster than 90.68% of C# online submissions for Single Number III.
     * Memory Usage: 30 MB, less than 14.29% of C# online submissions for Single Number III.
     */
    public int[] Solution(int[] nums)
    {

      //Runtime: 252 ms, faster than 90.68% of C# online submissions for Single Number III.
      //Memory Usage: 29.8 MB, less than 14.29 % of C# online submissions for Single Number III.
      ISet<int> sets = new HashSet<int>();

      //Dictionary ContainsKey 快于 list container
//      Dictionary<int, int> dictionary = new Dictionary<int, int>();

      for (int i = 0; i < nums.Length; i++)
      {
        if (sets.Contains(nums[i])) sets.Remove(nums[i]);
        else
          sets.Add(nums[i]);
      }

      return sets.ToArray();
    }

    /**
     *
     * Runtime: 256 ms, faster than 70.34% of C# online submissions for Single Number III.
     * Memory Usage: 29.5 MB, less than 28.57% of C# online submissions for Single Number III.
     *
     * 罪过。，
     *
     * */
    public int[] Try(int[] nums)
    {
      var list = new List<int>();

      Array.Sort(nums);

      for (int i = 0; i < nums.Length; i += 2)
      {
        if (i == nums.Length - 1)
        {
          list.Add(nums[i]);
          break;
        }

        if (nums[i] != nums[i + 1])
        {
          list.Add(nums[i--]);
        }
      }

      return list.ToArray();
    }

    /**
     * Runtime: 252 ms, faster than 90.68% of C# online submissions for Single Number III.
     * Memory Usage: 29.4 MB, less than 57.14% of C# online submissions for Single Number III.
     */
    public int[] OtherSolution(int[] nums)
    {
      // Pass 1 : 
      // Get the XOR of the two numbers we need to find
      int diff = 0;
      foreach (int num in nums)
      {
        diff ^= num;
      }
      // Get its last set bit
      diff &= -diff;

      // Pass 2 :
      int[] rets = { 0, 0 }; // this array stores the two numbers we will return
      foreach (int num in nums)
      {
        if ((num & diff) == 0) // the bit is not set
        {
          rets[0] ^= num;
        }
        else // the bit is set
        {
          rets[1] ^= num;
        }
      }
      return rets;
    }

  }
}