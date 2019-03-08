using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ConsoleTest.LeetCode
{
  /// <summary>
  /// @source:https://leetcode.com/problems/x-of-a-kind-in-a-deck-of-cards/
  /// </summary>
  public class HasGroupsSizeX
  {
    /**
     * Runtime: 112 ms, faster than 97.92% of C# online submissions for X of a Kind in a Deck of Cards.
     * Memory Usage: 26.5 MB, less than 72.73% of C# online submissions for X of a Kind in a Deck of Cards.
     *
     * Unbelievable
     *
     */
    public bool Solution(int[] deck)
    {
      if (deck.Length < 2)
        return false;

      var dictionary = new Dictionary<int, int>();

      for (var i = 0; i < deck.Length; i++)
        if (dictionary.ContainsKey(deck[i]))
          dictionary[deck[i]]++;
        else
          dictionary.Add(deck[i], 1);

      Console.WriteLine(JsonConvert.SerializeObject(dictionary));

      var num = -1;

      foreach (var item in dictionary.Values)
      {
        if (item < 2)
          return false;
        if (num == -1)
          num = item;
        else if (item != num && GCD(item, num) == 1)
          return false;
      }

      return true;
    }
    
    /// <summary>
    /// 获取最小公倍数
    /// </summary>
    /// <param name="m"></param>
    /// <param name="n"></param>
    /// <returns></returns>
    private static int GCD(int m, int n)
    {
      int r, t;
      if (m < n)
      {
        t = n;
        n = m;
        m = t;
      }

      while (n != 0)
      {
        r = m % n;
        m = n;
        n = r;
      }

      return m;
    }

    #region OtherSolution

    public bool hasGroupsSizeX(int[] deck)
    {
      var dictionary = new Dictionary<int, int>();

      for (var i = 0; i < deck.Length; i++)
        if (dictionary.ContainsKey(deck[i]))
          dictionary[deck[i]]++;
        else
          dictionary.Add(deck[i], 1);

      int res = 0;
      foreach (int i in dictionary.Values) res = gcd(i, res);
      return res > 1;
    }

    public int gcd(int a, int b)
    {
      return b > 0 ? gcd(b, a % b) : a;
    }

    #endregion
  }
}