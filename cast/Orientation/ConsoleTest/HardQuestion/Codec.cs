using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleTest.Domain.StructModel;

namespace ConsoleTest.HardQuestion
{
  /// <summary>
  /// @desc : Codec  
  /// @author : mons
  /// @create : 2019/6/17 16:35:10 
  /// @source : 
  /// </summary>
  [Obsolete]
  public class Codec
  {
    // Encodes a tree to a single string.
    public string serialize(TreeNode root)
    {
      if (root == null) return "[]";
      else
      {
        List<string> res = new List<string>();

        res.Add(root.val.ToString());

        HelperWrite(res, root);

        for (int i = res.Count - 1; i >= 0; i--)
        {
          if (res[i] != null) break;
          res.RemoveAt(i);
        }

        return $"[{string.Join(',', res.Select(u => u ?? "null"))}]";
      }
    }

    // Decodes your encoded data to tree.
    public TreeNode deserialize(string data)
    {
      return HelperGet(data.AsSpan(1, data.Length - 2).ToString().Split(','), 0);
    }

    private void HelperWrite(List<string> list, TreeNode node)
    {
      if (node == null) return;

      list.Add(node.left?.val.ToString());

      list.Add(node.right?.val.ToString());

      HelperWrite(list, node.left);

      HelperWrite(list, node.right);
    }

    private TreeNode HelperGet(string[] data, int index)
    {
      if (index > data.Length - 1) return null;
      if (data[index] == "null") return null;

      var node = new TreeNode(int.Parse(data[index]));

      index = index * 2 + 1;

      node.left = HelperGet(data, index);

      if (node.left != null) index++;

      node.right = HelperGet(data, index);

      return node;
    }
  }
}