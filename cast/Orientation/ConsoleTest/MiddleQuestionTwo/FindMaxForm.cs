using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleTest.MiddleQuestionTwo
{
  /// <summary>
  /// @desc : FindMaxForm  
  /// @author :mons
  /// @create : 2019/4/15 10:46:21 
  /// @source : https://leetcode.com/problems/ones-and-zeroes/
  /// </summary>
  public class FindMaxForm
  {

    public int Solution(string[] strs, int m, int n)
    {

      int count = 0,prevM,prevN;
      bool flag;

      foreach (var str in strs.OrderBy(u=>u.Length))
      {
        prevM = m;prevN = n;
        flag = true;

        for (int i = 0; i < str.Length; i++)
        {

          if (str[i] == '0') m--;
          else n--;
          
          if (prevN < 0 || prevM < 0)
          {
            flag = false;
            break;
          }
        }

        if (flag)
        {
          count++;
        }
        else
        {
          m = prevM;
          n = prevN;
        }

        if (m == 0 && n == 0) return count;

      }

      return count;

    }

  }
}
