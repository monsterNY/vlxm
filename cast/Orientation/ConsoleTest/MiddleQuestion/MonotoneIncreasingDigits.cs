using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : MonotoneIncreasingDigits  
  /// @author :mons
  /// @create : 2019/4/10 17:18:01 
  /// @source : https://leetcode.com/problems/monotone-increasing-digits/
  /// </summary>
  public class MonotoneIncreasingDigits
  {

    /**
     * Runtime: 36 ms, faster than 100.00% of C# online submissions for Monotone Increasing Digits.
     * Memory Usage: 13 MB, less than 100.00% of C# online submissions for Monotone Increasing Digits.
     *
     * ? 天。。。。！！！！！！！
     *
     * Runtime: 52 ms, faster than 12.50% of C# online submissions for Monotone Increasing Digits.
     * Memory Usage: 12.9 MB, less than 100.00% of C# online submissions for Monotone Increasing Digits.
     *
     * what???
     *
     */
    public int Solution(int N)
    {
      var list = new List<int>();
      var prevNum = 0;

      while (N > 0)
      {
        list.Add(N % 10);
        N /= 10;
      }

      for (int i = 0; i < list.Count - 1; i++)
      {
        if (list[i + 1] > list[i])
        {
          for (int j = i; j >= 0 && list[j] != 9; j--)
          {
            list[j] = 9;
          }

          list[i] = 9;
          list[i + 1]--;
        }

      }

      var num = 0;
      for (int i = list.Count - 1; i >= 0; i--)
      {
        num = num * 10 + list[i];
      }

      return num;
    }

    public int Try(int N)
    {
      var list = new List<int>();
      var item = 0;

      while (N > 0)
      {
        list.Add(N % 10);

        N /= 10;
      }

      var num = 0;
      bool flag = false;
      for (int i = list.Count - 1; i >= 0; i--)
      {
        if (flag)
        {
          num = num * 10 + 9;
          continue;
        }

        num *= 10;
        if (i > 0 && list[i] >= list[i - 1])
        {
          num += list[i] - 1;
          list[i - 1] = 9;
          flag = true;
        }
        else
        {
          num += list[i];
        }
      }

      return num;
    }

    public int Try2(int N)
    {
      var list = new List<int>();
      var prevNum = 0;

      while (N > 0)
      {
        list.Add(N % 10);

        N /= 10;
      }

      int mul = 1, num = 0;
      for (int i = 0; i < list.Count - 1; i++)
      {
        if (list[i + 1] > list[i])
        {
          list[i] = 9;
          list[i + 1]--;
        }

        num += list[i] * mul;
        mul *= 10;
      }

      num += list[list.Count - 1] * mul;

      return num;
    }
  }
}