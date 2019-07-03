using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ConsoleTest.HardQuestion
{
  /// <summary>
  /// @desc : LongestIncreasingPath  
  /// @author : mons
  /// @create : 2019/7/2 11:46:04 
  /// @source : 
  /// </summary>
  public class LongestIncreasingPath
  {
    /**
     *
     * Runtime: 128 ms, faster than 99.05% of C# online submissions for Longest Increasing Path in a Matrix.
     *
     * Memory Usage: 26.7 MB, less than 77.78% of C# online submissions for Longest Increasing Path in a Matrix.
     *
     * Runtime: 124 ms, faster than 99.53% of C# online submissions for Longest Increasing Path in a Matrix.
     * Memory Usage: 26.7 MB, less than 77.78% of C# online submissions for Longest Increasing Path in a Matrix.
     *
     */
    public int Solution(int[][] matrix)
    {
      if (matrix.Length == 0) return 0;

      int len = matrix.Length, colLen = matrix[0].Length, max = 1;

      int[][] lenArr = new int[len][];

//      bool[][] visited = new bool[len][];

      for (int i = 0; i < len; i++)
      {
        lenArr[i] = new int[colLen];
//        visited[i] = new bool[colLen];
      }

      for (int i = 0; i < len; i++)
      {
        for (int j = 0; j < colLen; j++)
        {
//          max = Math.Max(max, GetMaxLen(i, j, lenArr, matrix, visited));
          max = Math.Max(max, GetMaxLen(i, j, lenArr, matrix));
        }
      }

      Console.WriteLine(JsonConvert.SerializeObject(lenArr));

      return max;
    }


    //由于有值比较 ，所有不需要visited
    private int GetMaxLen(int i, int j, int[][] lenArr, int[][] matrix)
    {
      if (lenArr[i][j] > 0) return lenArr[i][j];

      var max = 0;
      if (i > 0 && matrix[i][j] > matrix[i - 1][j])
      {
        max = GetMaxLen(i - 1, j, lenArr, matrix);
      }

      if (j > 0 && matrix[i][j] > matrix[i][j - 1])
      {
        max = Math.Max(max, GetMaxLen(i, j - 1, lenArr, matrix));
      }

      if (i < matrix.Length - 1 && matrix[i][j] > matrix[i + 1][j])
      {
        max = Math.Max(max, GetMaxLen(i + 1, j, lenArr, matrix));
      }

      if (j < matrix[0].Length - 1 && matrix[i][j] > matrix[i][j + 1])
      {
        max = Math.Max(max, GetMaxLen(i, j + 1, lenArr, matrix));
      }

      lenArr[i][j] = max + 1;

      return max + 1;
    }

    private int GetMaxLen(int i, int j, int[][] lenArr, int[][] matrix, bool[][] visited)
    {
      if (visited[i][j]) return 0;
      if (lenArr[i][j] > 0) return lenArr[i][j];

      var max = 0;
      visited[i][j] = true;
      if (i > 0 && matrix[i][j] > matrix[i - 1][j])
      {
        max = GetMaxLen(i - 1, j, lenArr, matrix, visited);
      }

      if (j > 0 && matrix[i][j] > matrix[i][j - 1])
      {
        max = Math.Max(max, GetMaxLen(i, j - 1, lenArr, matrix, visited));
      }

      if (i < matrix.Length - 1 && matrix[i][j] > matrix[i + 1][j])
      {
        max = Math.Max(max, GetMaxLen(i + 1, j, lenArr, matrix, visited));
      }

      if (j < matrix[0].Length - 1 && matrix[i][j] > matrix[i][j + 1])
      {
        max = Math.Max(max, GetMaxLen(i, j + 1, lenArr, matrix, visited));
      }

      visited[i][j] = false;

      lenArr[i][j] = max + 1;

      return max + 1;
    }

    /**
     * https://leetcode.com/problems/longest-increasing-path-in-a-matrix/discuss/78308/15ms-Concise-Java-Solution
     *
     * 效果差不多，但这种更容易调整dirs位置变动。
     *
     */

    #region OtherSolution

    public static readonly int[][] dirs = {new[] {0, 1}, new[] {1, 0}, new[] {0, -1}, new[] {-1, 0}};

    public int longestIncreasingPath(int[][] matrix)
    {
      if (matrix.Length == 0) return 0;
      int m = matrix.Length, n = matrix[0].Length;
      int[][] cache = new int[m][];

      for (int i = 0; i < m; i++)
        cache[i] = new int[n];

      int max = 1;
      for (int i = 0; i < m; i++)
      {
        for (int j = 0; j < n; j++)
        {
          int len = dfs(matrix, i, j, m, n, cache);
          max = Math.Max(max, len);
        }
      }

      return max;
    }

    public int dfs(int[][] matrix, int i, int j, int m, int n, int[][] cache)
    {
      if (cache[i][j] != 0) return cache[i][j];
      int max = 1;
      foreach (int[] dir in dirs)
      {
        int x = i + dir[0], y = j + dir[1];
        if (x < 0 || x >= m || y < 0 || y >= n || matrix[x][y] <= matrix[i][j]) continue;
        int len = 1 + dfs(matrix, x, y, m, n, cache);
        max = Math.Max(max, len);
      }

      cache[i][j] = max;
      return max;
    }

    #endregion
  }
}