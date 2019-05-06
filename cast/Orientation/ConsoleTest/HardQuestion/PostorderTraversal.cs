using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using ConsoleTest.Domain.StructModel;

namespace ConsoleTest.HardQuestion
{
  /// <summary>
  /// @desc : PostorderTraversal  
  /// @author : mons
  /// @create : 2019/5/6 11:17:35 
  /// @source : https://leetcode.com/problems/binary-tree-postorder-traversal/
  /// </summary>
  public class PostorderTraversal
  {

    /**
     * Runtime: 252 ms, faster than 51.42% of C# online submissions for Binary Tree Postorder Traversal.
     * Memory Usage: 28.3 MB, less than 6.35% of C# online submissions for Binary Tree Postorder Traversal.
     *
     * Runtime: 252 ms, faster than 51.42% of C# online submissions for Binary Tree Postorder Traversal.
     * Memory Usage: 28.1 MB, less than 31.75% of C# online submissions for Binary Tree Postorder Traversal.
     *
     * same fast...
     *
     */
    public IList<int> Solution(TreeNode root) //iteratively
    {

      List<int> res = new List<int>();

      if (root == null) return res;

      Stack<TreeNode> stack = new Stack<TreeNode>();
      stack.Push(root);
      while (stack.Count != 0)
      {
        var node = stack.Pop();

        if (node == null) continue;
        res.Add(node.val);

        stack.Push(node.left);
        stack.Push(node.right);
      }
      
      res.Reverse();
      return res;
    }

    /**
     * Runtime: 252 ms, faster than 51.42% of C# online submissions for Binary Tree Postorder Traversal.
     * Memory Usage: 28.3 MB, less than 6.35% of C# online submissions for Binary Tree Postorder Traversal.
     */
    public IList<int> Simple(TreeNode root) //Recursive 
    {
      List<int> res = new List<int>();

      Helper(root, res);

      return res;
    }

    public void Helper(TreeNode node, List<int> res)
    {
      if (node == null) return;

      Helper(node.left, res);
      Helper(node.right, res);

      res.Add(node.val);
    }
  }
}