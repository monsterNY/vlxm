using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestionTwo
{
  /// <summary>
  /// @desc : MaximumSwap  
  /// @author :mons
  /// @create : 2019/4/15 17:03:37 
  /// @source : https://leetcode.com/problems/maximum-swap/
  /// </summary>
  public class MaximumSwap
  {
    /**
     * Runtime: 36 ms, faster than 100.00% of C# online submissions for Maximum Swap.
     * Memory Usage: 12.8 MB, less than 100.00% of C# online submissions for Maximum Swap.
     *
     * ??? 这就最优了,我还想着用dp优化呢。。。
     *
     */
    public int Simple(int num)
    {
      IList<int> list = new List<int>();

      while (num > 0)
      {
        list.Add(num % 10);
        num /= 10;
      }

      bool flag = true;

      for (int i = list.Count - 1; i >= 0; i--)
      {
        if (flag)
        {
          var maxIndex = i;

          for (int j = 0; j < i; j++)
          {
            if (list[j] > list[maxIndex]) maxIndex = j;
          }

          if (maxIndex != i)
          {
            flag = false;
            var temp = list[maxIndex];
            list[maxIndex] = list[i];
            list[i] = temp;
          }
        }

        num = num * 10 + list[i];
      }

      return num;
    }

    /// <summary>
    /// 虽然上面的可行但还是要尝试dp
    ///
    /// Runtime: 40 ms, faster than 58.54% of C# online submissions for Maximum Swap.
    /// Memory Usage: 12.9 MB, less than 33.33% of C# online submissions for Maximum Swap.
    /// 
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public int DpSolution(int num)
    {
      IList<int> list = new List<int>();

      Dictionary<int, int> dictionary = new Dictionary<int, int>(); //保存下标 i 之前的最大值(包括i)

      dictionary.Add(-1, 0);

      int index = 0, remainder, max = 0;

      while (num > 0)
      {
        remainder = num % 10;

        if (remainder > max)
        {
          max = remainder;
          dictionary.Add(index, remainder);
        }

        list.Add(remainder);
        num /= 10;
        index++;
      }

      bool flag = true;

      for (int i = list.Count - 1; i >= 0; i--)
      {
        if (flag)
        {
          var maxIndex = i;

          foreach (var item in dictionary)
          {
            if (item.Key < i && item.Value > list[maxIndex]) maxIndex = item.Key;
          }

          if (maxIndex != i)
          {
            flag = false;
            var temp = list[maxIndex];
            list[maxIndex] = list[i];
            list[i] = temp;
          }
        }

        num = num * 10 + list[i];
      }

      return num;
    }
  }
}