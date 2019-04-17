using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestionTwo
{
  /// <summary>
  /// @desc : MinMutation  
  /// @author :mons
  /// @create : 2019/4/17 9:49:25 
  /// @source : https://leetcode.com/problems/minimum-genetic-mutation/
  /// </summary>
  [Obsolete("BFS")]
  public class MinMutation
  {

    //bug
    public int Solution(string start, string end, string[] bank)
    {
      //target: 使start 到达 end 且 改变路程必须在bank中

      var list = new List<int>();

      var charArray = start.ToCharArray();

      for (int i = 0; i < start.Length; i++)
      {
        if (start[i] != end[i])
        {
          list.Add(i);
        }
      }

      var count = list.Count;
      bool flag;
      while (list.Count > 0)
      {
        flag = false;
        for (int i = 0; i < list.Count; i++)
        {
          charArray[list[i]] = end[list[i]];
          if (CanChange(charArray, bank))
          {
            flag = true;
            list.RemoveAt(i);
            break;
          }
          else
            charArray[list[i]] = start[list[i]];
        }

        if (!flag) return -1;
      }

      return count;
    }

    public bool CanChange(char[] arr, string[] bank)
    {
      for (int i = 0; i < bank.Length; i++)
      {
        bool flag = true;
        for (int j = 0; j < arr.Length; j++)
        {
          if (arr[j] != bank[i][j])
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