using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : FindRedundantConnection  
  /// @author :mons
  /// @create : 2019/4/2 14:45:42 
  /// @source : https://leetcode.com/problems/redundant-connection/
  /// </summary>
  [Obsolete("no imagination - union find")]
  public class FindRedundantConnection
  {


    /**
     * Runtime: 248 ms, faster than 100.00% of C# online submissions for Redundant Connection.
     * Memory Usage: 29.7 MB, less than 68.75% of C# online submissions for Redundant Connection.
     */
    public int[] OtherSolution(int[][] edges)
    {
      int[] parent = new int[2001];
      for (int i = 0; i < parent.Length; i++) parent[i] = i;

      foreach (int[] edge in edges)
      {
        int f = edge[0], t = edge[1];
        if (find(parent, f) == find(parent, t)) return edge;
        else parent[find(parent, f)] = find(parent, t);
      }

      return new int[2];
    }

    private int find(int[] parent, int f)
    {
      if (f != parent[f])
      {
        parent[f] = find(parent, parent[f]);
      }
      return parent[f];
    }

  }
}