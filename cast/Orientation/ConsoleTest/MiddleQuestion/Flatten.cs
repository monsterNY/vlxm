using System;
using System.Collections.Generic;
using System.Text;
using ConsoleTest.Domain.StructModel;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : Flatten  
  /// @author :mons
  /// @create : 2019/4/10 15:21:30 
  /// @source : https://leetcode.com/problems/flatten-binary-tree-to-linked-list/
  /// </summary>
  public class Flatten
  {

    /**
     * Runtime: 96 ms, faster than 93.66% of C# online submissions for Flatten Binary Tree to Linked List.
     * Memory Usage: 23.2 MB, less than 33.33% of C# online submissions for Flatten Binary Tree to Linked List.
     *
     * cool
     *
     */
    public void Solution(TreeNode root)
    {
      var list = new List<TreeNode>();

      GetResult(root, list);

      var node = root;

      for (int i = 1; i < list.Count; i++)
      {
        node.left = null;
        node.right = list[i];
        node = node.right;
      }

    }

    /**
     * no fine~
     */
    public void Solution2(TreeNode root)
    {

      TreeNode node = null;

      GetResult(root, node);

    }

    public void GetResult(TreeNode root, List<TreeNode> list)
    {
      if (root == null) return;

      list.Add(root);

      GetResult(root.left, list);

      GetResult(root.right, list);
    }

    public void GetResult(TreeNode root, TreeNode node)
    {
      if (root == null) return;

      if (node == null) node = root;
      else
      {
        node.right = root;
        node.left = null;
      }

      GetResult(root.left, node);

      GetResult(root.right, node);
    }

  }
}