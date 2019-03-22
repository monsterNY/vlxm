using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.Domain.StructModel
{
  /// <summary>
  /// @desc : TreeNode  
  /// @author :mons
  /// @create : 2019/3/21 16:44:42 
  /// @source : 
  /// </summary>
  public class TreeNode
  {
    public int val;
    public TreeNode left;
    public TreeNode right;

    public TreeNode(int x)
    {
      val = x;
    }

    public static implicit operator TreeNode(int num)
    {
      return new TreeNode(num);
    }

    public TreeNode(int val, TreeNode left, TreeNode right)
    {
      this.val = val;
      this.left = left;
      this.right = right;
    }

    public void ShowNode(TreeNode node, string prefix = "root")
    {
      if (node == null) return;

      Console.WriteLine($"{prefix} -- {node.val}");

      ShowNode(node.left, "left");

      ShowNode(node.right, "right");
    }
  }
}