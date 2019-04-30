using System;
using System.Collections.Generic;
using System.Text;
using ConsoleTest.Domain.StructModel;

namespace ConsoleTest.HardQuestion
{
  /// <summary>
  /// @desc : RecoverFromPreorder  
  /// @author : mons
  /// @create : 2019/4/30 11:17:04 
  /// @source : https://leetcode.com/problems/recover-a-tree-from-preorder-traversal/
  /// </summary>
  public class RecoverFromPreorder
  {

    /**
     * Runtime: 80 ms, faster than 100.00% of C# online submissions for Recover a Tree From Preorder Traversal.
     * Memory Usage: 24 MB, less than 100.00% of C# online submissions for Recover a Tree From Preorder
     * Traversal.
     *
     * 旗开得胜~
     *
     */
    public TreeNode Solution(string S)
    {
      var i = 0;
      var res = Helper(0, S, ref i);

      return res;
    }

    public TreeNode Helper(int deep, string str, ref int index)
    {
      TreeNode node = null;
      int num = 0, deepLen = 0, firstIndex = index;
      bool hasLeft = false;
      for (; index < str.Length; index++)
      {
        if (str[index] >= '0' && str[index] <= '9')
        {
          if (deepLen != deep)
          {
            index = firstIndex;
            return null;
          }

          num = num * 10 + str[index] - '0';
          if (node == null) node = new TreeNode(num);
          else node.val = num;
        }
        else
        {
          if (node != null)
          {
            if (!hasLeft)
            {
              firstIndex = index;
              node.left = Helper(deep + 1, str, ref index);
              if (node.left == null)
              {
                index = firstIndex - 1;
                return node;
              }

              hasLeft = true;
            }
            else
            {
              firstIndex = index;
              node.right = Helper(deep + 1, str, ref index);
              if (node.right == null)
              {
                index = firstIndex - 1;
              }

              return node;
            }
          }
          else
            deepLen++;
        }
      }

      return node;
    }
  }
}