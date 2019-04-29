using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestionThree
{
  /// <summary>
  /// @desc : FractionToDecimal  
  /// @author : mons
  /// @create : 2019/4/29 16:53:30 
  /// @source : https://leetcode.com/problems/fraction-to-recurring-decimal/
  /// </summary>
  [Obsolete]
  public class FractionToDecimal
  {

    //bug
    public string Solution(int numerator, int denominator)
    {
      if (numerator % denominator == 0) return (numerator / denominator).ToString();

      StringBuilder builder = new StringBuilder();

      int multiply, dotIndex = -1;

      bool hasDot = false;

      string consult;

      while (numerator != 0)
      {
        multiply = 0;
        while (numerator < denominator)
        {
          numerator *= 10;
          multiply++;
        }

        consult = (numerator / denominator).ToString();

        if (multiply > 0 && !hasDot)
        {
          if (builder.Length == 0) builder.Append("0.");
          else builder.Append(".");
          dotIndex = builder.Length;
          hasDot = true;
        }

        for (int i = 1; i < multiply; i++)
        {
          builder.Append("0");
        }

        if (dotIndex > 0)
        {
          int start = 0, len = 0, lastIndex = 0;
          bool flag = false;
          for (int i = 1; i <= (builder.Length - dotIndex) / 2; i++) //bug
          {
            len = i;
            start = dotIndex;
            flag = true;
            
            for (int j = dotIndex + len; j < builder.Length; j++)
            {
              if (builder[j] != builder[dotIndex + (j - dotIndex) % len])
              {
                flag = false;
                break;
              }

              lastIndex = (j - dotIndex) % len;
            }

            if (flag && len >= consult.Length)
            {
              var str = builder.ToString().AsSpan();

              var reply = str.Slice(start, len).ToString();
              int j = 0;
              for (; j < consult.Length; j++)
              {
                if (reply[(lastIndex + 1 + j) % len] != consult[j])
                {
                  break;
                }
              }

              if (j != consult.Length) continue;

              return $"{str.Slice(0, start - len).ToString()}({reply})";
            }
          }
        }

        builder.Append(consult);

        numerator = numerator % denominator;
      }

      return builder.ToString();
    }


    //time limit bug
    public string Try(int numerator, int denominator)
    {
      if (numerator % denominator == 0) return (numerator / denominator).ToString();

      StringBuilder builder = new StringBuilder();

      int multiply, consult, prevNum = 0, dotIndex = -1, prevIndex = 0;

      bool hasDot = false;

      while (numerator != 0)
      {
        multiply = 0;
        while (numerator < denominator)
        {
          numerator *= 10;
          multiply++;
        }

        consult = numerator / denominator;

        if (multiply > 0 && !hasDot)
        {
          if (builder.Length == 0) builder.Append("0.");
          else builder.Append(".");
          dotIndex = builder.Length;
          hasDot = true;
        }

        for (int i = 1; i < multiply; i++)
        {
          builder.Append("0");
        }

        var str = consult.ToString();

        if (hasDot && prevNum == numerator)
        {
          if (prevIndex - dotIndex != str.Length)
          {
            builder.Remove(prevIndex - str.Length, str.Length);
          }

          builder.Append($"({str})");
          break;
        }
        else
        {
          builder.Append(str);
        }

        prevIndex = builder.Length;
        prevNum = numerator;
        numerator = numerator % denominator;
      }

      return builder.ToString();
    }
  }
}