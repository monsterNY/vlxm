using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : LargestSumOfAverages  
  /// @author :mons
  /// @create : 2019/4/4 14:40:43 
  /// @source : https://leetcode.com/problems/largest-sum-of-averages/
  /// </summary>
  [Obsolete("Dp em...")]
  public class LargestSumOfAverages
  {

    public double Solution(int[] A, int K)
    {
      return 0;
    }

    //要求:相邻组
    public double Try(int[] A, int K)
    {

      //将 A 分配到 K个非空数组中

      Array.Sort(A);

      double sum = 0;
      int i = A.Length - 1;
      for (; i >= 0 && K > 1; i--, K--)
      {
        sum += A[i];
      }

      double remainder = 0;
      //当k为1时
      for (int j = 0; j <= i; j++)
      {
        remainder += A[j];
      }

      sum += remainder / (i + 1);

      //求最大的 所有数组的平均数之和

      return sum;
    }
  }
}