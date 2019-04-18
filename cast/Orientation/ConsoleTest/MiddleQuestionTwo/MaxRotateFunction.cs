using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestionTwo
{
  /// <summary>
  /// @desc : MaxRotateFunction  
  /// @author :mons
  /// @create : 2019/4/18 17:52:19 
  /// @source : https://leetcode.com/problems/rotate-function/
  /// </summary>
  public class MaxRotateFunction
  {
    public int Solution(int[] A)
    {
      var num = 1;
      for (int i = 0; i < A.Length; i++)
      {
        mutiple += i;
      }

      foreach (var item in A)
      {
        sum += item * mutiple;
      }

      return sum;
    }
  }
}