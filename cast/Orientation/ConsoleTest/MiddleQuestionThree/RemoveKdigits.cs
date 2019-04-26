using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestionThree
{
  /// <summary>
  /// @desc : RemoveKdigits  
  /// @author : mons
  /// @create : 2019/4/26 14:00:54 
  /// @source : https://leetcode.com/problems/remove-k-digits/
  /// </summary>
  public class RemoveKdigits
  {
    public string Solution(string num, int k)
    {
      if (num.Length <= k) return "0";

      List<char> list = new List<char>();

      for (int i = 0; i < num.Length; i++)
      {
        if (num[i] == '1')
        {
          list.Add(num[i]);
        }else if (num[i] != '0')
        {

        }
      }

      return null;
    }
  }
}