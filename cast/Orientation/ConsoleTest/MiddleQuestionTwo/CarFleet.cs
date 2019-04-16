using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleTest.MiddleQuestionTwo
{
  /// <summary>
  /// @desc : CarFleet  
  /// @author :mons
  /// @create : 2019/4/16 13:46:36 
  /// @source : https://leetcode.com/problems/car-fleet/
  /// </summary>
  public class CarFleet
  {
    /**
     * Runtime: 136 ms, faster than 72.00% of C# online submissions for Car Fleet.
     * Memory Usage: 34 MB, less than 50.00% of C# online submissions for Car Fleet.
     *
     * good
     *
     */
    public int Solution(int target, int[] position, int[] speed)
    {
      if (position.Length == 0) return 0;

      int count = position.Length;
      double prev = -1, itemTime;

      Dictionary<int, int> dictionary = new Dictionary<int, int>();

      for (int i = 0; i < position.Length; i++)
        dictionary.Add(position[i], speed[i]);

      foreach (var item in dictionary.OrderByDescending(u => u.Key))
      {
        if (prev == -1)
          prev = (target - item.Key) / (double) item.Value;
        else
        {
          itemTime = (target - item.Key) / (double) item.Value;
          if (itemTime <= prev) count--;
          else prev = itemTime;
        }
      }

//      for (int i = 1; i < position.Length; i++)
//      {
//        itemTime = (target - position[i]) / speed[i];
//
//        if (itemTime >= prev) count--;
//        else prev = itemTime;
//      }

      return count;
    }
  }
}