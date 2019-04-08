using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : SubarraysDivByK  
  /// @author :mons
  /// @create : 2019/4/8 15:54:48 
  /// @source : https://leetcode.com/problems/subarray-sums-divisible-by-k/
  /// </summary>
  public class SubarraysDivByK
  {

    /**
     * otherSolution
     *
     * 神级操作。。。
     *
     */
    public int subarraysDivByK(int[] A, int K)
    {
      int[] map = new int[K];
      map[0] = 1;
      int count = 0, sum = 0;
      foreach (int a in A)
      {
        sum = (sum + a) % K;
        if (sum < 0) sum += K; // Because -1 % 5 = -1, but we need the positive mod 4
        count += map[sum];
        map[sum]++;
      }

      return count;
    }

    protected int count;

    /**
     * Runtime: 1048 ms, faster than 9.68% of C# online submissions for Subarray Sums Divisible by K.
     * Memory Usage: 33.2 MB, less than 40.00% of C# online submissions for Subarray Sums Divisible by K.
     *
     * ... dp ???
     *
     */
    public int Solution(int[] A, int K)
    {
      int count = 0, item;

      var dp = new int[A.Length + 1];

      for (int i = A.Length + 1; i >= 0; i--)
      {
        item = 0;
        for (int j = i; j < A.Length; j++)
        {
          if (dp[j] > 0) continue;
          item += A[j];

          if (item % K == 0)
          {
            dp[i] = dp[j + 1] + 1;
            count += dp[i];
            break;
          }
        }
      }

      return count;
    }

    //time limit
    public int Simple(int[] A, int K)
    {
      int item = 0;
      for (int i = 0; i < A.Length; i++)
      {
        item = 0;
        for (int j = i; j < A.Length; j++)
        {
          item += A[j];
          if (item % K == 0) count++;
        }
      }

      return count;
    }
  }
}