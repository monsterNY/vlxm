using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.LeetCode
{
  /// <summary>
  /// @desc : CountSegments  
  /// @author :mons
  /// @create : 2019/3/14 14:42:54 
  /// @source : https://leetcode.com/problems/number-of-segments-in-a-string/
  /// </summary>
  public class CountSegments
  {
    /**
     * Runtime: 72 ms, faster than 96.15% of C# online submissions for Number of Segments in a String.
     * Memory Usage: 19.6 MB, less than 50.00% of C# online submissions for Number of Segments in a String.
     *
     * easy ~
     *
     * I'm genius , ha ha~
     *
     */
    public int Solution(string s)
    {
      if (s.Length == 0)
        return 0;

      var flag = false;
      var count = 0;
      for (var i = 0; i < s.Length; i++)
        if (s[i] == ' ')
        {
          flag = false;
        }
        else if (!flag)
        {
          flag = true;
          count++;
        }

      return count;
    }
  }
}