using System;
using System.Collections.Generic;
using System.Text;
using Tools.CusAttr;
using Tools.CusMenu;

namespace ConsoleTest.MiddleQuestionThree
{
  /// <summary>
  /// @desc : MaximalSquare  
  /// @author : mons
  /// @create : 2019/4/23 17:46:28 
  /// @source : https://leetcode.com/problems/maximal-square/
  /// </summary>
  public class MaximalSquare
  {
    [Obsolete]
    [Question(QuestionTypes.Dp)]
    public int Solution(char[][] matrix)
    {
      int startI = 0, endI = matrix.Length, startJ = 0, endJ = matrix[0].Length, lon, wide;

      while (!Check(startI, endI, startJ, endJ))
      {
        lon = endI - startI;
        wide = endJ - startJ;
        Down(matrix, startI, endI, startJ, endJ);

        startI++;
        endI--;
        startJ++;
        endJ--;
      }

      return 1;
    }

    public void Down(char[][] matrix, int startI, int endI, int startJ, int endJ)
    {
      if (Check(startI, endI, startJ, endJ)) return;
      for (int k = startI; k < endI; k++)
      {
        if (matrix[k][startJ] == '0')
        {
          return;
        }
      }

      startJ++;
      Left(matrix, startI, endI, startJ, endJ);
    }

    public void Top(char[][] matrix, int startI, int endI, int startJ, int endJ)
    {
      if (Check(startI, endI, startJ, endJ)) return;

      for (int i = endI - 1; i >= startI; i--)
      {
        if (matrix[i][endJ - 1] == '0')
        {
          return;
        }
      }

      endJ--;

      Right(matrix, startI, endI, startJ, endJ);
    }

    public void Left(char[][] matrix, int startI, int endI, int startJ, int endJ)
    {
      if (Check(startI, endI, startJ, endJ)) return;

      for (int i = startJ; i < endJ; i++)
      {
        if (matrix[endI - 1][i] == '0')
        {
          return;
        }
      }

      endI--;

      Top(matrix, startI, endI, startJ, endJ);
    }

    public void Right(char[][] matrix, int startI, int endI, int startJ, int endJ)
    {
      if (Check(startI, endI, startJ, endJ)) return;
      for (int i = endJ - 1; i >= startJ; i--)
      {
        return;
      }

      startI++;

      Down(matrix, startI, endI, startJ, endJ);
    }

    protected bool Check(int startI, int endI, int startJ, int endJ)
    {
      return startI >= endI && startJ >= endJ;
    }
  }
}