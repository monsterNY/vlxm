using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.DP
{
  /// <summary>
  /// @desc : NumSubarrayBoundedMax  
  /// @author :mons
  /// @create : 2019/4/9 14:57:03 
  /// @source : https://leetcode.com/problems/number-of-subarrays-with-bounded-maximum/
  /// </summary>
  public class NumSubarrayBoundedMax
  {
    /**
     * Runtime: 136 ms, faster than 83.33% of C# online submissions for Number of Subarrays with Bounded Maximum.
     * Memory Usage: 33.1 MB, less than 100.00% of C# online submissions for Number of Subarrays with Bounded Maximum.
     *
     * fine
     *
     */
    public int Solution(int[] A, int L, int R)
    {
      var dp = new int[A.Length + 1];
      var dp2 = new int[A.Length + 1];
      var count = 0;

      for (int i = 0; i < A.Length; i++)
      {
        if (A[i] > R) continue;

        dp2[i + 1] = dp2[i] + 1;

        if (A[i] >= L)
          dp[i + 1] = dp2[i + 1];
        else if (dp[i] > 0)
          dp[i + 1] = dp[i];

        count += dp[i + 1];
      }

      return count;
    }

    /**
     * Runtime: 136 ms, faster than 83.33% of C# online submissions for Number of Subarrays with Bounded Maximum.
     * Memory Usage: 33 MB, less than 100.00% of C# online submissions for Number of Subarrays with Bounded Maximum.
     *
     *
     * emm....
     */
    public int Test2(int[] A, int L, int R)
    {
      var dp = new int[A.Length];
      var dp2 = new int[A.Length];
      var count = 0;

      for (int i = 0; i < A.Length; i++)
      {
        if (A[i] > R) continue;

        dp2[i] = (i > 0 ? dp2[i - 1] : 0) + 1;

        if (A[i] >= L)
          dp[i] = dp2[i];
        else if (i > 0 && dp[i - 1] > 0)
          dp[i] = dp[i - 1];

        count += dp[i];
      }

      return count;
    }

    /**
     * 都差不多
     * 。。。？？？
     */
    public int Test3(int[] A, int L, int R)
    {
      var dp = new int[A.Length];
      int num = 0;
      var count = 0;

      for (int i = 0; i < A.Length; i++)
      {
        if (A[i] > R)
        {
          num = 0;
          continue;
        }

        num++;

        if (A[i] >= L)
          dp[i] = count;
        else if (i > 0 && dp[i - 1] > 0)
          dp[i] = dp[i - 1];

        count += dp[i];
      }

      return count;
    }

  }
}