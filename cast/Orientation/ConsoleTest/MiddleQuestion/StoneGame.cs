using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : StoneGame  
  /// @author :monster_yj
  /// @create : 2019/3/23 16:15:09 
  /// @source : https://leetcode.com/problems/stone-game/
  /// </summary>
  public class StoneGame
  {
    public bool Solution(int[] piles)
    {
      return IsWin(0, 0, piles, 0, piles.Length - 1);
    }

    public bool IsWin(int alexNum, int leeNum, int[] piles, int start, int end)
    {
      if (start == end)
      {
        return alexNum > leeNum;
      }

      if (end - start > 1)
      {
        var cloneStart = start;
        var cloneEnd = end;

        return IsWin(alexNum + piles[start++], leeNum + piles[start] > piles[end] ? piles[start++] : piles[end--],
                 piles, start, end)
               || IsWin(alexNum + piles[cloneEnd--],
                 leeNum + piles[cloneStart] > piles[cloneEnd] ? piles[cloneStart++] : piles[cloneEnd--], piles,
                 cloneStart, cloneEnd);
      }
      else
      {
        alexNum += piles[start] > piles[end] ? piles[start++] : piles[end--];
        leeNum += piles[start] > piles[end] ? piles[start] : piles[end];
        return alexNum > leeNum;
      }
    }
  }
}