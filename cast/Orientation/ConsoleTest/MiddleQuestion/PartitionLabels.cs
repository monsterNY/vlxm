using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : PartitionLabels  
  /// @author :mons
  /// @create : 2019/3/22 11:05:19 
  /// @source : https://leetcode.com/problems/partition-labels/
  /// </summary>
  public class PartitionLabels
  {
    /**
     * Runtime: 264 ms, faster than 72.19% of C# online submissions for Partition Labels.
     * Memory Usage: 28.3 MB, less than 50.00% of C# online submissions for Partition Labels.
     *
     * 只是到了这样可行的程度
     *
     */
    public IList<int> Solution(string S)
    {
      IList<int> list = new List<int>();

      var indexDictionary = new Dictionary<char, int[]>();

      for (var i = 0; i < S.Length; i++)
        if (indexDictionary.ContainsKey(S[i]))
          indexDictionary[S[i]][1] = i;
        else
          indexDictionary.Add(S[i], new[] {i, i});

      int start = 0, end = 0;

      foreach (var item in indexDictionary.Values)
      {
        if (item[0] > end)
        {
          list.Add(end - start + 1);
          start = item[0];
          end = item[1];
        }
        else if (item[1] > end)
        {
          end = item[1];
        }

        if (end == S.Length - 1)
        {
          list.Add(end - start + 1);
          break;
        }
      }

      return list;
    }

    /**
     * https://leetcode.com/problems/partition-labels/discuss/113259/Java-2-pass-O(n)-time-O(1)-space-extending-end-pointer-solution
     *
     * same idea , more optimize~
     *
     */
    public IList<int> OtherSolution(String S)
    {
      List<int> list = new List<int>();
      int[] map = new int[26];  // record the last index of the each char

      for (int i = 0; i < S.Length; i++)
      {
        map[S[i] - 'a'] = i;
      }
      // record the end index of the current sub string
      int last = 0;
      int start = 0;
      for (int i = 0; i < S.Length; i++)
      {
        last = Math.Max(last, map[S[i] - 'a']);
        if (last == i)
        {
          list.Add(last - start + 1);
          start = last + 1;
        }
      }
      return list;
    }

    /**
     * Runtime: 272 ms, faster than 68.87% of C# online submissions for Partition Labels.
     * Memory Usage: 28.4 MB, less than 10.00% of C# online submissions for Partition Labels.
     */
    public IList<int> Try(string S)
    {
      IList<int> list = new List<int>();

      var indexDictionary = new Dictionary<int, int>();
      var startDictionary = new Dictionary<char, int>();

      for (var i = 0; i < S.Length; i++)
        if (startDictionary.ContainsKey(S[i]))
        {
          indexDictionary[startDictionary[S[i]]] = i;
        }
        else
        {
          startDictionary.Add(S[i], i);
          indexDictionary.Add(i, i);
        }

      int start = 0, end = 0;

      foreach (var item in indexDictionary.Keys)
      {
        if (item > end)
        {
          list.Add(end - start + 1);
          start = item;
          end = indexDictionary[item];
        }
        else if (indexDictionary[item] > end)
        {
          end = indexDictionary[item];
        }

        if (end == S.Length - 1)
        {
          list.Add(end - start + 1);
          break;
        }
      }

      return list;
    }
  }
}