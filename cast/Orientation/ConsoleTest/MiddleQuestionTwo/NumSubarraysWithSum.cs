using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestionTwo
{
  /// <summary>
  /// @desc : NumSubarraysWithSum  
  /// @author :mons
  /// @create : 2019/4/17 13:45:16 
  /// @source : https://leetcode.com/problems/binary-subarrays-with-sum/
  /// </summary>
  public class NumSubarraysWithSum
  {
    //Time Limit
    public int Simple(int[] A, int S)
    {
      int count = 0, num;

      for (int i = 0; i < A.Length; i++)
      {
        num = 0;
        for (int j = i; j < A.Length; j++)
        {
          num += A[j];
          if (num == S) count++;
        }
      }

      return count;
    }

    /**
     * Runtime: 136 ms, faster than 88.57% of C# online submissions for Binary Subarrays With Sum.
     * Memory Usage: 32.8 MB, less than 25.00% of C# online submissions for Binary Subarrays With Sum.
     *
     * amazing!
     *
     */
    public int Solution(int[] A, int S)
    {
      int count = 0, num = 0;

      Dictionary<int, int> dictionary = new Dictionary<int, int>();

      foreach (var item in A)
      {
        num += item;
        if (S == num) count++;

        if (dictionary.ContainsKey(num - S))
          count += dictionary[num - S];

        if (dictionary.ContainsKey(num)) dictionary[num]++;
        else
          dictionary.Add(num, 1);
      }

      return count;
    }
  }
}