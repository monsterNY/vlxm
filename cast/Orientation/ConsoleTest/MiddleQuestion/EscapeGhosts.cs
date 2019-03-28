using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : EscapeGhosts  
  /// @author :mons
  /// @create : 2019/3/28 17:13:19 
  /// @source : https://leetcode.com/problems/escape-the-ghosts/
  /// </summary>
  public class EscapeGhosts
  {
    /**
     *
     * Runtime: 96 ms, faster than 100.00% of C# online submissions for Escape The Ghosts.
     * Memory Usage: 23.2 MB, less than 50.00% of C# online submissions for Escape The Ghosts.
     *
     * so simple
     *
     */
    public bool Solution(int[][] ghosts, int[] target)
    {
      var step = Math.Abs(target[0]) + Math.Abs(target[1]);


      for (int i = 0; i < ghosts.Length; i++)
      {
        if (Math.Abs(ghosts[i][0] - target[0]) + Math.Abs(ghosts[i][1] - target[1]) <= step)
          return false;
      }

      return true;
    }
  }
}