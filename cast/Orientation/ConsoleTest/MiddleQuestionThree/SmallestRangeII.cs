using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleTest.MiddleQuestionThree
{
  /// <summary>
  /// @desc : SmallestRangeII  
  /// @author : mons
  /// @create : 2019/4/28 17:15:28 
  /// @source : https://leetcode.com/problems/smallest-range-ii/
  /// </summary>、
  [Obsolete]
  public class SmallestRangeII
  {

    public int OtherSolution(int[] A, int K)
    {
      int N = A.Length;
      Array.Sort(A);
      int ans = A[N - 1] - A[0];

      for (int i = 0; i < A.Length - 1; ++i)
      {
        //太秀了。。。 genius
        int a = A[i], b = A[i + 1];
        int high = Math.Max(A[N - 1] - K, a + K);
        int low = Math.Min(A[0] + K, b - K);
        ans = Math.Min(ans, high - low);

        Console.WriteLine($"i:{i},a:{a},b:{b},high:{high},low:{low},ans:{ans}");

      }
      return ans;
    }

    /**
     *
     * 参考
     *
     *[1,2,3,4,5,6,7,8,9,10]
1

7

[1,2,3,4,5,6,7,8,9,10]
2

5

[1,2,3,4,5,6,7,8,9,10]
3

5

[1,2,3,4,5,6,7,8,9,10]
4

7

[1,2,3,4,5,6,7,8,9,10]
5

9

[1,2,3,4,5,6,7,8,9,10]
6

9
     *
     */

    public int Solution(int[] A, int K)
    {
      if (A.Length == 1) return 0;

      Array.Sort(A);

      #region bug

      int add = A[0] + K, add2 = A[A.Length - 1] - K, de = A[0] - K, de2 = A[A.Length - 1] - K;

      int max = add, min = de2, res = int.MaxValue, item;

      if ((item = Math.Abs(add - de2)) < res)
      {
        res = item;
        max = Math.Max(add, de2);
        min = Math.Min(add, de2);
      }

      if ((item = Math.Abs(add - add2)) < res)
      {
        res = item;
        max = Math.Max(add, add2);
        min = Math.Min(add, add2);
      }

      if ((item = Math.Abs(de2 - add)) < res)
      {
        res = item;
        max = Math.Max(add, de2);
        min = Math.Min(add, de2);
      }

      if ((item = Math.Abs(de2 - de)) < res)
      {
        res = item;
        max = Math.Max(add, de);
        min = Math.Min(add, de);
      }

      #endregion

      for (int i = 1; i < A.Length - 1; i++)
      {
        add = A[i] + K;
        de = A[i] - K;
        if ((add >= min && add <= max) || (de >= min && de <= max)) continue;

        if (add < min)
        {
          min = add;
        }
        else if (de > max)
        {
          max = de;
        }
        else
        {
          if (Math.Abs(de - max) > Math.Abs(add - min))
          {
            max = add;
          }
          else
          {
            min = de;
          }
        }
      }

      return Math.Abs(max - min);
    }

    //Time Limit
    public int Simple(int[] A, int K)
    {
      Helper(A.Distinct().ToArray(), K, 0);

      return min;
    }

    private int min = int.MaxValue;

    public void Helper(int[] arr, int k, int index)
    {
      if (index == arr.Length)
      {
        var diff = arr.Max() - arr.Min();

        if (diff < min) min = diff;
      }

      arr[index] += k;
      Helper(arr, k, index + 1);

      arr[index] -= k * 2;
      Helper(arr, k, index + 1);

      arr[index] += k;
    }
  }
}