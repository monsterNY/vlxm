using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : LastRemaining  
  /// @author :mons
  /// @create : 2019/4/8 15:38:26 
  /// @source : https://leetcode.com/problems/elimination-game/
  /// </summary>
  [Obsolete("....")]
  public class LastRemaining
  {
    public int Solution(int n)
    {
      var list = new List<int>();

      for (int i = 0; i < n; i++)
      {
        list.Add(i + 1);
      }

      bool flag = true;
      while (list.Count > 1)
      {
        if (flag)
        {
          for (int i = 0; i < list.Count && list.Count > 1; i++)
          {
            list.RemoveAt(i);
          }

          flag = false;
        }
        else
        {
          for (int i = list.Count - 1; i >= 0 && list.Count > 1; i -= 2)
          {
            list.RemoveAt(i);
          }

          flag = true;
        }
      }

      return list[0];
    }
  }
}