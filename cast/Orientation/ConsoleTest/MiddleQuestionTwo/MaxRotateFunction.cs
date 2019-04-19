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
  [Obsolete]
  public class MaxRotateFunction
  {
    
    //bug get max
    public int Try(int[] A)
    {
      if (A.Length < 2) return 0;

      int num = 1, sum = 0;

      for (int i = 2; i < A.Length; i++)
      {
        Console.WriteLine($"num:{num},a:{A[i]}");
        sum += num++ * A[i];
      }

      return sum + num * A[0];
    }
  }
}