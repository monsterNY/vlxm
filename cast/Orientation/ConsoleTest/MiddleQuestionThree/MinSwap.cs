using System;
using System.Collections.Generic;
using System.Text;
using Tools.CusAttr;
using Tools.CusMenu;

namespace ConsoleTest.MiddleQuestionThree
{
  /// <summary>
  /// @desc : MinSwap  
  /// @author :mons
  /// @create : 2019/4/22 9:43:25 
  /// @source : https://leetcode.com/problems/minimum-swaps-to-make-sequences-increasing/
  /// </summary>
  [Obsolete]
  [Love(LoveTypes.Problem)]
  public class MinSwap
  {

    //don't understand
    public int OtherSolution(int[] A, int[] B)
    {
      int swapRecord = 1, fixRecord = 0;
      for (int i = 1; i < A.Length; i++)
      {
        if (A[i - 1] >= B[i] || B[i - 1] >= A[i])
        {
          // In this case, the ith manipulation should be same as the i-1th manipulation
          // fixRecord = fixRecord;
          swapRecord++;
          Console.WriteLine("swapRecord++");
        }
        else if (A[i - 1] >= A[i] || B[i - 1] >= B[i])
        {
          // In this case, the ith manipulation should be the opposite of the i-1th manipulation
          int temp = swapRecord;
          swapRecord = fixRecord + 1;
          fixRecord = temp;
          Console.WriteLine("swapRecord = fixRecord + 1");
        }
        else
        {
          // Either swap or fix is OK. Let's keep the minimum one
          int min = Math.Min(swapRecord, fixRecord);
          swapRecord = min + 1;
          fixRecord = min;
          Console.WriteLine("else");
        }

        Console.WriteLine($"{nameof(swapRecord)}:{swapRecord},{nameof(fixRecord)}:{fixRecord},i:{i}");
      }

      return Math.Min(swapRecord, fixRecord);
    }

    public int Simple(int[] A, int[] B)
    {
      var count = 0;

      for (int i = 1; i < A.Length; i++)
      {
        if ((A[i - 1] >= A[i] && B[i] > A[i - 1] && A[i] > B[i - 1]) ||
            (B[i] < B[i - 1] && A[i] > B[i - 1] && B[i] > A[i - 1]))
        {
          var temp = A[i];
          A[i] = B[i];
          B[i] = temp;
          count++;
        }
      }

      return count;
    }

    public bool CanChange(int[] arr, int[] arr2, int index)
    {
      return false;
    }
  }
}