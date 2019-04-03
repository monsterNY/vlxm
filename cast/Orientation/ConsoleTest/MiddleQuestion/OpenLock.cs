using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : OpenLock  
  /// @author :mons
  /// @create : 2019/4/3 16:31:36 
  /// @source : https://leetcode.com/problems/open-the-lock/
  /// </summary>
  [Obsolete("no imagination")]
  public class OpenLock
  {
    public int Solution(string[] deadends, string target)
    {
      int count = 0, step;
      var charArray = target.ToCharArray();

      for (int i = 0; i < charArray.Length; i++)
      {
        if (target[i] - 48 % 10 != 0)
        {
          if (target[i] > 5 + 48)
          {
            charArray[i]++;
          }
          else
          {
            charArray[i]--;
          }

          step = 0;
          while (CanLock(charArray, deadends))
          {
            step++;
            charArray[i + 1 % charArray.Length]++;
            count++;
            if (step > 4) return -1;
          }
        }
      }

      // 避开一个 deadend

      //要求: 避开所有 deadends 转到 target 并输出最小转数

      //即： 避开所有 deadends 使 target 转到 0000 并输出最小转数

      return count;
    }

    public bool CanLock(char[] arr, string[] deadends)
    {
      bool flag;
      for (int i = 0; i < deadends.Length; i++)
      {
        flag = true;
        for (int j = 0; j < arr.Length; j++)
        {
          if (deadends[i][j] != arr[j])
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