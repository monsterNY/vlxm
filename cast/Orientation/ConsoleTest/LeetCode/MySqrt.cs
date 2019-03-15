using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.LeetCode
{
  /// <summary>
  /// @desc : MySqrt  
  /// @author :mons
  /// @create : 2019/3/15 16:31:52 
  /// @source : https://leetcode.com/problems/sqrtx/
  /// </summary>
  public class MySqrt
  {
    /**
     * Runtime: 84 ms, faster than 11.82% of C# online submissions for Sqrt(x).
     * Memory Usage: 13.1 MB, less than 32.79% of C# online submissions for Sqrt(x).
     *
     * ha ha 老套路弄这边就没啥用了
     *
     */
    public int Solution(int num)
    {
      if (num < 2)
        return num;

      var start = 1;
      var temp = num;
      while ((temp = temp / 100) > 0)
      {
        start *= 10;
      }

      for (;; start++)
      {
        if (start * start > num || start * start < 0)
        {
          return start - 1;
        }

        if (start * start == num)
        {
          return start;
        }
      }
    }

    /**
     * Runtime: 40 ms, faster than 100.00% of C# online submissions for Sqrt(x).
     * Memory Usage: 13 MB, less than 78.69% of C# online submissions for Sqrt(x).
     *
     * cool~ =-= 这里二分又可以了
     *
     */
    public int Optimize(int num)
    {
      if (num < 2)
        return num;

      int start = 1, end, middle;

      var temp = num;
      while ((temp = temp / 100) > 0)
        start *= 10;

      end = start * 10;

      while (end > start + 1)
      {
        middle = start + (end - start) / 2;

        if (middle * middle > num || middle * middle < 0)
          end = middle;
        else if (middle * middle < num)
          start = middle;
        else
          return middle;
      }

      return end * end > num || end * end < 0 ? start : end;
    }
  }
}