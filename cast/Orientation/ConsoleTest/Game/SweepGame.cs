using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleTest.Game
{
  /// <summary>
  /// @desc : Sweep   扫雷游戏
  /// @author :mons
  /// @create : 2019/4/1 14:21:16 
  /// @source : 
  /// </summary>
  public class SweepGame
  {
    public const int EmptyFlag = -1;
    public const int SafeFlag = 0;
    public const int RiskFlag = -2;
    public const int FailureFlag = -3;

    /// <summary>
    /// 是否结束
    /// </summary>
    public bool IsOver { get; set; }

    protected Random rand = new Random();

    /// <summary>
    /// 是否胜利
    /// </summary>
    /// <param name="board"></param>
    /// <returns></returns>
    public bool IsWin(int[][] board)
    {
      for (int i = 0; i < board.Length; i++)
      {
        for (int j = 0; j < board.Length; j++)
        {
          if (board[i][j] == EmptyFlag) return false;
        }
      }

      return true;
    }

    public int[][] InitBoard(int riskNum)
    {
      IsOver = false;
      var level = 4;
      if (riskNum > 16)
      {
        level = 16;
      }
      else if (riskNum > 12)
      {
        level = 14;
      }
      else if (riskNum > 8)
      {
        level = 11;
      }
      else if (riskNum > 4)
      {
        level = 8;
      }

      var board = new int[level][];

      for (int i = 0; i < board.Length; i++)
      {
        board[i] = new int[level];
        Array.Fill(board[i], EmptyFlag);
      }

      int riskX, riskY;

      while (riskNum > 0)
      {
        riskX = rand.Next(level);
        riskY = rand.Next(level);
        if (board[riskY][riskX] != RiskFlag)
        {
          board[riskY][riskX] = RiskFlag;
          riskNum--;
        }
      }

      return board;
    }

    public void ShowBoard(int[][] board)
    {
      Console.WriteLine();
      for (int i = 0; i <= board[0].Length; i++)
      {
        Console.Write($" {i} ");
      }

      Console.WriteLine();
      Console.WriteLine();

      for (int i = 0; i < board.Length; i++)
      {
        for (int j = 0; j < board.Length; j++)
        {
          if (j == 0)
            Console.Write($" {i + 1} ");

          switch (board[i][j])
          {
            case RiskFlag:
            case EmptyFlag:
              Console.Write($"{(j+1>9?" ":"")} ? ");
              break;
            case SafeFlag:
              Console.Write($"{(j+1>9?" ":"")}   ");
              break;
            default:
              Console.Write($"{(board[i][j] < 10 ? " " : "")}{board[i][j]} ");
              break;
          }
        }

        Console.WriteLine();
        Console.WriteLine();
      }

      Console.WriteLine();
    }

    //下一步
    public int[][] GetStep(int[][] board, int clickY, int clickX)
    {
      if (board[clickY][clickX] == RiskFlag)
      {
        board[clickY][clickX] = FailureFlag;

        Console.WriteLine("\n Game Over ~ \n");

        IsOver = true;

        return board;
      }

      Dfs(board, clickY, clickX);

      return board;
    }

    protected void Dfs(int[][] board, int i, int j)
    {
      if (i < 0 || i >= board.Length || j < 0 || j >= board[0].Length) return;

      if (board[i][j] != EmptyFlag || board[i][j] == RiskFlag) return;

      var risk = GetRisk(board, i, j);

      if (risk > 0)
      {
        board[i][j] = risk;
        return;
      }

      board[i][j] = SafeFlag;

      Dfs(board, i, j + 1); //find right
      Dfs(board, i, j - 1); //find left
      Dfs(board, i + 1, j); //find top
      Dfs(board, i - 1, j); //find bottom
      Dfs(board, i + 1, j + 1);
      Dfs(board, i - 1, j - 1);
      Dfs(board, i + 1, j - 1);
      Dfs(board, i - 1, j + 1);
    }

    protected int GetRisk(int[][] board, int i, int j)
    {
      return GetScore(board, i + 1, j)
             + GetScore(board, i - 1, j)
             + GetScore(board, i, j + 1)
             + GetScore(board, i, j - 1)
             + GetScore(board, i + 1, j + 1)
             + GetScore(board, i - 1, j - 1)
             + GetScore(board, i + 1, j - 1)
             + GetScore(board, i - 1, j + 1);
    }

    public int GetScore(int[][] board, int i, int j)
    {
      if (i < 0 || i >= board.Length || j < 0 || j >= board[0].Length || board[i][j] != RiskFlag) return 0;
      else
        return 1;
    }
  }
}