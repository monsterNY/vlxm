using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestionThree
{
  /// <summary>
  /// @desc : FindSubstringInWraproundString  
  /// @author :mons
  /// @create : 2019/4/22 14:19:17 
  /// @source : https://leetcode.com/problems/unique-substrings-in-wraparound-string/
  /// </summary>
  public class FindSubstringInWraproundString
  {
    public int findSubstringInWraproundString(String p)
    {
      // count[i] is the maximum unique substring end with ith letter.
      // 0 - 'a', 1 - 'b', ..., 25 - 'z'.
      int[] count = new int[26];

      // store longest contiguous substring ends at current position.
      int maxLengthCur = 0;

      for (int i = 0; i < p.Length; i++)
      {
        if (i > 0 && (p[i] - p[i - 1] == 1 || (p[i - 1] - p[i] == 25)))
        {
          maxLengthCur++;
        }
        else
        {
          maxLengthCur = 1;
        }

        int index = p[i] - 'a';
        count[index] = Math.Max(count[index], maxLengthCur);
      }

      // Sum to get result
      int sum = 0;
      for (int i = 0; i < 26; i++)
      {
        sum += count[i];
      }

      return sum;
    }

    public int Solution(string p)
    {
      if (p.Length < 2) return p.Length;
      var count = 0;

      int sum = p[0] - 'a';

      ISet<int> set = new HashSet<int>() {sum};

      var dp = new int[p.Length];
      Array.Fill(dp, 1);

      for (int i = 1; i < p.Length; i++)
      {
        if (p[i] == p[i - 1] + 1 || (p[i] == 'a' && p[i - 1] == 'z'))
        {
          dp[i] = dp[i - 1] + 1;
          sum += 2 << (p[i] - 'a');
          set.Add(p[i] - 'a');
        }
        else
        {
          sum = p[i] - 'a';
        }

        if (!set.Contains(sum))
        {
          count += dp[i];
          set.Add(sum);
        }
      }

      return count;
    }
  }
}