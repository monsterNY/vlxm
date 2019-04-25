using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestionThree
{
  /// <summary>
  /// @desc : SpiralOrder  
  /// @author : mons
  /// @create : 2019/4/25 14:59:29 
  /// @source : https://leetcode.com/problems/spiral-matrix/  
  /// </summary>
  public class SpiralOrder
  {

    /**
     * Runtime: 248 ms, faster than 63.65% of C# online submissions for Spiral Matrix.
     * Memory Usage: 27.9 MB, less than 5.00% of C# online submissions for Spiral Matrix.
     */
    public IList<int> Solution(int[][] matrix)
    {

      IList<int> res = new List<int>();
      if (matrix.Length == 0) return res;

      Left(matrix, 0, matrix.Length, 0, matrix[0].Length, res);

      return res;
    }

    public void Down(int[][] matrix, int startI, int endI, int startJ, int endJ, IList<int> res)
    {
      if (startI == endI || startJ == endJ) return;
      for (int k = startI; k < endI; k++)
        res.Add(matrix[k][endJ - 1]);

      Right(matrix, startI, endI, startJ, endJ - 1, res);
    }

    public void Top(int[][] matrix, int startI, int endI, int startJ, int endJ, IList<int> res)
    {
      if (startI == endI || startJ == endJ) return;

      for (int i = endI - 1; i >= startI; i--)
        res.Add(matrix[i][startJ]);

      Left(matrix, startI, endI, startJ + 1, endJ, res);
    }

    public void Left(int[][] matrix, int startI, int endI, int startJ, int endJ, IList<int> res)
    {
      if (startI == endI || startJ == endJ) return;

      for (int i = startJ; i < endJ; i++)
        res.Add(matrix[startI][i]);

      Down(matrix, startI + 1, endI, startJ, endJ, res);
    }

    public void Right(int[][] matrix, int startI, int endI, int startJ, int endJ, IList<int> res)
    {
      if (startI == endI || startJ == endJ) return;
      for (int i = endJ - 1; i >= startJ; i--)
        res.Add(matrix[endI - 1][i]);

      Top(matrix, startI, endI - 1, startJ, endJ, res);
    }

  }
}