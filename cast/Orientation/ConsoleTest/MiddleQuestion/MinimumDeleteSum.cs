using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : MinimumDeleteSum  
  /// @author :mons
  /// @create : 2019/3/29 15:19:27 
  /// @source : https://leetcode.com/problems/minimum-ascii-delete-sum-for-two-strings/
  /// </summary>
  [Obsolete("I give up")]
  public class MinimumDeleteSum
  {

    public int OtherSolution(string s1, string s2)
    {
      int m = s1.Length;
      int n = s2.Length;
      int[][] dp = new int[m + 1][];
      Array.Fill(dp,new int[n+1]);
      for (int i = 0; i <= m; i++)
      {
        for (int j = 0; j <= n; j++)
        {
          if (i == 0 || j == 0)
          {
            int a = 0;
            for (int z = 1; z <= Math.Max(j, i); z++)
            {
              a += (i == 0 ? s2[z-1] : s1[z-1]);
            }
            dp[i][j] = a;
          }
          else if (s1[i-1] == s2[j-1])
          {
            dp[i][j] = dp[i - 1][j - 1];
          }
          else
          {
            dp[i][j] = Math.Min(s1[i-1] + dp[i - 1][j], s2[j-1] + dp[i][j - 1]);
            dp[i][j] = Math.Min(dp[i][j], s1[i-1] + s2[j-1] + dp[i - 1][j - 1]);
          }
        }
      }
      return dp[m][n];
    }


    public int Solution(string s1, string s2)
    {
      StringBuilder builderA = new StringBuilder(), builderB = new StringBuilder();
      int num = 0, i = 0, j = 0, index, index2, indexA, indexB;
      for (; i < s1.Length && j < s2.Length;)
      {
        if (s1[i] != s2[j])
        {
          index = s2.IndexOf(s1[i], j + 1);
          index2 = s1.IndexOf(s2[j], i + 1);

          if (index != -1 && index2 != -1)
          {

            var deal = Deal(s1, index2, s1[i], s2, index, s2[j]);

            if (deal == -1)
            {
              if (s1[i] > s2[j])
              {
                builderB.Append(s2[j]);
                num += s2[j++];
              }
              else
              {
                builderA.Append(s1[i]);
                num += s1[i++];
              }
            }else if (deal == 1)
            {
              builderA.Append(s1[i]);
              num += s1[i++];
            }
            else
            {
              builderB.Append(s2[j]);
              num += s2[j++];
            }

//            indexA = s1.IndexOf(s1[i], index2);
//            indexB = s2.IndexOf(s2[j], index);
//
//            if (indexA == -1 && indexB == -1)
//            {
//              if (s1[i] > s2[j])
//              {
//                builderB.Append(s2[j]);
//                num += s2[j++];
//              }
//              else
//              {
//                builderA.Append(s1[i]);
//                num += s1[i++];
//              }
//            }
//
//            if (s1.IndexOf(s1[i], index2) != -1)
//            {
//              builderA.Append(s1[i]);
//              num += s1[i++];
//            }
//            else if (s2.IndexOf(s2[j], index) != -1)
//            {
//              builderB.Append(s2[j]);
//              num += s2[j++];
//            }
          }
          else
          {
            if (index == -1)
            {
              builderA.Append(s1[i]);
              num += s1[i++];
            }

            if (index2 == -1)
            {
              builderB.Append(s2[j]);
              num += s2[j++];
            }
          }
        }
        else
        {
          i++;
          j++;
        }
      }

      for (; i < s1.Length || j < s2.Length;)
      {
        num += i < s1.Length ? s1[i++] : 0;
        num += j < s2.Length ? s2[j++] : 0;
      }

      return num;
    }

    public int Deal(string str, int startA, char itemA, string strB, int startB, char itemB)
    {
      startA = str.IndexOf(itemA, startA + 1);
      startB = strB.IndexOf(itemB, startB + 1);

      if (startA != -1 && startB != -1) return Deal(str, startA, itemA, strB, startB, itemB);
      if (startA != -1) return 1;
      if (startB != -1) return 0;

      return -1;
    }


    //bug
    public int Try(string s1, string s2)
    {
      int num = 0, i = 0, j = 0;
      for (; i < s1.Length && j < s2.Length;)
      {
        if (s1[i] > s2[j])
        {
          if (i + 1 < s1.Length && s1[i + 1] == s2[j] && (j + 1 >= s2.Length || s1[i] != s2[j + 1]))
            num += s1[i++];
          else
            num += s2[j++];
        }
        else if (s1[i] < s2[j])
        {
          if (j + 1 < s2.Length && s2[j + 1] == s1[i] && (i + 1 >= s1.Length || s2[j] != s1[i + 1]))
            num += s2[j++];
          else
            num += s1[i++];
        }
        else
        {
          i++;
          j++;
        }
      }

      for (; i < s1.Length || j < s2.Length;)
      {
        num += i < s1.Length ? s1[i++] : 0;
        num += j < s2.Length ? s2[j++] : 0;
      }

      return num;
    }

    //time limit
    public int Simple(string s1, string s2)
    {
      int sumA = 0, sumB = 0;

      for (int i = 0; i < s1.Length || i < s2.Length; i++)
      {
        sumA += i < s1.Length ? s1[i] : 0;
        sumB += i < s2.Length ? s2[i] : 0;
      }

      Dictionary<string, int> mapA = new Dictionary<string, int>();
      Dictionary<string, int> mapB = new Dictionary<string, int>();

      GetList(s1, 0, string.Empty, mapA, 0);
      GetList(s2, 0, string.Empty, mapB, 0);

      var maxNum = 0;

      foreach (var item in mapA)
      {
        if (mapB.ContainsKey(item.Key))
        {
          if (item.Value + mapB[item.Key] > maxNum) maxNum = item.Value + mapB[item.Key];
        }
      }

      return sumA + sumB - maxNum;
    }

    public void GetList(string str, int startIndex, string build, Dictionary<string, int> map, int score)
    {
      for (int i = startIndex; i < str.Length; i++)
      {
        var item = build + str[i];

        if (map.ContainsKey(item))
        {
          if (map[item] < score + str[i]) map[item] = score + str[i];
        }
        else
        {
          map.Add(item, score + str[i]);
        }

        GetList(str, i + 1, item, map, score + str[i]);
      }
    }
  }
}