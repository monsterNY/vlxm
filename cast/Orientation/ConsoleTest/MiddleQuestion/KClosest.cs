using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : KClosest  
  /// @author :mons
  /// @create : 2019/3/22 17:03:55 
  /// @source : https://leetcode.com/problems/k-closest-points-to-origin/
  /// </summary>
  public class KClosest
  {

    /**
     * Runtime: 508 ms, faster than 82.60% of C# online submissions for K Closest Points to Origin.
     * Memory Usage: 46.8 MB, less than 68.89% of C# online submissions for K Closest Points to Origin.
     *
     * ???.,
     *
     */
    public int[][] Solution(int[][] points, int K)
    {

      Dictionary<int,int> dictionary = new Dictionary<int, int>();

      for (int i = 0; i < points.Length; i++)
      {
        dictionary.Add(i,(points[i][0] * points[i][0] + points[i][1] * points[i][1])); 
      }

      int [][]result = new int[K][];

      int index = 0;

      foreach (var item in dictionary.OrderBy(u=>u.Value))
      {
        result[index++] = points[item.Key];
        if (index == K) break;
      }

      return result;

    }

    /**
     * Runtime: 488 ms, faster than 93.14% of C# online submissions for K Closest Points to Origin.
     * Memory Usage: 46.2 MB, less than 100.00% of C# online submissions for K Closest Points to Origin.
     *
     * 直接使用 ef
     *
     * 要是给我用官方的话，基本秒解  ha ha yeah
     *
     */
    public int[][] OtherSolution(int[][] points, int K)
    {

      return points.OrderBy(u => u[0] * u[0] + u[1] * u[1]).Take(K).ToArray();

    }

  }
}
