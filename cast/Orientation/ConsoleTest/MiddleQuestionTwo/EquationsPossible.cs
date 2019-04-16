using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Tools.CusAttr;
using Tools.CusMenu;

namespace ConsoleTest.MiddleQuestionTwo
{
  /// <summary>
  /// @desc : EquationsPossible  
  /// @author :mons
  /// @create : 2019/4/16 11:34:12 
  /// @source : https://leetcode.com/problems/satisfiability-of-equality-equations/
  /// </summary>
  [Love(LoveTypes.Question,LoveTypes.Fix)]
  [Description("dp")]
  public class EquationsPossible
  {
    /**
     * Runtime: 92 ms, faster than 96.58% of C# online submissions for Satisfiability of Equality Equations.
     * Memory Usage: 23.7 MB, less than 39.13% of C# online submissions for Satisfiability of Equality Equations.
     *
     * nice nice !!!
     *
     */
    public bool Solution(string[] equations)
    {
      var dp = new int[26][];
      for (int i = 0; i < dp.Length; i++)
        dp[i] = new int[26];

      foreach (var item in equations)
      {
        if (item[0] == item[3])
        {
          if (item[1] == '=') continue;
          return false;
        }

        if (item[1] == '=')
        {
          if (dp[item[0] - 'a'][item[3] - 'a'] == -1) return false;
          dp[item[0] - 'a'][item[3] - 'a'] = 1;
          dp[item[3] - 'a'][item[0] - 'a'] = 1;
        }
        else
        {
          if (dp[item[0] - 'a'][item[3] - 'a'] == 1) return false;

          dp[item[0] - 'a'][item[3] - 'a'] = -1;
          dp[item[3] - 'a'][item[0] - 'a'] = -1;
        }
      }

      var visited = new bool[26];
      for (int i = 0; i < 26; i++)
      {
        if (!ValidHelper(dp, i, visited)) return false;
      }

      return true;
    }

    public bool ValidHelper(int[][] dp, int i, bool[] visit)
    {
      if (visit[i]) return true;
      visit[i] = true;

      for (int k = 0; k < visit.Length; k++)
      {
        if (visit[k])
        {
          if (dp[i][k] == -1) return false;
        }
      }

      for (int j = 0; j < 26; j++)
      {
        if (visit[j]) continue;
        if (dp[i][j] == 1)
        {
          if (!ValidHelper(dp, j, visit))
          {
            return false;
          }
        }
      }

      visit[i] = false;

      return true;
    }
  }
}