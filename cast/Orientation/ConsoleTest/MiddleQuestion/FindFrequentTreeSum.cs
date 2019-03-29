using System;
using System.Collections.Generic;
using System.Text;
using ConsoleTest.Domain.StructModel;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : FindFrequentTreeSum  
  /// @author :mons
  /// @create : 2019/3/29 11:53:03 
  /// @source : https://leetcode.com/problems/most-frequent-subtree-sum/
  /// </summary>
  public class FindFrequentTreeSum
  {
    Dictionary<int, int> dictionary = new Dictionary<int, int>();
    private int maxCount = 0;

    /**
     * Runtime: 264 ms, faster than 69.32% of C# online submissions for Most Frequent Subtree Sum.
     * Memory Usage: 31.5 MB, less than 100.00% of C# online submissions for Most Frequent Subtree Sum.
     */
    public int[] Solution(TreeNode root)
    {
      List<int> list = new List<int>();

      GetAllSum(root);

      foreach (var item in dictionary)
      {
        if (item.Value == maxCount) list.Add(item.Key);
      }

      return list.ToArray();
    }

    public int[] Solution2(TreeNode root)
    {

      dictionary.Clear();
      maxCount = 0;

      List<int> list = new List<int>();

      Dictionary<int,int> map = new Dictionary<int, int>();

      GetSum2(root, map);

      foreach (var item in dictionary)
      {
        if (item.Value == maxCount) list.Add(item.Key);
      }

      return list.ToArray();
    }

    /**
     * try 2:
     *
     * Runtime: 276 ms, faster than 61.36% of C# online submissions for Most Frequent Subtree Sum.
     * Memory Usage: 31.6 MB, less than 33.33% of C# online submissions for Most Frequent Subtree Sum.
     *
     * ??? 没变化？
     *
     * 。。。外部成员更耗时？？？
     * Runtime: 260 ms, faster than 81.82% of C# online submissions for Most Frequent Subtree Sum.
     * Memory Usage: 31.5 MB, less than 83.33% of C# online submissions for Most Frequent Subtree Sum.
     *
     */
    public int GetSum2(TreeNode root,Dictionary<int,int> map)
    {
      if (root == null) return 0;
      
      var sum = root.val + GetSum2(root.left, map) + GetSum2(root.right, map);

      if (map.ContainsKey(sum)) map[sum]++;
      else
        map.Add(sum, 1);

      if (map[sum] > maxCount) maxCount = map[sum];

      return sum;

    }

    public void GetAllSum(TreeNode root)
    {
      if (root == null) return;

      var sum = GetSum(root);

      if (dictionary.ContainsKey(sum)) dictionary[sum]++;
      else
        dictionary.Add(sum, 1);

      if (dictionary[sum] > maxCount) maxCount = dictionary[sum];

      GetAllSum(root.left);

      GetAllSum(root.right);
    }

    public int GetSum(TreeNode root)
    {
      if (root == null) return 0;

      return root.val + GetSum(root.left) + GetSum(root.right);
    }
  }
}