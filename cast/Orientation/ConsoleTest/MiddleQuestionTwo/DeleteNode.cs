﻿using System;
using System.Collections.Generic;
using System.Text;
using ConsoleTest.Domain.StructModel;

namespace ConsoleTest.MiddleQuestionTwo
{
  /// <summary>
  /// @desc : DeleteNode  
  /// @author :mons
  /// @create : 2019/4/12 17:15:59 
  /// @source : https://leetcode.com/problems/delete-node-in-a-bst/
  /// </summary>
  [Obsolete("BST tree")]
  public class DeleteNode
  {

    public TreeNode deleteNode(TreeNode root, int key)
    {
      if (root == null)
      {
        return null;
      }
      if (key < root.val)
      {
        root.left = deleteNode(root.left, key);
      }
      else if (key > root.val)
      {
        root.right = deleteNode(root.right, key);
      }
      else
      {
        if (root.left == null)
        {
          return root.right;
        }
        else if (root.right == null)
        {
          return root.left;
        }

        TreeNode minNode = findMin(root.right);
        root.val = minNode.val;
        root.right = deleteNode(root.right, root.val);
      }
      return root;
    }

    private TreeNode findMin(TreeNode node)
    {
      while (node.left != null)
      {
        node = node.left;
      }
      return node;
    }

  }
}