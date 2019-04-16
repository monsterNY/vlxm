using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestionTwo
{
  /// <summary>
  /// @desc : LongestSubstring  
  /// @author :mons
  /// @create : 2019/4/16 16:24:39 
  /// @source : https://leetcode.com/problems/longest-substring-with-at-least-k-repeating-characters/
  /// </summary>
  [Obsolete]
  public class LongestSubstring
  {

    public int Solution(string s, int k)
    {
      int[] arr = new int[26];

      foreach (var c in s)
      {
        arr[c - 'a']++;
      }

      var count = 0;
      for (int i = 0; i < 26; i++)
      {
        if (arr[i] >= k)
        {
          count += arr[i];
        }
        else
        {
          count = 0;
        }
      }
      return count;

    }

    //bug 子串不能任意组合
    public int Try(string s, int k)
    {
      int[] arr = new int[26];

      foreach (var c in s)
      {
        arr[c - 'a']++;
      }

      var count = 0;
      for (int i = 0; i < 26; i++)
      {
        if (arr[i] >= k)
        {
          count += arr[i];
        }
      }

      return count;

    }

  }
}
