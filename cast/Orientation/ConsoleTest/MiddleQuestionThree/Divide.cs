using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestionThree
{
  /// <summary>
  /// @desc : Divide  
  /// @author : mons
  /// @create : 2019/4/30 9:54:42 
  /// @source : https://leetcode.com/problems/divide-two-integers/
  /// </summary>
  [Obsolete]
  public class Divide
  {
    //error int_max int_min
    //Time Limit
    public int Solution(int dividend, int divisor)
    {
      if (divisor == 1) return dividend;

      bool isPositive = dividend >= 0, isPositive2 = divisor >= 0;

      int num = (dividend == int.MinValue) ? int.MaxValue : isPositive ? dividend : -dividend,
        num2 = (dividend == int.MinValue) ? int.MaxValue : isPositive2 ? divisor : -divisor;

      if (num2 > num) return 0;

      if (num2 == 1) return isPositive == isPositive2 ? dividend : -dividend;

      var count = 0;

      while ((num -= num2) > 0)
      {
        count++;
      }

      return isPositive == isPositive2 ? count : -count;
    }
  }
}