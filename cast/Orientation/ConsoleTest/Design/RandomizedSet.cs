using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.Design
{
  /// <summary>
  /// @desc : RandomizedSet  
  /// @author :mons
  /// @create : 2019/4/9 16:47:10 
  /// @source : 
  /// </summary>
  public class RandomizedSet
  {
    private IList<int> _list;
    private Random rand = new Random();

    /** Initialize your data structure here. */
    public RandomizedSet()
    {
      _list = new List<int>();
    }

    /** Inserts a value to the set. Returns true if the set did not already contain the specified element. */
    public bool Insert(int val)
    {
      var i = IndexOf(val);
      if (i != -1) return false;

      _list.Add(val);
      return true;
    }

    private int IndexOf(int val)
    {
      for (int i = 0; i < _list.Count; i++)
      {
        if (_list[i] == val) return i;
      }

      return -1;
    }

    /** Removes a value from the set. Returns true if the set contained the specified element. */
    public bool Remove(int val)
    {
      return _list.Remove(val);
    }

    /** Get a random element from the set. */
    public int GetRandom()
    {

      return _list.Count == 0 ? -1 : _list[rand.Next(_list.Count)];
    }

  }
}
