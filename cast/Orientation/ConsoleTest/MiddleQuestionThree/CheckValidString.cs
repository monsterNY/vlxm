using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestionThree
{
  /// <summary>
  /// @desc : CheckValidString  
  /// @author : mons
  /// @create : 2019/4/24 14:12:00 
  /// @source : https://leetcode.com/problems/valid-parenthesis-string/
  /// </summary>
  public class CheckValidString
  {

    /**
     * Runtime: 80 ms, faster than 68.69% of C# online submissions for Valid Parenthesis String.
     * Memory Usage: 19.7 MB, less than 71.43% of C# online submissions for Valid Parenthesis String.
     *
     * Runtime: 72 ms, faster than 94.95% of C# online submissions for Valid Parenthesis String.
     * Memory Usage: 19.8 MB, less than 42.86% of C# online submissions for Valid Parenthesis String.
     *
     */
    public bool Solution(string s)
    {
      int left = 0, any = 0,needRight = 0;

      foreach (var c in s)
      {
        if (c == '(')
        {
          left++;
          needRight++;
        }
        else if (c == '*')
        {
          any++;
          if (needRight > 0)
            needRight--;
        }
        else
        {
          if (left > 0)
          {
            left--;
            if (needRight > 0)
              needRight--;
          }
          else if (any > 0) any--;
          else return false;
        }
      }

      return needRight == 0;
    }
  }
}