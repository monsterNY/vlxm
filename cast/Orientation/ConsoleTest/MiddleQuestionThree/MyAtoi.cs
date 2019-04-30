using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestionThree
{
  /// <summary>
  /// @desc : MyAtoi  
  /// @author : mons
  /// @create : 2019/4/30 10:14:25 
  /// @source : https://leetcode.com/problems/string-to-integer-atoi/
  /// </summary>
  [Obsolete]
  public class MyAtoi
  {
    public int Solution(string str)
    {
      bool isMax = false, isMin = false;

      bool? isPositive = null;

      int num = 0;

      for (int i = 0; i < str.Length; i++)
      {
        if (str[i] == ' ') continue;

        if (str[i] >= '0' && str[i] <= '9')
        {
          if (num == 0 && str[i] == '0') break;
          if (isPositive == null || isPositive.Value)
          {
            if (num * 10 + str[i] - '0' < 0)
            {
              isMax = true;
              break;
            }

            num = num * 10 + str[i] - '0';
          }
          else
          {
            if (num * 10 - (str[i] - '0') > num)
            {
              isMin = true;
              break;
            }

            num = num * 10 - (str[i] - '0');
          }
        }
        else if (str[i] == '-')
        {
          if (isPositive != null) break;
          isPositive = false;
        }
        else if (str[i] == '+')
        {
          if (isPositive != null) break;
          isPositive = true;
        }
        else
        {
          break;
        }
      }

      if (isMax)
      {
        return int.MaxValue;
      }

      if (isMin)
      {
        return int.MinValue;
      }

      return num;
    }
  }
}