using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestionThree
{
  /// <summary>
  /// @desc : ReverseWords  
  /// @author : mons
  /// @create : 2019/4/30 9:43:26 
  /// @source : https://leetcode.com/problems/reverse-words-in-a-string/
  /// </summary>
  public class ReverseWords
  {

    /**
     * Runtime: 88 ms, faster than 92.73% of C# online submissions for Reverse Words in a String.
     * Memory Usage: 23.2 MB, less than 27.69% of C# online submissions for Reverse Words in a String.
     */
    public string Solution(string s)
    {
      List<string> list = Split(s);

      StringBuilder builder = new StringBuilder();

      for (int i = list.Count - 1; i >= 0; i--)
      {
        builder.Append(list[i]);
        if (i != 0)
          builder.Append(' ');
      }

      return builder.ToString();
    }

    public List<string> Split(ReadOnlySpan<char> str)
    {
      var result = new List<string>();

      int start = 0, count = 0;

      for (int i = 0; i < str.Length; i++)
      {
        if (str[i] == ' ')
        {
          if (count > 0)
            result.Add(str.Slice(start, count).ToString());

          start = i + 1;
          count = 0;
        }
        else
          count++;
      }

      if (count > 0)
        result.Add(str.Slice(start, count).ToString());

      return result;
    }
  }
}