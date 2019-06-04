using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ConsoleTest.HardQuestion
{
  /// <summary>
  /// @desc : Trap  
  /// @author : mons
  /// @create : 2019/6/4 13:57:03 
  /// @source : https://leetcode.com/problems/trapping-rain-water/
  /// </summary>
  [Obsolete]
  public class Trap
  {
    //Memory Limit
    public int Try(int[] height)
    {
      int res = 0, len = height.Length, max = 0;

      for (int i = 0; i < height.Length; i++)
        max = Math.Max(height[i], max);

      var arr = new int[max][];

      for (int i = 0; i < max; i++)
        arr[i] = new int[len];

      for (int i = 0; i < height.Length; i++)
      for (int j = 0; j < height[i]; j++)
        arr[j][i] = 1;

      for (int i = 0; i < max; i++)
      {
        for (int j = 0; j < len; j++)
        {
          if (arr[i][j] == 1) continue;

          bool success = false;
          for (int k = 0; k < j; k++)
          {
            if (arr[i][k] == 1)
            {
              success = true;
              break;
            }
          }

          if (!success) continue;

          success = false;

          for (int k = j + 1; k < len; k++)
          {
            if (arr[i][k] == 1)
            {
              success = true;
              break;
            }
          }

          if (success)
            res++;
        }
      }

      Console.WriteLine(JsonConvert.SerializeObject(height));
      Console.WriteLine(JsonConvert.SerializeObject(arr));
      Console.WriteLine($"max:{max}");

      return res;
    }

    //Time Limit 
    public int Solution(int[] height)
    {
      int res = 0, max = 0, len = height.Length, start, end;

      foreach (var item in height)
        max = Math.Max(item, max);

      for (int i = 0; i < max; i++)
      {
        start = -1;
        end = -1;
        for (int j = 0; j < len; j++)
        {
          if (height[j] > i)
          {
            start = j;
            break;
          }
        }

        if (start == -1) continue;

        for (int j = len - 1; j > start; j--)
        {
          if (height[j] > i)
          {
            end = j;
            break;
          }
        }

        if (end == -1) continue;

        for (int j = start + 1; j < end; j++)
          if (height[j] <= i)
            res++;
      }

      return res;
    }

    //差一点点。。。 可惜了。
    //source:https://leetcode.com/problems/trapping-rain-water/discuss/17357/Sharing-my-simple-c%2B%2B-code%3A-O(n)-time-O(1)-space
    public int OtherSolution(int[] A)
    {
      int left = 0;
      int right = A.Length - 1;
      int res = 0;
      int maxleft = 0, maxright = 0;
      while (left <= right)
      {
        if (A[left] <= A[right])
        {
          if (A[left] >= maxleft) maxleft = A[left];
          else res += maxleft - A[left];
          left++;
        }
        else
        {
          if (A[right] >= maxright) maxright = A[right];
          else res += maxright - A[right];
          right--;
        }
      }

      return res;
    }
  }
}