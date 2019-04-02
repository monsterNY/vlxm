using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleTest.Domain.StructModel;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : KthSmallest  
  /// @author :mons
  /// @create : 2019/4/2 15:58:07 
  /// @source : https://leetcode.com/problems/kth-smallest-element-in-a-bst/
  /// </summary>
  
  public class KthSmallest
  {

    //source : https://leetcode.com/problems/kth-smallest-element-in-a-bst/discuss/63660/3-ways-implemented-in-JAVA-(Python)%3A-Binary-Search-in-order-iterative-and-recursive
    #region OtherSolution

    /**
     * Runtime: 104 ms, faster than 88.86% of C# online submissions for Kth Smallest Element in a BST.
     * Memory Usage: 26.1 MB, less than 52.00% of C# online submissions for Kth Smallest Element in a BST.
     */
    public int kthSmallest(TreeNode root, int k)
    {
      int count = countNodes(root.left);//获取此节点的所有节点数
      if (k <= count)//结合二叉树的特征  左子节点大于当前节点值  右子节点大于当前节点值
      {
        return kthSmallest(root.left, k);
      }
      else if (k > count + 1)
      {
        return kthSmallest(root.right, k - 1 - count); // 1 is counted as current node
      }

      return root.val;
    }

    public int countNodes(TreeNode n)
    {
      if (n == null) return 0;

      return 1 + countNodes(n.left) + countNodes(n.right);
    }

    #endregion

    /**
     * try1:
     * Runtime: 600 ms, faster than 5.16% of C# online submissions for Kth Smallest Element in a BST.
     * Memory Usage: 26.2 MB, less than 20.00% of C# online submissions for Kth Smallest Element in a BST.
     *
     * .., rookie ha ha
     *
     * try2:
     * Runtime: 108 ms, faster than 67.66% of C# online submissions for Kth Smallest Element in a BST.
     * Memory Usage: 26.8 MB, less than 8.00% of C# online submissions for Kth Smallest Element in a BST.
     *
     * 测试有毒 一堆 108
     *
     */
    public int Solution(TreeNode root, int k)
    {
      List<int> list = new List<int>();

      //GetAllValue(root, list);
      GetAllValue2(root, list, 0, k);

      int min = 0;
      while (k > 0)
      {
        min = list[0];
        for (int i = 1; i < list.Count; i++)
        {
          if (list[i] < min) min = list[i];
        }

        list.Remove(min);
        k--;
      }

      return min;

      //list.Sort();
      //return list.OrderBy(u => u).Skip(k - 1).Take(1).First();
      //return list[k - 1];
    }

    public void GetAllValue2(TreeNode root, List<int> list, int max, int limit)
    {
      if (root == null) return;

      if (list.Count == limit)
      {
        if (root.val < max) list.Add(root.val);
      }
      else
      {
        list.Add(root.val);
        if (root.val > max) max = root.val;
      }

      GetAllValue2(root.left, list, max, limit);
      GetAllValue2(root.right, list, max, limit);
    }

    public void GetAllValue(TreeNode root, IList<int> list)
    {
      if (root == null) return;
      bool flag = false;
      for (int i = 0; i < list.Count; i++)
      {
        if (list[i] > root.val)
        {
          list.Insert(i, root.val);
          flag = true;
          break;
        }
      }

      if (!flag) list.Add(root.val);

      GetAllValue(root.left, list);
      GetAllValue(root.right, list);
    }
  }
}