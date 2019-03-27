using System;
using System.Collections.Generic;
using System.Text;
using ConsoleTest.Domain.StructModel;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : LargestValues  
  /// @author :mons
  /// @create : 2019/3/27 17:00:17 
  /// @source : https://leetcode.com/problems/find-largest-value-in-each-tree-row/
  /// </summary>
  public class LargestValues
  {
    private IList<int> result = new List<int>();

    /**
     * Runtime: 256 ms, faster than 81.25% of C# online submissions for Find Largest Value in Each Tree Row.
     * Memory Usage: 30.6 MB, less than 77.78% of C# online submissions for Find Largest Value in Each Tree Row.
     *
     * 还真是一题简单一题难。。。。
     *
     */
    public IList<int> Solution(TreeNode root)
    {
      result.Clear();

      InMax(root, 0);

      return result;
    }

    public void InMax(TreeNode root, int deep)
    {
      if (root == null) return;

      if (result.Count == deep) result.Add(root.val);
      else if (result[deep] < root.val) result[deep] = root.val;

      InMax(root.left, deep + 1);
      InMax(root.right, deep + 1);
    }

  }
}