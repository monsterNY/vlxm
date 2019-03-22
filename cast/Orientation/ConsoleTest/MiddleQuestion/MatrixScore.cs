using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : MatrixScore  
  /// @author :mons
  /// @create : 2019/3/22 11:54:31 
  /// @source : https://leetcode.com/problems/score-after-flipping-matrix/
  /// </summary>
  public class MatrixScore
  {
    /**
     * Runtime: 92 ms, faster than 96.88% of C# online submissions for Score After Flipping Matrix.
     * Memory Usage: 22.2 MB, less than 100.00% of C# online submissions for Score After Flipping Matrix.
     *
     * nice~
     *
     */
    public int Solution(int[][] A)
    {
      for (var i = 0; i < A.Length; i++)
        if (A[i][0] == 0)
          for (int j = 0; j < A[i].Length; j++)
            A[i][j] = (A[i][j] + 1) % 2;

      for (var i = 0; i < A[0].Length; i++)
      {
        var zeroCount = 0;
        var flag = false;
        for (var j = 0; j < A.Length; j++)
        {
          if (flag)
            A[j][i] = (A[j][i] + 1) % 2;
          else if (A[j][i] == 0)
          {
            zeroCount++;
            if (flag = zeroCount > A.Length / 2)
              j = -1;
          }
        }
      }

      return GetSum(A);
    }

    /**
     * Runtime: 104 ms, faster than 59.38% of C# online submissions for Score After Flipping Matrix.
     * Memory Usage: 22 MB, less than 100.00% of C# online submissions for Score After Flipping Matrix.
     */
    public int Optimize(int[][] A)
    {
      for (var i = 0; i < A.Length; i++)
      {
        if (A[i][0] == 0)
        {
          for (int j = 0; j < A[i].Length; j++)
          {
            A[i][j] = (A[i][j] + 1) % 2;
          }
        }
      }

      for (int i = 0; i < A[0].Length; i++)
      {
        var zeroCount = 0;
        for (var j = 0; j < A.Length; j++)
        {
          if (A[j][i] == 0)
          {
            zeroCount++;
            if (zeroCount > A.Length / 2)
            {
              for (j = 0; j < A.Length; j++) //重新开循环 慢于 直接修改循环元素
              {
                A[j][i] = (A[j][i] + 1) % 2;
              }

              break;
            }
          }
        }
      }

      return GetSum(A);
    }

    public int GetSum(int[][] arr)
    {
      var sum = 0;

      for (int i = 0; i < arr[0].Length; i++)
      {
        sum *= 2;
        for (int j = 0; j < arr.Length; j++)//此处使用for 快于 foreach ??? 而外层foreach 内层 for 则无影响。。。 by leetcode
          sum += arr[j][i];
      }

      return sum;
    }

    public int Try(int[][] A)
    {
      var count = 1;

      for (int i = 1; i < A[0].Length; i++)
      {
        count = count * 2 + 1;
      }

      if (A.Length == 1)
      {
        return count;
      }

      IList<int> verticalList = new List<int>();

      bool verticalFlag = false;

      for (int i = 0; i < A[0].Length; i++)
      {
        if (!verticalFlag && A[0][i] == 1)
        {
          verticalFlag = true;
        }

        if (A[0][i] == 0)
          verticalList.Add(i);
      }

      if (verticalFlag)
      {
        foreach (var item in verticalList)
        {
          for (int i = 0; i < A.Length; i++)
          {
            A[i][item] = (A[i][item] + 1) % 2;
          }
        }
      }

      for (int i = 1; i < A.Length; i++)
      {
        if (A[i][0] == 0)
        {
          for (int j = 0; j < A[i].Length; j++)
          {
            A[i][j] = (A[i][j] + 1) % 2;
          }
        }
      }

      for (int i = 1; i < A.Length; i++)
      {
        for (int j = 1; j < A[i].Length; j++)
        {
          count = count * 2 + A[i][j];
        }
      }

      return count;
    }
  }
}