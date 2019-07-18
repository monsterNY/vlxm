using System;
using System.Collections.Generic;
using System.Text;

namespace Advance.Codewars
{
  /// <summary>
  /// @desc : GetMiddle  
  /// @author : mons
  /// @create : 2019/7/18 16:32:41 
  /// @source : 
  /// </summary>
  public class GetMiddle
  {
    /**
     * You are going to be given a word. Your job is to return the middle character of the word. If the word's length is odd, return the middle character. If the word's length is even, return the middle 2 characters.
     *
     * 你将会得到一个词，你的任务是返回这个词中间的字符，如果词的长度是奇数，返回中间的字符，如果是偶数，返回中间的两个字符
     *
     */

    public static string Solution(string s)
    {
      //Code goes here!

      var len = s.Length;

      if (len <= 1) return s;

      // 3 - 1
      // 4 - 2,3

      if (len % 2 == 0)
      {
        // return string.Concat(s[len / 2 - 1], s[len / 2]);
        return s.AsSpan(len / 2 - 1, 2).ToString();
      }
      else
      {
        return s[len / 2].ToString();
      }
    }
  }
}