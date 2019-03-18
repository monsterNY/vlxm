using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.LeetCode
{
  /// <summary>
  /// @desc : NumPairsDivisibleBy60  
  /// @author :mons
  /// @create : 2019/3/18 16:49:40 
  /// @source : https://leetcode.com/problems/pairs-of-songs-with-total-durations-divisible-by-60/
  /// </summary>
  public class NumPairsDivisibleBy60
  {
    public int Solution(int[] time)
    {
      var count = 0;
      for (int i = 0; i < time.Length; i++)
      {
        for (int j = i + 1; j < time.Length; j++)
        {
          if ((time[i] + time[j]) % 60 == 0) count++;
        }
      }

      return count;
    }

    /**
     * amazing
     *
     * Runtime: 120 ms, faster than 100.00% of C# online submissions for Pairs of Songs With Total Durations Divisible by 60.
     * Memory Usage: 30.1 MB, less than 100.00% of C# online submissions for Pairs of Songs With Total Durations Divisible by 60.
     *
     */
    public int Optimize(int[] time)
    {
      var count = 0;

      int[] arr = new int[60];

      for (int i = 0; i < time.Length; i++)
        arr[time[i] % 60]++;

      count += GetCount(arr[0]);
      count += GetCount(arr[30]);

      for (int i = 1; i < arr.Length / 2; i++)
        count += arr[i] * arr[60 - i];

      return count;
    }

    public int GetCount(int n)
    {
      var sum = 0;
      for (int i = 1; i < n; i++)
        sum += i;

      return sum;
    }
  }
}