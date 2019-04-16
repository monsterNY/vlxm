using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestionTwo
{
  /// <summary>
  /// @desc : IsValidSerialization  
  /// @author :mons
  /// @create : 2019/4/16 15:56:19 
  /// @source : https://leetcode.com/problems/verify-preorder-serialization-of-a-binary-tree/
  /// </summary>
  public class IsValidSerialization
  {
    /**
     * Runtime: 72 ms, faster than 100.00% of C# online submissions for Verify Preorder Serialization of a Binary Tree.
     * Memory Usage: 20.4 MB, less than 100.00% of C# online submissions for Verify Preorder Serialization of a Binary Tree.
     *
     * nice!
     *
     */
    public bool Solution(string preorder)
    {
      var index = 0;
      Helper(preorder, ref index);
      return index == preorder.Length - 1;
    }

    public bool Helper(string str, ref int index)
    {
      if (index >= str.Length) return false;

      if (str[index] >= '0' && str[index] <= '9')
      {
        index++;
        while (index < str.Length && str[index] >= '0' && str[index] <= '9')
        {
          index++;
        }

        index++;
        if (!Helper(str, ref index)) return false;
        index += 2;
        if (!Helper(str, ref index)) return false;
      }

      return true;
    }
  }
}