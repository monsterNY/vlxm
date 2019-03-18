using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleTest.LeetCode
{
  /// <summary>
  /// @desc : MinStack  
  /// @author :mons
  /// @create : 2019/3/18 14:06:21 
  /// @source : https://leetcode.com/problems/min-stack/
  /// </summary>
  public class MinStack
  {
    private int _min = Int32.MaxValue;

    private List<int> list = new List<int>();

    /**
     * Runtime: 136 ms, faster than 95.79% of C# online submissions for Min Stack.
     * Memory Usage: 33.2 MB, less than 91.84% of C# online submissions for Min Stack.
     *
     * easy...
     *
     */

    /** initialize your data structure here. */
    public MinStack()
    {
    }

    public void Push(int x)
    {
      list.Add(x);
      _min = Math.Min(x, _min);
    }

    public void Pop()
    {
      if (list.Count == 0) return;
      list.RemoveAt(list.Count - 1);
      if (list.Count == 0) _min = list.Min();
      else _min = int.MaxValue;
    }

    public int Top()
    {
      if (list.Count == 0) return -1;
      return list[list.Count - 1];
    }

    public int GetMin()
    {
      if (list.Count == 0) return -1;
      return _min;
    }
  }
}