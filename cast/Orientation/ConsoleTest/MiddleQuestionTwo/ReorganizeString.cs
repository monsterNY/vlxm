using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleTest.MiddleQuestionTwo
{
  /// <summary>
  /// @desc : ReorganizeString  
  /// @author :mons
  /// @create : 2019/4/11 9:46:35 
  /// @source : https://leetcode.com/problems/reorganize-string/
  /// </summary>
  public class ReorganizeString
  {

    /**
     * Runtime: 84 ms, faster than 97.06% of C# online submissions for Reorganize String.
     * Memory Usage: 20.9 MB, less than 42.86% of C# online submissions for Reorganize String.
     *
     * Runtime: 80 ms, faster than 100.00% of C# online submissions for Reorganize String.
     * Memory Usage: 20.9 MB, less than 42.86% of C# online submissions for Reorganize String.
     *
     * 果然数组更高效~
     *
     */
    public string Try2(string str)
    {
      int[] arr = new int[26];

      for (int i = 0; i < str.Length; i++)
      {
        arr[str[i] - 'a']++;
      }

      StringBuilder builder = new StringBuilder();

      var count = str.Length;

      while (count > 0)
      {
        int item = GetMaxChar(arr, -1), next; //获取出现次数最多的char

        if (arr[item] == 1) //如果次数为1直接添加
        {
          builder.Append((char) (item + 'a'));
          arr[item]--;
          count--;
        }
        else
        {
          builder.Append((char)(item + 'a'));
          next = GetMaxChar(arr, item); //找出第二个出现次数最多的char
          if (next == -1)
            return string.Empty; //若没有则无法成功

          builder.Append((char)(next + 'a'));
          arr[item]--;
          arr[next]--;
          count -= 2;
        }
      }

      return builder.ToString();
    }

    public int GetMaxChar(int[] arr, int compare)
    {
      var max = -1;
      var count = 0;
      for (int i = 0; i < arr.Length; i++)
      {
        if (arr[i] == 0) continue;
        if (i == compare) continue;
        if (arr[i] > count)
        {
          max = i;
          count = arr[i];
        }
      }

      return max;
    }

    /**
     * Runtime: 88 ms, faster than 76.12% of C# online submissions for Reorganize String.
     * Memory Usage: 20.8 MB, less than 57.14% of C# online submissions for Reorganize String.
     *
     * simple solution... 强行突破
     *
     */
    public string Solution(string str)
    {
      Dictionary<char, int> dictionary = new Dictionary<char, int>();

      for (int i = 0; i < str.Length; i++)
      {
        if (dictionary.ContainsKey(str[i]))
          dictionary[str[i]]++;
        else dictionary.Add(str[i], 1); //将str放置map
      }

      StringBuilder builder = new StringBuilder();

      while (dictionary.Count > 0)
      {
        char item = GetMaxChar(dictionary, ' '), next; //获取出现次数最多的char

        if (dictionary[item] == 1) //如果次数为1直接添加
        {
          builder.Append(item);
          dictionary.Remove(item);
        }
        else
        {
          builder.Append(item);
          next = GetMaxChar(dictionary, item); //找出第二个出现次数最多的char
          if (next == ' ')
            return string.Empty; //若没有则无法成功

          builder.Append(next);
          if (dictionary[next] == 1) dictionary.Remove(next);
          else dictionary[next]--;

          dictionary[item]--;
        }
      }

      return builder.ToString();
    }

    /// <summary>
    /// 获取出现次数最多的char
    /// </summary>
    /// <param name="dictionary"></param>
    /// <param name="compare">屏蔽词</param>
    /// <returns></returns>
    public char GetMaxChar(Dictionary<char, int> dictionary, char compare)
    {
      var max = ' ';
      var count = 0;
      foreach (var item in dictionary)
      {
        if (item.Key == compare) continue;
        if (item.Value > count)
        {
          max = item.Key;
          count = item.Value;
        }
      }

      return max;
    }

    public string Try(string str)
    {
      var arr = new int[26];

      for (int i = 0; i < str.Length; i++)
      {
        arr[str[i] - 'a']++;
      }

      StringBuilder builder = new StringBuilder();
      char prevChar = ' ';
      bool flag = false;
      while (!flag)
      {
        flag = true;
        for (int i = 0; i < 26; i++)
        {
          if (arr[i] == 0) continue;
          var c = (char) (i + 'a');
          if (c == prevChar) return string.Empty;
          builder.Append(c);
          arr[i]--;
          prevChar = c;
          if (flag) flag = false;
        }
      }

      return builder.ToString();
    }
  }
}