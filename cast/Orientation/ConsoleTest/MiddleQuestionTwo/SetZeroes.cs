using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestionTwo
{
  /// <summary>
  /// @desc : SetZeroes  
  /// @author :mons
  /// @create : 2019/4/15 17:31:38 
  /// @source : https://leetcode.com/problems/set-matrix-zeroes/
  /// </summary>
  public class SetZeroes
  {

    /**
     * java : Runtime: 1 ms, faster than 95.07% of Java online submissions for Set Matrix Zeroes.
     * Memory Usage: 43.9 MB, less than 79.29% of Java online submissions for Set Matrix Zeroes.
     *
     * C# :
     * Runtime: 268 ms, faster than 28.63% of C# online submissions for Set Matrix Zeroes.
     * Memory Usage: 31.4 MB, less than 6.52% of C# online submissions for Set Matrix Zeroes.
     *
     * 是要气死我。。。
     *
     */
    //emm... not diff ...
    public void Solution2(int[][] matrix)
    {

      bool[] rowFlag = new bool[matrix.Length], colFlag = new bool[matrix[0].Length];

      for (int i = 0; i < matrix.Length; i++)
      {
        for (int j = 0; j < matrix[i].Length; j++)
        {
          if (matrix[i][j] == 0)
          {
            rowFlag[i] = true;
            colFlag[j] = true;
            for (int k = 0; k < i; k++)
              matrix[k][j] = 0;
            for (int k = 0; k < j; k++)
              matrix[i][k] = 0;
          }
          else if (rowFlag[i] || colFlag[j])
            matrix[i][j] = 0;
        }
      }
    }

    //同样低效。。。
    public void Solution(int[][] matrix)
    {
      if (matrix.Length < 1) return;

      bool[] rowFlag = new bool[matrix.Length], colFlag = new bool[matrix[0].Length];

      for (int i = 0; i < matrix.Length; i++)
      {
        for (int j = 0; j < matrix[i].Length; j++)
        {
          if (matrix[i][j] == 0)
          {
            rowFlag[i] = true;
            colFlag[j] = true;
          }
        }
      }

      for (int i = 0; i < matrix.Length; i++)
      {
        for (int j = 0; j < matrix[i].Length; j++)
        {
          if (rowFlag[i] || colFlag[j])
          {
            matrix[i][j] = 0;
          }
        }
      }
    }

    public void Simple(int[][] matrix)
    {
      if (matrix.Length < 1) return;

      var flag = new bool[matrix.Length][];

      for (int i = 0; i < flag.Length; i++)
      {
        flag[i] = new bool[matrix[0].Length];
      }

      for (int i = 0; i < matrix.Length; i++)
      {
        for (int j = 0; j < matrix[i].Length; j++)
        {
          if (flag[i][j] == false && matrix[i][j] == 0)
          {
            Helper(matrix, i, j, flag);
          }
        }
      }
    }

    public void Helper(int[][] matrix, int i, int j, bool[][] flag)
    {
      for (int k = 0; k < matrix.Length; k++)
      {
        if (matrix[k][j] != 0)
          flag[k][j] = true;
        matrix[k][j] = 0;
      }

      for (int k = 0; k < matrix[0].Length; k++)
      {
        if (matrix[i][k] != 0)
          flag[i][k] = true;
        matrix[i][k] = 0;
      }
    }
  }
}