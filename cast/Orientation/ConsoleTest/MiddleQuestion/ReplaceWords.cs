using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : ReplaceWords  
  /// @author :mons
  /// @create : 2019/4/2 11:52:29 
  /// @source : https://leetcode.com/problems/replace-words/
  /// </summary>
  public class ReplaceWords
  {
    /**
     * Runtime: 1788 ms, faster than 10.64% of C# online submissions for Replace Words.
     * Memory Usage: 37.5 MB, less than 88.89% of C# online submissions for Replace Words.
     */
    public string Solution(IList<string> dict, string sentence)
    {
      IList<string> result = new List<string>();

      ReadOnlySpan<char> readOnlySpan = sentence.AsSpan(), itemSpan;
      bool[] flag = new bool[dict.Count];
      bool isExists;
      int index;

      while (true)
      {
        index = readOnlySpan.IndexOf(' ');
        itemSpan = readOnlySpan.Slice(0, index == -1 ? readOnlySpan.Length : index);

        Array.Fill(flag, true);
        isExists = false;
        for (int i = 0; i < itemSpan.Length; i++)
        {
          for (int j = 0; j < dict.Count; j++)
          {
            if (!flag[j] || i >= dict[j].Length) continue;
            if (dict[j][i] != itemSpan[i]) flag[j] = false;
            else if (dict[j].Length - 1 == i)
            {
              result.Add(dict[j]);
              isExists = true;
              break;
            }
          }

          if (isExists) break;
        }

        if (!isExists) result.Add(itemSpan.ToString());

        if (index == -1) break;

        readOnlySpan = readOnlySpan.Slice(index + 1, readOnlySpan.Length - 1 - index);
      }

      return string.Join(' ', result);
    }

    /**
     * Runtime: 216 ms, faster than 41.05% of C# online submissions for Replace Words.
     * Memory Usage: 37.6 MB, less than 88.89% of C# online submissions for Replace Words.
     */
    public string Solution2(IList<string> dict, string sentence)
    {
      IList<string> result = new List<string>();

      ReadOnlySpan<char> readOnlySpan = sentence.AsSpan(), itemSpan;
      bool isExists;
      int index;
      string str;

      while (true)
      {
        index = readOnlySpan.IndexOf(' ');
        itemSpan = readOnlySpan.Slice(0, index == -1 ? readOnlySpan.Length : index);

        str = itemSpan.ToString();

        for (int i = 0; i < dict.Count; i++)
        {
          if (dict[i].Length > str.Length) continue;
          isExists = true;
          for (int j = 0; j < itemSpan.Length && j < dict[i].Length; j++)
          {
            if (dict[i][j] != itemSpan[j])
            {
              isExists = false;
              break;
            }
          }

          if (isExists)
          {
            str = dict[i];
          }
        }

        result.Add(str);

        if (index == -1) break;

        readOnlySpan = readOnlySpan.Slice(index + 1, readOnlySpan.Length - 1 - index);
      }

      return string.Join(' ', result);
    }

    /**
     * Runtime: 140 ms, faster than 100.00% of C# online submissions for Replace Words.
     * Memory Usage: 36.8 MB, less than 100.00% of C# online submissions for Replace Words.
     *
     * nice.
     *
     */
    public string Solution3(IList<string> dict, string sentence)
    {
      StringBuilder builder = new StringBuilder();

      ReadOnlySpan<char> readOnlySpan = sentence.AsSpan(), itemSpan;
      bool isExists;
      int index;
      string str;

      while (true)
      {
        index = readOnlySpan.IndexOf(' ');
        itemSpan = readOnlySpan.Slice(0, index == -1 ? readOnlySpan.Length : index);

        str = itemSpan.ToString();

        for (int i = 0; i < dict.Count; i++)
        {
          if (dict[i].Length > str.Length) continue;
          isExists = true;
          for (int j = 0; j < itemSpan.Length && j < dict[i].Length; j++)
          {
            if (dict[i][j] != itemSpan[j])
            {
              isExists = false;
              break;
            }
          }

          if (isExists)
          {
            str = dict[i];
          }
        }

        builder.Append(str);

        if (index == -1) break;
        else builder.Append(' ');


        readOnlySpan = readOnlySpan.Slice(index + 1, readOnlySpan.Length - 1 - index);
      }

      return builder.ToString();
    }

  }
}