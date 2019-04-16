using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestionTwo
{
  /// <summary>
  /// @desc : GetHint  
  /// @author :mons
  /// @create : 2019/4/16 14:17:45 
  /// @source : https://leetcode.com/problems/bulls-and-cows/
  /// </summary>
  public class GetHint
  {
    /**
     * Runtime: 84 ms, faster than 100.00% of C# online submissions for Bulls and Cows.
     * Memory Usage: 21.8 MB, less than 42.86% of C# online submissions for Bulls and Cows.
     *
     * cool
     *
     */
    public string Solution(string secret, string guess)
    {
      int[] arr = new int[10];

      foreach (var c in secret)
        arr[c - '0']++;

      //bulls 完全匹配 cows 存在
      int bulls = 0, cows = 0;

      for (int i = 0; i < guess.Length; i++)
      {
        if (secret[i] == guess[i])
        {
          bulls++;
          arr[guess[i] - '0']--;
          if (arr[guess[i] - '0'] < 0) cows--;
        }
        else if (arr[guess[i] - '0'] > 0)
        {
          cows++;
          arr[guess[i] - '0']--;
        }
      }

      return $"{bulls}A{cows}B";
    }
  }
}