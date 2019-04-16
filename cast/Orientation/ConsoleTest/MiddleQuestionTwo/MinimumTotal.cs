using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tools.CusAttr;
using Tools.CusMenu;

namespace ConsoleTest.MiddleQuestionTwo
{
  /// <summary>
  /// @desc : MinimumTotal  
  /// @author :mons
  /// @create : 2019/4/16 14:43:37 
  /// @source : https://leetcode.com/problems/triangle/
  /// </summary>
  [Love(LoveTypes.Fix)]
  public class MinimumTotal
  {
    public int Solution(IList<IList<int>> triangle)
    {
      for (int i = 1; i < triangle.Count; ++i)
      for (int j = 0; j < triangle[i].Count; ++j)
      {
        triangle[i][j] += Math.Min(
          triangle[i - 1][Math.Max(j - 1, 0)],
          triangle[i - 1][Math.Min(j, triangle[i - 1].Count - 1)]
        );
      }

      return triangle[triangle.Count - 1].Min();
    }

    /**
     * Runtime: 96 ms, faster than 100.00% of C# online submissions for Triangle.
     * Memory Usage: 22.3 MB, less than 46.43% of C# online submissions for Triangle.
     *
     * nice~
     *
     */
    public int Solution2(IList<IList<int>> triangle)
    {
      int min;
      for (int i = 1; i < triangle.Count; ++i)
      for (int j = 0; j < triangle[i].Count; ++j)
      {
        min = int.MaxValue;

        for (int k = Math.Max(0, j - 1); k <= Math.Min(triangle[i - 1].Count - 1, j); k++)
          if (triangle[i - 1][k] < min)
            min = triangle[i - 1][k];

        triangle[i][j] += min;
      }

      min = triangle[triangle.Count - 1][0];

      for (int i = 1; i < triangle[triangle.Count - 1].Count; i++)
      {
        if (triangle[triangle.Count - 1][i] < min) min = triangle[triangle.Count - 1][i];
      }

      return min;
    }

    public int Solution3(IList<IList<int>> triangle)
    {
      int min, res = int.MaxValue;
      for (int i = 1; i < triangle.Count; ++i)
      for (int j = 0; j < triangle[i].Count; ++j)
      {
        min = int.MaxValue;

        for (int k = Math.Max(0, j - 1); k <= Math.Min(triangle[i - 1].Count - 1, j); k++)
          if (triangle[i - 1][k] < min)
            min = triangle[i - 1][k];

        triangle[i][j] += min;
        if (i == triangle.Count - 1 && triangle[i][j] < res) res = triangle[i][j];
      }

      return res;
    }
  }
}