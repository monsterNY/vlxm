using System;
using System.Collections.Generic;
using System.Text;
using Tools.CusAttr;
using Tools.CusMenu;

namespace ConsoleTest.MiddleQuestionTwo
{
  /// <summary>
  /// @desc : CanFinish  
  /// @author :mons
  /// @create : 2019/4/17 11:46:38 
  /// @source : https://leetcode.com/problems/course-schedule/
  /// </summary>
  [Love(LoveTypes.Question)]
  public class CanFinish
  {

    //Time Limit
    public bool Solution(int numCourses, int[][] prerequisites)
    {
      bool[][] dp = new bool[numCourses][];

      for (int i = 0; i < dp.Length; i++)
        dp[i] = new bool[numCourses];

      foreach (var item in prerequisites)
      {
        if (dp[item[1]][item[0]])
        {
          return false;
        }
        else
        {
          dp[item[0]][item[1]] = true;
        }
      }

      var visited = new bool[numCourses];

      for (int i = 0; i < dp.Length; i++)
      {
        if (!Helper(i, dp, visited)) return false;
      }

      return true;
    }

    public bool Helper(int find, bool[][] dp, bool[] visist)
    {
      for (int i = 0; i < visist.Length; i++)
      {
        if (visist[i] && dp[find][i]) return false;
      }

      visist[find] = true;
      for (int i = 0; i < dp.Length; i++)
      {
        if (visist[i]) continue;
        if (dp[find][i])
        {
          if (!Helper(i, dp, visist)) return false;
        }
      }

      visist[find] = false;
      return true;
    }
  }
}