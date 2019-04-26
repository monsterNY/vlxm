using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestionThree
{
  /// <summary>
  /// @desc : LongestPalindrome  
  /// @author : mons
  /// @create : 2019/4/26 11:34:01 
  /// @source : https://leetcode.com/problems/longest-palindromic-substring/
  /// </summary>
  [Obsolete("imitation")]
  public class LongestPalindrome
  {
    private int start = 0;
    private int len = 0;

    /**
     * Runtime: 104 ms, faster than 79.75% of C# online submissions for Longest Palindromic Substring.
     * Memory Usage: 24.4 MB, less than 29.25% of C# online submissions for Longest Palindromic Substring.
     *
     * Runtime: 96 ms, faster than 94.68% of C# online submissions for Longest Palindromic Substring.
     * Memory Usage: 21.3 MB, less than 63.21% of C# online submissions for Longest Palindromic Substring.
     *
     */
    public string Solution(string s)
    {
      if (s.Length < 2) return s;

      for (int i = 0; i < s.Length; i++)
      {
        // i is the mid point
        ExtendPalindrome(s, i, i); // odd length;
        ExtendPalindrome(s, i, i + 1); // even length
      }

      return s.AsSpan(start + 1, len - 1).ToString();
    }

    private void ExtendPalindrome(String s, int left, int right)
    {
      while (left >= 0 && right < s.Length && s[left] == s[right])
      {
        left--;
        right++;
      }

      if (right - left > len)
      {
        start = left;
        len = right - left;
      }
    }
  }
}