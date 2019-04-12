using System;
using System.Collections.Generic;
using System.Text;
using ConsoleTest.Domain.StructModel;

namespace ConsoleTest.MiddleQuestionTwo
{
  /// <summary>
  /// @desc : BuildTree  
  /// @author :mons
  /// @create : 2019/4/12 12:03:25 
  /// @source : https://leetcode.com/problems/construct-binary-tree-from-preorder-and-inorder-traversal/
  /// </summary>
  [Obsolete]
  public class BuildTree
  {

    #region OtherSolution
    public TreeNode buildTree(int[] preorder, int[] inorder)
    {
      return helper(0, 0, inorder.Length - 1, preorder, inorder);
    }

    public TreeNode helper(int preStart, int inStart, int inEnd, int[] preorder, int[] inorder)
    {
      if (preStart > preorder.Length - 1 || inStart > inEnd)
      {
        return null;
      }
      TreeNode root = new TreeNode(preorder[preStart]);
      int inIndex = 0; // Index of current root in inorder
      for (int i = inStart; i <= inEnd; i++)
      {
        if (inorder[i] == root.val)
        {
          inIndex = i;//计算出左右节点分割点
        }
      }
      root.left = helper(preStart + 1, inStart, inIndex - 1, preorder, inorder);
      root.right = helper(preStart + inIndex - inStart + 1, inIndex + 1, inEnd, preorder, inorder);
      return root;
    }

    #endregion

    public TreeNode Solution(int[] preorder, int[] inorder)
    {
      int i = 0, j = 0;
      return GetNode(preorder, inorder, ref i, ref j);
    }
    
    //no understand
    public TreeNode GetNode(int[] preorder, int[] inorder, ref int index, ref int index2)
    {
      if (index >= preorder.Length) return null;

      var node = new TreeNode(preorder[index]);

      if (preorder[index] == inorder[index2])
      {
        if (index == 0)
        {
          index++;
          node.right = GetNode(preorder, inorder, ref index, ref index2);
          return node;
        }
        else
        {
          index2 = index + 1;
          return node;
        }
      }

      index++;
      node.left = GetNode(preorder, inorder, ref index, ref index2);
      index++;
      node.right = GetNode(preorder, inorder, ref index, ref index2);

      return node;
    }
  }
}