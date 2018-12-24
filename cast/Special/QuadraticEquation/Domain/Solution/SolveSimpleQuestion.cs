using System;
using System.Collections.Generic;
using System.Text;
using QuadraticEquation.Domain.Arithmetic;
using QuadraticEquation.Domain.Entity;

namespace QuadraticEquation.Domain.Solution
{
  /// <summary>
  /// aX + bY = c
  /// </summary>
  public class SolveSimpleQuestion
  {
    public Question[] expression;

    public SolveSimpleQuestion(Question[] expression)
    {
      if (expression == null || expression.Length == 2)
        throw new Exception("参数不符合求解要求！");

      this.expression = expression;
    }

    public void Solve()
    {
      var a = expression[0].ConstA;
      var b = expression[1].ConstA;

      a = a > 0 ? a : -a;
      b = b > 0 ? b : -b;


      //1.获取两个系数共同倍数
      var minCommonMultiple = MathArithmetic.GetMinCommonMultiple(a, b);

      var needMultiple = minCommonMultiple / a;
      var needMultiple2 = minCommonMultiple / b;

      //消一元

      int x, y;

      if (MathArithmetic.IsSameChar(expression[0].ConstA, expression[1].ConstA))
      {
        //expression[0].ConstB * needMultiple - expression[1].ConstB * needMultiple2 ==
        //  expression[0].ConstResult * needMultiple - expression[1].ConstResult * needMultiple2;

        y = (expression[0].ConstResult * needMultiple - expression[1].ConstResult * needMultiple2) /
            expression[0].ConstB * needMultiple - expression[1].ConstB * needMultiple2;
        Console.WriteLine($"y = {y}");
      }
      else
      {
        y = (expression[0].ConstResult * needMultiple + expression[1].ConstResult * needMultiple2) /
            expression[0].ConstB * needMultiple + expression[1].ConstB * needMultiple2;
        Console.WriteLine($"y = {y}");
      }

      x = (expression[0].ConstResult - expression[0].ConstB * y) / expression[0].ConstA;

      Console.WriteLine($"{nameof(x)}={x}");

      /**
       * 没啥成就感....
       */

    }
  }
}