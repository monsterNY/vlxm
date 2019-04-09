using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : IsValidSudoku  
  /// @author :mons
  /// @create : 2019/4/9 16:27:30 
  /// @source : https://leetcode.com/problems/valid-sudoku/
  /// </summary>
  public class IsValidSudoku
  {

    /**
     * Runtime: 108 ms, faster than 100.00% of C# online submissions for Valid Sudoku.
     * Memory Usage: 25.4 MB, less than 92.00% of C# online submissions for Valid Sudoku.
     *
     * ?????????????????????????????????????
     *
     * 这就完了？？？
     *
     */
    public bool Solution(char[][] board)
    {
      //Each row must contain the digits 1 - 9 without repetition.
      //  每一行必须包含数字1 - 9，不能重复。
      bool[] flag = new bool[10];
      for (int i = 0; i < board.Length; i++)
      {
        flag = new bool[10];
        for (int j = 0; j < board[i].Length; j++)
        {
          if (board[i][j] >= '0' && board[i][j] <= '9')
          {
            if (flag[board[i][j] - '0']) return false;
            flag[board[i][j] - '0'] = true;
          }
        }

        flag = new bool[10];
        //Each column must contain the digits 1 - 9 without repetition.
        //  每一列必须包含数字1 - 9，不能重复。
        for (int j = 0; j < board[i].Length; j++)
        {
          if (board[j][i] >= '0' && board[j][i] <= '9')
          {
            if (flag[board[j][i] - '0']) return false;
            flag[board[j][i] - '0'] = true;
          }
        }

      }

      //Each of the 9 3x3 sub-boxes of the grid must contain the digits 1 - 9 without repetition.
      //  网格的9个3x3子框中的每一个都必须包含数字1 - 9，不能重复。

      for (int i = 0; i < board.Length; i += 3)
      {
        for (int j = 0; j < board.Length; j += 3)
        {
          flag = new bool[10];
          for (int k = i; k < i + 3; k++)
          {
            for (int l = j; l < j + 3; l++)
            {
              if (board[k][l] >= '0' && board[k][l] <= '9')
              {
                if (flag[board[k][l] - '0']) return false;
                flag[board[k][l] - '0'] = true;
              }
            }
          }
        }
      }


      return true;
    }
  }
}