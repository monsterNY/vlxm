using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : GenerateParenthesis  
  /// @author :mons
  /// @create : 2019/3/29 14:09:59 
  /// @source : https://leetcode.com/problems/generate-parentheses/
  /// </summary>
  public class GenerateParenthesis
  {
    public IList<string> Solution(int n)
    {
      if (n <= 0) return new List<string>();
      if (n == 1) return new List<string>() {"()"};

      var list = new List<string>();

      //GetList(n, string.Empty, string.Empty, list);

      GetList(string.Empty, n, n, list);

      return list;
    }

    // container bug
    public void GetList(int n, string prefix, string suffix, IList<string> result, bool canContain = false)
    {
      if (n == 1)
      {
        result.Add($"{prefix}(){suffix}");
        if (canContain)
          result.Add($"{prefix})({suffix}");
        return;
      }

      GetList(n - 1, prefix + "()", suffix, result, canContain);
      GetList(n - 1, prefix, suffix + "()", result, canContain);
      GetList(n - 1, prefix + "(", suffix + ")", result, true);
    }

    /**
     * Runtime: 244 ms, faster than 95.00% of C# online submissions for Generate Parentheses.
     * Memory Usage: 31 MB, less than 65.57% of C# online submissions for Generate Parentheses.
     *
     * good
     */
    public void GetList(string str, int left, int right, IList<string> result)
    {
      if (left < 0 || right < 0 || right < left) return;
      if (left == 0 && right == 0)
      {
        result.Add($"{str}");
        return;
      }

      GetList(str + "(", left - 1, right, result);
      //if(right>left) same idea.
      GetList(str + ")", left, right - 1, result);
    }
  }
}