using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ConsoleTest.LeetCode
{
  /// <summary>
  /// @desc : RepeatedSubstringPattern  
  /// @author :mons
  /// @create : 2019/3/14 11:08:31 
  /// @source : https://leetcode.com/problems/repeated-substring-pattern/
  /// </summary>
  public class RepeatedSubstringPattern
  {
    /**
     * Runtime: 540 ms, faster than 14.47% of C# online submissions for Repeated Substring Pattern.
     * Memory Usage: 31.3 MB, less than 50.00% of C# online submissions for Repeated Substring Pattern.
     *
     *  so bad =.
     *
     */
    public bool Solution(string s)
    {
      List<char> arr = new List<char>();

      arr.Add(s[0]);

      int startIndex = 1;

      for (int i = 1; i < s.Length;)
      {
        for (int j = 0; j < arr.Count; j++, i++)
        {
          if (i == s.Length)
          {
            return false;
          }

          if (arr[j] != s[i])
          {
            arr.Add(s[startIndex++]);
            i = startIndex;
            break;
          }
        }
      }

      CheckResult(s, arr, (s.Length % arr.Count == 0 && s.Length != arr.Count));

      return s.Length % arr.Count == 0 && s.Length != arr.Count;
    }

    public void CheckResult(string s, List<char> arr, bool result)
    {
      if (result)
      {
        StringBuilder builder = new StringBuilder();


        var subArr = arr.ToArray();

        for (int i = 0; i < s.Length / arr.Count; i++)
        {
          builder.Append(new string(subArr));
        }

        if (!builder.ToString().Equals(s))
          throw new Exception(s);

        Console.WriteLine($"subStr:{new string(subArr)}");
      }
      else
      {
        var list = new List<char>();

        var builder = new StringBuilder();

        for (int i = 0; i < s.Length; i++)
        {
          list.Add(s[i]);
          builder = new StringBuilder();
          if (s.Length % list.Count == 0 && s.Length != list.Count)
          {
            var subArr = list.ToArray();

            for (int j = 0; j < s.Length / list.Count; j++)
            {
              builder.Append(new string(subArr));
            }

            if (builder.ToString().Equals(s))
              throw new Exception(s);
          }
        }
      }
    }

    /**
     * Runtime: 132 ms, faster than 64.47% of C# online submissions for Repeated Substring Pattern.
     * Memory Usage: 42.5 MB, less than 50.00% of C# online submissions for Repeated Substring Pattern.
     *
     * Runtime: 112 ms, faster than 73.81% of C# online submissions for Repeated Substring Pattern.
     * Memory Usage: 33.5 MB, less than 50.00% of C# online submissions for Repeated Substring Pattern.
     *
     * 使用span后稍微提升了一点。，，，，
     *
     */
    public bool Optimize(string str)
    {
      int l = str.Length;
      var onlySpan = str.AsSpan();
      for (int i = l / 2; i >= 1; i--)
      {
        if (l % i == 0)
        {
          int m = l / i;
          var slice = onlySpan.Slice(0, i).ToString();
          var flag = false;
          for (int j = 0; j < m; j++)
          {
            if (!onlySpan.Slice(((j * i)), i).ToString().Equals(slice))
            {
              flag = true;
              break;
            }
          }

          if (!flag)
            return true;
        }
      }

      return false;
    }

    /**
     * Runtime: 128 ms, faster than 67.11% of C# online submissions for Repeated Substring Pattern.
     * Memory Usage: 42.4 MB, less than 50.00% of C# online submissions for Repeated Substring Pattern.
     *
     * 还是比较差 ， 不过这不就是我的测试方法吗=-=
     *
     */
    public bool OtherSolution(String str)
    {
      int l = str.Length;
      for (int i = l / 2; i >= 1; i--)
      {
        if (l % i == 0)
        {
          int m = l / i;
          String subS = str.Substring(0, i);
          StringBuilder sb = new StringBuilder();
          for (int j = 0; j < m; j++)
          {
            sb.Append(subS);
          }

          if (sb.ToString().Equals(str)) return true;
        }
      }

      return false;
    }
  }
}