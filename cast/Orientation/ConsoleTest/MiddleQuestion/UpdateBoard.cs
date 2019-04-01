using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : UpdateBoard  
  /// @author :mons
  /// @create : 2019/4/1 11:05:47 
  /// @source : https://leetcode.com/problems/minesweeper/submissions/
  /// </summary>
  public class UpdateBoard
  {
    /**
     * Runtime: 360 ms, faster than 7.55% of C# online submissions for Minesweeper.
     * Memory Usage: 34.1 MB, less than 50.00% of C# online submissions for Minesweeper.
     *
     * ha ha ~ over~
     *
     */
    public char[][] Solution(char[][] board, int[] click)
    {
      Dfs(board, click[0], click[1]);

      return board;
    }


    public void Dfs(char[][] board, int i, int j)
    {
      if (i < 0 || i >= board.Length || j < 0 || j >= board[0].Length) return;
      if (board[i][j] == 'M')
      {
        board[i][j] = 'X';
        return;
      }

      if (board[i][j] != 'E') return;

      var risk = GetRisk(board, i, j);

      if (risk > 0)
      {
        board[i][j] = (char) (risk + 48);
        return;
      }

      board[i][j] = 'B';

      Dfs(board, i, j + 1); //find right
      Dfs(board, i, j - 1); //find left
      Dfs(board, i + 1, j); //find top
      Dfs(board, i - 1, j); //find bottom
      Dfs(board, i + 1, j + 1);
      Dfs(board, i - 1, j - 1);
      Dfs(board, i + 1, j - 1);
      Dfs(board, i - 1, j + 1);
    }

    public int GetRisk(char[][] board, int i, int j)
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

    public int GetScore(char[][] board, int i, int j)
    {
      if (i < 0 || i >= board.Length || j < 0 || j >= board[0].Length || board[i][j] != 'M') return 0;
      else
        return 1;
    }

    #region Other Solution

    public char[][] OtherDfs(char[][] board, int[] click)
    {
      int m = board.Length, n = board[0].Length;
      int row = click[0], col = click[1];

      if (board[row][col] == 'M')
      { // Mine
        board[row][col] = 'X';
      }
      else
      { // Empty
        // Get number of mines first.
        int count = 0;
        for (int i = -1; i < 2; i++)
        {
          for (int j = -1; j < 2; j++)
          {
            if (i == 0 && j == 0) continue;
            int r = row + i, c = col + j;
            if (r < 0 || r >= m || c < 0 || c < 0 || c >= n) continue;
            if (board[r][c] == 'M' || board[r][c] == 'X') count++;
          }
        }

        if (count > 0)
        { // If it is not a 'B', stop further DFS.
          board[row][col] = (char)(count + '0');
        }
        else
        { // Continue DFS to adjacent cells.
          board[row][col] = 'B';
          for (int i = -1; i < 2; i++)
          {
            for (int j = -1; j < 2; j++)
            {
              if (i == 0 && j == 0) continue;
              int r = row + i, c = col + j;
              if (r < 0 || r >= m || c < 0 || c < 0 || c >= n) continue;
              if (board[r][c] == 'E') OtherDfs(board, new int[] { r, c });
            }
          }
        }
      }

      return board;
    }

    public char[][] OtherDfs2(char[][] board, int[] click)
    {
      if (board[click[0]][click[1]] == 'E')
      {
        board[click[0]][click[1]] = '0';
        for (int i = -1; i <= 1; i++) for (int j = -1; j <= 1; j++)
          if (click[0] + i >= 0 && click[1] + j >= 0 && click[0] + i < board.Length && click[1] + j < board[click[0] + i].Length && board[click[0] + i][click[1] + j] == 'M')
            board[click[0]][click[1]]++;
        if (board[click[0]][click[1]] == '0')
        {
          board[click[0]][click[1]] = 'B';
          for (int i = -1; i <= 1; i++) for (int j = -1; j <= 1; j++)
            if (click[0] + i >= 0 && click[1] + j >= 0 && click[0] + i < board.Length && click[1] + j < board[click[0] + i].Length && board[click[0] + i][click[1] + j] == 'E')
              OtherDfs2(board, new int[] { click[0] + i, click[1] + j });
        }
      }
      else if (board[click[0]][click[1]] == 'M') board[click[0]][click[1]] = 'X';

      return board;
    }

    #endregion
  }
}