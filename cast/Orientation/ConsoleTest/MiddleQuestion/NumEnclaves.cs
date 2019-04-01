using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Newtonsoft.Json;
using Tools.CusAttr;
using Tools.CusMenu;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : NumEnclaves  
  /// @author :mons
  /// @create : 2019/4/1 9:57:51 
  /// @source : https://leetcode.com/problems/number-of-enclaves/
  /// </summary>
  [Description("DFS")]
  [Love(LoveTypes.Skill)]
  public class NumEnclaves
  {
    /**
     * Runtime: 156 ms, faster than 100.00% of C# online submissions for Number of Enclaves.
     * Memory Usage: 29.6 MB, less than 100.00% of C# online submissions for Number of Enclaves.
     */
    public int Solution(int[][] A)
    {
      var count = 0;

      for (int i = 0; i < A[0].Length; i++)
      {
        Dfs(A, 0, i); //第一行
        Dfs(A, A.Length - 1, i); //最后一行
      }

      for (int i = 0; i < A.Length; i++)
      {
        Dfs(A, i, 0); //第一列
        Dfs(A, i, A[0].Length - 1); //最后一列
      }

      for (int i = 0; i < A.Length; i++)
      {
        for (int j = 0; j < A[i].Length; j++)
        {
          if (A[i][j] == 1) count++;
        }
      }

      Console.WriteLine(JsonConvert.SerializeObject(A));

      return count;
    }

    public void Dfs(int[][] arr, int i, int j)
    {
      if (i < 0 || i >= arr.Length || j < 0 || j >= arr[i].Length || arr[i][j] == 0)
        return;

      arr[i][j] = 0;

      Dfs(arr, i, j + 1);//find right
      Dfs(arr, i, j - 1);//find left
      Dfs(arr, i + 1, j);//find top
      Dfs(arr, i - 1, j);//find bottom
    }
  }
}