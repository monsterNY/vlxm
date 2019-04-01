using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : FindCircleNum  
  /// @author :mons
  /// @create : 2019/3/29 17:13:02 
  /// @source : https://leetcode.com/problems/friend-circles/
  /// </summary>
  [Obsolete("failure DFS")]
  public class FindCircleNum
  {
    #region other solution

    //I should have done this....

    public void dfs(int[][] M, int[] visited, int i) //深度优先搜索算法（英语：Depth-First-Search，DFS 搜索i的朋友圈
    {
      for (int j = 0; j < M.Length; j++)
      {
        if (i == j) continue;
        if (M[i][j] == 1 && visited[j] == 0)
        {
          visited[j] = 1; //标记为已访问
          dfs(M, visited, j); // 由于j和i的朋友  所以可以访问朋友的朋友圈 来扩充自己的朋友圈
        }
      }
    }

    public int findCircleNum(int[][] M)
    {
      int[] visited = new int[M.Length];
      int count = 0;
      for (int i = 0; i < M.Length; i++)
      {
        if (visited[i] == 0) //不成组
        {
          dfs(M, visited, i);
          count++;
        }
      }

      return count;
    }

    #endregion

    public int Solution(int[][] M)
    {
      Dictionary<int, bool[]> dictionary = new Dictionary<int, bool[]>();
      var circleNum = M.Length;

      for (int i = 0; i < M.Length; i++)
      {
        for (int j = 0; j < M[i].Length; j++)
        {
          if (i == j || dictionary.ContainsKey(j)) continue;

          if (M[i][j] == 1)
          {
            if (!dictionary.ContainsKey(i))
            {
              circleNum--;
              if (circleNum == 1) return 1;
              var arr = new bool[M.Length];
              arr[j] = true;
              dictionary.Add(i, arr);
            }
            else
            {
              dictionary[i][j] = true;
            }
          }
        }
      }

      foreach (var item in dictionary)
      {
        for (int i = 0; i < item.Value.Length; i++)
        {
          if (item.Value[i])
          {
            if (dictionary.ContainsKey(i)) circleNum--;
            if (circleNum == 1) return 1;
          }
        }
      }

      return circleNum;
    }

    public int Try2(int[][] M)
    {
      var circleNum = M.Length; //first : all personal no friend

      ISet<int> set = new HashSet<int>();

      for (int i = 0; i < M.Length; i++)
      {
        for (int j = 0; j < M[i].Length; j++)
        {
          if (i == j || set.Contains(i)) continue; //me is my friend
          if (set.Contains(i) && set.Contains(j)) continue;
          //if (set.Contains(i))// you already have a friend,so ,when you have new friend,the circle narrow
          //continue;
          // you has other personal friend
          if (M[i][j] == 1)
          {
            circleNum--;
            set.Add(i);
            set.Add(j);
          }
        }
      }

      //var circleNum = M.Length - count;//not group personal haven't friends

      return circleNum;
    }

    public int Try(int[][] M)
    {
      var circleNum = M.Length; //first : all personal no friend

      Dictionary<int, List<int>> dictionary = new Dictionary<int, List<int>>();

      bool[] flag = new bool[M.Length];

      for (int i = 0; i < M.Length; i++)
      {
        for (int j = 0; j < M[i].Length; j++)
        {
          if (i == j) continue; //me is my friend
          if (dictionary.ContainsKey(j)) // you already have a friend,so ,when you have new friend,the circle narrow
          {
            flag[i] = true;
            continue;
          }

          ; // you has other personal friend
          if (M[i][j] == 1)
          {
            if (flag[i]) circleNum--;
            if (dictionary.ContainsKey(i))
            {
              dictionary[i].Add(j);
            }
            else
            {
              circleNum--;
              dictionary.Add(i, new List<int>() {j});
            }
          }
        }
      }

      //var circleNum = M.Length - count;//not group personal haven't friends

      return circleNum;
    }
  }
}