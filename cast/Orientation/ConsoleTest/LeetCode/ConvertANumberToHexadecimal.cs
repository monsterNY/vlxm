using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.LeetCode
{

  /// <summary>
  /// @source:https://leetcode.com/problems/convert-a-number-to-hexadecimal/
  /// </summary>
  public class ConvertANumberToHexadecimal
  {
    private static readonly char[] valueArr = new[]
    {
      '0',
      '1',
      '2',
      '3',
      '4',
      '5',
      '6',
      '7',
      '8',
      '9',
      'a',
      'b',
      'c',
      'd',
      'e',
      'f',
    };

    public string ToHex(int num)
    {
      List<char> result = new List<char>();

      long value = num >= 0 ? num : (0xffffffff + num + 1);

      result.Add(valueArr[value % 16]);

      while (value >= 16)
      {
        result.Add(valueArr[(value /= 16) % 16]);
      }

      result.Reverse();
      return new string(result.ToArray());
    }
  }
}