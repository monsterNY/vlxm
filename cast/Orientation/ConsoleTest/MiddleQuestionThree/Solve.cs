using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestionThree
{
  /// <summary>
  /// @desc : Solve  
  /// @author : mons
  /// @create : 2019/4/29 14:11:01 
  /// @source : https://leetcode.com/problems/surrounded-regions/
  /// </summary>
  public class Solve
  {
    /**
     * 勉强过关...
     *
     * Runtime: 332 ms, faster than 25.50% of C# online submissions for Surrounded Regions.
     * Memory Usage: 32.6 MB, less than 16.67% of C# online submissions for Surrounded Regions.
     *
     */
    public void Solution(char[][] board)
    {
      if (board.Length == 0) return;

      int row = board.Length, col = board[0].Length;

      bool[][] flag = new bool[row][], visited = new bool[row][];

      for (int i = 0; i < row; i++)
      {
        flag[i] = new bool[col];
        visited[i] = new bool[col];
      }

      for (int i = 0; i < row; i++)
      {
        Helper(i, 0, board, flag, visited);
        Helper(i, col - 1, board, flag, visited);
      }

      for (int i = 0; i < col; i++)
      {
        Helper(0, i, board, flag, visited);
        Helper(row - 1, i, board, flag, visited);
      }

      for (int i = 1; i < row - 1; i++)
      {
        for (int j = 1; j < col - 1; j++)
        {
          if (board[i][j] == 'O' && flag[i][j] == false)//差不多的解决。
          {
            board[i][j] = 'X';
          }
        }
      }
    }

    public void Helper(int i, int j, char[][] board, bool[][] flag, bool[][] visited)
    {

      //if (i < 0 || i == board.Length || j < 0 || j == board[0].Length) return;

      if (board[i][j] == 'X') return;

      if (visited[i][j]) return;

      visited[i][j] = true;
      flag[i][j] = true;

      /**
       * Runtime: 324 ms, faster than 52.50% of C# online submissions for Surrounded Regions.
       * Memory Usage: 32.6 MB, less than 16.67% of C# online submissions for Surrounded Regions.
       */
      if (i < board.Length - 1)
        Helper(i + 1, j, board, flag, visited);
      if (i > 0)
        Helper(i - 1, j, board, flag, visited);
      if (j < board[0].Length - 1)
        Helper(i, j + 1, board, flag, visited);
      if (j > 0)
        Helper(i, j - 1, board, flag, visited);
    }

    //Time Limit
    public void Try(char[][] board)
    {
      if (board.Length == 0) return;

      int row = board.Length, col = board[0].Length;

      bool[][] flag = new bool[row][], visited = new bool[row][];

      for (int i = 0; i < row; i++)
      {
        flag[i] = new bool[col];
        visited[i] = new bool[col];
      }

      for (int i = 0; i < row; i++)
      {
        if (board[i][0] == 'O') flag[i][0] = true;
        if (board[i][col - 1] == 'O') flag[i][col - 1] = true;
      }

      for (int i = 0; i < col; i++)
      {
        if (board[0][i] == 'O') flag[0][i] = true;
        if (board[row - 1][i] == 'O') flag[row - 1][i] = true;
      }

      for (int i = 1; i < row - 1; i++)
      {
        for (int j = 1; j < col - 1; j++)
        {
          if (!GetFlag(i, j, board, flag, visited))
          {
            board[i][j] = 'X';
          }
        }
      }
    }

    public bool GetFlag(int i, int j, char[][] board, bool[][] flag, bool[][] visited)
    {
      if (visited[i][j]) return flag[i][j];
      if (board[i][j] == 'X') return false;
      if (flag[i][j]) return true;
      visited[i][j] = true;

      var res = GetFlag(i + 1, j, board, flag, visited)
                || GetFlag(i - 1, j, board, flag, visited)
                || GetFlag(i, j + 1, board, flag, visited)
                || GetFlag(i, j - 1, board, flag, visited);

      if (res) flag[i][j] = true;

      visited[i][j] = false;

      return res;
    }
  }
}