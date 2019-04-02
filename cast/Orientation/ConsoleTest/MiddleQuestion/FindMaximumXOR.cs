using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : FindMaximumXOR  
  /// @author :mons
  /// @create : 2019/4/2 15:51:21 
  /// @source : https://leetcode.com/problems/maximum-xor-of-two-numbers-in-an-array/
  /// </summary>
  public class FindMaximumXOR
  {

    /**
     *
     * simple solution
     *
     * Runtime: 488 ms, faster than 20.90% of C# online submissions for Maximum XOR of Two Numbers in an Array.
     * Memory Usage: 23.1 MB, less than 100.00% of C# online submissions for Maximum XOR of Two Numbers in an Array.
     *
     * 位操作实在是经验太少。
     *
     */
    public int Solution(int[] nums)
    {
      var max = 0;

      for (int i = 0; i < nums.Length; i++)
      {
        if (nums[i] < max) continue;
        for (int j = i+1; j < nums.Length; j++)
        {
          if ((nums[i] ^ nums[j]) > max) max = nums[i] ^ nums[j];
        }
      }

      return max;
    }

    /**
     * Runtime: 128 ms, faster than 97.01% of C# online submissions for Maximum XOR of Two Numbers in an Array.
     * Memory Usage: 40.8 MB, less than 71.43% of C# online submissions for Maximum XOR of Two Numbers in an Array.
     */
    public int OtherSolution(int[] nums)
    {
      int max = 0, mask = 0;
      for (int i = 31; i >= 0; i--)
      {
        mask = mask | (1 << i);
        ISet<int> set = new HashSet<int>();
        foreach (int num in nums)
        {
          set.Add(num & mask);
        }
        int tmp = max | (1 << i);
        foreach (int prefix in set)
        {
          if (set.Contains(tmp ^ prefix))
          {
            max = tmp;
            break;
          }
        }
      }
      return max;
    }

  }
}