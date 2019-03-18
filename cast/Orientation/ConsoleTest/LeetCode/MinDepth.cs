using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.LeetCode
{
  /// <summary>
  /// @desc : MinDepth  
  /// @author :mons
  /// @create : 2019/3/18 10:33:54 
  /// @source : https://leetcode.com/problems/minimum-depth-of-binary-tree/
  /// </summary>
  public class MinDepth
  {

    /**
     * Runtime: 96 ms, faster than 99.40% of C# online submissions for Minimum Depth of Binary Tree.
     * Memory Usage: 24.1 MB, less than 55.56% of C# online submissions for Minimum Depth of Binary Tree.
     *
     * nice~
     *
     */
    public int Solution(TreeNode root)
    {
      return FindDepth(root, 0);
    }

    public int FindDepth(TreeNode node, int nowDepth)
    {
      if (node == null) return nowDepth;
      if (node.left == null && node.right == null) return nowDepth + 1;
      if (node.left == null) return FindDepth(node.right, nowDepth + 1);
      if (node.right == null) return FindDepth(node.left, nowDepth + 1);

      return Math.Min(FindDepth(node.left, nowDepth + 1), FindDepth(node.right, nowDepth + 1));
    }
  }

  public class TreeNode
  {
    public int val;
    public TreeNode left;
    public TreeNode right;

    public TreeNode(int x)
    {
      val = x;
    }

    public TreeNode(int val, TreeNode left, TreeNode right)
    {
      this.val = val;
      this.left = left;
      this.right = right;
    }
  }
}