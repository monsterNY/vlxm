using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : CountSubstrings  
  /// @author :mons
  /// @create : 2019/3/28 9:37:21 
  /// @source : https://leetcode.com/problems/palindromic-substrings/
  /// </summary>
  public class CountSubstrings
  {
    /**
     * Runtime: 304 ms, faster than 27.59% of C# online submissions for Palindromic Substrings.
     * Memory Usage: 19.9 MB, less than 71.43% of C# online submissions for Palindromic Substrings.
     *
     * Runtime: 300 ms, faster than 27.59% of C# online submissions for Palindromic Substrings.
     * Memory Usage: 19.9 MB, less than 71.43% of C# online submissions for Palindromic Substrings.
     *
     * Runtime: 296 ms, faster than 27.59% of C# online submissions for Palindromic Substrings.
     * Memory Usage: 20.3 MB, less than 28.57% of C# online submissions for Palindromic Substrings.
     *
     */
    public int Solution(string s)
    {
      var count = s.Length;

      for (int i = 0; i < s.Length; i++)
      {
        for (int j = i + 1; j < s.Length; j++)
        {
          // try 3:
          if (s[j] == s[i] && IsPalindromic2(s, i, j)) count++;
          // try 2:
          //if (s[j] == s[i] && IsPalindromic2(s, i, j)) count++;
          // try 1:
          //if (IsPalindromic2(s, i, j)) count++;
        }
      }

      return count;
    }

    public bool IsPalindromic2(string str, int start, int end)
    {
      if (end <= start) return false;
      for (; start <= end;)
      {
        if (str[start++] != str[end--])
        {
          return false;
        }
      }

      return true;
    }

    public bool IsPalindromic(ReadOnlySpan<char> str)
    {
      for (int i = 0; i < str.Length / 2; i++)
      {
        if (str[i] != str[str.Length - 1 - i])
        {
          return false;
        }
      }

      return true;
    }

    /**
     * Runtime: 72 ms, faster than 100.00% of C# online submissions for Palindromic Substrings.
     * Memory Usage: 19.7 MB, less than 100.00% of C# online submissions for Palindromic Substrings.
     *
     * amazing
     *
     */
    #region OtherSolution

    int count = 0;

    public int OtherSolution(String s)
    {
      if (string.IsNullOrEmpty(s)) return 0;

      for (int i = 0; i < s.Length; i++)
      { // i is the mid point
        ExtendPalindrome(s, i, i); // odd length;
        ExtendPalindrome(s, i, i + 1); // even length
      }

      return count;
    }

    private void ExtendPalindrome(String s, int left, int right)
    {
      while (left >= 0 && right < s.Length && s[left] == s[right])
      {
        count++; left--; right++;
      }
    }

    #endregion

  }
}