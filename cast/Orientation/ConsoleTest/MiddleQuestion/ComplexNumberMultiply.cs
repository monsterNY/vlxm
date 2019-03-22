using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : ComplexNumberMultiply  
  /// @author :mons
  /// @create : 2019/3/22 16:39:46 
  /// @source : https://leetcode.com/problems/complex-number-multiplication/
  /// </summary>
  public class ComplexNumberMultiply
  {


    /**
     *
     * Runtime: 84 ms, faster than 100.00% of C# online submissions for Complex Number Multiplication.
     * Memory Usage: 20.2 MB, less than 100.00% of C# online submissions for Complex Number Multiplication.
     *
     * 测试不稳定 一下快一下慢。。。
     *
     */
    public string Solution(string a, string b)
    {

      int realA = 0, imaginaryA = 0, realB = 0, imaginaryB = 0, resultReal, resultImaginary;

      GetNum(ref realA, ref imaginaryA, a);
      GetNum(ref realB, ref imaginaryB, b);

      resultReal = realA * realB - (imaginaryA * imaginaryB);
      resultImaginary = realA * imaginaryB + realB * imaginaryA;

      return $"{resultReal}+{resultImaginary}i";
    }

    public void GetNum(ref int realNum, ref int imaginaryNum, string str)
    {
      var span = str.AsSpan();

      var operationIndex = span.IndexOf('+');

      realNum = int.Parse(span.Slice(0, operationIndex));

      imaginaryNum = int.Parse(span.Slice(operationIndex + 1, str.Length - operationIndex - 2));
    }
  }
}