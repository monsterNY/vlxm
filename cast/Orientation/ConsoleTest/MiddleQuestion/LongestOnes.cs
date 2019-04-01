using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : LongestOnes  
  /// @author :mons
  /// @create : 2019/4/1 16:13:56 
  /// @source : https://leetcode.com/problems/max-consecutive-ones-iii/
  /// </summary>
  [Obsolete(" no imagination")]
  public class LongestOnes
  {

    public int OtherSolution(int[] A, int K)
    {
      int i = 0, j;
      for (j = 0; j < A.Length; ++j)
      {
        if (A[j] == 0) K--;
        if (K < 0 && A[i++] == 0) K++;
      }
      return j - i;
    }

    public int Solution(int[] A, int K)
    {
      int itemCount = 0, maxCount = 0, changeCount = K, startIndex = 0,zeroCount = 0;
      bool flag = false;

      for (int i = 0; i < A.Length; i++)
      {
        if (A[i] == 0)
        {
          if (zeroCount > itemCount)
          {

          }
          zeroCount++;
        }
        else
        {
          itemCount++;
        }

      }

      for (int i = 0; i < A.Length; i++)
      {
        if (A[i] == 0)
        {
          if (itemCount > 0 && changeCount > 0)
          {
            changeCount--;
            itemCount++;
          }
          else
          {
            flag = false;

            for (int j = startIndex - 1; j >= 0 && changeCount > 0; j--, changeCount--)
            {
              itemCount++;
            }

            if (itemCount > maxCount) maxCount = itemCount + changeCount;
            changeCount = K;
            itemCount = 0;
          }
        }
        else
        {
          if (!flag)
          {
            startIndex = i;
            flag = true;
          }

          itemCount++;
        }
      }

      for (int j = startIndex - 1; j >= 0 && changeCount > 0; j--, changeCount--)
      {
        itemCount++;
      }

      return itemCount > maxCount ? itemCount : maxCount;
    }
  }
}