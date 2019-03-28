using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : Clumsy  
  /// @author :mons
  /// @create : 2019/3/28 17:35:12 
  /// @source : https://leetcode.com/problems/clumsy-factorial/
  /// </summary>
  public class Clumsy
  {

    /**
     * Runtime: 36 ms, faster than 100.00% of C# online submissions for Clumsy Factorial.
     * Memory Usage: 12.8 MB, less than 100.00% of C# online submissions for Clumsy Factorial.
     *
     * ??? 感觉像是简单的
     *
     */
    public int Solution(int N)
    {
      int sum = 0, item;

      for (int i = N; i >= 1; i -= 4)
      {
        item = i;
        for (int j = i - 1; j >= 1 && j > i - 4; j--)
        {
          switch ((i - j) % 4)
          {
            case 1:
              item *= i;
              break;
            case 2:
              item /= i;
              break;
            case 3:
              sum += i;
              break;
          }
        }

        if (i == N)
        {
          sum += item;
        }
        else
        {
          sum -= item;
        }
      }

//      for (int i = N - 1; i >= 1; i--)
//      {
//        switch ((N - i) % 4)
//        {
//          case 1:
//            sum *= i;
//            break;
//          case 2:
//            sum /= i;
//            break;
//          case 3:
//            sum += i;
//            break;
//          case 0:
//            sum -= i;
//            break;
//        }
//      }

      return sum;
    }

    public int OtherSolution(int N)
    {
      if (N == 1) return 1;
      if (N == 2) return 2;
      if (N == 3) return 6;
      if (N == 4) return 7;
      if (N % 4 == 1) return N + 2;
      if (N % 4 == 2) return N + 2;
      if (N % 4 == 3) return N - 1;
      return N + 1;
    }

  }
}