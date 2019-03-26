using System;
using System.Collections.Generic;
using System.Text;
using ConsoleTest.Domain.StructModel;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : InsertIntoMaxTree  
  /// @author :monster_yj
  /// @create : 2019/3/23 12:47:25 
  /// @source : https://leetcode.com/problems/maximum-binary-tree-ii/
  /// </summary>
  [Obsolete("don't understand")]
  public class InsertIntoMaxTree
  {

    public TreeNode Solution(TreeNode root, int val)
    {
      if (root == null)
        return null;

      if (root.val <= val)
      {
        TreeNode node = new TreeNode(val);
        node.left = root;
        return node;
      }

      if((root.left = Solution(root.left, val)) == null)
        root.right = Solution(root.right, val);

      return root;

    }

  }
}
