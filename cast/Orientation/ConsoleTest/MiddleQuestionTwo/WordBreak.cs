using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestionTwo
{
  /// <summary>
  /// @desc : WordBreak  
  /// @author :mons
  /// @create : 2019/4/19 10:11:25 
  /// @source : https://leetcode.com/problems/word-break/
  /// </summary>
  [Obsolete]
  public class WordBreak
  {
    #region otherSolution

    /**
     * Runtime: 104 ms, faster than 92.50% of C# online submissions for Word Break.\
     * Memory Usage: 25.7 MB, less than 15.79% of C# online submissions for Word Break.
     *
     * 不是吧。。。,相似的方案只是没用dp存储。。。
     *
     */
    public bool wordBreak(String s, IList<string> dict)
    {
      bool[] f = new bool[s.Length + 1];

      f[0] = true;


      /* First DP
      for(int i = 1; i <= s.length(); i++){
          for(String str: dict){
              if(str.length() <= i){
                  if(f[i - str.length()]){
                      if(s.substring(i-str.length(), i).equals(str)){
                          f[i] = true;
                          break;
                      }
                  }
              }
          }
      }*/

      //Second DP
      for (int i = 1; i <= s.Length; i++)
      {
        for (int j = 0; j < i; j++)
        {
          if (f[j] && dict.Contains(s.Substring(j, i - j)))
          {
            f[i] = true;
            break;
          }
        }
      }

      return f[s.Length];
    }

    #endregion

    public bool Solution(string s, IList<string> wordDict)
    {
      return Helper(s, wordDict, 0);
    }


    public bool Helper(string s, IList<string> wordDict, int index)
    {
      if (index == s.Length) return true;

      bool flag = false;

      foreach (var item in wordDict)
      {
        if (item.Length > s.Length - index) continue;

        if (item[0] == s[index])
        {
          int i = 0;
          for (; i < item.Length; i++)
          {
            if (item[i] != s[index + i]) break;
          }

          if (i == item.Length) flag = Helper(s, wordDict, index + i);
          if (flag) return true;
        }
      }

      return false;
    }

    //Time Limit
    public bool Try(string s, IList<string> wordDict)
    {
      Dictionary<char, List<int>> dictionary = new Dictionary<char, List<int>>();

      for (int i = 0; i < wordDict.Count; i++)
      {
        if (dictionary.ContainsKey(wordDict[i][0]))
        {
          dictionary[wordDict[i][0]].Add(i);
        }
        else
        {
          dictionary.Add(wordDict[i][0], new List<int>() {i});
        }
      }

      return Helper(s, wordDict, dictionary, 0);
    }

    public bool Helper(string s, IList<string> wordDict, Dictionary<char, List<int>> dictionary, int index)
    {
      if (index == s.Length) return true;
      if (!dictionary.ContainsKey(s[index])) return false;

      bool flag = false;
      foreach (var item in dictionary[s[index]])
      {
        if (wordDict[item].Length > s.Length - index) continue;
        int i = 0;
        for (; i < wordDict[item].Length; i++)
        {
          if (wordDict[item][i] != s[index + i]) break;
        }

        if (i == wordDict[item].Length) flag = Helper(s, wordDict, dictionary, index + i);
        if (flag) return true;
      }

      return false;
    }
  }
}