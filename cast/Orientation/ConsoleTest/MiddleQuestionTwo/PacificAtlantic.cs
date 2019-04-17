using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Newtonsoft.Json;
using Tools.CusAttr;
using Tools.CusMenu;

namespace ConsoleTest.MiddleQuestionTwo
{
  /// <summary>
  /// @desc : PacificAtlantic  
  /// @author :mons
  /// @create : 2019/4/17 14:39:26 
  /// @source : https://leetcode.com/problems/pacific-atlantic-water-flow/
  /// </summary>
  [Love(LoveTypes.Question,LoveTypes.Fix)]
  [Description("There will always be rewards by giving, there will be gains by making efforts.")]
  public class PacificAtlantic
  {

    /**
     * Runtime: 288 ms, faster than 100.00% of C# online submissions for Pacific Atlantic Water Flow.
     * Memory Usage: 32.2 MB, less than 100.00% of C# online submissions for Pacific Atlantic Water Flow.
     *
     * 努力就会有收获
     *
     */
    public IList<int[]> Solution(int[][] matrix)
    {
      IList<int[]> res = new List<int[]>();

      if (matrix.Length == 0) return res;

      int rowLen = matrix.Length, colLen = matrix[0].Length;

      bool[][] pacificFlag = new bool[matrix.Length][], atlanticFlag = new bool[matrix.Length][];

      for (int i = 0; i < rowLen; i++)
      {
        pacificFlag[i] = new bool[colLen];
        atlanticFlag[i] = new bool[colLen];
      }

      for (int i = 0; i < rowLen; i++)
      {
        pacificFlag[i][0] = true;
        atlanticFlag[i][colLen - 1] = true;
      }

      for (int i = 0; i < colLen; i++)
      {
        pacificFlag[0][i] = true;
        atlanticFlag[rowLen - 1][i] = true;
      }

      for (int i = 0; i < rowLen; i++)
      {
        SetCanFlow(i, 1, matrix, pacificFlag);
        SetCanFlow(i, colLen - 2, matrix, atlanticFlag);
      }

      for (int i = 0; i < colLen; i++)
      {
        SetCanFlow(1, i, matrix, pacificFlag);
        SetCanFlow(rowLen - 2, i, matrix, atlanticFlag);
      }

      for (int i = 0; i < rowLen; i++)
        for (int j = 0; j < colLen; j++)
          if (pacificFlag[i][j] && atlanticFlag[i][j]) res.Add(new[] {i, j});

      return res;
    }

    private void SetCanFlow(int i, int j, int[][] matrix, bool[][] flag)
    {
      if (i == matrix.Length || j == matrix[0].Length || i == -1 || j == -1 || flag[i][j]) return;

      if (i > 0 && matrix[i][j] >= matrix[i - 1][j] && flag[i - 1][j])
        flag[i][j] = true;
      else if (j > 0 && matrix[i][j] >= matrix[i][j - 1] && flag[i][j - 1])
        flag[i][j] = true;
      else if (i < matrix.Length - 1 && matrix[i][j] >= matrix[i + 1][j] && flag[i + 1][j])
        flag[i][j] = true;
      else if (j < matrix[0].Length - 1 && matrix[i][j] >= matrix[i][j + 1] && flag[i][j + 1])
        flag[i][j] = true;

      if (flag[i][j])
      {
        SetCanFlow(i + 1, j, matrix, flag);
        SetCanFlow(i, j + 1, matrix, flag);
        SetCanFlow(i - 1, j, matrix, flag);
        SetCanFlow(i, j - 1, matrix, flag);
      }
    }

    [Obsolete("bug")]
    public IList<int[]> Try2(int[][] matrix)
    {
      IList<int[]> res = new List<int[]>();

      bool[][] pacificFlag = new bool[matrix.Length][], atlanticFlag = new bool[matrix.Length][];

      for (int i = 0; i < pacificFlag.Length; i++)
      {
        pacificFlag[i] = new bool[matrix[0].Length];
        atlanticFlag[i] = new bool[matrix[0].Length];
      }

      for (int i = 0; i < matrix.Length; i++)
      {
        for (int j = 0; j < matrix[i].Length; j++)
        {
          if (i == 35 && j == 3)
          {
          }

          HelperPacific(i, j, matrix, pacificFlag);

          HelperAtlantic(matrix.Length - 1 - i,
            matrix[i].Length - 1 - j, matrix, atlanticFlag);
        }
      }

      for (int i = 0; i < matrix.Length; i++)
      {
        for (int j = 0; j < matrix[i].Length; j++)
        {
          if (pacificFlag[i][j] && atlanticFlag[i][j]) res.Add(new[] {i, j});
        }
      }

      Console.WriteLine(JsonConvert.SerializeObject(matrix));
      Console.WriteLine(JsonConvert.SerializeObject(pacificFlag));
      Console.WriteLine(JsonConvert.SerializeObject(atlanticFlag));
      Console.WriteLine(JsonConvert.SerializeObject(res));

      return res;
    }

