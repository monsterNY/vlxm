using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestionTwo
{
  /// <summary>
  /// @desc : UpdateMatrix  
  /// @author :mons
  /// @create : 2019/4/18 17:18:06 
  /// @source : https://leetcode.com/problems/01-matrix/
  /// </summary>
  [Obsolete("BFS")]
  public class UpdateMatrix
  {

    /**
     * Runtime: 1844 ms, faster than 5.74% of C# online submissions for 01 Matrix.
     * Memory Usage: 37.9 MB, less than 84.21% of C# online submissions for 01 Matrix.
     *
     * ha ha .,
     */
    public int[][] Solution2(int[][] matrix)
    {

      for (int i = 0; i < matrix.Length; i++)
      {
        for (int j = 0; j < matrix[i].Length; j++)
        {
          if (matrix[i][j] == 1)
          {
            matrix[i][j] = int.MaxValue;
          }
        }
      }

      for (int i = 0; i < matrix.Length; i++)
      {
        for (int j = 0; j < matrix[i].Length; j++)
        {
          if (matrix[i][j] == 0)
          {
            Helper(i, j + 1, matrix, 1);
            Helper(i + 1, j, matrix, 1);
            Helper(i - 1, j, matrix, 1);
            Helper(i, j - 1, matrix, 1);
          }
        }
      }

      return matrix;
    }

    public void Helper(int i, int j, int[][] matrix, int distance)
    {
      if (i == -1 || j == -1 || i == matrix.Length || j == matrix[0].Length || matrix[i][j] == 0) return;

      if (matrix[i][j] <= distance) return;

      matrix[i][j] = distance;

      Helper(i, j + 1, matrix, distance + 1);
      Helper(i + 1, j, matrix, distance + 1);
      Helper(i - 1, j, matrix, distance + 1);
      Helper(i, j - 1, matrix, distance + 1);
    }

    //Time Limit
    public int[][] Solution(int[][] matrix)
    {
      var visited = new bool[matrix.Length][];
      for (int i = 0; i < visited.Length; i++)
      {
        visited[i] = new bool[matrix[0].Length];
      }

      for (int i = 0; i < matrix.Length; i++)
      {
        for (int j = 0; j < matrix[i].Length; j++)
        {
          if (matrix[i][j] == 0)
          {
            Helper(i, j + 1, matrix, 1, visited);
            Helper(i + 1, j, matrix, 1, visited);
            Helper(i - 1, j, matrix, 1, visited);
            Helper(i, j - 1, matrix, 1, visited);
          }
        }
      }

      return matrix;
    }

    public void Helper(int i, int j, int[][] matrix, int distance, bool[][] visited)
    {
      if (i == -1 || j == -1 || i == matrix.Length || j == matrix[0].Length || matrix[i][j] == 0) return;

      if (visited[i][j])
      {
        if (matrix[i][j] <= distance) return;
      }

      matrix[i][j] = distance;
      visited[i][j] = true;

      Helper(i, j + 1, matrix, distance + 1, visited);
      Helper(i + 1, j, matrix, distance + 1, visited);
      Helper(i - 1, j, matrix, distance + 1, visited);
      Helper(i, j - 1, matrix, distance + 1, visited);
    }
  }
}