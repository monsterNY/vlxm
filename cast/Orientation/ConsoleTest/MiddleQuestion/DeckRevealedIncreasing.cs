using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : DeckRevealedIncreasing  
  /// @author :mons
  /// @create : 2019/3/20 17:59:20 
  /// @source : https://leetcode.com/problems/reveal-cards-in-increasing-order/
  /// </summary>
  public class DeckRevealedIncreasing
  {
    /// <summary>
    /// 暴力破解
    /// </summary>
    /// <param name="deck"></param>
    /// <returns></returns>
    /**
     *
     * Runtime: 248 ms, faster than 99.03% of C# online submissions for Reveal Cards In Increasing Order.
     * Memory Usage: 29.6 MB, less than 87.50% of C# online submissions for Reveal Cards In Increasing Order.
     * 要不要这样搞我。。。
     *
     */
    public int[] Simple(int[] deck)
    {
      List<int> list = new List<int>();

      Array.Sort(deck);

      for (int i = deck.Length - 1; i >= 0; i--)
      {
        if (list.Count > 1)
        {
          list.Insert(0, list[list.Count - 1]);
          list.RemoveAt(list.Count - 1);
        }

        list.Insert(0, deck[i]);
      }

      return list.ToArray();
    }

    /// <summary>
    /// non bu chu lai =-=
    /// </summary>
    /// <param name="deck"></param>
    /// <returns></returns>
    [Obsolete]
    public int[] Solution(int[] deck)
    {
      Array.Sort(deck);

      var result = new int[deck.Length];

      var start = 0;

      for (var i = 0; i <= deck.Length - 1; i += 2)
        result[i] = deck[start++];

      InArr(deck, start, deck.Length - 1, result, 1, 2, deck.Length);

      return result;
    }

    public void InArr(int[] source, int start, int end, int[] target, int index, int stepAdd, int count)
    {
      if (start > end) return;
      if (start == end)
      {
        target[index] = source[start];
        return;
      }

      var flag = count % 2 == 0;

      if (flag)
      {
        for (var i = index; i <= source.Length - 1; i += stepAdd * 2)
          target[i] = source[start++];
      }
      else
      {
        for (var i = index; i <= source.Length - 1; i += stepAdd * 2)
          target[(source.Length - i - 1)] = source[end--];
      }


      InArr(source, start, end, target, index + stepAdd, stepAdd * 2, count / 2);
    }

    public void Check(int[] result)
    {
      var sortArr = new int[result.Length];

      Array.Copy(result, sortArr, result.Length);

      Array.Sort(sortArr);

      var list = new List<int>(result);

      var showList = new List<int>();

      while (list.Count > 0)
      {
        showList.Add(list[0]);
        list.RemoveAt(0);

        if (list.Count > 0)
        {
          list.Add(list[0]);
          list.RemoveAt(0);
        }
        else
        {
          break;
        }
      }

      if (showList.Count != sortArr.Length) throw new Exception("error");

      for (var i = 0; i < sortArr.Length; i++)
        if (showList[i] != sortArr[i])
          throw new Exception("error");
    }

    public void NotUnderstand(int[] deck)
    {
      Array.Sort(deck);

      var result = new int[deck.Length];

      #region error

      var remainder = deck.Length % 2;

      var start = deck.Length / 2 + remainder;
      if (remainder == 1 && deck.Length > start + 1) start += remainder;

      for (var i = 1; i < result.Length; i += 4, start += remainder == 0 || start + 2 >= result.Length ? 1 : 2)
        result[i] = deck[start];

      if (remainder == 1)
        start = deck.Length / 2 + remainder;
      for (var i = 3; i < result.Length; i += 4, start += 1 + remainder) result[i] = deck[start];

      #endregion

      var flag = result.Length % 2;
      if (flag == 0)
      {
        var up = (deck.Length - 1) / 2;
        for (var i = 1; i < result.Length; i += 4) //
          deck[i] = deck[up++];

        for (var i = 3; i < result.Length; i += 4) deck[i] = deck[up++];
      }
      else
      {
        var up = (deck.Length - 1) / 4 + (deck.Length - 1) / 2 % 2;
        var right = deck.Length - 1;
        for (var i = 1; i < result.Length; i += 4) //
          deck[i] = deck[up--];

        for (var i = 3; i < result.Length; i += 4) deck[i] = deck[right--];
      }
    }
  }
}