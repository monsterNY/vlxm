using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using ConsoleTest.Domain.StructModel;
using Tools.CusAttr;
using Tools.CusMenu;

namespace ConsoleTest.MiddleQuestionTwo
{
  /// <summary>
  /// @desc : ZigzagLevelOrder  
  /// @author :mons
  /// @create : 2019/4/11 14:17:00 
  /// @source : https://leetcode.com/problems/binary-tree-zigzag-level-order-traversal/
  /// </summary>
  [Description("tree的之子转换")]
  [Love(LoveTypes.Skill)]
  public class ZigzagLevelOrder
  {
    /**
     * Runtime: 256 ms, faster than 62.73% of C# online submissions for Binary Tree Zigzag Level Order Traversal.
     * Memory Usage: 28.6 MB, less than 64.10% of C# online submissions for Binary Tree Zigzag Level Order Traversal.
     *
     * Runtime: 248 ms, faster than 98.96% of C# online submissions for Binary Tree Zigzag Level Order Traversal.
     * Memory Usage: 28.7 MB, less than 48.72% of C# online submissions for Binary Tree Zigzag Level Order Traversal.
     *
     */
    public IList<IList<int>> Solution(TreeNode root)
    {
      IList<IList<int>> res = new List<IList<int>>();

      GetAllList(root, 0, res);

      return res;
    }

    public void GetAllList(TreeNode root, int deep, IList<IList<int>> res)
    {
      if (root == null) return;

      if (deep == res.Count)
        res.Add(new List<int>());

      if (deep % 2 == 0)
        res[deep].Add(root.val);
      else
        res[deep].Insert(0, root.val);

      GetAllList(root.left, deep + 1, res);
      GetAllList(root.right, deep + 1, res);
    }

    /**
     * Runtime: 248 ms, faster than 98.96% of C# online submissions for Binary Tree Zigzag Level Order Traversal.
     * Memory Usage: 28.6 MB, less than 64.10% of C# online submissions for Binary Tree Zigzag Level Order Traversal.
     *
     * 差不多。。。
     *
     */
    public IList<IList<int>> Solution2(TreeNode root)
    {
      IList<IList<int>> res = new List<IList<int>>();

      GetAllList2(root, 0, res);

      for (int i = 1; i < res.Count; i += 2)
      {
        for (int j = 0; j < res[i].Count / 2; j++)
        {
          var empty = res[i][j];

          res[i][j] = res[i][res[i].Count - 1 - j];
          res[i][res[i].Count - 1 - j] = empty;

        }
      }

      return res;
    }

    public void GetAllList2(TreeNode root, int deep, IList<IList<int>> res)
    {
      if (root == null) return;

      if (deep == res.Count)
        res.Add(new List<int>());

      res[deep].Add(root.val);

      GetAllList(root.left, deep + 1, res);
      GetAllList(root.right, deep + 1, res);
    }
  }
}