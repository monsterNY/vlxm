using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.WeekTest
{
  /// <summary>
  /// @desc : MaxScoreSightseeingPair  
  /// @author :mons
  /// @create : 2019/3/27 10:02:23 
  /// @source : https://leetcode.com/contest/weekly-contest-129/problems/best-sightseeing-pair/
  /// </summary>
  public class MaxScoreSightseeingPair
  {
    /**
     * time limit~
     */
    public int Solution(int[] A)
    {
      var maxScore = 0;
      int num;
      for (int i = 0; i < A.Length - 1; i++)
      {
        for (int j = i + 1; j < A.Length; j++)
        {
          if ((num = A[i] + A[j] + i - j) > maxScore)
          {
            maxScore = num;
          }
        }
      }

      return maxScore;
    }

    /**
     * time limit
     */
    public int Optimize(int[] A)
    {
      var maxScore = 0;
      int num, maxIndex = -1, startIndex = 0;
      for (int i = 0; i < A.Length - 1; i++)
      {
        if (i < maxIndex && A[i] - A[startIndex] + i - startIndex <= 0) continue;
        for (int j = i + 1; j < A.Length; j++)
        {
          if (i - j >= A[i]) break;
          if ((num = A[i] + A[j] + i - j) > maxScore)
          {
            maxScore = num;
            maxIndex = j;
            startIndex = i;
          }
        }
      }

      return maxScore;
    }

    /**
     * Runtime: 780 ms
     * Memory Usage: 34.9 MB
     *
     * emm... 勉强过关~
     *
     */
    public int Optimize2(int[] A)
    {
      int maxIndex = -1, startIndex = 0;
      for (int i = 0; i < A.Length - 1; i++)
      {
        if (maxIndex != -1 && i + 1 < maxIndex &&
            (A[i + 1] > A[i] || A[i] - A[startIndex] + i - startIndex < 0)) continue;

        for (int j = i + 1; j < A.Length && j - i <= A[i]; j++)
        {
          if ((A[i] + A[j] + i - j) > A[startIndex] + A[maxIndex] + startIndex - maxIndex)
          {
            maxIndex = j;
            startIndex = i;
          }
        }
      }

      return A[startIndex] + A[maxIndex] + startIndex - maxIndex;
    }

    /**
     * Runtime: 152 ms, faster than 91.67% of C# online submissions for Best Sightseeing Pair.
     * Memory Usage: 34.8 MB, less than 100.00% of C# online submissions for Best Sightseeing Pair.
     *
     * ha ha , I'm rookie
     *
     */
    public int OtherSolution(int[] A)
    {
      int res = 0, cur = 0;
      foreach (int a in A)
      {
        res = Math.Max(res, cur + a);
        cur = Math.Max(cur, a) - 1;
      }
      return res;
    }

  }
}