using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : SmallestRepunitDivByK  
  /// @author :mons
  /// @create : 2019/3/26 17:42:33 
  /// @source : https://leetcode.com/contest/weekly-contest-129/problems/smallest-integer-divisible-by-k/
  /// </summary>
  public class SmallestRepunitDivByK
  {
    List<int> arr = new List<int>()
    {
      1, 3, 7, 11, 13, 21, 33, 37, 39, 41, 77, 91, 101, 111, 143, 231, 259, 271, 273, 407, 429, 481, 777, 1001, 1111,
      1221, 1443, 2849, 3003, 3367, 5291, 8547
    };

    public int Solution(int K)
    {
      if (!arr.Contains(K)) return -1;

      var i = 1;
      var count = 1;
      while (i < Int32.MaxValue)
      {
        if (i % K == 0) return count;
        i = i * 10 + 1;
        count++;
      }

      return -1;
    }

    public IList<int> list = new List<int>();

    public void Test(int num)
    {
      for (int i = num > 10 ? num / 10 : 1; i <= num; i++)
      {
        if (num % i == 0) list.Add(i);
      }
    }
  }
}