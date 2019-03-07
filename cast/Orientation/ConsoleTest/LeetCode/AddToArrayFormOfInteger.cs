using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleTest.LeetCode
{
  /// <summary>
  /// @source:https://leetcode.com/problems/add-to-array-form-of-integer/
  /// </summary>
  public class AddToArrayFormOfInteger
  {
    /**
     * Runtime: 304 ms, faster than 93.58% of C# online submissions for Add to Array-Form of Integer.
     * Memory Usage: 38.5 MB, less than 5.55% of C# online submissions for Add to Array-Form of Integer.
     */
    public IList<int> AddToArrayForm(int[] A, int K)
    {
      IList<int> list = new List<int>();

      int isopsephy = 0, sum;

      for (int i = A.Length - 1; i >= 0; i--)
      {
        sum = K % 10 + A[i] + isopsephy;
        isopsephy = sum / 10;
        list.Add(sum % 10);
        K /= 10;
      }

      K += isopsephy;

      while (K > 0)
      {
        list.Add(K % 10);
        K /= 10;
      }

      return list.Reverse().ToList();
    }

    public IList<int> OtherSolution(int[] A, int K)
    {
      IList<int> list = new List<int>();

      for (int i = A.Length - 1; i >= 0; i--)
      {
        list.Insert(0,(K + A[i]) % 10);
        K = (K + A[i]) / 10;
      }

      while (K > 0)
      {
        list.Insert(0,K % 10);
        K /= 10;
      }

      return list.Reverse().ToList();
    }

    /// <summary>
    /// int 增长超出
    /// </summary>
    /// <param name="A"></param>
    /// <param name="K"></param>
    /// <returns></returns>
    public IList<int> SimpleDeal(int[] A, int K)
    {
      IList<int> list = new List<int>();

      int sum = K;
      int multiply = 1;

      for (int i = A.Length - 1; i >= 0; i--)
      {
        sum += A[i] * multiply;
        multiply *= 10;
      }

      while (sum > 0)
      {
        list.Add(sum % 10);
        sum /= 10;
      }

      return list.Reverse().ToList();
    }
  }
}