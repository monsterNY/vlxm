using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.LeetCode
{
  /// <summary>
  /// @desc : CountPrimes  
  /// @author :mons
  /// @create : 2019/3/15 11:18:00 
  /// @source : https://leetcode.com/problems/count-primes/
  /// </summary>
  public class CountPrimes
  {

    /**
     * so nice
     *
     * genius
     *
     */
    public int OtherSolution(int n)
    {

      var isPrime = new bool[n];

      for (int i = 2; i < n; i++)
      {
        isPrime[i] = true;
      }
      // Loop's ending condition is i * i < n instead of i < sqrt(n)
      // to avoid repeatedly calling an expensive function sqrt().
      for (int i = 2; i * i < n; i++)
      {
        if (!isPrime[i]) continue;
        for (int j = i * i; j < n; j += i)
        {
          isPrime[j] = false;
        }
      }
      int count = 0;
      for (int i = 2; i < n; i++)
      {
        if (isPrime[i]) count++;
      }

      return 0;

    }

  }
}
