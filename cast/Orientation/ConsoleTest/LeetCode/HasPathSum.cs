using System;
using System.Collections.Generic;
using System.Text;
using ConsoleTest.Domain.StructModel;

namespace ConsoleTest.LeetCode
{
  /// <summary>
  /// @desc : HasPathSum  
  /// @author :mons
  /// @create : 2019/3/18 11:09:30 
  /// @source : 
  /// </summary>
  public class HasPathSum
  {
    /**
     * Runtime: 100 ms, faster than 81.34% of C# online submissions for Path Sum.
     * Memory Usage: 23.9 MB, less than 95.83% of C# online submissions for Path Sum.
     *
     * great 看来我用递归还行~
     *
     */
    public bool Solution(TreeNode root, int sum)
    {
      if (root == null) return false;
      if (root.left == null && root.right == null) return root.val == sum;
      if (root.left == null)
      {
        root.right.val += root.val;
        return Solution(root.right, sum);
      }

      if (root.right == null)
      {
        root.left.val += root.val;
        return Solution(root.left, sum);
      }

      root.left.val += root.val;
      if (Solution(root.left, sum)) return true;

      root.right.val += root.val;
      return Solution(root.right, sum);
    }

    /**
     * genius
     */
    public bool OtherSolution(TreeNode root, int sum)
    {
      if (root == null) return false;
      if (root.left == null && root.right == null) return root.val == sum;
      return OtherSolution(root.left, sum - root.val) || OtherSolution(root.right, sum - root.val);
    }
  }
}