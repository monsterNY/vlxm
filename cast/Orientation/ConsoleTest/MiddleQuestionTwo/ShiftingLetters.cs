using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleTest.MiddleQuestionTwo
{
  /// <summary>
  /// @desc : ShiftingLetters  
  /// @author :mons
  /// @create : 2019/4/12 14:50:38 
  /// @source : https://leetcode.com/problems/shifting-letters/
  /// </summary>
  public class ShiftingLetters
  {
    /**
     * Runtime: 132 ms, faster than 100.00% of C# online submissions for Shifting Letters.
     * Memory Usage: 33.4 MB, less than 50.00% of C# online submissions for Shifting Letters.
     */
    public string Solution(string S, int[] shifts)
    {
      var count = 0;

      char[] arr = new char[S.Length];

      for (int i = S.Length - 1; i >= 0; i--)
      {
        count = (count + shifts[i]) % 26;
        arr[i] = (char) (((S[i] - 'a' + count) % 26) + 'a');
      }

      return new string(arr);
    }
  }
}