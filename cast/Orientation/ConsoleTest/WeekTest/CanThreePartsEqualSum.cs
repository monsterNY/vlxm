using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.WeekTest
{
  /// <summary>
  /// @desc : CanThreePartsEqualSum  
  /// @author :mons
  /// @create : 2019/3/26 16:35:06 
  /// @source : https://leetcode.com/contest/weekly-contest-129/problems/partition-array-into-three-parts-with-equal-sum/
  /// </summary>
  public class CanThreePartsEqualSum
  {

    /**
     * Runtime: 140 ms
     * Memory Usage: 34.1 MB
     */
    public bool Solution(int[] A)
    {
      var sum = 0;

      for (int i = 0; i < A.Length; i++)
      {
        sum += A[i];
      }

      if (sum % 3 != 0) return false;

      var targetNum = sum / 3;
      int first = 0, second = 0, third = 0;

      for (int i = A.Length - 1; i >= 0; i--)
      {
        var item = A[i];
        if (targetNum != first)
        {
          first += item;
        }
        else if (targetNum != second)
        {
          second += item;
        }
        else
        {
          third += item;
        }
      }

      return first == targetNum && second == targetNum && third == targetNum;
    }
  }
}