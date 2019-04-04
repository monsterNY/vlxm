using System;
using System.Collections.Generic;
using System.Text;
using ConsoleTest.Domain.StructModel;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : FindDuplicateSubtrees  
  /// @author :mons
  /// @create : 2019/4/4 9:48:00 
  /// @source : https://leetcode.com/problems/find-duplicate-subtrees/
  /// </summary>
  [Obsolete("time limit")]
  public class FindDuplicateSubtrees
  {
    #region amazing

    /**
     * Runtime: 504 ms, faster than 51.75% of C# online submissions for Find Duplicate Subtrees.
     * Memory Usage: 49.8 MB, less than 50.00% of C# online submissions for Find Duplicate Subtrees.
     */
    public List<TreeNode> findDuplicateSubtrees(TreeNode root)
    {
      List<TreeNode> res = new List<TreeNode>();
      postorder(root, new Dictionary<string, int>(), res);
      return res;
    }

    public String postorder(TreeNode cur, Dictionary<string, int> map, List<TreeNode> res)
    {
      if (cur == null) return "#";
      String serial = cur.val + "," + postorder(cur.left, map, res) + "," + postorder(cur.right, map, res);
      var num = 0;
      if (map.ContainsKey(serial)) num = map[serial];

      if (num == 1) res.Add(cur);
      if (map.ContainsKey(serial))
        map[serial] = num + 1;
      else
        map.Add(serial, num + 1);
      return serial;
    }

    #endregion


    /**
     * Runtime: 380 ms, faster than 91.23% of C# online submissions for Find Duplicate Subtrees.
     * Memory Usage: 41.2 MB, less than 100.00% of C# online submissions for Find Duplicate Subtrees.
     */
    public IList<TreeNode> Imitation(TreeNode root)
    {
      IList<TreeNode> res = new List<TreeNode>();

      Postorder(root, new Dictionary<string, int>(), res);

      return res;
    }

    public string Postorder(TreeNode cur, Dictionary<string, int> map, IList<TreeNode> res)
    {
      if (cur == null) return "#";

      StringBuilder builder = new StringBuilder();
      builder.Append(cur.val);
      builder.Append(Postorder(cur.left, map, res));
      builder.Append(Postorder(cur.right, map, res));

      string serial = builder.ToString();

      var num = 0;
      if (map.ContainsKey(serial)) num = map[serial];

      if (num == 1) res.Add(cur);

      if (num == 0)
        map.Add(serial, 1);
      else
        map[serial]++;

      return serial;
    }

    /**
     * Time Limit Exceeded
     */
    public IList<TreeNode> Solution(TreeNode root)
    {
      IList<TreeNode> res = new List<TreeNode>();

      Dictionary<int, IList<TreeNode>> dictionary = new Dictionary<int, IList<TreeNode>>();

      PutToMap(root, dictionary);

      foreach (var item in dictionary)
      {
        if (item.Value.Count > 1)
        {
          for (int i = 0; i < item.Value.Count; i++)
          {
            bool flag = false;
            for (int j = i + 1; j < item.Value.Count; j++)
            {
              if (IsSame(item.Value[i], item.Value[j]))
              {
                item.Value.RemoveAt(j--);
                if (!flag) flag = true;
              }
            }

            if (flag) res.Add(item.Value[i]);
          }
        }
      }

      return res;
    }

    public bool IsSame(TreeNode node, TreeNode compareNode)
    {
      if (node == null && compareNode == null) return true;
      if (node == null || compareNode == null) return false;
      if (node.val == compareNode.val)
      {
        return IsSame(node.left, compareNode.left) && IsSame(node.right, compareNode.right);
      }

      return false;
    }

    public void PutToMap(TreeNode root, Dictionary<int, IList<TreeNode>> dictionary)
    {
      if (root == null) return;
      if (dictionary.ContainsKey(root.val))
        dictionary[root.val].Add(root);
      else
        dictionary.Add(root.val, new List<TreeNode>() {root});

      PutToMap(root.left, dictionary);
      PutToMap(root.right, dictionary);
    }

    public void PutToMap2(TreeNode root, Dictionary<int, IList<TreeNode>> dictionary, IList<TreeNode> result) //存在重复
    {
      if (root == null) return;
      if (dictionary.ContainsKey(root.val))
      {
        bool flag = false;
        for (int i = 0; i < dictionary[root.val].Count; i++)
        {
          if (IsSame(root, dictionary[root.val][i]))
          {
            dictionary[root.val].RemoveAt(i);
            result.Add(root);
            flag = true;
            break;
          }
        }

        if (!flag) dictionary[root.val].Add(root);
      }
      else
        dictionary.Add(root.val, new List<TreeNode>() {root});

      PutToMap2(root.left, dictionary, result);
      PutToMap2(root.right, dictionary, result);
    }
  }
}