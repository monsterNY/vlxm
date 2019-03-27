using System;
using System.Collections.Generic;

namespace ConsoleTest.WeekTest
{
  /// <summary>
  /// @desc : SmallestRepunitDivByK  
  /// @author :mons
  /// @create : 2019/3/26 17:42:33 
  /// @source : https://leetcode.com/contest/weekly-contest-129/problems/smallest-integer-divisible-by-k/
  /// </summary>
  public class SmallestRepunitDivByK
  {

    /**
     * Runtime: 36 ms
     * Memory Usage: 12.9 MB
     *
     * amazing ~
     *
     * source:https://blog.csdn.net/qq_34937637/article/details/81330331
     *
     */
    public int Solution(int K)
    {
      if (K % 2 == 0 || K % 5 == 0) return -1;

      int yiCount = 0;
      int tempNum = K;

      int yiNum = 0;
      while (tempNum > 0)
      {
        tempNum /= 10;
        yiCount++;
        yiNum = yiNum * 10 + 1;
      }

      int yuNum = yiNum % K;
      while (yuNum != 0)
      {
        yuNum = (yuNum * 10 + 1) % K;
        yiCount++;
      }

      return yiCount;

    }

    public IList<long> list = new List<long>();

    public List<long[,]> ArrList = new List<long[,]>();

    public void Test(long num)
    {
      for (var i = 1; i < 100000; i += 2)
        if (num % i == 0)
        {
          ArrList.Add(new long[1, 2] {{num, i}});
          Console.WriteLine($"i:{i},num:{num},len:{num.ToString().Length}");
          list.Add(i);
        }
    }
  }
}