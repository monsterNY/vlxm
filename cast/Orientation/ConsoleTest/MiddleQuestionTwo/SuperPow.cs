using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestionTwo
{
  /// <summary>
  /// @desc : SuperPow  
  /// @author :mons
  /// @create : 2019/4/18 16:40:40 
  /// @source : https://leetcode.com/problems/super-pow/
  /// </summary>
  [Obsolete("Math")]
  public class SuperPow
  {

    //time limit
    public int Solution(int a, int[] b)
    {
      a = a % 1337;
      int res = 1,mutiple = 1;

      for (int i = b.Length - 1; i >= 0; i--,mutiple*=10)
      {
        for (int j = 0; j < b[i] * mutiple; j++)
        {
          res = res * a % 1337;
        }
      }
      return res;
    }

    public int superPow(int a, int[] b)
    {
      if (a % 1337 == 0) return 0;
      int p = 0;
      foreach (int i in b) p = (p * 10 + i) % 1140;
      if (p == 0) p += 1440;
      return power(a, p, 1337);
    }
    public int power(int a, int n, int mod)
    {
      a %= mod;
      int ret = 1;
      while (n != 0)
      {
        if ((n & 1) != 0) ret = ret * a % mod;
        a = a * a % mod;
        n >>= 1;
      }
      return ret;
    }

  }
}