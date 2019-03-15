using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.LeetCode
{
  /// <summary>
  /// @desc : LongestCommonPrefix  
  /// @author :mons
  /// @create : 2019/3/15 14:06:10 
  /// @source : 
  /// </summary>
  public class LongestCommonPrefix
  {
    /**
     * Runtime: 104 ms, faster than 87.83% of C# online submissions for Longest Common Prefix.
     * Memory Usage: 22.5 MB, less than 62.60% of C# online submissions for Longest Common Prefix.
     *
     * easy
     *
     */
    public string Solution(string[] strs)
    {
      if (strs.Length < 1)
        return string.Empty;
      if (strs.Length == 1)
        return strs[0];

      StringBuilder builder = new StringBuilder();

      for (var i = 0; i < strs[0].Length; i++)
      {
        var item = strs[0][i];
        for (var j = 1; j < strs.Length; j++)
          if (i >= strs[j].Length || strs[j][i] != item)
            return builder.ToString();

        builder.Append(item);
      }

      return builder.ToString();
    }
  }
}