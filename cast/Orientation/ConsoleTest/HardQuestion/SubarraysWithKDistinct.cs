using System;
using System.Collections.Generic;
using System.Text;
using Tools.CusExtension;

namespace ConsoleTest.HardQuestion
{
  /// <summary>
  /// @desc : SubarraysWithKDistinct  
  /// @author : mons
  /// @create : 2019/5/30 16:06:19 
  /// @source : https://leetcode.com/problems/subarrays-with-k-different-integers/
  /// </summary>
  [Obsolete("no think")]
  public class SubarraysWithKDistinct
  {

    //time limit
    public int Simple(int[] A, int K)
    {

      var count = 0;

      ISet<int> set = new HashSet<int>();

      for (int i = 0; i < A.Length; i++)
      {
        for (int j = i; j < A.Length; j++)
        {
          set.Add(A[j]);

          if (set.Count == K) count++;
          if (set.Count > K) break;
        }

        set.Clear();
      }

      return count;
    }


    public int Solution(int[] A, int K)
    {
      if (K == 1) return A.Length;

      int count = 0, start = 0, end = K, len = A.Length;

      int[] dp = new int[len];

      Dictionary<int, int> dictionary = new Dictionary<int, int>();

      ISet<int> set = new HashSet<int>();
      for (int i = 0; i < A.Length; i++)
      {
        var item = A[i];
        if (dictionary.ContainsKey(item)) dictionary[item]++;
        else dictionary.Add(item, 0);

        set.Add(item);

        if (set.Count == K)
        {
          dp[i] = dp[i - 1] + 1;

          count += dp[i];
        }
      }

      return count;
    }

    public int subarraysWithKDistinct(int[] A, int K)
    {
      return atMostK(A, K) - atMostK(A, K - 1);
    }
    int atMostK(int[] A, int K)
    {
      int i = 0, res = 0;
      Dictionary<int,int> count = new Dictionary<int, int>();
      for (int j = 0; j < A.Length; ++j)
      {
        if (count.Get(A[j], 0) == 0) K--;
        count.Increase(A[j]);
        while (K < 0)
        {
          count.Decrease(A[i]);
          if (count[A[i]] == 0) K++;
          i++;
        }
        res += j - i + 1;
      }
      return res;
    }

  }
}