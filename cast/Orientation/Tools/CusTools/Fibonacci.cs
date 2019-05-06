using System;
using System.Collections.Generic;
using System.Text;

namespace Tools.CusTools
{
  /// <summary>
  /// @desc : Fibonacci   斐波那契数列
  /// @author : mons
  /// @create : 2019/4/30 16:29:21 
  /// @source : 
  /// </summary>
  public static class Fibonacci
  {
    public static int GetResult(int number)
    {
      if (number < 2) return number;

      int prev = 1, num = 2, temp;

      while (number-- > 2)
      {
        temp = num;
        num = num + prev;
        prev = temp;
      }

      return num;
    }
  }
}