using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : MaxProfit  
  /// @author :mons
  /// @create : 2019/4/8 13:38:33 
  /// @source : https://leetcode.com/problems/best-time-to-buy-and-sell-stock-with-cooldown/
  /// </summary>
  public class MaxProfit
  {
    /**
     * Runtime: 124 ms, faster than 8.77% of C# online submissions for Best Time to Buy and Sell Stock with Cooldown.
     * Memory Usage: 22.3 MB, less than 50.00% of C# online submissions for Best Time to Buy and Sell Stock with Cooldown.
     *
     * anyway,I fix this~~~~
     *
     */
    public int Solution(int[] prices)
    {
      var dp = new int[prices.Length + 3];

      for (int i = prices.Length - 2; i >= 0; i--)
      {
        for (int j = i + 1; j < prices.Length; j++)
        {
          dp[i] = Math.Max(dp[i], prices[j] - prices[i] + dp[j + 2]);
          dp[i] = Math.Max(dp[i], dp[j]);
        }
      }

      return dp[0];
    }

    /**
     * Runtime: 104 ms, faster than 38.60% of C# online submissions for Best Time to Buy and Sell Stock with Cooldown.
     * Memory Usage: 22.2 MB, less than 50.00% of C# online submissions for Best Time to Buy and Sell Stock with Cooldown.
     *
     * 是测试坏了？？？
     *
     */
    #region otherSolution

    public int maxProfit(int[] prices)
    {
      int sell = 0, prev_sell = 0, buy = Int32.MinValue, prev_buy;
      foreach (int price in prices)
      {
        prev_buy = buy;
        buy = Math.Max(prev_sell - price, prev_buy);
        prev_sell = sell;
        sell = Math.Max(prev_buy + price, prev_sell);
      }

      return sell;
    }

    /**
     * Runtime: 92 ms, faster than 68.42% of C# online submissions for Best Time to Buy and Sell Stock with Cooldown.
     * Memory Usage: 22.2 MB, less than 50.00% of C# online submissions for Best Time to Buy and Sell Stock with Cooldown.
     */
    public int OtherSolution(int[] prices)
    {
      if (prices.Length < 2) return 0;
      int buy = -prices[0], sell = 0, cooldown = 0;
      for (int i = 1; i < prices.Length; i++)
      {
        int temp = buy;
        buy = Math.Max(buy, cooldown - prices[i]);
        cooldown = Math.Max(sell, cooldown);
        sell = Math.Max(sell, temp + prices[i]);
      }
      return sell > cooldown ? sell : cooldown;
    }

    #endregion
  }
}