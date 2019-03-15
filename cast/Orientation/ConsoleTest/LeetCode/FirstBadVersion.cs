using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.LeetCode
{
  /// <summary>
  /// @desc : FirstBadVersion  
  /// @author :mons
  /// @create : 2019/3/15 9:39:54 
  /// @source : https://leetcode.com/problems/first-bad-version/
  /// </summary>
  public class FirstBadVersion
  {
    private int randBadVersion = 0;
    Random rand = new Random();
    private int invokCount = 0;

    public int Solution(int n, int? badVersion = null)
    {
      if (badVersion == null)
        randBadVersion = rand.Next(n) + 1;
      else
        randBadVersion = badVersion.Value;

      var middle = (n / 2) + (n % 2 == 1 ? 1 : 0);

      while (!IsBadVersion(middle))
      {
        middle = middle + (n - middle) / 2 + ((n - middle) % 2 == 1 ? 1 : 0);
      }

//      while (!IsBadVersion(middle))
//      {
//        middle = middle + (n - middle) / 2;
//      }
//
      for (middle--; middle >= 1; middle--)
      {
        if (!IsBadVersion(middle))
        {
          Console.WriteLine(invokCount);

          invokCount = 0;
          return middle + 1;
        }
      }

      Console.WriteLine(invokCount);

      invokCount = 0;
      return middle + 1;
    }

    /**
     * Runtime: 36 ms, faster than 100.00% of C# online submissions for First Bad Version.
     * Memory Usage: 12.8 MB, less than 75.00% of C# online submissions for First Bad Version.
     *
     * so cool~
     *
     */
    public int Optimize(int n)
    {
      var start = 0;
      var end = n;
      int middle;

      while (end > start + 1)
      {
        middle = start + (end - start) / 2 + ((end - start) % 2 == 0 ? 0 : 1);

        if (IsBadVersion(middle))
        {
          end = middle;
        }
        else
        {
          start = middle;
        }
      }
      
      return IsBadVersion(start) ? start : end;
    }

    public void CheckResult(int n, int result)
    {
      for (int i = 1; i <= n; i++)
      {
        if (IsBadVersion(i))
        {
          if (i != result)
          {
            throw new Exception("error");
          }

          break;
        }
      }
    }

    public bool IsBadVersion(int version)
    {
      invokCount++;
      return version >= randBadVersion;
    }
  }
}