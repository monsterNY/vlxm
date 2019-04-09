using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : MinIncrementForUnique  
  /// @author :mons
  /// @create : 2019/4/9 17:17:39 
  /// @source : https://leetcode.com/problems/minimum-increment-to-make-array-unique/
  /// </summary>
  public class MinIncrementForUnique
  {

    /**
     * Runtime: 164 ms, faster than 92.50% of C# online submissions for Minimum Increment to Make Array Unique.
     * Memory Usage: 33.1 MB, less than 60.00% of C# online submissions for Minimum Increment to Make Array Unique.
     *
     * emm....,.,.,.
     *
     */
    public int OtherSolution(int[] A)
    {
      Array.Sort(A);
      int res = 0, need = 0;
      foreach (int a in A)
      {
        res += Math.Max(need - a, 0);
        need = Math.Max(a, need) + 1;
      }
      return res;
    }

    /**
     * Runtime: 192 ms, faster than 45.00% of C# online submissions for Minimum Increment to Make Array Unique.
     * Memory Usage: 38.3 MB, less than 20.00% of C# online submissions for Minimum Increment to Make Array Unique.
     *
     * Runtime: 172 ms, faster than 62.50% of C# online submissions for Minimum Increment to Make Array Unique.
     * Memory Usage: 38.4 MB, less than 20.00% of C# online submissions for Minimum Increment to Make Array Unique.
     *
     * ~~~ 
     *
     */
    public int Solution(int[] A)
    {
      if (A.Length < 2) return 0;
      int count = 0;
      Array.Sort(A);

      var dp = new int[80000];

      dp[A[A.Length - 1]] = 1;

      for (int i = A.Length - 2; i >= 0; i--)
      {
        if (A[i] == A[i + 1])
        {
          count += dp[A[i]];
          dp[A[i]] = dp[A[i]] + dp[A[i] + dp[A[i]] + 1] + 1;
        }
        else
        {
          dp[A[i]] = dp[A[i] + 1] + 1;
        }
      }

      return count;
    }

    public int Solution2(int[] A)
    {
      if (A.Length < 2) return 0;
      int count = 0;
      Array.Sort(A);

      var dp = new int[40000];

      dp[A[A.Length - 1]] = 1;

      for (int i = A.Length - 2; i >= 0; i--)
      {
        if (A[i] == A[i + 1])
        {
          count += dp[A[i]];
          dp[A[i]] = dp[A[i]] + (A[i] + dp[A[i]] + 1 >= dp.Length - 1 ? 0 : dp[A[i] + dp[A[i]] + 1]) + 1;
        }
        else
        {
          dp[A[i]] = dp[A[i] + 1] + 1;
        }
      }

      return count;
    }

    //time limit
    public int Try2(int[] A)
    {
      int count = 0, itemTag;

      Dictionary<int, int> dictionary = new Dictionary<int, int>();

      for (int i = 0; i < A.Length; i++)
      {
        if (dictionary.ContainsKey(A[i]))
          dictionary[A[i]]++;
        else
          dictionary.Add(A[i], 1);
      }

      var list = dictionary.Keys.ToList();
      for (int i = 0; i < list.Count; i++)
      {
        if (dictionary[list[i]] > 1)
        {
          itemTag = list[i];
          while (dictionary.ContainsKey(++itemTag)) ;
          count += (itemTag - list[i]) * (dictionary[list[i]] - 1);
          dictionary.Add(itemTag, dictionary[list[i]] - 1);
          list.Add(itemTag);
        }
      }

      return count;
    }

    //time limit
    public int Try(int[] A)
    {
      int count = 0, itemTag;

      ISet<int> set = new HashSet<int>();

      for (int i = 0; i < A.Length; i++)
      {
        if (set.Contains(A[i]))
        {
          itemTag = A[i];
          while (set.Contains(++itemTag)) ;

          count += itemTag - A[i];
          set.Add(itemTag);
        }
        else set.Add(A[i]);
      }

      return count;
    }
  }
}