    [Obsolete("bug")]
    public void HelperPacific(int i, int j, bool[][] arr, int[][] matrix)
    {
      if (i == 0 || j == 0 || arr[i][j]) return;

      if (i + 1 < matrix.Length && arr[i + 1][j] && matrix[i][j] >= matrix[i + 1][j])
      {
        arr[i][j] = true;
        HelperPacific(i - 1, j, arr, matrix);
        HelperPacific(i, j - 1, arr, matrix);
      }

      if (j + 1 < matrix[0].Length && arr[i][j + 1] && matrix[i][j] >= matrix[i][j + 1])
      {
        arr[i][j] = true;
        HelperPacific(i - 1, j, arr, matrix);
        HelperPacific(i, j - 1, arr, matrix);
      }
    }

    [Obsolete("bug")]
    public void HelperAtlantic(int i, int j, bool[][] arr, int[][] matrix)
    {
      if (i == matrix.Length || j == matrix[0].Length || arr[i][j]) return;


      if (i > 0 && arr[i - 1][j] && matrix[i][j] >= matrix[i - 1][j])
      {
        arr[i][j] = true;
        HelperAtlantic(i + 1, j, arr, matrix);
        HelperAtlantic(i, j + 1, arr, matrix);
      }

      if (j > 0 && arr[i][j - 1] && matrix[i][j] >= matrix[i][j - 1])
      {
        arr[i][j] = true;
        HelperAtlantic(i + 1, j, arr, matrix);
        HelperAtlantic(i, j + 1, arr, matrix);
      }
    }

    //can flow pacific
    [Obsolete("bug")]
    public void HelperPacific(int i, int j, int[][] matrix, bool[][] arr)
    {
      if (i == 0 || j == 0)
      {
        arr[i][j] = true;
        return;
      }

      bool compareTop = matrix[i - 1][j] <= matrix[i][j], compareLeft = matrix[i][j - 1] <= matrix[i][j];

      if (compareTop)
      {
        if (arr[i - 1][j])
        {
          arr[i][j] = true;
          HelperPacific(i, j - 1, arr, matrix);
        }
      }

      if (compareLeft)
      {
        if (arr[i][j - 1])
        {
          arr[i][j] = true;
          HelperPacific(i - 1, j, arr, matrix);
        }
      }
    }

    //can flow pacific
    [Obsolete("bug")]
    public void HelperAtlantic(int i, int j, int[][] matrix, bool[][] arr)
    {
      if (i == matrix.Length - 1 || j == matrix[0].Length - 1)
      {
        arr[i][j] = true;
        return;
      }

      bool compareBottom = matrix[i + 1][j] <= matrix[i][j], compareRight = matrix[i][j + 1] <= matrix[i][j];

      if (compareBottom)
      {
        if (arr[i + 1][j])
        {
          arr[i][j] = true;
          HelperAtlantic(i, j + 1, arr, matrix);
        }
      }

      if (compareRight)
      {
        if (arr[i][j + 1])
        {
          arr[i][j] = true;
          HelperAtlantic(i + 1, j, arr, matrix);
        }
      }
    }

    [Obsolete("bug")]
    public IList<int[]> Try(int[][] matrix)
    {
      IList<int[]> res = new List<int[]>();

      bool[][] visited = new bool[matrix.Length][];

      for (int i = 0; i < visited.Length; i++)
      {
        visited[i] = new bool[matrix[0].Length];
      }

      for (int i = 0; i < matrix.Length; i++)
      {
        for (int j = 0; j < matrix[i].Length; j++)
        {
          Helper(i, j, res, matrix);
        }
      }

      return res;
    }

    [Obsolete("bug")]
    public void Helper(int i, int j, IList<int[]> res, int[][] matrix)
    {
      if (CanFlowAtlantic(int.MaxValue, i, j, matrix) && CanFlowPacific(int.MaxValue, i, j, matrix))
      {
        res.Add(new[] {i, j});
      }
    }

    /// <summary>
    /// 可以流向太平洋 ← ↑
    /// </summary>
    [Obsolete("bug")]
    public bool CanFlowPacific(int prevNum, int i, int j, int[][] matrix)
    {
      if (i == -1 || j == -1) return true;
      if (i == matrix.Length || j == matrix[0].Length) return false;

      if (matrix[i][j] <= prevNum)
      {
        return CanFlowPacific(matrix[i][j], i - 1, j, matrix) ||
               CanFlowPacific(matrix[i][j], i, j - 1, matrix) ||
               CanFlowPacific(matrix[i][j], i + 1, j, matrix) ||
               CanFlowPacific(matrix[i][j], i, j + 1, matrix);
      }

      return false;
    }

    /// <summary>
    /// 可以流向大西洋  → ↓
    /// </summary>
    [Obsolete("bug")]
    public bool CanFlowAtlantic(int prevNum, int i, int j, int[][] matrix)
    {
      if (i == matrix.Length || j == matrix[0].Length) return true;
      if (i == -1 || j == -1) return false;

      if (matrix[i][j] <= prevNum)
      {
        return CanFlowAtlantic(matrix[i][j], i - 1, j, matrix) ||
               CanFlowAtlantic(matrix[i][j], i, j - 1, matrix) ||
               CanFlowAtlantic(matrix[i][j], i + 1, j, matrix) ||
               CanFlowAtlantic(matrix[i][j], i, j + 1, matrix);
      }

      return false;
    }
  }
}