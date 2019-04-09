using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : NumMatchingSubseq  
  /// @author :mons
  /// @create : 2019/4/9 16:54:57 
  /// @source : https://leetcode.com/problems/number-of-matching-subsequences/
  /// </summary>
  [Obsolete("no understand")]
  public class NumMatchingSubseq
  {
    public int Solution(string S, string[] words)
    {
      var count = 0;
      foreach (var word in words)
      {
        int i = 0, j = 0;
        for (; i < word.Length; i++)
        {
          for (; j < S.Length; j++)
          {
            if (S[j] == word[i]) break;
          }

          if (j == S.Length - 1) break;
          if (i == word.Length - 1)
          {
            count++;
            break;
          }
        }

        if (i == word.Length - 1 && j == S.Length - 1 && S[S.Length - 1] == word[word.Length - 1]) count++;
      }

      return count;
    }

    //no sort
    public int Try(string S, string[] words)
    {
      int[] compareArr, itemArr;

      compareArr = GetArr(S);
      var count = 0;
      foreach (var word in words)
      {
        bool flag = true;
        itemArr = GetArr(word);
        for (int i = 0; i < 26; i++)
        {
          if (itemArr[i] > compareArr[i])
          {
            flag = false;
            break;
          }
        }

        if (flag) count++;
      }

      return count;
    }

    public int[] GetArr(string str)
    {
      var arr = new int[26];
      foreach (var item in str)
      {
        arr[item - 'a']++;
      }

      return arr;
    }
  }
}