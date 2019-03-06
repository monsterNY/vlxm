using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.LeetCode
{
  /// <summary>
  /// @source:
  /// </summary>
  public class IntegerToRoman
  {
    /**
     * Symbol       Value
    * I             1
    * V             5
    * X             10
    * L             50
    * C             100
    * D             500
    * M             1000
     *
     */

    //I can be placed before V(5) and X(10) to make 4 and 9. 
    //X can be placed before L(50) and C(100) to make 40 and 90. 
    //C can be placed before D(500) and M(1000) to make 400 and 900.
    
    private int[] valueArr = new[]
    {
      1,
      4,
      5,
      9,
      10,
      40,
      50,
      90,
      100,
      400,
      500,
      900,
      1000
    };

    private static readonly string[] strArr = new[]
    {
      "I",
      "IV",
      "V",
      "IX",
      "X",
      "XL",
      "L",
      "XC",
      "C",
      "CD",
      "D",
      "CM",
      "M"
    };

    public string Solution(int num)
    {
      var builder = new StringBuilder();

      for (var i = valueArr.Length - 1; i >= 0; i--)
      {
        if (num == 0)
          break;
        if (num >= valueArr[i])
          for (var j = 0; j < num / valueArr[i]; j++) builder.Append(strArr[i]);

        num = num % valueArr[i];
      }

      return builder.ToString();
    }
  }
}