using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.LeetCode
{
  /// <summary>
  /// @desc : TrailingZeroes  
  /// @author :mons
  /// @create : 2019/3/18 15:11:20 
  /// @source : https://leetcode.com/problems/factorial-trailing-zeroes/
  /// </summary>
  public class TrailingZeroes
  {
    public int Solution(int n)
    {
      if (n < 5) return 0;

      var count = 0;

      long sum = 1;

      for (int i = 2; i <= n; i++)
      {
        if (i % 2 != 0 && i % 5 != 0) continue;
        sum *= i;

        if (i == 78)
        {
          Console.WriteLine("---");
        }

        while (sum % 10 == 0)
        {
          count++;
          sum /= 10;
        }
      }

      return count;
    }

    public long GetSum(int n)
    {
      long sum = 1;
      var validFlag = 10;
      for (int i = 2; i <= n; i++)
      {
        sum *= i;
        Console.WriteLine($"{sum} --------------- {i}");
      }

      return sum;
    }

    /**
     * @source:https://leetcode.com/problems/factorial-trailing-zeroes/discuss/52371/My-one-line-solutions-in-3-languages
     *
     * This question is pretty straightforward.
     * Because all trailing 0 is from factors 5 * 2.
     *
     * But sometimes one number may have several 5 factors, for example, 25 have two 5 factors, 125 have three 5 factors. In the n! operation, factors 2 is always ample. So we just count how many 5 factors in all number from 1 to n.
     *
     *
     * Runtime: 36 ms, faster than 100.00% of C# online submissions for Factorial Trailing Zeroes.
     * Memory Usage: 12.9 MB, less than 88.89% of C# online submissions for Factorial Trailing Zeroes.
     *
     * 这也太酷了吧
     *
     */
    public int OtherSolution(int n)
    {
      return n == 0 ? 0 : n / 5 + OtherSolution(n / 5);
    }
  }
}