using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : MapSum  
  /// @author :mons
  /// @create : 2019/4/2 11:07:17 
  /// @source : 
  /// </summary>
  public class MapSum
  {
    /**
     * Runtime: 116 ms, faster than 70.24% of C# online submissions for Map Sum Pairs.
     * Memory Usage: 24.7 MB, less than 75.00% of C# online submissions for Map Sum Pairs.
     */
    private Dictionary<string, int> _dictionary;

    /** Initialize your data structure here. */
    public MapSum()
    {
      _dictionary = new Dictionary<string, int>();
    }

    public void Insert(string key, int val)
    {
      if (_dictionary.ContainsKey(key))
      {
        _dictionary[key] = val;
      }
      else
      {
        _dictionary.Add(key, val);
      }
    }

    public int Sum(string prefix)
    {
      var sum = 0;
      foreach (var item in _dictionary)
      {
        if (item.Key.StartsWith(prefix)) sum += item.Value;
      }

      return sum;
    }
  }
}