using System;
using System.Collections.Generic;
using System.Text;

namespace Advance.Codewars
{
  /// <summary>
  /// @desc : UniqueInOrder  
  /// @author : mons
  /// @create : 2019/7/18 16:22:28 
  /// @source : 
  /// </summary>
  public class UniqueInOrder
  {
    /**
     * Implement the function unique_in_order which takes as argument a sequence and returns a list of items without any elements
     * with the same value next to each other and preserving the original order of elements.
     *
     * uniqueInOrder('AAAABBBCCDAABBB') == ['A', 'B', 'C', 'D', 'A', 'B']

      uniqueInOrder('ABBCcAD')         == ['A', 'B', 'C', 'c', 'A', 'D']

      uniqueInOrder([1,2,2,3,3])       == [1,2,3]
     *
     */

    public static IEnumerable<T> Solution<T>(IEnumerable<T> iterable)
    {
      IList<T> list = new List<T>();

      foreach (var item in iterable)
      {
        if (list.Count == 0) list.Add(item);
        else if (list[list.Count - 1].Equals(item)) list.Add(item);
      }

      //your code here...
      return list;
    }
  }
}