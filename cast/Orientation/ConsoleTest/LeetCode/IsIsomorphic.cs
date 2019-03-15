using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.LeetCode
{
  /// <summary>
  /// @desc : IsIsomorphic  
  /// @author :mons
  /// @create : 2019/3/15 11:50:55 
  /// @source : https://leetcode.com/problems/isomorphic-strings/
  /// </summary>
  public class IsIsomorphic
  {

    /**
     * Runtime: 80 ms, faster than 84.45% of C# online submissions for Isomorphic Strings.
     * Memory Usage: 21.3 MB, less than 55.00% of C# online submissions for Isomorphic Strings.
     *
     * for the first , it's enough~
     *
     */
    public bool Solution(string s, string t)
    {
      //You may assume both s and t have the same length.

      if (s.Length < 2)
        return true;

      Dictionary<char, int> dictionary = new Dictionary<char, int>();
      Dictionary<char, int> nextDictionary = new Dictionary<char, int>();

      for (int i = 0; i < s.Length; i++)
      {
        if (dictionary.ContainsKey(s[i]))
        {
          if ((!nextDictionary.ContainsKey(t[i])) || dictionary[s[i]] != nextDictionary[t[i]])
          {
            return false;
          }

          dictionary[s[i]] = i;
          nextDictionary[t[i]] = i;
        }
        else if (nextDictionary.ContainsKey(t[i]))
        {
          if ((!dictionary.ContainsKey(s[i])) || dictionary[s[i]] != nextDictionary[t[i]])
          {
            return false;
          }

          dictionary[s[i]] = i;
          nextDictionary[t[i]] = i;
        }
        else
        {
          dictionary.Add(s[i], i);
          nextDictionary.Add(t[i], i);
        }
      }

      return true;
    }

    /// <summary>
    /// @source : https://leetcode.com/problems/isomorphic-strings/discuss/57796/My-6-lines-solution
    ///
    ///
    /// cool ~  use m1 replace map , key -> (char -> int)
    /// 
    /// </summary>
    /// <param name="s"></param>
    /// <param name="t"></param>
    /// <returns></returns>
    public bool OtherSolution(string s, string t)
    {
      int[] m1 = new int[256], m2 = new int[256];
      int n = s.Length;
      for (int i = 0; i < n; ++i)
      {
        if (m1[s[i]] != m2[t[i]]) return false;
        m1[s[i]] = i + 1;
        m2[t[i]] = i + 1;
      }
      return true;
    }

  }
}