using System;
using System.Collections.Generic;
using System.Text;
using ConsoleTest.Domain.StructModel;

namespace ConsoleTest.MiddleQuestionThree
{
  /// <summary>
  /// @desc : IsValidBST  
  /// @author : mons
  /// @create : 2019/4/26 15:23:53 
  /// @source : https://leetcode.com/problems/validate-binary-search-tree/
  /// </summary>
  public class IsValidBST
  {
    /**
     * Runtime: 96 ms, faster than 99.84% of C# online submissions for Validate Binary Search Tree.
     * Memory Usage: 24.2 MB, less than 57.35% of C# online submissions for Validate Binary Search Tree.
     *
     * 再不过就疯了。。。
     *
     */
    public bool Solution(TreeNode root)
    {
      return Helper(null, null, root);
    }

    public bool Helper(int? max, int? min, TreeNode root)
    {
      if (root == null)
        return true;

      if ((min != null && root.val <= min) || (max != null && root.val >= max)) return false;

      return Helper(max.HasValue ? Math.Min(max.Value, root.val) : root.val, min, root.left) &&
             Helper(max, min.HasValue ? Math.Max(min.Value, root.val) : root.val, root.right);
    }

    public bool IsValidBST2(TreeNode root)
    {
      return Helper(int.MaxValue, int.MinValue, root, false, false);
    }

    public bool Helper(int max, int min, TreeNode root, bool hasMax, bool hasMin)
    {
      if (root == null)
        return true;

      if ((hasMin && root.val <= min) || (hasMax && root.val >= max)) return false;

      return Helper(Math.Min(max, root.val), min, root.left, true, hasMin) &&
             Helper(max, Math.Max(min, root.val), root.right, hasMax, true);
    }
  }
}