using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : ReconstructQueue  
  /// @author :mons
  /// @create : 2019/3/26 13:54:41 
  /// @source : https://leetcode.com/problems/queue-reconstruction-by-height/
  /// </summary>
  public class ReconstructQueue
  {
    /**
     *
     * Runtime: 296 ms, faster than 5.88% of C# online submissions for Queue Reconstruction by Height.
     * Memory Usage: 32.3 MB, less than 10.00% of C# online submissions for Queue Reconstruction by Height.
     *
     */
    public int[][] Solution(int[][] people)
    {
      int[][] result = new int[people.Length][];

      var max = 0;
      var min = 0;

      foreach (var item in people.OrderBy(u => u[0]))
      {
        max = item[1];
      }

      return result;
    }

    /**
     *
     * Runtime: 292 ms, faster than 5.88% of C# online submissions for Queue Reconstruction by Height.
     * Memory Usage: 32.4 MB, less than 10.00% of C# online submissions for Queue Reconstruction by Height.
     *
     * =-= what??? 差不多的就没必要了。。
     *
     */
    [Obsolete]
    public int[][] OtherSolution(int[][] people)
    {
      int[][] result = new int[people.Length][];

      Array.Sort(people, ((a, b) => a[0] == b[0] ? a[1] - b[1] : b[0] - a[0]));
      for (int i = 0; i < people.Length; i++)
      {
        int pos = people[i][1];
        for (int j = i; j > pos; j--)
        {
          result[j] = result[j - 1];
        }

        result[pos] = people[i];
      }

      return result;
    }

    /**
     * java:
     * Runtime: 7 ms, faster than 97.21% of Java online submissions for Queue Reconstruction by Height.
     * Memory Usage: 46.3 MB, less than 34.45% of Java online submissions for Queue Reconstruction by Height.
     *
     * C#:
     * Runtime: 280 ms, faster than 5.88% of C# online submissions for Queue Reconstruction by Height.
     * Memory Usage: 33.2 MB, less than 10.00% of C# online submissions for Queue Reconstruction by Height.
     *
     * 有毒吧。
     *
     */
    public int[][] OtherSolution2(int[][] people)
    {
      if (people == null || people.Length == 0 || people[0].Length == 0)
        return new int[0][];

      Array.Sort(people, (a, b) =>
        b[0] == a[0] ? a[1] - b[1] : b[0] - a[0]
      );

      int n = people.Length;
      List<int[]> tmp = new List<int[]>();
      int i;
      for (i = 0; i < n; i++)
        tmp.Insert(people[i][1], new int[]
        {
          people[i][0], people[i][1]
        });

      int[][] res = new int[people.Length][];
      Array.Fill(res,new int[2]);
      i = 0;
      foreach (int[] k in tmp)
      {
        res[i][0] = k[0];
        res[i++][1] = k[1];
      }

      return res;
    }

    /**
     * 
     * Runtime: 300 ms, faster than 5.88% of C# online submissions for Queue Reconstruction by Height.
     * Memory Usage: 32.3 MB, less than 10.00% of C# online submissions for Queue Reconstruction by Height.
     *
     */
    public int[][] Simple(int[][] people)
    {
      int[][] result = new int[people.Length][];

      Array.Sort(people, ((num1, num2) => num1[0] - num2[0]));

      for (var i = 0; i < people.Length; i++)
      {
        var item = people[i];
        var count = 0;

        for (int j = 0; j < result.Length; j++)
        {
          if (result[j] != null && result[j][0] < item[0]) continue;

          if (count == item[1])
          {
            result[j] = people[i];
            break;
          }

          count++;
        }
      }

      return result;
    }
  }
}