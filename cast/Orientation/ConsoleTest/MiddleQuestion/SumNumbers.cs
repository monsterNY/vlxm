using System;
using System.Collections.Generic;
using System.Text;
using ConsoleTest.Domain.StructModel;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : SumNumbers  
  /// @author :mons
  /// @create : 2019/4/10 14:19:58 
  /// @source : https://leetcode.com/problems/sum-root-to-leaf-numbers/
  /// </summary>
  public class SumNumbers
  {
    /**
     * Runtime: 92 ms, faster than 96.96% of C# online submissions for Sum Root to Leaf Numbers.
     * Memory Usage: 22.4 MB, less than 19.05% of C# online submissions for Sum Root to Leaf Numbers.
     *
     */
    public int Solution(TreeNode root)
    {
      return GetSum(root, 0);
    }

    public int GetSum(TreeNode root, int sum)
    {
      if (root == null) return 0;

      sum = sum * 10 + root.val;

      if (root.left == null && root.right == null) return sum;

      return GetSum(root.left, sum) + GetSum(root.right, sum);
    }
  }
}