using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestionThree
{
  /// <summary>
  /// @desc : MaxSubarraySumCircular  
  /// @author : mons
  /// @create : 2019/4/25 10:13:26 
  /// @source : https://leetcode.com/problems/maximum-sum-circular-subarray/
  /// </summary>
  [Obsolete]
  public class MaxSubarraySumCircular
  {

    public int OtherSolution(int[] A)
    {

      //求最大和 
      // 情况1:无循环最大和
      // 情况2:循环获取最大值  即 总和减去 无循环的最小和 
      // cool!

      int total = 0, maxSum = -30000, curMax = 0, minSum = 30000, curMin = 0;
      foreach (int a in A)
      {
        curMax = Math.Max(curMax + a, a);
        maxSum = Math.Max(maxSum, curMax);
        curMin = Math.Min(curMin + a, a);
        minSum = Math.Min(minSum, curMin);
        total += a;
      }
      return maxSum > 0 ? Math.Max(maxSum, total - minSum) : maxSum;
    }

    public int Solution(int[] A)
    {
      int max = 0, sum = 0, negativeIndex = 0;

      var dp = new int[A.Length];

      for (int i = 0; i < A.Length; i++)
      {
        sum += A[i];

        if (sum > max)
          max = sum;

        dp[i] = sum;
        if (sum < 0)
        {
          negativeIndex = i;
          sum = 0;
        }
      }

      sum = dp[A.Length - 1] > 0 ? dp[A.Length - 1] : 0;

      for (int i = 0; i < negativeIndex; i++)
      {
        if (dp[i] > 0) sum += dp[i];
        else break;
      }

      return Math.Max(sum, max);
    }
  }
}