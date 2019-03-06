using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleTest.LeetCode
{
  /// <summary>
  /// @source:https://leetcode.com/problems/find-the-town-judge/
  /// </summary>
  public class FindTheTownJudge
  {

    /// <summary>
    /// 满足提议 但未审查所有条件 导致逻辑复杂。。。
    /// </summary>
    /// <param name="N"></param>
    /// <param name="trust"></param>
    /// <returns></returns>
    public int FindJudge(int N, int[][] trust)
    {
      Dictionary<int, List<int>> dictionary = new Dictionary<int, List<int>>();

      var judgeList = new List<int>();

      for (int i = 1; i <= N; i++)
      {
        dictionary.Add(i, new List<int>());
      }

      foreach (var item in trust)
      {
        var key = item[item.Length - 1];

        if (!judgeList.Contains(key))
          judgeList.Add(key);
        dictionary[item[item.Length - 1]].Add(item[0]);
      }

      var judge = -1;
      bool isJudge;
      List<int> trustList;
      for (int i = 0; i < judgeList.Count; i++)
      {
        isJudge = true;
        trustList = dictionary[judgeList[i]];
        if (trustList.Count != N - 1)
          continue;
        for (int j = 0; j < judgeList.Count; j++)
        {
          if (dictionary[judgeList[j]].Contains(judgeList[i]))
          {
            isJudge = false;
            break;
          }

          if (j != i)
          {
            if ((!trustList.Contains(judgeList[j])))
            {
              isJudge = false;
              break;
            }
          }
        }

        if (isJudge)
          judge = judgeList[i];
      }

      return judge;
    }

    /**
     * result:
     *
     * Runtime: 372 ms, faster than 96.77% of C# online submissions for Find the Town Judge.
     * Memory Usage: 42.3 MB, less than 100.00% of C# online submissions for Find the Town Judge.
     *
     */

    public int Optimize(int N, int[][] trust)
    {
      int[] trustArr = new int[N];
      int[] byTrustArr = new int[N];

      foreach (var item in trust)
      {
        trustArr[item[1] - 1]++;
        byTrustArr[item[0] - 1]++;
      }

      for (int i = 0; i < trustArr.Length; i++)
        if (trustArr[i] == N - 1 && byTrustArr[i] == 0)
          return i + 1;

      return -1;
    }

    /// <summary>
    /// .,加强版 [a 差一点，，，
    /// </summary>
    /// <param name="N"></param>
    /// <param name="trust"></param>
    /// <returns></returns>
    public int OtherSolution(int N, int[][] trust)
    {
      int[] trustArr = new int[N];

      foreach (var item in trust)
      {
        trustArr[item[1] - 1]++;
        trustArr[item[0] - 1]--;
      }

      for (int i = 0; i < trustArr.Length; i++)
        if (trustArr[i] == N - 1)
          return i + 1;

      return -1;
    }
  }
}