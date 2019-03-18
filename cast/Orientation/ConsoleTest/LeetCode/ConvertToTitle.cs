using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleTest.LeetCode
{
  /// <summary>
  /// @desc : ConvertToTitle  
  /// @author :mons
  /// @create : 2019/3/18 14:50:08 
  /// @source : https://leetcode.com/problems/excel-sheet-column-title/
  /// </summary>
  public class ConvertToTitle
  {

    /**
     * Runtime: 76 ms, faster than 100.00% of C# online submissions for Excel Sheet Column Title.
     * Memory Usage: 20.4 MB, less than 5.26% of C# online submissions for Excel Sheet Column Title.
     */
    public string Solution(int n)
    {
      if (n <= 0) return string.Empty;

      int remainder;
      List<char> list = new List<char>();

      while (n > 0)
      {
        remainder = n % 26;
        list.Add((char) ((remainder == 0 ? 26 : remainder) + 64));
        n = (remainder == 0 ? n - 26 : n) / 26;
      }

      list.Reverse();
      return new string(list.ToArray());
    }
  }
}