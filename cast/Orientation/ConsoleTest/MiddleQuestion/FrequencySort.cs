using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : FrequencySort  
  /// @author :mons
  /// @create : 2019/3/28 11:47:13 
  /// @source : https://leetcode.com/problems/sort-characters-by-frequency/
  /// </summary>
  public class FrequencySort
  {

    /**
     * Runtime: 100 ms, faster than 96.05% of C# online submissions for Sort Characters By Frequency.
     * Memory Usage: 24.7 MB, less than 50.00% of C# online submissions for Sort Characters By Frequency.
     *
     * emm... isn't that the answer?
     *
     */
    public string Solution(string s)
    {
      Dictionary<char, int> dictionary = new Dictionary<char, int>();

      for (int i = 0; i < s.Length; i++)
      {
        if (dictionary.ContainsKey(s[i]))
          dictionary[s[i]]++;
        else
          dictionary.Add(s[i], 1);
      }

      StringBuilder builder = new StringBuilder();

      foreach (var item in dictionary.OrderByDescending(u=>u.Value))
      {
        for (int i = 0; i < item.Value; i++)
        {
          builder.Append(item.Key);
        }
      }

      return builder.ToString();
    }

    /// <summary>
    ///
    /// 耗时更多。。。
    /// 
    /// Runtime: 112 ms, faster than 71.75% of C# online submissions for Sort Characters By Frequency.
    /// Memory Usage: 24.5 MB, less than 80.00% of C# online submissions for Sort Characters By Frequency.
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public string Try(string s)
    {

      int[] arr = new int[58];// 97 + 26 - 65 

      HashSet<char> set = new HashSet<char>();

      for (int i = 0; i < s.Length; i++)
      {
        arr[s[i] - 65]++;
        if (!set.Contains(s[i])) set.Add(s[i]);
      }

      StringBuilder builder = new StringBuilder();

      foreach (var item in set.OrderByDescending(u => arr[u-65]))
      {
        for (int i = 0; i < arr[item - 65]; i++)
        {
          builder.Append(item);
        }
      }

      return builder.ToString();
    }

  }
}