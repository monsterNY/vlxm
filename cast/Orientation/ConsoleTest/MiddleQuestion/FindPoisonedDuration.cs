using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : FindPoisonedDuration  
  /// @author :mons
  /// @create : 2019/4/1 15:42:33 
  /// @source : https://leetcode.com/problems/teemo-attacking/
  /// </summary>
  public class FindPoisonedDuration
  {


    /**
     *
     * teemo must die  ha ha ha~
     *
     * Runtime: 136 ms, faster than 59.52% of C# online submissions for Teemo Attacking.
     * Memory Usage: 33.8 MB, less than 100.00% of C# online submissions for Teemo Attacking.
     *
     * Runtime: 132 ms, faster than 100.00% of C# online submissions for Teemo Attacking.
     * Memory Usage: 33.8 MB, less than 100.00% of C# online submissions for Teemo Attacking.
     *
     * what ??? 测试工具有bug吧
     *
     */
    public int Solution(int[] timeSeries, int duration)
    {
      if (timeSeries.Length == 0) return 0;

      var countTime = duration;

      int diff;

      for (int i = 1; i < timeSeries.Length; i++)
      {
        diff = timeSeries[i] - timeSeries[i - 1];
        if (diff >= duration)
          countTime += duration;
        else
          countTime += diff;
      }

      return countTime;
    }
  }
}