using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.LeetCode
{
  /// <summary>
  /// @source:https://leetcode.com/problems/valid-mountain-array/
  /// </summary>
  public class ValidMountainArray
  {
    /**
     *
     * 修改重重bug后。， 提示坑~
     *
     * Runtime: 120 ms, faster than 98.32% of C# online submissions for Valid Mountain Array.
     * Memory Usage: 30.4 MB, less than 100.00% of C# online submissions for Valid Mountain Array.
     */
    public bool Solution(int[] A)
    {
      if (A.Length < 3)
        return false;

      int count = 0;

      for (int i = 1; i < A.Length - 1; i++)
      {
        if (A[i] == A[i - 1])
          return false;

        if (A[i] > A[i - 1] && A[i] > A[i + 1])
          count++;

        if (count > 1 || (count == 0 && A[i] < A[i - 1]))
          return false;
      }

      return count == 1 && A[A.Length - 1] <= A[A.Length - 2];
    }
  }
}