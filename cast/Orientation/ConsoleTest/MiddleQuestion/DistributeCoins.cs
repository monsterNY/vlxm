using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleTest.Domain.StructModel;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : DistributeCoins  
  /// @author :mons
  /// @create : 2019/3/22 14:31:05 
  /// @source : https://leetcode.com/problems/distribute-coins-in-binary-tree/
  /// </summary>
  [Obsolete]
  public class DistributeCoins
  {
    #region Office

    private int ans;

    public int distributeCoins(TreeNode root)
    {
      ans = 0;
      dfs(root);
      return ans;
    }

    public int dfs(TreeNode node)
    {
      if (node == null) return 0;
      var L = dfs(node.left);
      var R = dfs(node.right);
      ans += Math.Abs(L) + Math.Abs(R);
      return node.val + L + R - 1;
    }

    #endregion

    /**
     * 按照理解 或许不是最优解 但觉得也完成了任务，
     *
     * 不过好像理解有些出入。。。
     *
     */
    public int Solution(TreeNode root)
    {
      var needDictionary = new Dictionary<TreeNode, int>();
      var moreDictionary = new Dictionary<TreeNode, int>();

      GetStep(needDictionary, moreDictionary, root, 0);

      var count = 0;

      foreach (var item in moreDictionary.OrderBy(u => u.Value))
      foreach (var needItem in needDictionary.OrderBy(u => item.Value == u.Value
        ? 2
        : item.Value > u.Value
          ? item.Value - u.Value
          : u.Value - item.Value))
      {
        if (needItem.Key.val == 1) continue;

        needItem.Key.val = 1;
        item.Key.val--;
        count += item.Value == needItem.Value
          ? 2
          : item.Value > needItem.Value
            ? item.Value - needItem.Value
            : needItem.Value - item.Value;
        if (item.Key.val == 1) break;
      }


      return count;
    }

    public void GetStep(Dictionary<TreeNode, int> needDictionary, Dictionary<TreeNode, int> moreDictionary,
      TreeNode node, int deep)
    {
      if (node == null) return;

      if (node.val == 0)
        needDictionary.Add(node, deep);
      else if (node.val > 1) moreDictionary.Add(node, deep);

      var flag = deep == 0;
      if (deep > 0) deep++;
      else deep--;

      GetStep(needDictionary, moreDictionary, node.left, flag ? 1 : deep);
      GetStep(needDictionary, moreDictionary, node.right, flag ? -1 : deep);
    }
  }
}