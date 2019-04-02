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
  /// @source :   https://leetcode.com/problems/print-binary-tree/
  /// </summary>
  [Obsolete(" no enjoy")]
  public class PrintTree
  {
    public IList<IList<string>> Solution(TreeNode root)
    {
      IList<IList<string>> result = new List<IList<string>>();

      GetList(result, root, 0, 0);

      if (result.Count > 1)
      {
        IList<string> list;

        int addCount = 1, remaind, len = result[result.Count - 1].Count, inCount;

//        for (var i = result.Count - 2; i >= 0; i--)
//        {
//          list = result[i];
//
//          if (i > 0)
//          {
//            inCount = (len - list.Count - addCount) / 2;
//            for (var j = 2; j < list.Count; j += 2)
//            for (var k = 0; k < inCount; k++)
//              list.Insert(j++, "");
//          }
//
//          for (var j = 0; j < addCount; j++)
//          {
//            list.Add("");
//            list.Insert(0, "");
//          }
//          remaind = list.Count;
//
//          for (int j = 0; j < len - remaind; j++)
//          {
//            list.Insert(remaind / 2, "");
//          }
//
//          addCount = addCount * 2 + 1;
//        }
      }

      return result;
    }

    public void GetList(IList<IList<string>> result, TreeNode root, int deep, int place)
    {
      if (root == null) return;

      IList<string> list;

      if (result.Count == deep)
      {
        list = new List<string>();
        for (var i = 0; i < (result.Count == 0 ? 1 : result[result.Count - 1].Count * 2 + 1); i++) list.Add("");

        result.Add(list);
      }
      else
      {
        list = result[deep];
      }

      list[place] = root.val.ToString();

      GetList(result, root.left, deep + 1, place * 2);
      GetList(result, root.right, deep + 1, (place + 1) * 2);
    }
  }
}