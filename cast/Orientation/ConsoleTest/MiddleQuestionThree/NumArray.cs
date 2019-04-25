using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestionThree
{
  /// <summary>
  /// @desc : NumArray  
  /// @author : mons
  /// @create : 2019/4/25 17:55:56 
  /// @source : https://leetcode.com/problems/range-sum-query-mutable/
  /// </summary>
  [Obsolete("time limit")]
  public class NumArray
  {

    private int[][] arr;

    public NumArray(int[] nums)
    {

      var len = nums.Length;

      arr = new int[len][];

      for (int i = 0; i < arr.Length; i++)
      {
        arr[i] = new int[len];
      }

      for (int i = 0; i < len; i++)
      {
        arr[i][i] = nums[i];
        for (int j = i+1; j < len; j++)
        {
          arr[i][j] = arr[i][j - 1] + nums[j];
        }
      }

    }

    public void Update(int i, int val)
    {
      var diff = val - arr[i][i];

      for (int j = 0; j <= i; j++)
      {
        for (int k = i; k < arr.Length; k++)
        {
          arr[j][k] += diff;
        }
      }

    }

    public int SumRange(int i, int j)
    {
      return arr[i][j];
    }

  }
}
