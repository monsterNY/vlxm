using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestionTwo
{
  /// <summary>
  /// @desc : MaxWidthRamp  
  /// @author :mons
  /// @create : 2019/4/11 15:35:28 
  /// @source : https://leetcode.com/problems/maximum-width-ramp/
  /// </summary>
  [Obsolete]
  public class MaxWidthRamp
  {

    public int GetLen(int[] arr, int startIndex, int endIndex)
    {
      if (startIndex == endIndex) return 0;

      if (arr[endIndex] >= arr[startIndex]) return endIndex - startIndex;

      return Math.Max(GetLen(arr, startIndex + 1, endIndex), GetLen(arr, startIndex, endIndex - 1));
    }

    //time limit
    public int Solution(int[] A)
    {
      return GetLen(A, 0, A.Length - 1);
    }

    //Time Limit
    public int Try(int[] A)
    {
      var max = 0;
      for (int i = 0; i < A.Length; i++)
      {
        for (int j = i + 1 + max; j < A.Length; j++)
        {
          if (A[j] > A[i])
            max = j - i;
        }
      }

      return max;
    }
  }
}