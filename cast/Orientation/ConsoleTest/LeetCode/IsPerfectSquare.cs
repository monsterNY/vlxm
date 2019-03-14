using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.LeetCode
{
  /// <summary>
  /// @desc : IsPerfectSquare  
  /// @author :mons
  /// @create : 2019/3/14 16:41:09 
  /// @source : https://leetcode.com/problems/valid-perfect-square/
  /// </summary>
  public class IsPerfectSquare
  {

    /**
     * Runtime: 36 ms, faster than 100.00% of C# online submissions for Valid Perfect Square.
     * Memory Usage: 13 MB, less than 9.09% of C# online submissions for Valid Perfect Square.
     *
     * amazing~~
     *
     */
    public bool Solution(int num)
    {
      if (num < 2)
        return true;

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
          break;
        }

        if (start * start == num)
        {
          return true;
        }
        
      }

      return false;
    }
  }
}