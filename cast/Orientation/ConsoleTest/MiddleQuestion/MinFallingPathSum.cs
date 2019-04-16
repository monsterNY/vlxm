using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tools.CusAttr;
using Tools.CusMenu;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : MinFallingPathSum  
  /// @author :mons
  /// @create : 2019/3/27 13:55:15 
  /// @source : https://leetcode.com/problems/minimum-falling-path-sum/
  /// </summary>
  [Obsolete("no solution,ha ha , is my technology limit~")]
  [Love(LoveTypes.Fix,LoveTypes.Question)]
  public class MinFallingPathSum
  {
    /**
     *
     * @source:https://leetcode.com/problems/minimum-falling-path-sum/discuss/186666/C%2B%2BJava-4-lines-DP
     *
     * [1, 2, 3]
     * [4, 5, 6] => [5, 6, 8]
     * [7, 8, 9] => [7, 8, 9] => [12, 13, 15]
     *
     * 神奇。，
     *
     */
    public int OtherSolution(int[][] A)
    {
      for (int i = 1; i < A.Length; ++i)
      for (int j = 0; j < A.Length; ++j)
        A[i][j] += Math.Min(A[i - 1][j],
          Math.Min(A[i - 1][Math.Min(0, j - 1)], A[i - 1][Math.Min(A.Length - 1, j + 1)]));
      return A[A.Length - 1].Min();
    }

    public int Solution(int[][] A)
    {
      minNum = int.MaxValue;

      for (int i = 0; i < A[0].Length; i++)
      {
        GetMax(A, 0, i, 0);
      }

      return minNum;
    }

    private int minNum;

    public void GetMax(int[][] arr, int lineIndex, int colIndex, int num)
    {
      if (colIndex >= arr[0].Length || colIndex < 0) return;

      if (lineIndex == arr.Length - 1)
      {
        if (num + arr[lineIndex][colIndex] < minNum)
        {
          minNum = num + arr[lineIndex][colIndex];
        }

        return;
      }

      GetMax(arr, lineIndex + 1, colIndex - 1, num + arr[lineIndex][colIndex]);
      GetMax(arr, lineIndex + 1, colIndex, num + arr[lineIndex][colIndex]);
      GetMax(arr, lineIndex + 1, colIndex + 1, num + arr[lineIndex][colIndex]);
    }

    public int Optimize(int[][] A)
    {
      minNum = int.MaxValue;

      minArr = new int[A.Length][];

      for (int i = 0; i < A[0].Length; i++)
      {
        GetMin2(A, 0, i, 0);
      }

      return minNum;
    }

    int[][] minArr;

    public int GetMin2(int[][] arr, int lineIndex, int colIndex, int num)
    {
      if (colIndex >= arr[0].Length || colIndex < 0 || lineIndex >= arr.Length) return num;
      int min = GetMin2(arr, lineIndex + 1, colIndex - 1, num), item;
      for (int i = colIndex - 1; i <= colIndex + 1; i++)
      {
        if ((item = GetMin2(arr, lineIndex + 1, i, num)) > min)
        {
          min = item;
        }
      }

      return min;
    }
  }
}