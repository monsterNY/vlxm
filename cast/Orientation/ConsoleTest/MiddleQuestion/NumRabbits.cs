using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : NumRabbits  
  /// @author :mons
  /// @create : 2019/4/2 11:25:53 
  /// @source : https://leetcode.com/problems/rabbits-in-forest/
  /// </summary>
  public class NumRabbits
  {
    /**
     * Runtime: 96 ms, faster than 71.43% of C# online submissions for Rabbits in Forest.
     * Memory Usage: 22.3 MB, less than 100.00% of C# online submissions for Rabbits in Forest.
     */
    public int Solution(int[] answers)
    {
      var count = 0;

      Dictionary<int, int> dictionary = new Dictionary<int, int>();

      foreach (var item in answers)
      {
        if (item == 0) count++;
        else if (dictionary.ContainsKey(item))
          dictionary[item]++;
        else
          dictionary.Add(item, 1);
      }

      int times;

      foreach (var item in dictionary)
      {
        times = item.Value;
        while (times > 0)
        {
          count += item.Key + 1;
          times -= item.Key + 1;
        }
      }

      return count;
    }

    /**
     * Runtime: 104 ms, faster than 66.67% of C# online submissions for Rabbits in Forest.
     * Memory Usage: 22.5 MB, less than 50.00% of C# online submissions for Rabbits in Forest.
     * */
    public int Solution2(int[] answers)
    {
      var count = 0;

      Dictionary<int, int> dictionary = new Dictionary<int, int>();

      foreach (var item in answers)
      {
        if (item == 0) count++;
        else if (dictionary.ContainsKey(item))
        {
          if (dictionary[item] % (item + 1) == 0) count += item + 1;
          dictionary[item]++;
        }
        else
        {
          count += item + 1;
          dictionary.Add(item, 1);
        }
      }

      return count;
    }

  }
}