using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.HardQuestion
{
  /// <summary>
  /// @desc : MinKBitFlips  
  /// @author : mons
  /// @create : 2019/5/6 9:41:19 
  /// @source : https://leetcode.com/problems/minimum-number-of-k-consecutive-bit-flips/
  /// </summary>
  [Obsolete("Time Limit")]
  public class MinKBitFlips
  {

    //amazing....
    public int minKBitFlips(int[] A, int K)
    {
      int n = A.Length, flipped = 0, res = 0;
      int[] isFlipped = new int[n];
      for (int i = 0; i < A.Length; ++i)
      {
        if (i >= K)
          flipped ^= isFlipped[i - K];
        if (flipped == A[i])
        {
          if (i + K > A.Length)
            return -1;
          isFlipped[i] = 1;
          flipped ^= 1;
          res++;
        }
      }
      return res;
    }

    public int Solution(int[] A, int K)
    {
      List<int[]> area = new List<int[]>();
      int step = 0, start = 0, containerCount = 0, loseCount = 0;
      for (int i = 0; i < A.Length; i++)
      {

        bool isChange = false;

        for (var j = start; j < area.Count; j++)
        {
          if (i > area[j][0] && i < area[j][1])
          {
            isChange = !isChange;
            if (area[j][1] == i + 1) start++;
          }
          else
          {
            start++;
          }
        }

        if (isChange && A[i] == 1 || (!isChange && A[i] == 0))
        {
          if (i + K > A.Length) return -1;
          area.Add(new[] {i, i + K});
          step++;
        }
      }

      return step;
    }

    public int Try4(int[] A, int K)
    {
      int step = 0, start = -1, end = -1, start2 = -1, end2 = -1;
      bool isChange;
      for (int i = 0; i < A.Length; i++)
      {
        isChange = (i > start && i < end) || (i > start2 && i < end2); //bug

        if (isChange && A[i] == 1 || (!isChange && A[i] == 0))
        {
          if (i + K > A.Length) return -1;

          if (i >= end2)
          {
            start = i;
            end = i + K;
            start2 = i;
            end2 = i + K;
          }
          else
          {
            start2 = end - 1;
            end2 = i + K;
            start = Math.Min(start, i);
            end = i;
          }

          step++;
        }
      }

      return step;
    }

    //time limit
    public int Try3(int[] A, int K)
    {
      List<int[]> area = new List<int[]>();
      int step = 0;
      bool isChange;
      for (int i = 0; i < A.Length; i++)
      {
        isChange = false;
        foreach (var item in area) //太耗时了。
        {
          if (i > item[0] && i < item[1])
          {
            isChange = !isChange;
          }
        }

//        for (int j = 0; j < area.Count;)//Time Limit
//        {
//          if (i > area[j][0] && i < area[j][1])
//          {
//            isChange = !isChange;
//            j++;
//          }
//          else
//          {
//            area.RemoveAt(j);
//          }
//        }

        if (isChange && A[i] == 1 || (!isChange && A[i] == 0))
        {
          if (i + K > A.Length) return -1;
          area.Add(new[] {i, i + K});
          step++;
        }
      }

      return step;
    }

    public int Try2(int[] A, int K)
    {
      int step = 0, start = -1, end = -1;

      for (int i = 0; i < A.Length; i++)
      {
        //bug 只考虑双重影响
        if (i > start && i <= end)
        {
          if (A[i] == 1)
          {
            if (i + K > A.Length) return -1;
            start = Math.Max(i, end);
            end = i + K - 1;
            step++;
          }
        }
        else if (A[i] == 0)
        {
          if (i + K > A.Length) return -1;
          start = Math.Max(i, end);
          end = i + K - 1;
          step++;
        }
      }

      return step;
    }

    //Time Limit
    //那么猜想应该是对的。
    public int Try(int[] A, int K)
    {
      var step = 0;

      for (int i = 0; i < A.Length; i++)
      {
        if (A[i] == 0)
        {
          if (i + K >= A.Length) return -1;
          for (int j = i; j < K + i; j++)
          {
            A[j] ^= 1;
          }

          step++;
        }
      }

      return step;
    }
  }
}