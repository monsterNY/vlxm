using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : MincostTickets  怎么买票最划算。，
  /// @author :mons
  /// @create : 2019/3/27 16:00:04 
  /// @source : https://leetcode.com/problems/minimum-cost-for-tickets/
  /// </summary>
  [Obsolete("解决一题，放弃一题 有毒。。。")]
  public class MincostTickets
  {

    /**
     * dp??
     *
     * I don't understand
     *
     */
    #region office

    int[] costs;
    int[] memo;
    ISet<int> dayset;

    public int mincostTickets(int[] days, int[] costs)
    {
      this.costs = costs;
      memo = new int[366];
      dayset = new HashSet<int>();
      foreach (int d in days) dayset.Add(d);

      return dp(1);
    }

    public int dp(int i)
    {
      if (i > 365)
        return 0;
      if (memo[i] != 0)
        return memo[i];

      int ans;
      if (dayset.Contains(i))
      {
        ans = Math.Min(dp(i + 1) + costs[0],
          dp(i + 7) + costs[1]);
        ans = Math.Min(ans, dp(i + 30) + costs[2]);
      }
      else
      {
        ans = dp(i + 1);
      }

      memo[i] = ans;
      return ans;
    }

    #endregion
  }
}