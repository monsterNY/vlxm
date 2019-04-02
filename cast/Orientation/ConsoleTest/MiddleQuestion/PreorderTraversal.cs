using System;
using System.Collections.Generic;
using System.Text;
using ConsoleTest.Domain.StructModel;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : PreorderTraversal  
  /// @author :mons
  /// @create : 2019/4/2 15:35:07 
  /// @source : https://leetcode.com/problems/binary-tree-preorder-traversal/
  /// </summary>
  public class PreorderTraversal
  {

    /**
     * Runtime: 260 ms, faster than 60.45% of C# online submissions for Binary Tree Preorder Traversal.
     * Memory Usage: 28 MB, less than 58.69% of C# online submissions for Binary Tree Preorder Traversal.
     *
     * Runtime: 244 ms, faster than 97.77% of C# online submissions for Binary Tree Preorder Traversal.
     * Memory Usage: 28 MB, less than 54.35% of C# online submissions for Binary Tree Preorder Traversal.
     *
     * java 0ms =-= 还是多提交几次
     * Runtime: 0 ms, faster than 100.00% of Java online submissions for Binary Tree Preorder Traversal.
     * Memory Usage: 36.3 MB, less than 16.01% of Java online submissions for Binary Tree Preorder Traversal.
     *
     * so simple ...
     *
     */
    public IList<int> Solution(TreeNode root)
    {
      IList<int> result = new List<int>();

      GetAllValue(root, result);

      return result;
    }

    public void GetAllValue(TreeNode node, IList<int> list)
    {
      if (node == null) return;
      list.Add(node.val);

      GetAllValue(node.left, list);
      GetAllValue(node.right, list);
    }
  }
}