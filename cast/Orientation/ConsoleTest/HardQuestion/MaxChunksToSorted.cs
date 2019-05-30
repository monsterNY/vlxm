using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ConsoleTest.HardQuestion
{
  /// <summary>
  /// @desc : MaxChunksToSorted  
  /// @author : mons
  /// @create : 2019/5/30 11:29:00 
  /// @source : https://leetcode.com/problems/max-chunks-to-make-sorted-ii/
  /// </summary>
  public class MaxChunksToSorted
  {
    /**
     * Runtime: 112 ms, faster than 15.79% of C# online submissions for Max Chunks To Make Sorted II.
     * Memory Usage: 23.9 MB, less than 53.33% of C# online submissions for Max Chunks To Make Sorted II.
     */
    public int Solution(int[] arr)
    {
      var count = 0;

      int start = 0, end = 0, min = 0, max = 0, realMax = 0, len = arr.Length;

      while (end < len)
      {
        for (int i = start + 1; i < arr.Length; i++)
        {
          if (arr[i] < arr[min])
          {
            min = i;
            end = i;
            max = realMax;
          }

          if (arr[i] > arr[realMax])
          {
            realMax = i;
          }

          if (arr[i] >= arr[min] && arr[i] < arr[max])
          {
            max = realMax;
            end = i;
          }
        }

        count++;

        start = end + 1;
        end = start;
        min = start;
        realMax = start;
        max = start;
      }

      return count;
    }

    [Obsolete]
    public int Optimize(int[] arr)
    {
      int len = arr.Length, count = 0, min = arr[len - 1], max = arr[0];

      int[] minDp = new int[len], maxDp = new int[len];

      minDp[len - 1] = arr[len - 1];
      maxDp[0] = max;

      for (int i = len - 1; i >= 0; i--)
      {
        if (arr[i] < min) min = arr[i];
        minDp[i] = min;
      }

      for (int i = 0; i < len; i++)
      {
        if (arr[i] > max) max = arr[i];
        maxDp[i] = max;
      }

      Console.WriteLine(JsonConvert.SerializeObject(minDp));
      Console.WriteLine(JsonConvert.SerializeObject(maxDp));

      var flag = new bool[len];

      for (int i = 0; i < len; i++)
      {
        if (arr[i] == minDp[i] && arr[i] == maxDp[i])
        {
          count++;
          flag[i] = true;
        }

        if (i > 0 && flag[i] != flag[i - 1]) count++;
      }

      return count == 0 ? 1 : count;
    }

    public int OtherSolution(int[] arr)
    {
      int n = arr.Length;
      int[] maxOfLeft = new int[n];
      int[] minOfRight = new int[n];

      maxOfLeft[0] = arr[0];
      for (int i = 1; i < n; i++)
      {
        maxOfLeft[i] = Math.Max(maxOfLeft[i - 1], arr[i]);
      }

      minOfRight[n - 1] = arr[n - 1];
      for (int i = n - 2; i >= 0; i--)
      {
        minOfRight[i] = Math.Min(minOfRight[i + 1], arr[i]);
      }

      Console.WriteLine(JsonConvert.SerializeObject(minOfRight));
      Console.WriteLine(JsonConvert.SerializeObject(maxOfLeft));
      int res = 0;
      for (int i = 0; i < n - 1; i++)
      {
        if (maxOfLeft[i] <= minOfRight[i + 1]) res++;//optimize验证错误。。。。
      }

      return res + 1;
    }

  }
}