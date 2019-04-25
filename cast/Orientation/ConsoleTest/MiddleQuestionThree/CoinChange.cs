using System;
using System.Collections.Generic;
using System.Text;
using Tools.CusAttr;
using Tools.CusMenu;

namespace ConsoleTest.MiddleQuestionThree
{
  /// <summary>
  /// @desc : CoinChange  
  /// @author : mons
  /// @create : 2019/4/25 16:23:18 
  /// @source : https://leetcode.com/problems/coin-change/
  /// </summary>
  [Obsolete]
  [Question(QuestionTypes.Dp)]
  public class CoinChange
  {
    public int Simple(int[] coins, int amount)
    {
      var count = 0;

      Array.Sort(coins);

      for (int i = coins.Length - 1; i >= 0 && amount > 0; i--)
      {
        if (coins[i] <= amount)
        {
          count += amount / coins[i];
          amount %= coins[i];
        }
      }

      return count;
    }
  }
}