using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleTest.Domain.StructModel;

namespace ConsoleTest.MiddleQuestionThree
{
  /// <summary>
  /// @desc : VerticalTraversal  
  /// @author : mons
  /// @create : 2019/4/24 14:47:19 
  /// @source : https://leetcode.com/problems/vertical-order-traversal-of-a-binary-tree/
  /// </summary>
  public class VerticalTraversal
  {

    /**
     * Runtime: 272 ms, faster than 72.36% of C# online submissions for Vertical Order Traversal of a Binary Tree.
     * Memory Usage: 28.8 MB, less than 100.00% of C# online submissions for Vertical Order Traversal of a Binary Tree.
     *
     * Runtime: 252 ms, faster than 100.00% of C# online submissions for Vertical Order Traversal of a Binary Tree.
     * Memory Usage: 28.9 MB, less than 100.00% of C# online submissions for Vertical Order Traversal of a Binary Tree.
     *
     */
    public IList<IList<int>> Solution(TreeNode root)
    {
      IList<IList<int[]>> negativeList = new List<IList<int[]>>(), positiveList = new List<IList<int[]>>();

      Helper(root, 0, 0, negativeList, positiveList);


      IList<IList<int>> res = new List<IList<int>>();

      //从左到右进行报告
      for (int i = negativeList.Count - 1; i >= 0; i--)
      {
        res.Add(new List<int>());

        foreach (var arr in negativeList[i])
        {
          res[res.Count - 1].Add(arr[1]);
        }
      }

      for (int i = 0; i < positiveList.Count; i++)
      {
        res.Add(new List<int>());

        foreach (var arr in positiveList[i])
        {
          res[res.Count - 1].Add(arr[1]);
        }
      }

      return res;
    }

    public void Helper(TreeNode node, int index, int deep, IList<IList<int[]>> negativeList,
      IList<IList<int[]>> positiveList)
    {
      if (node == null)
        return;

      IList<IList<int[]>> list;

      var abs = Math.Abs(index);

      if (index >= 0)
      {
        list = positiveList;
      }
      else
      {
        list = negativeList;
        abs--;
      }

      if (abs == list.Count)
      {
        list.Add(new List<int[]>());
      }

      int i = list[abs].Count - 1;
      for (; i >= 0; i--)
      {
        //按从上到下的顺序
        //如果两个节点具有相同的位置，那么首先报告的节点的值就是较小的值。
        if (list[abs][i][0] > deep || (list[abs][i][0] == deep && list[abs][i][1] < node.val)) break;
      }

      list[abs].Insert(i + 1, new[] {deep, node.val});

      Helper(node.left, index - 1, deep - 1, negativeList, positiveList);
      Helper(node.right, index + 1, deep - 1, negativeList, positiveList);
    }

    public IList<IList<int>> Try(TreeNode root)
    {
      IList<IList<int>> negativeList = new List<IList<int>>(), positiveList = new List<IList<int>>();

      Helper(root, 0, 0, negativeList, positiveList);

      negativeList = negativeList.Reverse().ToList();
      (negativeList as List<IList<int>>).AddRange(positiveList);
      return negativeList;
    }

    public void Helper(TreeNode node, int index, int deep, IList<IList<int>> negativeList,
      IList<IList<int>> positiveList)
    {
      if (node == null)
        return;

      IList<IList<int>> list;

      var abs = Math.Abs(index);

      if (index >= 0)
      {
        list = positiveList;
      }
      else
      {
        list = negativeList;
        abs--;
      }

      if (abs == list.Count)
      {
        list.Add(new List<int>());
      }

      list[abs].Add(node.val); //如何确定此处的位置

      Helper(node.left, index - 1, deep - 1, negativeList, positiveList);
      Helper(node.right, index + 1, deep - 1, negativeList, positiveList);
    }
  }
}