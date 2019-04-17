using System;
using System.Collections.Generic;
using System.Text;
using ConsoleTest.Domain;

namespace ConsoleTest.MiddleQuestionTwo
{
  /// <summary>
  /// @desc : Connect  
  /// @author :mons
  /// @create : 2019/4/17 11:26:30 
  /// @source : https://leetcode.com/problems/populating-next-right-pointers-in-each-node/
  /// </summary>
  public class Connect
  {

    /**
     * Runtime: 164 ms, faster than 46.80% of C# online submissions for Populating Next Right Pointers in Each Node.
     * Memory Usage: 26.5 MB, less than 66.67% of C# online submissions for Populating Next Right Pointers in Each Node.
     */
    public Node Solution(Node root)
    {
      Helper(root, 0, new List<List<Node>>());

      return root;
    }

    public void Helper(Node node, int deep, List<List<Node>> list)
    {
      if (node == null)
        return;

      if (list.Count == deep)
        list.Add(new List<Node>());

      list[deep].Add(node);

      for (int i = list[deep].Count - 2; i >= 0; i--)
      {
        list[deep][i].next = list[deep][i + 1];
      }

      Helper(node.left, deep + 1, list);
      Helper(node.right, deep + 1, list);
    }
  }
}