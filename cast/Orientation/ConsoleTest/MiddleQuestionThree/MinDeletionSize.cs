using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestionThree
{
  /// <summary>
  /// @desc : MinDeletionSize  
  /// @author : mons
  /// @create : 2019/4/25 10:57:05 
  /// @source : https://leetcode.com/problems/delete-columns-to-make-sorted-ii/
  /// </summary>
  [Obsolete]
  public class MinDeletionSize
  {

    //理解有出入 
    public int OtherSolution(string[] A)
    {
      if (A.Length == 0) return 0;

      int count = 0, len = A.Length, strLen = A[0].Length, j;

      var flag = new bool[len - 1];

      for (int i = 0; i < strLen; i++)
      {
        for (j = 0; j < len - 1; j++)
        {
          if (!flag[j] && A[j][i] > A[j + 1][i])
          {
            count++;
            break;
          }
        }

        if (j < len - 1) continue;

        for (j = 0; j < len - 1; j++)
          if (A[j][i] < A[j + 1][i])
            flag[j] = true;//此行已排序

      }

      return count;
    }
  }
}