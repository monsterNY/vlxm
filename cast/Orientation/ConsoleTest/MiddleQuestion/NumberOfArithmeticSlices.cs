using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : NumberOfArithmeticSlices  
  /// @author :mons
  /// @create : 2019/3/28 11:15:12 
  /// @source : https://leetcode.com/problems/arithmetic-slices/
  /// </summary>
  public class NumberOfArithmeticSlices
  {

    /**
     * Runtime: 88 ms, faster than 100.00% of C# online submissions for Arithmetic Slices.
     * Memory Usage: 21.8 MB, less than 22.22% of C# online submissions for Arithmetic Slices.
     *
     * Runtime: 88 ms, faster than 100.00% of C# online submissions for Arithmetic Slices.
     * Memory Usage: 21.7 MB, less than 33.33% of C# online submissions for Arithmetic Slices.
     *
     * doo doo ~
     *
     */
    public int Solution(int[] A)
    {
      if (A.Length < 3) return 0;

      int right = 1, diff, addCount, count = 0;
      while (right < A.Length)
      {
        diff = A[right] - A[right - 1];
        addCount = 1;
        for (right++; right < A.Length; right++)
          if (A[right] - A[right - 1] == diff)
            count += addCount++;
          else
            break;
      }

      return count;
    }

    /**
     * Runtime: 88 ms, faster than 100.00% of C# online submissions for Arithmetic Slices.
     * Memory Usage: 21.8 MB, less than 11.11% of C# online submissions for Arithmetic Slices.
     *
     * same idea
     *
     */
    public int OtherSolution(int[] A)
    {
      int curr = 1, sum = 0;
      for (int i = 2; i < A.Length; i++)
        if (A[i] - A[i - 1] == A[i - 1] - A[i - 2])
          sum += curr++;
        else
          curr = 1;
      return sum;
    }

  }
}