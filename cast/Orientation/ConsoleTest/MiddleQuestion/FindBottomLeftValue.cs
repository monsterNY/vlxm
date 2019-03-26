using System;
using System.Collections.Generic;
using System.Text;
using ConsoleTest.Domain.StructModel;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : FindBottomLeftValue  
  /// @author :mons
  /// @create : 2019/3/26 15:56:00 
  /// @source : https://leetcode.com/problems/find-bottom-left-tree-value/
  /// </summary>
  public class FindBottomLeftValue
  {
    private int maxDeep, maxValue, minleftIndex;

    /**
     * Runtime: 96 ms, faster than 100.00% of C# online submissions for Find Bottom Left Tree Value.
     * Memory Usage: 24.3 MB, less than 100.00% of C# online submissions for Find Bottom Left Tree Value.
     *
     * java: 0ms 不解释了-v 神仙操作
     *
     */
    public int Solution(TreeNode root)
    {
      maxDeep = -1;
      GetValue(root, 0, 1);
      return maxValue;
    }

    public void GetValue(TreeNode root, int deep, int leftIndex)
    {
      if (root == null) return;

      if (deep > maxDeep || (deep == maxDeep && minleftIndex > leftIndex))
      {
        maxDeep = deep;
        maxValue = root.val;
        minleftIndex = leftIndex;
      }

      GetValue(root.left, deep + 1, (leftIndex - 1) * 2 + 1);
      GetValue(root.right, deep + 1, (leftIndex - 1) * 2 + 2);
    }
  }
}