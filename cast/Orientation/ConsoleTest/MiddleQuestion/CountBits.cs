using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : CountBits  
  /// @author :mons
  /// @create : 2019/3/22 17:23:21 
  /// @source : https://leetcode.com/problems/counting-bits/
  /// </summary>
  public class CountBits
  {

    /**
     * Runtime: 232 ms, faster than 84.21% of C# online submissions for Counting Bits.
     * Memory Usage: 26.9 MB, less than 100.00% of C# online submissions for Counting Bits.
     *
     * 太假
     *
     */
    public int[] Solution(int num)
    {
      int[] result = new int[num + 1];

      for (int i = 0; i <= num; i++)
      {
        if (i % 2 == 1)
        {
          result[i] = result[i - 1];
        }
        else
        {
          result[i] = Count(i);
        }
      }

      return result;
    }

    public int Count(int num)
    {
      var count = 0;
      while (num > 0)
      {
        if (num % 2 == 1) count++;
        num /= 2;
      }

      return count;
    }
  }
}