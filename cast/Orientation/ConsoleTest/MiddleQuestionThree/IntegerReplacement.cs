using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestionThree
{
  /// <summary>
  /// @desc : IntegerReplacement  
  /// @author : mons
  /// @create : 2019/4/25 10:41:02 
  /// @source : https://leetcode.com/problems/integer-replacement/
  /// </summary>
  public class IntegerReplacement
  {
    public int Solution(int n)
    {
      var count = 0;

      while (n > 1)
      {
        if (n % 2 == 0) n /= 2;
        else if ((n + 1) % 4 == 0) n++;
        else n--;

        count++;
      }

      return count;
    }
  }
}