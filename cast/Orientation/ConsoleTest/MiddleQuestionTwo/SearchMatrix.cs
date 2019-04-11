using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestionTwo
{
  /// <summary>
  /// @desc : SearchMatrix  
  /// @author :mons
  /// @create : 2019/4/11 17:37:03 
  /// @source : https://leetcode.com/problems/search-a-2d-matrix-ii/
  /// </summary>
  public class SearchMatrix
  {
    public bool Solution(int[,] matrix, int target)
    {
      int i = 0, len = matrix.GetLength(0), colLen = matrix.GetLength(1);

      while (i < len)
      {
        if (target > matrix[i, colLen - 1])
          i++;
        else
          for (int k = 0; k < colLen; k++)
          {
            if (matrix[i, k] == target) return true;
            if (matrix[i, k] > target) return false;
          }
      }

      return false;
    }

    //bug 
    public bool Try(int[,] matrix, int target)
    {
      int i = 0, j = 0, len = matrix.GetLength(0), colLen = matrix.GetLength(1);

      while (i < len && j < colLen)
      {
        if (matrix[i, j] == target) return true;
        if (matrix[i, j] > target) return true;

        if (i + 1 == len)
        {
          j++;
          continue;
        }

        if (j + 1 == colLen)
        {
          i++;
          continue;
        }

        int num1 = matrix[i + 1, j], num2 = matrix[i, j + 1];

        if (num1 <= target && num2 <= target)
        {
          if (num1 > num2) i++;
          else j++;
        }
        else if (num1 > target) j++;
        else if (num2 > target) i++;
        else return false;
      }

      return false;
    }

    //time limit
    public bool GetResult(int[,] matrix, int target, int i, int j)
    {
      if (i == matrix.GetLength(0) || j == matrix.GetLength(1) || matrix[i, j] > target) return false;

      if (matrix[i, j] == target) return true;

      return GetResult(matrix, target, i + 1, j) || GetResult(matrix, target, i, j + 1);
    }
  }
}