using System;
using System.Collections.Generic;
using System.Text;
using ConsoleTest.Domain.StructModel;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : PrintTree  
  /// @author :mons
  /// @create : 2019/4/1 17:55:04 
  /// @source : https://leetcode.com/problems/print-binary-tree/
  /// </summary>
  public class PrintTree
  {
    public IList<IList<string>> Solution(TreeNode root)
    {
      IList<IList<string>> result = new List<IList<string>>();


      return result;
    }

    public void GetList(IList<IList<string>> result, TreeNode root, int deep, int place)
    {
      if (root == null) return;

      IList<string> list;

      if (result.Count == deep)
      {
        list = new List<string>();
        for (int i = 0; i < deep * 2 + 1; i++)
        {
          list.Add("");
        }
      }
      else
      {
        list = result[deep];
      }

      list[place] = root.val.ToString();

      GetList(result, root.left, deep + 1, place);
      GetList(result, root.right, deep + 1, place + 1);
    }
  }
}