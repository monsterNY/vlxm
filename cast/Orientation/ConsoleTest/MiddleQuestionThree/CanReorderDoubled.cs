using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleTest.MiddleQuestionThree
{
  /// <summary>
  /// @desc : CanReorderDoubled  
  /// @author :mons
  /// @create : 2019/4/22 10:17:43 
  /// @source : https://leetcode.com/problems/array-of-doubled-pairs/
  /// </summary>
  public class CanReorderDoubled
  {
    /**
     * Runtime: 212 ms, faster than 88.89% of C# online submissions for Array of Doubled Pairs.
     * Memory Usage: 37 MB, less than 90.00% of C# online submissions for Array of Doubled Pairs.
     *
     * Runtime: 208 ms, faster than 92.59% of C# online submissions for Array of Doubled Pairs.
     * Memory Usage: 37 MB, less than 90.00% of C# online submissions for Array of Doubled Pairs.
     *
     */
    public bool Solution(int[] A)
    {
      Dictionary<int, int> dictionary = new Dictionary<int, int>();

      foreach (var num in A)
      {
        if (dictionary.ContainsKey(num)) dictionary[num]++;
        else dictionary.Add(num, 1);
      }

      var target = A.Length / 2;

      if (dictionary.ContainsKey(0))
      {
        if (dictionary[0] % 2 != 0) return false;
        target -= dictionary[0] / 2;
        dictionary.Remove(0);
      }

      foreach (var key in dictionary.Keys.OrderBy(u => u))
      {
        if (dictionary[key] == 0) continue;

        if (dictionary.ContainsKey(key * 2) && dictionary[key * 2] > 0)
        {
          var reduce = Math.Min(dictionary[key], dictionary[key * 2]);
          dictionary[key * 2] -= reduce;
          dictionary[key] -= reduce;
          target -= reduce;
        }
      }

      return target == 0;
    }

    /**
     * Runtime: 208 ms, faster than 92.59% of C# online submissions for Array of Doubled Pairs.
     * Memory Usage: 37 MB, less than 90.00% of C# online submissions for Array of Doubled Pairs.
     */
    public bool Solution2(int[] A)
    {
      Dictionary<int, int> dictionary = new Dictionary<int, int>();

      List<int> list = new List<int>();

      foreach (var num in A)
      {
        if (dictionary.ContainsKey(num)) dictionary[num]++;
        else
        {
          dictionary.Add(num, 1);
          list.Add(num);
        }
      }

      var target = A.Length / 2;

      if (dictionary.ContainsKey(0))
      {
        if (dictionary[0] % 2 != 0) return false;
        target -= dictionary[0] / 2;
        dictionary.Remove(0);
        list.Remove(0);
      }

      list.Sort();

      var len = list.Count / 2;

      for (int i = 0; i < len; i++)
      {
        var key = list[i];
        if (dictionary[key] == 0)
        {
          len++;
          continue;
        }

        if (dictionary.ContainsKey(key * 2) && dictionary[key * 2] > 0)
        {
          var reduce = Math.Min(dictionary[key], dictionary[key * 2]);
          dictionary[key * 2] -= reduce;
          dictionary[key] -= reduce;
          target -= reduce;
        }
      }

      return target == 0;
    }

    /**
     * Runtime: 264 ms, faster than 78.95% of C# online submissions for Array of Doubled Pairs.
     * Memory Usage: 37 MB, less than 90.00% of C# online submissions for Array of Doubled Pairs.
     *
     * ...
     *
     */
    public bool Solution3(int[] A)
    {
      Dictionary<int, int> dictionary = new Dictionary<int, int>();

      Array.Sort(A);

      var target = A.Length / 2;

      for (int i = A.Length - 1; i >= 0; i--)
      {
        if (dictionary.ContainsKey(A[i] * 2) && dictionary[A[i] * 2] > 0)
        {
          dictionary[A[i] * 2]--;
          target--;
        }
        else if (dictionary.ContainsKey(A[i] / 2) && dictionary[A[i] / 2] > 0)
        {
          dictionary[A[i] / 2]--;
          target--;
        }
        else if(dictionary.ContainsKey(A[i])) dictionary[A[i]]++;
        else dictionary.Add(A[i], 1);
      }

      return target == 0;
    }
  }
}