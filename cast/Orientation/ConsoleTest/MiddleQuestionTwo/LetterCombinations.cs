using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestionTwo
{
  /// <summary>
  /// @desc : LetterCombinations  
  /// @author :mons
  /// @create : 2019/4/11 14:36:38 
  /// @source : https://leetcode.com/problems/letter-combinations-of-a-phone-number/
  /// </summary>
  public class LetterCombinations
  {
    /// <summary>
    /// 按键映射
    /// </summary>
    private static string[] arr = new[]
    {
      "abc",
      "def",
      "ghi",
      "jkl",
      "mno",
      "pqrs",
      "tuv",
      "wxyz"
    };

    /**
     * Runtime: 248 ms, faster than 74.73% of C# online submissions for Letter Combinations of a Phone Number.
     * Memory Usage: 29.4 MB, less than 30.12% of C# online submissions for Letter Combinations of a Phone Number.
     */
    public IList<string> Solution(string digits)
    {
      if (digits.Length == 0)
        return new List<string>();

      List<string> list = new List<string>(), res = new List<string>();

      foreach (var digit in digits)
        list.Add(arr[digit - '2']);

      GetList(list, new StringBuilder(), res, 0);

      return res;
    }

    public void GetList(List<string> arr, StringBuilder build, List<string> result, int index)
    {
      if (arr.Count == index)
        result.Add(build.ToString());
      else
      {
        var str = build.ToString();
        foreach (var item in arr[index])
        {
          StringBuilder builder = new StringBuilder(str);
          builder.Append(item);
          GetList(arr, builder, result, index + 1);
        }
      }
    }

    /**
     * Runtime: 244 ms, faster than 97.91% of C# online submissions for Letter Combinations of a Phone Number.
     * Memory Usage: 29.3 MB, less than 46.99% of C# online submissions for Letter Combinations of a Phone Number.
     *
     * nice~
     *
     */
    public IList<string> Solution2(string digits)
    {
      if (digits.Length == 0)
        return new List<string>();

      List<string> res = new List<string>();

      GetList(digits, new StringBuilder(), res, 0);

      return res;
    }

    public void GetList(string str, StringBuilder build, List<string> result, int index)
    {
      if (str.Length == index)
        result.Add(build.ToString()); //当遍历完成后添加str
      else
      {
        var buildStr = build.ToString();
        foreach (var item in arr[str[index] - '2'])
        {
          StringBuilder builder = new StringBuilder(buildStr);
          builder.Append(item);
          GetList(str, builder, result, index + 1);
        }
      }
    }

    /**
     * Runtime: 244 ms, faster than 97.91% of C# online submissions for Letter Combinations of a Phone Number.
     * Memory Usage: 29.3 MB, less than 62.65% of C# online submissions for Letter Combinations of a Phone Number.
     *
     * 差不多
     *
     */
    public void GetList(string str, string build, List<string> result, int index)
    {
      if (str.Length == index)
        result.Add(build); //当遍历完成后添加str
      else
      {
        foreach (var item in arr[str[index] - '2'])
        {
          GetList(str, build + item, result, index + 1);
        }
      }
    }

    public IList<string> Clear(string digits)
    {
      return Clear(digits, string.Empty, new List<string>(), 0);
    }

    public IList<string> Clear(string str, string build, IList<string> result, int index)
    {
      if (str.Length == 0) return result;

      if (str.Length == index)
        result.Add(build); //当遍历完成后添加str
      else
        foreach (var item in arr[str[index] - '2'])
          Clear(str, build + item, result, index + 1);

      return result;
    }
  }
}