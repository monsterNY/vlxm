using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestionThree
{
  /// <summary>
  /// @desc : MaximalSquare  
  /// @author :mons
  /// @create : 2019/4/23 17:46:28 
  /// @source : https://leetcode.com/problems/maximal-square/
  /// </summary>
  public class MaximalSquare
  {
    public int Solution(char[][] matrix)
    {
      int lon = matrix.Length, wide = matrix.Length, start = 0;

      for (int i = start; i < lon; i++)
      {
        if (matrix[i][0] == 0)
        {
          start++;
          break;
        }

        if (matrix[i][wide - 1] == 0)
        {
          break;
        }
      }

      return 1;
    }

    public void Down(int i, int endI, int j, char[][] matrix)
    {
      for (int k = i; k <= endI; k++)
      {
        Console.WriteLine(matrix[k][j]);
      }

      Left(endI, j + 1, matrix[0].Length - 1 - j, matrix);
    }

    public void Top(int i, int endI, int j, char[][] matrix)
    {
      for (int k = endI; k >= i; k--)
      {
        Console.WriteLine(matrix[k][j]);
      }

      Right(i, matrix[0].Length - 1 - j, j, matrix);
    }

    public void Left(int i, int j, int endJ, char[][] matrix)
    {
      for (int k = j; k <= endJ; k++)
      {
        Console.WriteLine(matrix[i][k]);
      }

      Top(i - 1, matrix.Length - 1 - i, endJ, matrix);
    }

    public void Right(int i, int j, int endJ, char[][] matrix)
    {
      for (int k = endJ; k >= j; k++)
      {
        Console.WriteLine(matrix[i][k]);
      }

      Down(i + 1, matrix.Length - 1 - i, j, matrix);
    }
  }
}