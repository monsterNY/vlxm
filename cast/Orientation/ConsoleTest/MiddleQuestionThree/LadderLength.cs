using System;
using System.Collections.Generic;
using System.Text;
using Tools.CusAttr;
using Tools.CusMenu;

namespace ConsoleTest.MiddleQuestionThree
{
  /// <summary>
  /// @desc : LadderLength  
  /// @author : mons
  /// @create : 2019/4/28 11:30:08 
  /// @source : https://leetcode.com/problems/word-ladder/
  /// </summary>
  [Obsolete]
  [Question(QuestionTypes.BFS)]
  public class LadderLength
  {
    public int Simple(string beginWord, string endWord, IList<string> wordList)
    {
      bool[] visited = new bool[wordList.Count], res = new bool[wordList.Count];

      var first = wordList.IndexOf(endWord);

      if (first == -1) return 0;

      visited[first] = true;


      var step = 0;

      return step;
    }

    public int Helper(IList<string> list, char[] builder, string compareStr, int step,bool[] visited)
    {
      if (CanChange(compareStr, builder)) return step;

      for (int i = 0; i < list.Count; i++)
      {
        if(visited[i])continue;
      }

      return 0;
    }

    public bool CanChange(string str, char[] arr)
    {
      var diffCount = 0;

      for (int i = 0; i < str.Length && diffCount < 2; i++)
      {
        if (str[i] != arr[i]) diffCount++;
      }

      return diffCount == 1;
    }

    public bool Contains(IList<string> list, char[] str)
    {
      bool flag;
      foreach (var item in list)
      {
        flag = true;
        for (int i = 0; i < item.Length; i++)
        {
          if (item[i] != str[i])
          {
            flag = false;
            break;
          }
        }

        if (flag) return true;
      }

      return false;
    }

    public int[] Helper(string str)
    {
      var arr = new int[26];

      foreach (var c in str)
      {
        arr[c - 'a']++;
      }

      return arr;
    }
  }
}