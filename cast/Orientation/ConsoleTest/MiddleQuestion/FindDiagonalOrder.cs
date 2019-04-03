using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : FindDiagonalOrder  
  /// @author :mons
  /// @create : 2019/4/3 16:58:19 
  /// @source : https://leetcode.com/problems/diagonal-traverse/
  /// </summary>
  public class FindDiagonalOrder
  {
    /**
     *
     * Runtime: 292 ms, faster than 100.00% of C# online submissions for Diagonal Traverse.
     * Memory Usage: 35.8 MB, less than 100.00% of C# online submissions for Diagonal Traverse.
     *
     * nice
     *
     *
     */
    public int[] Optimize(int[][] matrix)
    {
      if (matrix.Length == 0) return new int[0];
      if (matrix.Length == 1) return matrix[0];

      int[] res = new int[matrix.Length * matrix[0].Length];
      int index = 0, i = 0, j = 0;
      bool flag = true;

      while (true)
      {
        res[index++] = matrix[i][j];

        if (i == matrix.Length - 1 && j == matrix[0].Length - 1) break;

        if (flag) //↗
        {
          if (j == matrix[0].Length - 1)
          {
            flag = false;
            i++;
          }
          else
          {
            if (i == 0)
              flag = false;
            else
              i--;

            j++;
          }
        }
        else //↙
        {
          if (i == matrix.Length - 1)
          {
            flag = true;
            j++;
          }
          else
          {
            if (j == 0)
              flag = true;
            else
              j--;

            i++;
          }
        }
      }

      return res;
    }

    /**
     * Runtime: 468 ms, faster than 12.62% of C# online submissions for Diagonal Traverse.
     * Memory Usage: 35.8 MB, less than 100.00% of C# online submissions for Diagonal Traverse.
     *
     * ？？？black mark,exm? 
     *
     */
    public int[] Solution(int[][] matrix)
    {
      if (matrix.Length == 0) return new int[0];
      if (matrix.Length == 1) return matrix[0];

      int[] res = new int[matrix.Length * matrix[0].Length];
      int index = 0, i = 0, j = 0;
      bool flag = true;

      while (true)
      {
        if (i < matrix.Length && j < matrix[0].Length)
        {
          res[index++] = matrix[i][j];

          if (i == matrix.Length - 1 && j == matrix[0].Length - 1) break;
        }

        if (flag)
        {
          i--;
          j++;
          if (i < 0)
          {
            flag = false;
            i++;
          }
        }
        else
        {
          i++;
          j--;
          if (j < 0)
          {
            flag = true;
            j++;
          }
        }
      }

      return res;
    }

    private bool IsOver(int[][] arr, int i, int j)
    {
      return i > 0 && i < arr.Length && j > 0 && j < arr[0].Length;
    }
  }
}