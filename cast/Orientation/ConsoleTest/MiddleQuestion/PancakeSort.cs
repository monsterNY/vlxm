using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : PancakeSort  
  /// @author :monster_yj
  /// @create : 2019/3/23 15:06:32 
  /// @source : https://leetcode.com/problems/pancake-sorting/
  /// </summary>
  [Obsolete("I give up, sort is not good.,")]
  public class PancakeSort
  {
    /// <summary>
    /// 不错 不过还可以优化
    /// 场景 : 最大num = arr 长度 ... 局限性。
    /// </summary>
    /// <param name="A"></param>
    /// <returns></returns>
    public List<int> OtherSolution(int[] A)
    {
      List<int> res = new List<int>();
      for (int x = A.Length, i; x > 0; --x)
      {
        for (i = 0; A[i] != x; ++i) ;
        Reverse(A, i + 1);
        res.Add(i + 1);
        Reverse(A, x);
        res.Add(x);
      }

      return res;
    }

    public void Reverse(int[] A, int k)
    {
      for (int i = 0, j = k - 1; i < j; i++, j--)
      {
        int tmp = A[i];
        A[i] = A[j];
        A[j] = tmp;
      }
    }
  }
}