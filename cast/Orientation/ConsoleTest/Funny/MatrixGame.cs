using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.Funny
{
  /// <summary>
  /// @desc : MatrixGame  
  /// @author :monster_yj
  /// @create : 2019/4/23 19:35:16 
  /// @source : 
  /// </summary>
  public class MatrixGame
  {
    private int count;
    private int startJ;
    private int endJ;
    private int startI;
    private int endI;

    public void Run(int[][] matrix)
    {
      startI = 0;
      startJ = 0;
      endI = matrix.Length;
      endJ = matrix[0].Length;
      Down(matrix);
    }

    public void Down(int[][] matrix)
    {
      if (Check()) return;
      ShowConfirm(nameof(Down));
      for (int k = startI; k < endI; k++)
      {
        Console.WriteLine(matrix[k][startJ]);
      }

      startJ++;
      Left(matrix);
    }

    public void Top(int[][] matrix)
    {
      if (Check()) return;
      ShowConfirm(nameof(Top));

      for (int i = endI - 1; i >= startI; i--)
      {
        Console.WriteLine(matrix[i][endJ - 1]);
      }

      endJ--;

      Right(matrix);
    }

    public void Left(int[][] matrix)
    {
      if (Check()) return;
      ShowConfirm(nameof(Left));

      for (int i = startJ; i < endJ; i++)
      {
        Console.WriteLine(matrix[endI - 1][i]);
      }

      endI--;

      Top(matrix);
    }

    public void Right(int[][] matrix)
    {
      if (Check()) return;
      ShowConfirm(nameof(Right));
      for (int i = endJ - 1; i >= startJ; i--)
      {
        Console.WriteLine(matrix[startI][i]);
      }

      startI++;

      Down(matrix);
    }

    protected void ShowConfirm(string flag)
    {
      Console.WriteLine(
        $"{flag}[{nameof(startI)}:{startI},{nameof(endI)}:{endI},{nameof(startJ)}:{startJ},{nameof(endJ)}:{endJ}]");
    }

    protected bool Check()
    {
      return startI == endI && startJ == endJ;
    }
  }
}