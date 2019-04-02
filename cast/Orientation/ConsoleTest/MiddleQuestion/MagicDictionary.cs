using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : MagicDictionary  
  /// @author :mons
  /// @create : 2019/4/2 11:15:05 
  /// @source : https://leetcode.com/problems/implement-magic-dictionary/
  /// </summary>
  public class MagicDictionary
  {

    /**
     * Runtime: 104 ms, faster than 100.00% of C# online submissions for Implement Magic Dictionary.
     * Memory Usage: 23.3 MB, less than 100.00% of C# online submissions for Implement Magic Dictionary.
     */
    private List<string> _list;

    /** Initialize your data structure here. */
    public MagicDictionary()
    {
      _list = new List<string>();
    }

    /** Build a dictionary through a list of words */
    public void BuildDict(string[] dict)
    {
      _list.AddRange(dict);
    }

    /** Returns if there is any word in the trie that equals to the given word after modifying exactly one character */
    public bool Search(string word)
    {
      for (int i = 0; i < _list.Count; i++)
      {
        if (_list[i].Length != word.Length) continue;
        var count = 0;
        for (int j = 0; j < word.Length; j++)
          if (word[j] == _list[i][j]) count++;

        if (count == word.Length - 1) return true;
      }

      return false;
    }
  }
}