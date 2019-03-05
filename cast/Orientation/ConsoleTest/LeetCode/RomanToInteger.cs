using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.LeetCode
{
  /// <summary>
  /// @source:https://leetcode.com/problems/roman-to-integer/
  /// </summary>
  public class RomanToInteger
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

    static readonly Dictionary<char, int> map = new Dictionary<char, int>()
    {
      {'I', 1},
      {'V', 5},
      {'X', 10},
      {'L', 50},
      {'C', 100},
      {'D', 500},
      {'M', 1000},
    };

    public int RomanToInt(string s)
    {
      int sum = 0, observable = 1000, item;

      for (var i = 0; i < s.Length; i++)
      {
        item = map[s[i]];

        if (item > observable && ((observable - 1) % 9 == 0) && observable * 10 >= item)
          sum += item - observable - observable;
        else
          sum += item;

        observable = item;
      }

      return sum;
    }

    public int Optimize(string s)
    {
      int sum = 0, observable = 1000, item;

      for (var i = 0; i < s.Length; i++)
      {
        item = map[s[i]];

        sum += (item > observable && ((observable - 1) % 9 == 0) && observable * 10 >= item)
          ? item - observable - observable
          : item;

        observable = item;
      }

      return sum;
    }
  }
}