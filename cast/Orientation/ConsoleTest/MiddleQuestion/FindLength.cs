using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Newtonsoft.Json;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : FindLength  
  /// @author :mons
  /// @create : 2019/4/3 14:45:28 
  /// @source : https://leetcode.com/problems/maximum-length-of-repeated-subarray/
  /// </summary>
  [Description("dp")]
  [Obsolete("no efficient , should be use df")]
  public class FindLength
  {
    public int OtherSolution(int[] A, int[] B)
    {
      int m = A.Length, n = B.Length;
      if (m == 0 || n == 0) return 0;
      int[][] dp = new int[m + 1][];
      for (int i = 0; i < dp.Length; i++)
        dp[i] = new int[n + 1];
//      Array.Fill(dp, new int[n + 1]);//吐血bug...
      int max = 0;
      for (int i = m - 1; i >= 0; i--)
      for (int j = n - 1; j >= 0; j--)
      {
        if (A[i] == B[j])
        {
          max = Math.Max(max, dp[i][j] = 1 + dp[i + 1][j + 1]);
        }
      }

      return max;
    }

    /**
     * Runtime: 508 ms, faster than 21.33% of C# online submissions for Maximum Length of Repeated Subarray.
     * Memory Usage: 23.5 MB, less than 100.00% of C# online submissions for Maximum Length of Repeated Subarray.
     *
     * simple solution....
     *
     */
    public int Solution(int[] A, int[] B)
    {
      int count = 0;

      for (int i = 0; i < A.Length; i++)
      {
        for (int j = 0; j < B.Length; j++)
        {
          if (i + count > A.Length || j + count > B.Length) continue;
          if (A[i] == B[j])
          {
            var itemCount = GetMaxLen(A, B, i + 1, j + 1);
            if (itemCount > count) count = itemCount;
          }
        }
      }

      return count;
    }

    public int Optimize(int[] A, int[] B)
    {
      int count = 0;

      for (int i = 0; i < A.Length; i++)
      {
        for (int j = 0; j < B.Length; j++)
        {
          if (i + count > A.Length || j + count > B.Length) continue;
          if (A[i] == B[j])
          {
            var itemCount = GetMaxLen(A, B, i + 1, j + 1);
            if (itemCount > count) count = itemCount;
          }
        }
      }

      return count;
    }

    public int GetMaxLen(int[] A, int[] B, int indexA, int indexB)
    {
      var count = 1;

      for (int i = 0; i + indexA < A.Length && i + indexB < B.Length; i++)
      {
        if (A[i + indexA] != B[i + indexB]) break;
        count++;
      }

      return count;
    }

    //Time Limit Exceeded
    public int Simple(int[] A, int[] B)
    {
      int count = 0;

      while (count < A.Length && count < B.Length)
      {
        count++;
        bool flag = false;
        for (int i = 0; i + count <= A.Length; i++)
        {
          for (int j = 0; j + count <= B.Length; j++)
          {
            flag = true;
            for (int k = 0; k < count; k++)
            {
              if (A[i + k] != B[j + k])
              {
                flag = false;
                break;
              }
            }

            if (flag) break;
          }

          if (flag) break;
        }

        if (!flag)
        {
          count--;
          break;
        }
      }

      return count;
    }
  }
}