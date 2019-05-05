using System;
using System.Collections.Generic;
using System.Text;
using Tools.CusExtension;

namespace ConsoleTest.HardQuestion
{
  /// <summary>
  /// @desc : FreqStack  
  /// @author : mons
  /// @create : 2019/5/5 15:41:43 
  /// @source : https://leetcode.com/problems/maximum-frequency-stack/
  /// </summary>
  /**
   * Runtime: 448 ms, faster than 33.65% of C# online submissions for Maximum Frequency Stack.
   * Memory Usage: 55.7 MB, less than 25.00% of C# online submissions for Maximum Frequency Stack.
   */
  public class FreqStack
  {
    private List<int> list;
    private Dictionary<int, int> dictionary;
    private Dictionary<int, int> countMap;
    private int maxCount;

    public FreqStack()
    {
      list = new List<int>();
      dictionary = new Dictionary<int, int>();
      countMap = new Dictionary<int, int>();
    }

    public void Push(int x)
    {
      list.Add(x);

      if (dictionary.ContainsKey(x))
        dictionary[x]++;
      else dictionary.Add(x, 1);

      if (dictionary[x] > maxCount)
        maxCount = dictionary[x];

      var count = dictionary[x];

      if (countMap.ContainsKey(count - 1))
        countMap[count - 1]--;

      if (countMap.ContainsKey(count))
        countMap[count]++;
      else countMap.Add(count, 1);
    }

    public int Pop()
    {
      var res = -1;
      if (list.Count == 0) return res;


      for (int i = list.Count - 1; i >= 0; i--)
      {
        if (dictionary[list[i]] == maxCount)
        {
          countMap[maxCount]--;
          if (maxCount > 1)
            countMap[maxCount - 1]++;
          res = list[i];
          dictionary[list[i]]--;
          list.RemoveAt(i);
          break;
        }
      }

      if (countMap[maxCount] == 0)
        maxCount--;

      return res;
    }

    #region otherSolution

    //类似的解决方案，只是将list 与 countMap 合并 
    //优秀~！

    Dictionary<int, int> freq = new Dictionary<int, int>();
    Dictionary<int, Stack<int>> m = new Dictionary<int, Stack<int>>();
    int maxfreq = 0;

    public void push(int x)
    {
      int f = freq.Get(x, 0) + 1;
      freq.AddOrSet(x, f);
      maxfreq = Math.Max(maxfreq, f);
      if (!m.ContainsKey(f)) m.Add(f, new Stack<int>());
      m[f].Push(x);
    }

    public int pop()
    {
      int x = m[maxfreq].Pop();
      freq.AddOrSet(x, maxfreq - 1);
      if (m[maxfreq].Count == 0) maxfreq--;
      return x;
    }

    #endregion
  }
}