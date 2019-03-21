using System;
using System.Collections.Generic;
using System.Text;
using ConsoleTest.Domain.StructModel;
using ConsoleTest.LeetCode;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : RangeSumBST  
  /// @author :mons
  /// @create : 2019/3/20 14:26:20 
  /// @source : https://leetcode.com/problems/range-sum-of-bst/
  /// </summary>
  public class RangeSumBST
  {
    /**
     * Runtime: 184 ms, faster than 85.63% of C# online submissions for Range Sum of BST.
     * Memory Usage: 42.2 MB, less than 93.75% of C# online submissions for Range Sum of BST.
     *
     * it's so simple...,
     *
     */
    public int Solution(TreeNode root, int L, int R)
    {
      if (root == null) return 0;

      return (root.val >= L && root.val <= R ? root.val : 0) + Solution(root.left, L, R) + Solution(root.right, L, R);
    }
  }
}