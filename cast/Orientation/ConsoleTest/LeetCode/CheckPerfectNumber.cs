using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.LeetCode
{
  /// <summary>
  /// @desc : CheckPerfectNumber  
  /// @author :mons
  /// @create : 2019/3/14 15:19:14 
  /// @source : https://leetcode.com/problems/perfect-number/
  /// </summary>
  public class CheckPerfectNumber
  {

    /**
     * Runtime: 3008 ms, faster than 40.24% of C# online submissions for Perfect Number.
     * Memory Usage: 12.9 MB, less than 42.86% of C# online submissions for Perfect Number.
     *
     * 题目简单，但高校就要耗点时间了 ha ha
     * life always no easy
     *
     */
    public bool Solution(int num)
    {
      if (num <= 1)
        return false;

      var sum = 1;

      for (int i = 2; i < num; i++)
      {
        if (num % i == 0)
        {
          sum += i;
          if (sum > num)
            return false;
        }
      }

      return sum == num;
    }

    public bool OtherSolution(int num)
    {
      if (num == 1) return false;

      int sum = 1;
      var temp = (int) Math.Sqrt(num);
      for (int i = 2; i <= temp; i++)
      {
        if (num % i == 0)
        {
          sum += i + num / i;
        }
      }

      return sum == num;
    }

  }
}