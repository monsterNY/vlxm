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
  public class FindCircleNum
  {
    public int Solution(int[][] M)
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