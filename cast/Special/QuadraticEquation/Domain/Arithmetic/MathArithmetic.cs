using System;
using System.Collections.Generic;
using System.Text;

namespace QuadraticEquation.Domain.Arithmetic
{
  public class MathArithmetic
  {
    /// <summary>
    /// 获取两个值的最小公倍数
    /// </summary>
    /// <param name="num"></param>
    /// <param name="num2"></param>
    /// <returns></returns>
    public static int GetMinCommonMultiple(int num, int num2)
    {
      num = num > 0 ? num : -num;
      num2 = num2 > 0 ? num2 : -num2;

      var max = num > num2 ? num : num2;

      for (var i = max; i < (num * num2); i++)
      {
        if (i % num == 0 && i % num2 == 0)
        {
          return i;
        }
      }

      return 0;
    }

    /// <summary>
    /// 符号是否相同
    /// </summary>
    /// <param name="num"></param>
    /// <param name="num2"></param>
    /// <returns></returns>
    public static bool IsSameChar(int num, int num2)
    {
      return (num >= 0 && num2 >= 0) || (num < 0 && num2 < 0);
    }
  }
}