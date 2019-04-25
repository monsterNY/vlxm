using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestionThree
{
  /// <summary>
  /// @desc : Exist  
  /// @author : mons
  /// @create : 2019/4/25 11:43:22 
  /// @source : https://leetcode.com/problems/word-search/
  /// </summary>
  public class Exist
  {
    /**
     * Runtime: 116 ms, faster than 99.84% of C# online submissions for Word Search.
     * Memory Usage: 26.7 MB, less than 100.00% of C# online submissions for Word Search.
     *
     * easy~
     *
     */
    public bool Solution(char[][] board, string word)
    {
      if (board.Length == 0) return false;

      int rowLen = board.Length, colLen = board[0].Length;

      var visited = new bool[rowLen][];

      for (int i = 0; i < rowLen; i++)
        visited[i] = new bool[colLen];

      for (int i = 0; i < rowLen; i++)
      for (int j = 0; j < colLen; j++)
        if (Helper(board, word, 0, i, j, visited))
          return true;

      return false;
    }

    public bool Helper(char[][] board, string word, int index, int i, int j, bool[][] visited)
    {
      if (index == word.Length) return true;
      if (i < 0 || i >= board.Length || j < 0 || j >= board[0].Length || board[i][j] != word[index] ||
          visited[i][j]) return false;

      visited[i][j] = true;

      var res = Helper(board, word, index + 1, i + 1, j, visited) ||
                Helper(board, word, index + 1, i - 1, j, visited) ||
                Helper(board, word, index + 1, i, j + 1, visited) ||
                Helper(board, word, index + 1, i, j - 1, visited);

      visited[i][j] = false;

      return res;
    }
  }
}