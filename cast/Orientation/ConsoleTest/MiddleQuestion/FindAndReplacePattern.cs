using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : FindAndReplacePattern  
  /// @author :mons
  /// @create : 2019/3/21 16:58:24 
  /// @source : https://leetcode.com/problems/find-and-replace-pattern/
  /// </summary>
  public class FindAndReplacePattern
  {
    /**
     *
     * Runtime: 280 ms, faster than 74.38% of C# online submissions for Find and Replace Pattern.
     * Memory Usage: 29.4 MB, less than 100.00% of C# online submissions for Find and Replace Pattern.
     *
     */
    public IList<string> Solution(string[] words, string pattern)
    {
      IList<string> list = new List<string>();

      Dictionary<char, int> patternDictionary = new Dictionary<char, int>();

      Dictionary<char, int> wordDictionary = new Dictionary<char, int>();

      bool flag;
      string word;

      for (int i = 0; i < words.Length; i++)
      {
        word = words[i];
        flag = true;
        patternDictionary.Clear();
        wordDictionary.Clear();
        for (int j = 0; j < word.Length; j++)
        {
          if (patternDictionary.ContainsKey(pattern[j]))
          {
            if (wordDictionary.ContainsKey(word[j]))
            {
              if (patternDictionary[pattern[j]] == wordDictionary[word[j]])
              {
                patternDictionary[pattern[j]] = j;
                wordDictionary[word[j]] = j;
              }
              else
              {
                flag = false;
                break;
              }
            }
            else
            {
              flag = false;
              break;
            }
          }
          else if (wordDictionary.ContainsKey(word[j]))
          {
            flag = false;
            break;
          }
          else
          {
            patternDictionary.Add(pattern[j], j);
            wordDictionary.Add(word[j], j);
          }
        }

        if (flag) list.Add(word);
      }

      return list;
    }

    /**
     *
     *
     * Runtime: 252 ms, faster than 99.17% of C# online submissions for Find and Replace Pattern.
     * Memory Usage: 29.1 MB, less than 100.00% of C# online submissions for Find and Replace Pattern.
     *
     * 果然简化程序验证 还是非常有必要的~
     *
     */
    public IList<string> Optimize(string[] words, string pattern)
    {
      IList<string> list = new List<string>();

      Dictionary<char, int> patternDictionary = new Dictionary<char, int>();

      Dictionary<char, int> wordDictionary = new Dictionary<char, int>();

      foreach (var word in words)
      {
        var flag = true;
        patternDictionary.Clear();
        wordDictionary.Clear();
        for (int j = 0; j < word.Length; j++)
        {
          var patterContainsKey = patternDictionary.ContainsKey(pattern[j]);
          var wordContainsKey = wordDictionary.ContainsKey(word[j]);

          if (patterContainsKey && wordContainsKey && patternDictionary[pattern[j]] == wordDictionary[word[j]])
          {
            patternDictionary[pattern[j]] = j;
            wordDictionary[word[j]] = j;
          }
          else if (!patterContainsKey && !wordContainsKey)
          {
            patternDictionary.Add(pattern[j], j);
            wordDictionary.Add(word[j], j);
          }
          else
          {
            flag = false;
            break;
          }
        }

        if (flag) list.Add(word);
      }

      return list;
    }

    [Obsolete]
    public IList<string> Try(string[] words, string pattern)
    {
      IList<string> list = new List<string>();

      Dictionary<char, int> patternDictionary = new Dictionary<char, int>();

      foreach (var item in pattern)
      {
        if (patternDictionary.ContainsKey(item)) patternDictionary[item]++;
        else
          patternDictionary.Add(item, 1);
      }

      var nums = patternDictionary.Values.OrderBy(u => u).ToArray();

      string word;
      Dictionary<char, int> worDictionary = new Dictionary<char, int>();

      for (int i = 0; i < words.Length; i++)
      {
        word = words[i];
        worDictionary.Clear();
        foreach (var item in word)
        {
          if (worDictionary.ContainsKey(item))
          {
            if (worDictionary[item] > nums[nums.Length - 1] - 1) break;
            worDictionary[item]++;
          }
          else
            worDictionary.Add(item, 1);
        }

        if (worDictionary.Count != nums.Length) continue;

        var wordArr = worDictionary.Values.OrderBy(u => u).ToArray();

        var flag = true;

        for (int j = 0; j < nums.Length; j++)
        {
          if (nums[j] != wordArr[j])
          {
            flag = false;
            break;
          }
        }

        if (flag) list.Add(word);
      }

      return list;
    }
  }
}