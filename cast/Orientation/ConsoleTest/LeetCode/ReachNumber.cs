using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.LeetCode
{
  /// <summary>
  /// @desc : ReachNumber  
  /// @author :mons
  /// @create : 2019/3/8 16:09:06 
  /// @source : https://leetcode.com/problems/reach-a-number/
  /// </summary>
  public class ReachNumber
  {
    /**
     *
     * what???
     *
     * Runtime: 44 ms, faster than 86.21% of C# online submissions for Reach a Number.
     * Memory Usage: 12.7 MB, less than 100.00% of C# online submissions for Reach a Number.
     */
    public int Solution(int target)
    {
      target = target > 0 ? target : -target; //考虑负数。。。
      var count = 0;
      var sum = 0;
      for (int i = 1; sum != target; i++)
      {
        sum += i;
        count++;

        if (sum > target && (sum - target) % 2 == 0)
          return count;
      }

      return count;
    }

    public int Optimize(int target)
    {
      target = target > 0 ? target : -target; //考虑负数。。。
      var count = 0;
      var sum = 0;
      while (sum != target)
      {
        sum += ++count;

        if (sum > target && (sum - target) % 2 == 0)
          return count;
      }

      return count;
    }

    /// <summary>
    /// @source:https://leetcode.com/problems/reach-a-number/discuss/112968/Short-JAVA-Solution-with-Explanation
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    public int OtherSolution(int target)
    {
      target = target > 0 ? target : -target; //考虑负数。。。
      var count = 0;
      var sum = 0;
      //本来就比较优化  还能优化  鬼才=-= 秀
      while (sum < target)
      {
        sum += ++count;
      }

      while ((sum - target) % 2 != 0)
      {
        sum += ++count;
      }

      return count;
    }
  }
}