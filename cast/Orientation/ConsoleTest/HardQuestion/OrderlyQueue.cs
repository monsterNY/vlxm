using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleTest.HardQuestion
{
  /// <summary>
  /// @desc : OrderlyQueue  
  /// @author : mons
  /// @create : 2019/5/29 17:21:49 
  /// @source : https://leetcode.com/problems/orderly-queue/
  /// </summary>
  public class OrderlyQueue
  {
    //1 <= K <= S.length <= 1000
    //S consists of lowercase letters only.
    /**
     * Runtime: 96 ms, faster than 93.33% of C# online submissions for Orderly Queue.
     * Memory Usage: 21.5 MB, less than 87.50% of C# online submissions for Orderly Queue.
     *
     * 发现规律，有点取巧...
     *
     */
    public string Solution(string S, int K)
    {
      if (K == 1)
      {
        /*
         * plan a:
         *
         * //cbfdahosifhgoiaafsdjfoipuawopeiufjooaisnvaurpeoiw
         *
         * res:error
        int len = S.Length,min = 0;

        for (int i = 1; i < len; i++)
          if (S[i] < S[min]) min = i;

        var span = S.AsSpan();

        return $"{span.Slice(min, len - min).ToString()}{span.Slice(0, min).ToString()}";*/

        var arr = new List<List<int>>();

        for (int i = 0; i < 26; i++)
        {
          arr.Add(new List<int>());
        }

        int len = S.Length, min = 26;

        for (int i = 0; i < len; i++)
        {
          var index = S[i] - 'a';

          if (index > min) continue;

          min = index;

          arr[index].Add(i);
        }

        for (int i = 0; i < 26; i++)
        {
          if (arr[i].Count > 0)
          {
            var start = arr[i][0];

            for (int j = 1; j < arr[i].Count; j++)
            {
              var add = 1;
              while (start + add < len && arr[i][j] + add < len)
              {
                if (S[start + add] > S[arr[i][j] + add])
                {
                  start = arr[i][j];
                  break;
                }

                if (S[start + add] < S[arr[i][j] + add])
                  break;

                add++;
              }
            }

            var span = S.AsSpan();
            return $"{span.Slice(start, len - start).ToString()}{span.Slice(0, start).ToString()}";
          }
        }

        return string.Empty;
      }

      return new string(S.ToCharArray().OrderBy(u => u).ToArray());
    }


    public string Optimize(string S, int K)
    {
      if (K == 1)
      {
        var lenArr = new int[26];

        int len = S.Length, min = 26;

        for (int i = 0; i < len; i++)
          lenArr[S[i] - 'a']++;

        for (int i = 0; i < 26; i++)
        {
          if (lenArr[i] > 0)
          {
          }
        }

        return string.Empty;
      }
      else
      {
        return new string(S.ToCharArray().OrderBy(u => u).ToArray());
      }
    }
  }
}