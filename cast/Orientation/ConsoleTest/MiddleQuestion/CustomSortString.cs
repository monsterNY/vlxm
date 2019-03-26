using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : CustomSortString  
  /// @author :monster_yj
  /// @create : 2019/3/23 15:42:11 
  /// @source : https://leetcode.com/problems/custom-sort-string/
  /// </summary>
  public class CustomSortString
  {

    /**
     * Runtime: 84 ms, faster than 92.31% of C# online submissions for Custom Sort String.
     * Memory Usage: 20.4 MB, less than 83.33% of C# online submissions for Custom Sort String.
     *
     */
    public string Solution(string S, string T)
    {
      Dictionary<char, int> dictionary = new Dictionary<char, int>();
      for (int i = 0; i < S.Length; i++)
      {
        dictionary.Add(S[i], 0);
      }

      StringBuilder builder = new StringBuilder();

      foreach (var item in T)
      {
        if (dictionary.ContainsKey(item))
          dictionary[item]++;
        else
          builder.Append(item);
      }

      foreach (var item in dictionary)
      {
        if (item.Value > 0)
        {
          for (int i = 0; i < item.Value; i++)
          {
            builder.Append(item.Key);
          }
        }
      }

      return builder.ToString();
    }
  }
}