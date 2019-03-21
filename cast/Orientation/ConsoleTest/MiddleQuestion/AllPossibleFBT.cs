using System;
using System.Collections.Generic;
using System.Text;
using ConsoleTest.Domain.StructModel;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : AllPossibleFBT  
  /// @author :mons
  /// @create : 2019/3/21 17:37:55 
  /// @source : https://leetcode.com/problems/all-possible-full-binary-trees/
  /// </summary>
  public class AllPossibleFBT
  {
    public IList<TreeNode> Solution(int N)
    {
      IList<TreeNode> list = new List<TreeNode>();


      return list;
    }

    public TreeNode FindPossible(int num,T)
    {
      if (num % 2 == 0)
      {
        TreeNode node = 0;
        node.left = FindPossible(num - 1);

        TreeNode rightNode = 0;
        rightNode.right = FindPossible(num - 1);
      }

      if (num > 2)
      {
        TreeNode node = 0;
        node.left = 1;
        node.right = 1;

        node.left.left = FindPossible(num - 3);
        node.right.left
      }

      return num == 0 ? null : new TreeNode(0);
    }
  }
}