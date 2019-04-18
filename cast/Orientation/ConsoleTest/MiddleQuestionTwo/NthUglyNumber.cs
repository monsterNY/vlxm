using System;
using System.Collections.Generic;
using System.Text;
using Tools.CusAttr;
using Tools.CusMenu;

namespace ConsoleTest.MiddleQuestionTwo
{
  /// <summary>
  /// @desc : NthUglyNumber  
  /// @author :mons
  /// @create : 2019/4/18 14:59:32 
  /// @source : https://leetcode.com/problems/ugly-number-ii/
  /// </summary>
  [Obsolete("math")]
  [Love(LoveTypes.Question)]
  public class NthUglyNumber
  {
    public int Solution(int n)
    {
      if (n == 1) return 1;
      n--;
      var num = 2;

      while (true)
      {
        if (num % 3 == 0)
        {
          if (n < 6)
            break;
          n -= 6;
        }
        else
        {
          if (n < 8)
            break;
          n -= 8;
        }

        num++;
      }

      var res = (num - 2) * 10;

      for (int i = res + 2; n > 0; i++)
      {
        if (i % 2 == 0 || i % 3 == 0 || i % 5 == 0)
        {
          res = i;
          n--;
        }
      }

      return res;
    }
  }
}