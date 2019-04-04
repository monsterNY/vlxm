using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : WordSubsets  
  /// @author :mons
  /// @create : 2019/4/4 10:56:58 
  /// @source : https://leetcode.com/problems/word-subsets/
  /// </summary>
  public class WordSubsets
  {
    #region other solution

    public List<String> wordSubsets(String[] A, String[] B)
    {
      int[] uni = new int[26], tmp;
      int i;
      foreach (String b in B)
      {
        tmp = counter(b);
        for (i = 0; i < 26; ++i)
          uni[i] = Math.Max(uni[i], tmp[i]); //... 每次循环时比较 大于 之后一次循环 ??? black question mark
      }

      List<string> res = new List<string>();
      foreach (String a in A)
      {
        tmp = counter(a);
        for (i = 0; i < 26; ++i)
          if (tmp[i] < uni[i])
            break;
        if (i == 26) res.Add(a);
      }

      return res;
    }

    int[] counter(String word)
    {
      int[] count = new int[26];
      foreach (char c in word.ToCharArray()) count[c - 'a']++;
      return count;
    }

    #endregion

    #region Imitation

    /**
     * Runtime: 380 ms, faster than 100.00% of C# online submissions for Word Subsets.
     * Memory Usage: 46.7 MB, less than 100.00% of C# online submissions for Word Subsets.
     *
     * ???
     */
    public List<string> Imitation(string[] a, string[] b)
    {
      int[] compare = new int[26], temp;

      foreach (var item in b)
      {
        temp = Counter(item);
        for (int j = 0; j < compare.Length; j++)
          if (temp[j] > compare[j])
            compare[j] = temp[j];
      }

      List<string> res = new List<string>();

      foreach (var str in a)
      {
        temp = counter(str);
        int j = 0;
        for (; j < compare.Length; j++)
          if (temp[j] < compare[j])
            break;
        if (j == compare.Length) res.Add(str);
      }

      return res;
    }

    protected int[] Counter(string word)
    {
      int[] count = new int[26];

      foreach (var c in word)
        count[c - 'a']++;

      return count;
    }

    #endregion

    /**
     * Runtime: 516 ms, faster than 22.86% of C# online submissions for Word Subsets.
     * Memory Usage: 51 MB, less than 50.00% of C# online submissions for Word Subsets.
     */
    public IList<string> Solution(string[] A, string[] B)
    {
      IList<string> res = new List<string>();

      IList<Dictionary<char, int>> list = new List<Dictionary<char, int>>();

      for (int i = 0; i < B.Length; i++)
      {
        var dic = new Dictionary<char, int>();
        for (int j = 0; j < B[i].Length; j++)
        {
          if (dic.ContainsKey(B[i][j])) dic[B[i][j]]++;
          else dic.Add(B[i][j], 1);
        }

        list.Add(dic);
      }

      Dictionary<char, int> matchDictionary = new Dictionary<char, int>(), itemDictionary;
      int needCount = 0;
      for (int i = 0; i < list.Count; i++)
      {
        foreach (var item in list[i])
        {
          if (matchDictionary.ContainsKey(item.Key))
            matchDictionary[item.Key] = Math.Max(item.Value, matchDictionary[item.Key]);
          else matchDictionary.Add(item.Key, item.Value);
        }
      }

      for (int i = 0; i < A.Length; i++)
      {
        itemDictionary = new Dictionary<char, int>(matchDictionary);
        for (int j = 0; j < A[i].Length; j++)
        {
          if (itemDictionary.ContainsKey(A[i][j]))
          {
            if (itemDictionary[A[i][j]] == 1) itemDictionary.Remove(A[i][j]);
            else
              itemDictionary[A[i][j]]--;
          }
        }

        if (itemDictionary.Count == 0) res.Add(A[i]);
      }

      return res;
    }

    /**
     * Runtime: 420 ms, faster than 51.43% of C# online submissions for Word Subsets.
     * Memory Usage: 52.7 MB, less than 50.00% of C# online submissions for Word Subsets.
     */
    public IList<string> Solution2(string[] A, string[] B)
    {
      IList<string> res = new List<string>();

      IList<int[]> list = new List<int[]>();
      for (int i = 0; i < B.Length; i++)
      {
        var arr = new int[26];
        for (int j = 0; j < B[i].Length; j++)
        {
          arr[B[i][j] - 97]++;
        }

        list.Add(arr);
      }

      int[] flagArr = new int[26];

      int needCount = 0, itemCount;

      for (int i = 0; i < flagArr.Length; i++)
      {
        var max = 0;
        for (int j = 0; j < list.Count; j++)
        {
          if (list[j][i] > max) max = list[j][i];
        }

        flagArr[i] = max;
        needCount += max;
      }

      for (int i = 0; i < A.Length; i++)
      {
        itemCount = 0;
        var itemArr = new int[26];
        for (int j = 0; j < A[i].Length && itemCount < needCount; j++)
        {
          var item = A[i][j] - 97;
          if (itemArr[item] < flagArr[item])
          {
            itemArr[item]++;
            itemCount++;
          }
        }

        if (itemCount == needCount) res.Add(A[i]);
      }

      return res;
    }

    // not match  , this is order match
    public IList<string> Imagination(string[] A, string[] B)
    {
      IList<string> res = new List<string>();

      int[] indexArr;
      bool flag;

      for (int i = 0; i < A.Length; i++)
      {
        indexArr = new int[B.Length];
        for (int j = 0; j < A[i].Length; j++)
        {
          for (int k = 0; k < B.Length; k++)
          {
            if (indexArr[k] == B[k].Length) continue;
            if (A[i][j] == B[k][indexArr[k]])
              indexArr[k]++;
          }
        }

        flag = true;
        for (int j = 0; j < B.Length; j++)
        {
          if (indexArr[j] < B[j].Length)
          {
            flag = false;
            break;
          }
        }

        if (flag) res.Add(A[i]);
      }

      return res;
    }

    //B-item not one char
    public IList<string> Try(string[] A, string[] B)
    {
      IList<string> res = new List<string>();

      int shouldShowCount = 0, itemCount;
      bool[] flagArr = new bool[26];

      for (int i = 0; i < B.Length; i++)
      {
        for (int j = 0; j < B[i].Length; j++)
        {
          if (!flagArr[B[i][j] - 97])
          {
            flagArr[B[i][j] - 97] = true;
            shouldShowCount++;
          }
        }
      }

      ISet<char> set;

      for (int i = 0; i < A.Length; i++)
      {
        itemCount = 0;
        set = new HashSet<char>();
        for (int j = 0; j < A[i].Length; j++)
        {
          if (set.Contains(A[i][j])) continue;
          if (flagArr[A[i][j] - 97]) itemCount++;
          set.Add(A[i][j]);
        }

        if (itemCount == shouldShowCount) res.Add(A[i]);
      }

      return res;
    }
  }
}