using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestionTwo
{
  /// <summary>
  /// @desc : Flipgame  
  /// @author :mons
  /// @create : 2019/4/12 11:20:04 
  /// @source : https://leetcode.com/problems/card-flipping-game/
  /// </summary>
  [Obsolete("no understand")]
  public class Flipgame
  {
    public int Solution(int[] fronts, int[] backs)
    {
      ISet<int> set = new HashSet<int>(),set2 = new HashSet<int>();

      for (int i = 0; i < fronts.Length; i++)
      {
        set.Add(fronts[i]);

        set2.Add(backs[i]);

      }

      var min = 0;

      foreach (var item in set)
      {
        if(!set2.Contains(item))
          if (min == 0 || min > item)
            min = item;
      }

      foreach (var item in set2)
      {
        if (!set.Contains(item))
          if (min == 0 || min > item)
            min = item;
      }

      return min;
    }

    //no understand???
    public int Try(int[] fronts, int[] backs)
    {
      Dictionary<int, int> dictionary = new Dictionary<int, int>();

      for (int i = 0; i < fronts.Length; i++)
      {
        if (dictionary.ContainsKey(fronts[i])) dictionary[fronts[i]]++;
        else dictionary.Add(fronts[i], 1);
      }

      var min = 0;

      for (int i = 0; i < fronts.Length; i++)
      {
        if (fronts[i] == backs[i]) continue;

        if (dictionary[fronts[i]] == 1)
          if (min == 0 || min > fronts[i])
            min = fronts[i];
      }

      return min;
    }
  }
}