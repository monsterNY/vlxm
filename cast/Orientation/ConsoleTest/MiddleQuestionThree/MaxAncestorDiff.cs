using System;
using System.Collections.Generic;
using System.Text;
using ConsoleTest.Domain.StructModel;

namespace ConsoleTest.MiddleQuestionThree
{
  /// <summary>
  /// @desc : MaxAncestorDiff  
  /// @author :mons
  /// @create : 2019/4/23 10:03:31 
  /// @source : https://leetcode.com/problems/maximum-difference-between-node-and-ancestor/
  /// </summary>
  public class MaxAncestorDiff
  {
    #region otherSolution

    /**
     * Runtime: 92 ms, faster than 100.00% of C# online submissions for Maximum Difference Between Node and Ancestor.
     * Memory Usage: 22.6 MB, less than 100.00% of C# online submissions for Maximum Difference Between Node and Ancestor.
     */
    public int maxAncestorDiff(TreeNode root)
    {
      return dfs(root, root.val, root.val);
    }

    public int dfs(TreeNode root, int mn, int mx)
    {
      if (root == null) return mx - mn;
      mx = Math.Max(mx, root.val);
      mn = Math.Min(mn, root.val);
      return Math.Max(dfs(root.left, mn, mx), dfs(root.right, mn, mx));
    }

    #endregion

    private int maxDiff;

    /**
     * Runtime: 228 ms, faster than 9.02% of C# online submissions for Maximum Difference Between Node and Ancestor.
     * Memory Usage: 22.8 MB, less than 100.00% of C# online submissions for Maximum Difference Between Node and Ancestor.
     */
    public int Solution(TreeNode root)
    {
      var list = new List<int>() {root.val};
      Helper(root.left, list);
      Helper(root.right, list);
      return maxDiff;
    }

    public void Helper(TreeNode root, List<int> prevList)
    {
      if (root == null) return;

      foreach (var item in prevList) //optimize???
      {
        var diff = Math.Abs(item - root.val);
        if (diff > maxDiff) maxDiff = diff;
      }

      prevList.Add(root.val);

      Helper(root.left, prevList);
      Helper(root.right, prevList);

      prevList.Remove(root.val);
    }
  }
}