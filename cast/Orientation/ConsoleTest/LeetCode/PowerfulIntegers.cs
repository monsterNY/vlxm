using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.LeetCode
{
  /// <summary>
  /// @source:https://leetcode.com/problems/powerful-integers/
  /// 2019-3-8
  /// </summary>
  public class PowerfulIntegers
  {
    /**
     * problem1:1 2 100
     *
     * Runtime: 212 ms, faster than 100.00% of C# online submissions for Powerful Integers.
     * Memory Usage: 23.1 MB, less than 100.00% of C# online submissions for Powerful Integers.
     *
     */
    public IList<int> Solution(int x, int y, int bound)
    {
      IList<int> list = new List<int>();

      if (bound < 2)
        return list;

      var numX = 1;
      var numY = 1;

      while (true)
      {
        while (true)
        {
          if (numX + numY <= bound)
          {
            if (!list.Contains(numX + numY))
              list.Add(numX + numY);
          }
          else
          {
            break;
          }

          if (y == 1) break;

          numY *= y;
        }

        numY = 1;
        numX *= x;
        if (numX == 1 || numY + numX > bound) return list;
      }
    }
  }
}