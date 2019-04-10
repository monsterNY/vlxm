using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : Change  
  /// @author :mons
  /// @create : 2019/4/10 9:44:41 
  /// @source : https://leetcode.com/problems/coin-change-2/
  /// </summary>
  [Obsolete("I'm rookie,ha ha ~~~")]
  public class Change
  {
    public int Solution(int amount, int[] coins)
    {
      if (amount == 0) return 1;

      var res = 0;
      for (var i = 0; i < coins.Length; i++)
        if (amount >= coins[i])
          res += Try(amount - coins[i], coins);

      return res;
    }

    public void AddToMap(Dictionary<int, int> map, int index, int[] maxArr)
    {
      if (map.ContainsKey(index))
        map[index]++;
      else map.Add(index, 1);

      if (maxArr[index] < map[index]) maxArr[index] = map[index];

    }

    //bug has sort
    public int Try(int amount, int[] coins)
    {
      if (amount == 0) return 1;

      var res = 0;
      for (var i = 0; i < coins.Length; i++)
        if (amount >= coins[i])
          res += Try(amount - coins[i], coins);

      return res;
    }
  }
}