using System;
using System.Collections.Generic;
using System.Text;
using ConsoleTest.Domain.StructModel;

namespace ConsoleTest.MiddleQuestionTwo
{
  /// <summary>
  /// @desc : LowestCommonAncestor  
  /// @author :mons
  /// @create : 2019/4/18 9:55:41 
  /// @source : https://leetcode.com/problems/lowest-common-ancestor-of-a-binary-tree/
  /// </summary>
  [Obsolete]
  public class LowestCommonAncestor
  {

    //great
    public TreeNode OtherSolution(TreeNode root, TreeNode p, TreeNode q)
    {
      if (root == null || root == p || root == q) return root;
      TreeNode left = OtherSolution(root.left, p, q);
      TreeNode right = OtherSolution(root.right, p, q);
      return left == null ? right : right == null ? left : root;
    }

    public TreeNode Solution(TreeNode root, TreeNode p, TreeNode q)
    {
      List<TreeNode[]> list = new List<TreeNode[]>();
      List<bool[]> flag = new List<bool[]>(), flag2 = new List<bool[]>();

      Helper(root, list, flag, flag2, 0, 0, p, q);

      for (int i = 0; i < list.Count; i++)
      {
        for (int j = 0; j < list[i].Length; j++)
        {
          if (list[i][j] != null && flag[i][j] && flag2[i][j]) root = list[i][j];
        }
      }

      return root;
    }

    public void Helper(TreeNode root, List<TreeNode[]> list, List<bool[]> flag, List<bool[]> flag2, int index, int deep,
      TreeNode p,
      TreeNode q)
    {
      if (root == null) return;

      if (list.Count == deep)
      {
        list.Add(new TreeNode[1 << deep]);
        flag.Add(new bool[1 << deep]);
        flag2.Add(new bool[1 << deep]);//此处溢出。。。
      }

      if (root.val == p.val)
      {
        flag[deep][index] = true;
        RefreshTop(deep, index, list, flag);
      }

      if (root.val == q.val)
      {
        flag2[deep][index] = true;
        RefreshTop(deep, index, list, flag2);
      }

      list[deep][index] = root;

      Helper(root.left, list, flag, flag2, index * 2, deep + 1, p, q);
      Helper(root.right, list, flag, flag2, index * 2 + 1, deep + 1, p, q);
    }

    public void RefreshTop(int i, int j, List<TreeNode[]> list, List<bool[]> flag)
    {
      for (i--, j /= 2; i >= 0; i--, j /= 2)
      {
        if (flag[i][j]) return;
        flag[i][j] = true;
      }
    }
  }
}