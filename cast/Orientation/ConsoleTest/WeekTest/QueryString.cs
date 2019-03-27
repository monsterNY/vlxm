using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.WeekTest
{
  /// <summary>
  /// @desc : QueryString  
  /// @author :mons
  /// @create : 2019/3/27 11:30:03 
  /// @source : https://leetcode.com/contest/weekly-contest-129/problems/binary-string-with-substrings-representing-1-to-n/
  /// </summary>
  public class QueryString
  {

    /**
     *
     * Runtime: 72 ms, faster than 100.00% of C# online submissions for Binary String With Substrings Representing 1 To N.
     * Memory Usage: 19.7 MB, less than 100.00% of C# online submissions for Binary String With Substrings Representing 1 To N.
     *
     * Runtime: 72 ms
     * Memory Usage: 19.8 MB
     */
    public bool Solution(string S, int N)
    {
      ReadOnlySpan<char> str, span = S.AsSpan(), itemSpan;
      bool flag = false;
      for (int j = 1; j <= N; j++)
      {
        str = Convert.ToString(j, 2).AsSpan();
        for (int i = 0; i <= S.Length - str.Length; i++)
        {
          flag = true;
          itemSpan = span.Slice(i, str.Length);
          for (int k = 0; k < str.Length; k++)
          {
            if (str[k] != itemSpan[k])
            {
              flag = false;
              break;
            }
          }

          if (flag) break;
        }

        if (!flag) return false;
      }


      return true;
    }
  }
}