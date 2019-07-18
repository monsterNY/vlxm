using System;
using System.Collections.Generic;
using System.Text;
using Tools.CusAttr;
using Tools.CusMenu;

namespace ConsoleTest.MiddleQuestionTwo
{
  /// <summary>
  /// @desc : PossibleBipartition  
  /// @author :mons
  /// @create : 2019/4/12 10:37:57 
  /// @source : https://leetcode.com/problems/possible-bipartition/
  /// </summary>
  [Obsolete("DFS")]
  [Love(LoveTypes.Fix)]
  public class PossibleBipartition
  {

    /**
     * amazing
     *
     * 建议可用bool 替代 int
     *
     */
    public bool OtherSolution(int N, int[][] dislikes)
    {
      int[][] graph = new int[N][];//存储person - dislike and dislike - person
      for (int i = 0; i < N; i++)
      {
        graph[i] = new int[N];
      }

      foreach (int[] d in dislikes)
      {
        graph[d[0] - 1][d[1] - 1] = 1;
        graph[d[1] - 1][d[0] - 1] = 1;
      }
      int[] group = new int[N];
      for (int i = 0; i < N; i++)//遍历所有人
      {
        if (group[i] == 0 && !Dfs(graph, group, i, 1))//如果i尚未被访问
        {
          return false;
        }
      }
      return true;
    }
    private bool Dfs(int[][] graph, int[] group, int index, int g)
    {
      group[index] = g;//标记被访问
      for (int i = 0; i < graph.Length; i++)
      {
        if (graph[index][i] == 1)//==1 表示 index dislike i or i dislike index
        {
          if (group[i] == g)//如果 i已访问 
          {
            //such as : 
            // 1  g  in 1
            // 3  -g 1 - > 3
            // 2  g  3 - > 2
            // 1 false 2 -> 1
            return false;//此处为false最终返回点 
          }
          //若i未被访问 继续观察 将i标记为未观察
          //注意此处-g target:避免 1->2 2->1 
          //当1->2 时 传递 -g 当遍历到2->1时  由于 1为g 2为-g 且 1为已标记 则会直接跳过。
          if (group[i] == 0 && !Dfs(graph, group, i, -g))
          {
            return false;
          }
        }
      }
      return true;
    }

    public bool Solution(int N, int[][] dislikes)
    {

      ISet<int> group = new HashSet<int>(),group2 = new HashSet<int>();

      foreach (var dislike in dislikes)
      {

        var personal = dislike[0];
        var dislikePersonal = dislike[1];

        if (group.Contains(personal))
        {
          if (group.Contains(dislikePersonal)) return false;
          if (!group2.Contains(dislikePersonal)) group2.Add(dislikePersonal);
        }else if (group2.Contains(personal))
        {
          if (group2.Contains(dislikePersonal)) return false;
          if (!group.Contains(dislikePersonal)) group.Add(dislikePersonal);
        }
        else
        {
          group.Add(personal);
          group2.Add(dislikePersonal); 
        }

      }

      return true;
    }

  }
}
