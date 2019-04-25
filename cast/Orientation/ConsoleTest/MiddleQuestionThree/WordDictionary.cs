using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestionThree
{
  /// <summary>
  /// @desc : WordDictionary  
  /// @author : mons
  /// @create : 2019/4/25 15:33:37 
  /// @source : https://leetcode.com/problems/add-and-search-word-data-structure-design/
  /// </summary>
  [Obsolete]
  public class WordDictionary
  {
    ISet<string> set = new HashSet<string>();

    /** Initialize your data structure here. */
    public WordDictionary()
    {
    }

    /** Adds a word into the data structure. */
    public void AddWord(string word)
    {
//      set.Add(word);
    }

    public bool Search(string word)
    {
      

      return false;
    }

    /** Returns if the word is in the data structure. A word could contain the dot character '.' to represent any one letter. */
    //Time Limit
    public bool Try(string word)
    {
      foreach (var item in set)
      {
        if (item.Length != word.Length) continue;
        int i = 0;
        for (; i < word.Length; i++)
        {
          if (word[i] != '.' && word[i] != item[i]) break;
        }

        if (i == word.Length) return true;
      }

      return false;
    }
  }
}