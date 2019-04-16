using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Tools.CusAttr;
using Tools.CusMenu;

namespace ConsoleTest.MiddleQuestionTwo
{
  /// <summary>
  /// @desc : CheckInclusion  
  /// @author :mons
  /// @create : 2019/4/16 17:16:23 
  /// @source : https://leetcode.com/problems/permutation-in-string/
  /// @fix : https://leetcode.com/problems/find-all-anagrams-in-a-string/discuss/92007/Sliding-Window-algorithm-template-to-solve-all-the-Leetcode-substring-search-problem
  /// </summary>
  [Love(LoveTypes.Fix)]
  [Description("sliding")]
  public class CheckInclusion
  {
    #region otherSolution

    public bool checkInclusion(String s1, String s2)
    {
      int len1 = s1.Length, len2 = s2.Length;
      if (len1 > len2) return false;

      int[] count = new int[26];
      for (int i = 0; i < len1; i++)
      {
        count[s1[i] - 'a']++;
        count[s2[i] - 'a']--;
      }

      if (allZero(count)) return true;

      for (int i = len1; i < len2; i++)
      {
        count[s2[i] - 'a']--;
        count[s2[i - len1] - 'a']++;
        if (allZero(count)) return true;
      }

      return false;
    }

    private bool allZero(int[] count)
    {
      for (int i = 0; i < 26; i++)
      {
        if (count[i] != 0) return false;
      }

      return true;
    }

    #endregion

    /**
     * Runtime: 192 ms, faster than 26.70% of C# online submissions for Permutation in String.
     * Memory Usage: 24.4 MB, less than 17.39% of C# online submissions for Permutation in String.
     *
     * ... not efficient
     *
     */
    public bool Solution(string s1, string s2)
    {
      int[] compareArr = GetArr(s1, 0, s1.Length), itemArr;

      bool flag;

      for (int i = 0; i <= s2.Length - s1.Length; i++)
      {
        if (compareArr[s2[i] - 'a'] == 0) continue;

        flag = true;

        itemArr = GetArr(s2, i, i + s1.Length);

        for (int j = 0; j < 26; j++)
        {
          if (itemArr[j] > compareArr[j])
          {
            flag = false;
            break;
          }
        }

        if (flag) return true;
      }

      return false;
    }

    //更低效。
    public bool Solution2(string s1, string s2)
    {
      int[] compareArr = GetArr(s1, 0, s1.Length), itemArr;

      bool flag;

      for (int i = 0; i <= s2.Length - s1.Length; i++)
      {
        if (compareArr[s2[i] - 'a'] == 0) continue;

        flag = true;

        itemArr = (int[]) compareArr.Clone();

        for (int j = 0; j < s1.Length; j++)
        {
          if (itemArr[s2[i] - 'a']-- == 0)
          {
            flag = false;
            break;
          }
        }

        if (flag) return true;
      }

      return false;
    }

    public int[] GetArr(string str, int startIndex, int endIndex)
    {
      int[] arr = new int[26];

      for (; startIndex < endIndex; startIndex++)
      {
        arr[str[startIndex] - 'a']++;
      }

      return arr;
    }

    //bug 需要考虑s1的所有排列组合
    public bool Try(string s1, string s2)
    {
      bool flag = true;

      for (int i = 0; i < s2.Length; i++)
      {
        flag = true;

        for (int j = 0; j < s1.Length; j++)
        {
          if (i + j >= s2.Length || s1[j] != s2[i + j])
          {
            flag = false;
            break;
          }
        }

        if (flag) return true;

        flag = true;

        for (int j = 0; j < s1.Length; j++)
        {
          if (i + j >= s2.Length || s1[s1.Length - 1 - j] != s2[i + j])
          {
            flag = false;
            break;
          }
        }

        if (flag) return true;
      }

      return false;
    }
  }
}