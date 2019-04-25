using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestionThree
{
  /// <summary>
  /// @desc : LengthOfLongestSubstring  
  /// @author : mons
  /// @create : 2019/4/25 17:19:08 
  /// @source : https://leetcode.com/problems/longest-substring-without-repeating-characters/
  /// </summary>
  public class LengthOfLongestSubstring
  {
    /**
     * Runtime: 88 ms, faster than 79.60% of C# online submissions for Longest Substring Without Repeating Characters.
     * Memory Usage: 23.6 MB, less than 74.02% of C# online submissions for Longest Substring Without Repeating Characters.
     */
    public int Solution(string s)
    {
      if (s.Length == 0) return 0;

      int count = 0, itemCount = 0, start = 0;

      Dictionary<char, int> dictionary = new Dictionary<char, int>();

      for (int i = 0; i < s.Length; i++)
      {
        if (dictionary.ContainsKey(s[i]))
        {
          itemCount = i - dictionary[s[i]];
          for (; start < dictionary[s[i]]; start++)
            dictionary.Remove(s[start]);
          dictionary[s[i]] = i;
          start++;
        }
        else
        {
          itemCount++;
          dictionary.Add(s[i], i);
        }


        if (itemCount > count) count = itemCount;
      }

      return count;
    }

    /**
     * Runtime: 76 ms, faster than 100.00% of C# online submissions for Longest Substring Without Repeating Characters.
     * Memory Usage: 22.9 MB, less than 99.21% of C# online submissions for Longest Substring Without Repeating Characters.
     *
     * Runtime: 76 ms, faster than 100.00% of C# online submissions for Longest Substring Without Repeating Characters.
     * Memory Usage: 22.8 MB, less than 100.00% of C# online submissions for Longest Substring Without Repeating Characters.
     *
     * nice
     *
     */
    public int Solution2(string s)
    {
      int count = 0, itemCount = 0, start = 0;

      for (int i = 0; i < s.Length; i++)
      {
        itemCount++;
        for (int j = start; j < i; j++)
        {
          if (s[i] == s[j])
          {
            start = j + 1;
            itemCount = i - j;
            break;
          }
        }

        if (itemCount > count) count = itemCount;
      }

      return count;
    }
  }
}