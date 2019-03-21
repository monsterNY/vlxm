using System;
using System.Collections.Generic;
using System.Text;
using ConsoleTest.Domain.StructModel;
using ConsoleTest.LeetCode;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : InsertIntoBST  
  /// @author :mons
  /// @create : 2019/3/20 16:20:52 
  /// @source : https://leetcode.com/problems/insert-into-a-binary-search-tree/
  ///
  /// 可能没太理解
  /// 
  /// </summary>
  [Obsolete]
  public class InsertIntoBST
  {
    public TreeNode Solution(TreeNode root, int val)
    {
      if (root == null) return null;

      if (root.val > val)
      {
        if (root.left != null)
        {
          if (root.left.val < val)
          {
            var insertNode = new TreeNode(val);

            insertNode.left = root.left;
            root.left = insertNode;
            return root;
          }
        }
        else
        {
          root.left = new TreeNode(val);
          return root;
        }
      }
      else
      {
        if (root.right != null)
        {
          if (root.right.val > val)
          {
            var insertNode = new TreeNode(val);
            insertNode.right = root.right;
            root.right = insertNode;
            return root;
          }
        }
        else
        {
          root.right = new TreeNode(val);
          return root;
        }
      }

      if (Solution(root.left, val) != null) return root;

      return Solution(root.right, val);
    }
  }
}