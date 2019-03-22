using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : MinAddToMakeValid  
  /// @author :mons
  /// @create : 2019/3/22 10:39:50 
  /// @source : https://leetcode.com/problems/minimum-add-to-make-parentheses-valid/
  /// </summary>
  public class MinAddToMakeValid
  {
    /**
     * error 1: ((())
     *
     * Runtime: 72 ms, faster than 98.80% of C# online submissions for Minimum Add to Make Parentheses Valid.
     * Memory Usage: 19.8 MB, less than 90.00% of C# online submissions for Minimum Add to Make Parentheses Valid.
     *
     * ... 这反差也太大了吧  一下超级难 一下这样=。
     *
     *
     */
    public int Solution(string S)
    {
      var count = 0;

      int needCount = 0;

      for (int i = 0; i < S.Length; i++)
      {
        if (S[i] == '(')
        {
          needCount++;
        }
        else
        {
          if (needCount > 0) needCount--;
          else count++;
        }
      }

      return count + needCount;
    }
  }
}