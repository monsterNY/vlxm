using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.HardQuestion
{
  /// <summary>
  /// @desc : CountTriplets  
  /// @author : mons
  /// @create : 2019/5/5 16:27:48 
  /// @source : https://leetcode.com/problems/triples-with-bitwise-and-equal-to-zero/
  /// </summary>
  public class CountTriplets
  {

    /**
     * Runtime: 1220 ms, faster than 49.25% of C# online submissions for Triples with Bitwise AND Equal To Zero.
     * Memory Usage: 21.8 MB, less than 62.50% of C# online submissions for Triples with Bitwise AND Equal To Zero.
     *
     * 还以为会超时呢。
     *
     */
    public int Simple(int[] A)
    {
      var count = 0;

      //first i&i = i;

      for (int i = 0; i < A.Length; i++)
      {
        if (A[i] == 0) count++;

        for (int j = i + 1; j < A.Length; j++)
        {
          if ((A[i] & A[j]) == 0)
          {
            count += 6;
          }

          for (int k = j + 1; k < A.Length; k++)
          {
            if ((A[i] & A[j] & A[k]) == 0)
            {
              count += 3;
            }
          }
        }
      }

      return count;
    }
  }
}