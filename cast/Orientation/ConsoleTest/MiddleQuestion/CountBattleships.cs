using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : CountBattleships  
  /// @author :mons
  /// @create : 2019/3/22 16:04:37 
  /// @source : https://leetcode.com/problems/battleships-in-a-board/
  /// </summary>
  public class CountBattleships
  {
    /**
     * Runtime: 116 ms, faster than 86.73% of C# online submissions for Battleships in a Board.
     * Memory Usage: 24.4 MB, less than 42.86% of C# online submissions for Battleships in a Board.
     *
     * 想法简单~~
     *
     */
    public int Solution(char[,] board)
    {
      var count = 0;

      for (int i = 0; i < board.GetLength(0); i++)
      {
        for (int j = 0; j < board.GetLength(1); j++)
        {
          if (board[i, j] == 'X')
          {
            count++;

            for (int k = j + 1; k < board.GetLength(1); k++)
            {
              if (board[i, k] == 'X')
              {
                board[i, k] = '.';
              }
              else
              {
                break;
              }
            }

            for (int k = i + 1; k < board.GetLength(0); k++)
            {
              if (board[k, j] == 'X')
              {
                board[k, j] = '.';
              }
              else
              {
                break;
              }
            }
          }
        }
      }

      return count;
    }

    /**
     * Runtime: 112 ms, faster than 100.00% of C# online submissions for Battleships in a Board.
     * Memory Usage: 24.2 MB, less than 85.71% of C# online submissions for Battleships in a Board.
     *
     * amazing~ 果然方法比字段更耗时
     *
     */
    public int Optimize(char[,] board)
    {
      var count = 0;
      var len = board.GetLength(0);
      var subLen = board.GetLength(1);

      for (int i = 0; i < len; i++)
      for (int j = 0; j < subLen; j++)
        if (board[i, j] == 'X')
        {
          count++;

          for (int k = j + 1; k < subLen; k++)
            if (board[i, k] == 'X')
              board[i, k] = '.';
            else
              break;

          for (int k = i + 1; k < len; k++)
            if (board[k, j] == 'X')
              board[k, j] = '.';
            else
              break;
        }

      return count;
    }
  }
}