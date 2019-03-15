using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.LeetCode
{
  /// <summary>
  /// @desc : LengthOfLastWord  
  /// @author :mons
  /// @create : 2019/3/15 16:03:56 
  /// @source : https://leetcode.com/problems/length-of-last-word/
  /// </summary>
  public class LengthOfLastWord
  {

    /**
     * Runtime: 72 ms, faster than 100.00% of C# online submissions for Length of Last Word.
     * Memory Usage: 20 MB, less than 82.98% of C# online submissions for Length of Last Word.
     *
     * 简直没压力。。。
     *
     */
    public int Solution(string s)
    {
      var count = 0;

      for (int i = 0; i < s.Length; i++)
        if (s[i] == ' ')
        {
          for (i = i + 1; i < s.Length; i++)
            if (s[i] != ' ')
            {
              count = 1;
              break;
            }
        }
        else
          count++;

      return count;
    }
  }
}