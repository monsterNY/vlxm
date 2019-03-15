using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleTest.LeetCode
{
  /// <summary>
  /// @source:https://leetcode.com/problems/add-binary/
  /// </summary>
  public class AddBinary
  {

    /**
     * Runtime: 88 ms, faster than 92.26% of C# online submissions for Add Binary.
     * Memory Usage: 23.1 MB, less than 57.57% of C# online submissions for Add Binary.
     *
     * 之前有参考过。。。
     *
     */
    public string Solution(string a, string b)
    {
      StringBuilder builder = new StringBuilder();

      int eachIndexA = a.Length - 1, eachIndexB = b.Length - 1, carry = 0, sum;

      for (; eachIndexA >= 0 || eachIndexB >= 0; eachIndexA--, eachIndexB--)
      {
        sum = carry;
        if (eachIndexA >= 0)
        {
          sum += a[eachIndexA] - 48;
        }

        if (eachIndexB >= 0)
        {
          sum += b[eachIndexB] - 48;
        }

        builder.Append(sum % 2);

        carry = sum / 2;
      }

      if (carry > 0)
      {
        builder.Append(carry);
      }

      return new string(builder.ToString().Reverse().ToArray());
    }

    public string OtherSolution(String a, String b)
    {
      StringBuilder sb = new StringBuilder();
      int i = a.Length - 1, j = b.Length - 1, carry = 0;

      var arr = new List<int>();

      while (i >= 0 || j >= 0)
      {
        int sum = carry;
        if (j >= 0) sum += b[j--] - '0';
        if (i >= 0) sum += a[i--] - '0';
        sb.Append(sum % 2);
        arr.Add(sum % 2);
        carry = sum / 2;
      }

      if (carry != 0)
        //        sb.Append(carry);
        arr.Add(carry);

      //      return sb.reverse().toString();

      return new string(sb.ToString().ToCharArray().Reverse().ToArray());
    }
  }
}