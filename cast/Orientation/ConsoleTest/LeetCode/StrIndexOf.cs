using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.LeetCode
{
  /// <summary>
  /// @desc : StrIndexOf  
  /// @author :mons
  /// @create : 2019/3/15 15:38:36 
  /// @source : https://leetcode.com/problems/implement-strstr/
  /// </summary>
  public class StrIndexOf
  {

    /**
     * Runtime: 72 ms, faster than 100.00% of C# online submissions for Implement strStr().
     * Memory Usage: 20.1 MB, less than 84.06% of C# online submissions for Implement strStr().
     *
     * .net core 大法好  ha ha nice~
     *
     */
    public int StrStr(string haystack, string needle)
    {
      if (needle.Length == 0)
        return 0;

      var readOnlySpan = haystack.AsSpan();
      var result = true;
      ReadOnlySpan<char> onlySpan;

      for (var i = 0; i < haystack.Length - needle.Length + 1; i++)
      {
        onlySpan = readOnlySpan.Slice(i, needle.Length);

        result = true;

        for (var j = 0; j < onlySpan.Length; j++)
          if (onlySpan[j] != needle[j])
          {
            result = false;
            break;
          }

        if (result)
          return i;
      }

      return -1;

      return haystack.IndexOf(needle, StringComparison.Ordinal);
    }
  }
}