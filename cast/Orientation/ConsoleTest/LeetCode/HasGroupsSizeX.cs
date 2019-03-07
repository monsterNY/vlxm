using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.LeetCode
{
  /// <summary>
  /// @source:https://leetcode.com/problems/x-of-a-kind-in-a-deck-of-cards/
  /// </summary>
  public class HasGroupsSizeX
  {
    public bool Solution(int[] deck)
    {
      if (deck.Length < 2)
        return false;

      Dictionary<int, int> dictionary = new Dictionary<int, int>();

      for (int i = 0; i < deck.Length; i++)
      {
        if (dictionary.ContainsKey(deck[i]))
          dictionary[deck[i]]++;
        else
          dictionary.Add(deck[i], 1);
      }

      var num = -1;
      foreach (var item in dictionary.Values)
      {
        if (item < 2)
          return false;
        if (num == -1)
          num = item;
        else if (num != item && num % item != 0 && item % num != 0)
          return false;
      }

      return true;
    }
  }
}