using System;
using System.Collections.Generic;
using System.Text;
using ConsoleTest.Domain.StructModel;

namespace ConsoleTest.MiddleQuestionTwo
{
  /// <summary>
  /// @desc : PathSum  
  /// @author :mons
  /// @create : 2019/4/12 15:10:20 
  /// @source : https://leetcode.com/problems/path-sum-ii/
  /// </summary>
  public class PathSum
  {
    public IList<IList<int>> Solution(TreeNode root, int sum)
    {
      return GetResult2(new List<IList<int>>(), root, sum, new List<int>());
    }

    /**
     * Runtime: 292 ms, faster than 49.86% of C# online submissions for Path Sum II.
     * Memory Usage: 34.3 MB, less than 5.09% of C# online submissions for Path Sum II.
     */
    public IList<IList<int>> GetResult(IList<IList<int>> res, TreeNode root, int sum, IList<int> build)
    {
      if (root == null)
        return res;

      sum = sum - root.val;

      if (sum == 0)
      {
        if (root.left == null && root.right == null)
          res.Add(build);
        return res;
      }

      build.Add(root.val);

      GetResult(res, root.left, sum - root.val, new List<int>(build));

      GetResult(res, root.right, sum - root.val, new List<int>(build));

      return res;
    }

    /**
     * Runtime: 256 ms, faster than 95.39% of C# online submissions for Path Sum II.
     * Memory Usage: 30.8 MB, less than 79.66% of C# online submissions for Path Sum II.
     *
     * Runtime: 256 ms, faster than 95.39% of C# online submissions for Path Sum II.
     *  Memory Usage: 30.5 MB, less than 98.31% of C# online submissions for Path Sum II.
     *
     * nice~
     *
     */
    public IList<IList<int>> GetResult2(IList<IList<int>> res, TreeNode root, int sum, IList<int> build)
    {
      if (root == null)
        return res;

      sum = sum - root.val;

      build.Add(root.val);

      if (sum == 0 && root.left == null && root.right == null)
      {
        res.Add(new List<int>(build));
        build.RemoveAt(build.Count - 1);
        return res;
      }

      GetResult2(res, root.left, sum, build);

      GetResult2(res, root.right, sum, build);

      build.RemoveAt(build.Count - 1);

      return res;
    }
  }
}