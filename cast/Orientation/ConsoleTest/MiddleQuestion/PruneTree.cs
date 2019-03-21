using System;
using System.Collections.Generic;
using System.Text;
using ConsoleTest.Domain.StructModel;
using ConsoleTest.LeetCode;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : PruneTree  
  /// @author :mons
  /// @create : 2019/3/21 16:43:42 
  /// @source : https://leetcode.com/problems/binary-tree-pruning/
  /// </summary>
  public class PruneTree
  {

    /**
     *
     * java:Runtime: 1 ms, faster than 100.00% of Java online submissions for Binary Tree Pruning.
     * Memory Usage: 36.9 MB, less than 34.38% of Java online submissions for Binary Tree Pruning.
     *
     * C#:Runtime: 92 ms, faster than 96.24% of C# online submissions for Binary Tree Pruning.
     * Memory Usage: 22 MB, less than 85.71% of C# online submissions for Binary Tree Pruning.
     *
     * 果然这才是我擅长的。
     *
     * C# 90+% == java 100%
     * C#: same code , the java speed more than C# 100 times, I don't want to lose face???
     *
     */
    public TreeNode Solution(TreeNode root)
    {

      if (root == null) return null;

      root.left = Solution(root.left);
      root.right = Solution(root.right);

      if (root.left == null && root.val == 0 && root.right == null) return null;

      return root;

    }
    
  }
}
