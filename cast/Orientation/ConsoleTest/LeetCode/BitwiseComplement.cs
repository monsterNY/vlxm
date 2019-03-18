using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.LeetCode
{
  /// <summary>
  /// @desc : BitwiseComplement  
  /// @author :mons
  /// @create : 2019/3/18 16:38:48 
  /// @source : 
  /// </summary>
  public class BitwiseComplement
  {
    /**
     * Runtime: 36 ms, faster than 100.00% of C# online submissions for Complement of Base 10 Integer.
     * Memory Usage: 12.8 MB, less than 100.00% of C# online submissions for Complement of Base 10 Integer.
     */
    public int Solution(int num)
    {
      List<int> list = new List<int>() {num % 2};

      while ((num /= 2) > 0)
      {
        list.Add(num % 2);
      }

      var result = 0;

      for (int i = list.Count - 1; i >= 0; i--)
      {
        result = result * 2 + ((list[i] + 1) % 2);
      }

      return result;
    }

    /**
     * Runtime: 36 ms, faster than 100.00% of C# online submissions for Complement of Base 10 Integer.
     * Memory Usage: 12.9 MB, less than 100.00% of C# online submissions for Complement of Base 10 Integer.
     *
     * 效率差不多______.
     * 不过这是刚开始打算做的方案。
     *
     */
    public int OtherSolution(int num)
    {
      int X = 1;
      while (num > X) X = X * 2 + 1;
      return num ^ X;
    }
  }
}