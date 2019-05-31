using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ConsoleTest.HardQuestion
{
  /// <summary>
  /// @desc : FindSecretWord  
  /// @author : mons
  /// @create : 2019/5/31 17:00:59 
  /// @source : https://leetcode.com/problems/guess-the-word/
  /// </summary>
  [Obsolete]
  public class FindSecretWord
  {
    //bug 
    public void Try(string[] wordlist, Master master)
    {
      var visited = new bool[wordlist.Length];

      int guess = 0, same = 0, len = 6, diff, use = 1; //保存最后一次猜测的相同数和下标

      for (int i = 0; i < wordlist.Length; i++)
      {
        visited[i] = true;
        same = master.Guess(wordlist[i]);
        guess = i;
        if (same > 0) break;
      }

      while (same != len)
      {
        var guessStr = wordlist[guess];
        for (int i = 0; i < wordlist.Length; i++)
        {
          if (visited[i]) continue;
          visited[i] = true;

          diff = 0;

          for (int j = 0; j < len; j++) //查找与最后一次猜测想符合的元素
          {
            if (guessStr[j] != wordlist[i][j]) diff++;
            if (diff > len - same) break;
          }

          if (diff == len - same)
          {
            var newSame = master.Guess(wordlist[i]);
            use++;

            if (newSame > same)
            {
              same = newSame;
              guess = i;
            }
          }
        }
      }

      Console.WriteLine($"answer is {wordlist[guess]},use count is:{use}");
    }

    // bug 次数还是多了
    public void Optimize(string[] wordlist, Master master)
    {
      int same, len = 6, diff, maxSame = 0;

      bool flag;

      Dictionary<int, int> dictionary = new Dictionary<int, int>();

      for (int i = 0; i < wordlist.Length; i++)
      {
        if (dictionary.Count == 0)
        {
          same = master.Guess(wordlist[i]);
          dictionary.Add(i, same);
          maxSame = same;
          continue;
        }

        flag = true;
        foreach (var item in dictionary)
        {
          diff = 0;
          for (int j = 0; j < len; j++)
          {
            if (wordlist[i][j] != wordlist[item.Key][j]) diff++;
            if (diff > len - item.Value)
              break;
          }

          if (diff != len - item.Value)
          {
            flag = false;
            break;
          }
        }

        if (flag)
        {
          same = master.Guess(wordlist[i]);
          dictionary.Add(i, same);
          maxSame = Math.Max(same, maxSame);
        }
      }

      Console.WriteLine(JsonConvert.SerializeObject(dictionary));
    }

    public void OptimizeRand(string[] wordlist, Master master)
    {
      int same, len = 6, diff, maxSame = 0, i, arrLen = wordlist.Length;

      bool flag;

      Random rand = new Random();

      Dictionary<int, int> dictionary = new Dictionary<int, int>();

      while (maxSame != len)
      {
        i = rand.Next(arrLen);

        if (dictionary.ContainsKey(i)) continue;

        if (dictionary.Count == 0)
        {
          same = master.Guess(wordlist[i]);
          dictionary.Add(i, same);
          maxSame = same;
          continue;
        }

        flag = true;
        foreach (var item in dictionary)
        {
          diff = 0;
          for (int j = 0; j < len; j++)
          {
            if (wordlist[i][j] != wordlist[item.Key][j]) diff++;
            if (diff > len - item.Value)
              break;
          }

          if (diff != len - item.Value)
          {
            flag = false;
            break;
          }
        }

        if (flag)
        {
          same = master.Guess(wordlist[i]);
          dictionary.Add(i, same);
          maxSame = Math.Max(same, maxSame);
        }
      }

      Console.WriteLine(JsonConvert.SerializeObject(dictionary));
    }
  }

  public class Master
  {
    public Master(string answer)
    {
      this.answer = answer;
    }

    private int use;

    private string answer = "000000";

    public int Guess(string word)
    {
      var same = 0;
      for (int i = 0; i < word.Length; i++)
      {
        if (word[i] == answer[i]) same++;
      }

      use++;

      if (same == 6)
        Console.WriteLine($"you has find answer , use count:{use}");

      return same;
    }
  }
}