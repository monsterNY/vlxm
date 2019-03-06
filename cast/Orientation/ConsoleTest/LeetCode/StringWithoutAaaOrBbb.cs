using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.LeetCode
{
  /// <summary>
  /// @source:https://leetcode.com/problems/string-without-aaa-or-bbb/
  /// </summary>
  public class StringWithoutAaaOrBbb
  {
    private const char CharA = 'a';
    private const char CharB = 'b';

    /**
     *
     * 还很多优化空间。。。
     *
     * Runtime: 96 ms, faster than 69.70% of C# online submissions for String Without AAA or BBB.
     * Memory Usage: 20.6 MB, less than 25.00% of C# online submissions for String Without AAA or BBB.
     */
    public string StrWithout3a3b(int A, int B)
    {
      var charArr = new char[A + B];
      char addChar;

      for (var i = 0; i < charArr.Length; i++)
      {
        if (A > B)
          addChar = CharA;
        else
          addChar = CharB;

        if (i > 1 && charArr[i - 1] == addChar && charArr[i - 2] == addChar) addChar = addChar == CharA ? CharB : CharA;

        if (addChar == CharA)
          A--;
        else
          B--;

        charArr[i] = addChar;
      }

      return new string(charArr);
    }

    /**
     *
     * whhhh...
     *
     * perfect~
     * Runtime: 80 ms, faster than 100.00% of C# online submissions for String Without AAA or BBB.
     * Memory Usage: 20.6 MB, less than 25.00% of C# online submissions for String Without AAA or BBB.
     *
     */
    public string Optimize(int A, int B)
    {
      var charArr = new char[A + B];
      char addChar;

      for (var i = 0; i < charArr.Length; i++)
      {
        if (i > 1 && charArr[i - 1] == charArr[i - 2]) addChar = charArr[i - 1] == CharA ? CharB : CharA;
        else if (A > B)
          addChar = CharA;
        else
          addChar = CharB;

        if (addChar == CharA)
          A--;
        else
          B--;

        charArr[i] = addChar;
      }

      return new string(charArr);
    }

  }
}