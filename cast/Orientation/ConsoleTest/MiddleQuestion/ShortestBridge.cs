using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : ShortestBridge  
  /// @author :mons
  /// @create : 2019/4/4 16:23:11 
  /// @source : https://leetcode.com/problems/shortest-bridge/
  /// </summary>
  [Obsolete("DFS")]
  public class ShortestBridge
  {
    public int Solution(int[][] A)
    {
      for (int i = 0; i < A.Length; i++)
      {
        for (int j = 0; j < A[j].Length; j++)
        {
        }
      }

      return 0;
    }

    public int GetLenght(int[][] arr, int i, int j, int len)
    {
      if (i < 0 || j < 0 || i >= arr.Length || j >= arr[0].Length) return -1;
      if (arr[i][j] == 1) return len - 1;

      len = GetLenght(arr, i + 1, j, len + 1);
      if (len < 1)
        len = GetLenght(arr, i, j + 1, len + 1);
      if (len < 1)
        len = GetLenght(arr, i + 1, j + 1, len + 1);
      if (len < 1)
        len = GetLenght(arr, i - 1, j - 1, len + 1);

      return len;
    }
  }
}