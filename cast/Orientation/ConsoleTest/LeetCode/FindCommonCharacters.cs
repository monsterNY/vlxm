using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleTest.LeetCode
{
  /// <summary>
  /// @source:https://leetcode.com/problems/find-common-characters/
  /// </summary>
  public class FindCommonCharacters
  {
    public IList<string> CommonChars(string[] a)
    {
      Dictionary<char, int> dictionary = new Dictionary<char, int>();

      Dictionary<char, int> rivalDictionary;

      List<string> resultList = new List<string>();

      foreach (var subItem in a[0])
      {
        if (dictionary.ContainsKey(subItem))
          dictionary[subItem]++;
        else
          dictionary.Add(subItem, 1);
      }

      for (int i = 1; i < a.Length; i++)
      {
        rivalDictionary = new Dictionary<char, int>();
        foreach (var item in a[i])
        {
          if (dictionary.ContainsKey(item))
          {
            if (rivalDictionary.ContainsKey(item))
              rivalDictionary[item]++;
            else
              rivalDictionary.Add(item, 1);
          }
        }

        foreach (var key in dictionary.Keys.ToArray())
        {
          if (rivalDictionary.ContainsKey(key))
          {
            dictionary[key] = dictionary[key] > rivalDictionary[key] ? rivalDictionary[key] : dictionary[key];
          }
          else
          {
            dictionary.Remove(key);
          }
        }

        if (dictionary.Count == 0)
          return new List<string>();
      }

      foreach (var key in dictionary.Keys)
      {
        for (int i = 0; i < dictionary[key]; i++)
        {
          resultList.Add(key.ToString());
        }
      }

      return resultList;
    }

    public IList<string> Optimize(string[] a)
    {
      Dictionary<char, int> dictionary = new Dictionary<char, int>();

      Dictionary<char, int> rivalDictionary;

      List<string> resultList = new List<string>();

      foreach (var subItem in a[0])
        if (dictionary.ContainsKey(subItem))
          dictionary[subItem]++;
        else
          dictionary.Add(subItem, 1);

      for (int i = 1; i < a.Length; i++)
      {
        rivalDictionary = new Dictionary<char, int>();
        foreach (var item in a[i])
          if (dictionary.ContainsKey(item))
            if (rivalDictionary.ContainsKey(item))
              rivalDictionary[item]++;
            else
              rivalDictionary.Add(item, 1);

        foreach (var key in dictionary.Keys.ToArray())
          if (rivalDictionary.ContainsKey(key))
            dictionary[key] = dictionary[key] > rivalDictionary[key] ? rivalDictionary[key] : dictionary[key];
          else
            dictionary.Remove(key);

        if (dictionary.Count == 0)
          return new List<string>();
      }

      foreach (var key in dictionary.Keys)
        for (int i = 0; i < dictionary[key]; i++)
          resultList.Add(key.ToString());

      return resultList;
    }

    public List<String> OtherSolution(String[] A)
    {
      List<string> resultList = new List<string>();
      int[] arr = new int[26];
      Array.Fill(arr, int.MaxValue);

        foreach (var item in A)
      {
        var itemArr = new int[26];

        foreach (var c in item)
        {
          ++itemArr[c - 'a'];
        }

        for (int i = 0; i < 26; i++)
        {
          arr[i] = Math.Min(itemArr[i], arr[i]);
        }
      }

      for (int i = 0; i < 26; i++)
      {
        while (arr[i]-- > 0)
        {
          resultList.Add("" + (char) (i + 'a'));
        }
      }

      return resultList;
    }
  }
}