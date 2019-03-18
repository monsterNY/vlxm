using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.LeetCode
{
  /// <summary>
  /// @desc : StrIsPalindrome  
  /// @author :mons
  /// @create : 2019/3/18 11:28:20 
  /// @source : https://leetcode.com/problems/valid-palindrome/
  /// </summary>
  public class StrIsPalindrome
  {

    /**
     * Runtime: 76 ms, faster than 100.00% of C# online submissions for Valid Palindrome.
     * Memory Usage: 21.9 MB, less than 94.25% of C# online submissions for Valid Palindrome.
     */
    public bool IsPalindrome(string s)
    {
      // a - 97 A - 65  0 -48

      for (int head = 0, foot = s.Length - 1; head < foot; head++, foot--)
      {
        while (head < foot && !((s[head] >= 65 && s[head] < 91) || (s[head] >= 97 && s[head] < 124) ||
                                (s[head] >= 48 && s[head] < 58))) head++;

        if (foot == head) return true;

        while (foot > head && !((s[foot] >= 65 && s[foot] < 91) || (s[foot] >= 97 && s[foot] < 124) ||
                                (s[foot] >= 48 && s[foot] < 58))) foot--;

        if (s[head] != s[foot])
        {
          if (s[head] < 58 || s[foot] < 58) return false;
          if (s[head] - 32 != s[foot] && s[head] + 32 != s[foot]) return false;
        }
      }

      return true;
    }
  }
}