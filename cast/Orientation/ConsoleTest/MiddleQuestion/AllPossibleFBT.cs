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
  [Obsolete]
  public class AllPossibleFBT
  {

    /// <summary>
    ///
    /// @source:https://leetcode.com/problems/all-possible-full-binary-trees/discuss/163433/Java-Recursive-Solution-with-Explanation
    /// 
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public IList<TreeNode> OtherSolution(int num)
    {
      IList<TreeNode> res = new List<TreeNode>();
      if (num == 1)
      {
        res.Add(new TreeNode(0));
        return res;
      }

      num--;
      for (int i = 1; i < num; i += 2)
      {
        IList<TreeNode> left = OtherSolution(i);
        IList<TreeNode> right = OtherSolution(num - i);
        foreach (TreeNode nl in left)
        {
          foreach (TreeNode nr in right)
          {
            TreeNode cur = new TreeNode(0);
            cur.left = nl;
            cur.right = nr;
            res.Add(cur);
          }
        }
      }

      return res;

    }

    public void RightMergerPossible(int num, TreeNode root, bool flag = true)
    {
      if (num <= 0) return;

      if (num % 2 != 1)
      {
        root.left = 0;
        num--;
      }

      if (num <= 0) return;

      root.right = 0;
      num--;
      RightMergerPossible(num, root.right);
    }

    public TreeNode Copy(TreeNode root)
    {
      if (root == null) return null;

      TreeNode node = root.val;

      node.right = root.left;
      node.left = root.right;

      return node;
    }

    public void LeftMergerPossible(int num, TreeNode root, bool flag = true)
    {
      if (num <= 0) return;

      if (num % 2 != 1)
      {
        root.right = 0;
        num--;
      }

      if (num <= 0) return;

      root.left = 0;
      num--;

      if (flag)
        LeftMergerPossible(num, root.left);
      else
        RightMergerPossible(num, root, false);
    }
  }
}