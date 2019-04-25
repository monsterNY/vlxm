using System;
using System.Collections.Generic;
using System.Text;
using ConsoleTest.LeetCode;

namespace ConsoleTest.MiddleQuestionThree
{
  /// <summary>
  /// @desc : ReorderList  
  /// @author : mons
  /// @create : 2019/4/25 13:46:38 
  /// @source : https://leetcode.com/problems/reorder-list/
  /// </summary>
  public class ReorderList
  {

    public void Solution(ListNode head)
    {
      if (head == null) return;

      List<ListNode> list = new List<ListNode>();

      var node = head;

      while (node != null)
      {
        list.Add(node);
        node = node.next;
      }

      if (list.Count > 2)
      {
        for (int i = 0; i < list.Count / 2; i++)
        {
          if (list.Count - 1 - i != i + 1)
            list[list.Count - 1 - i].next = list[i].next;
          else
            list[list.Count - 1 - i].next = null;

          list[i].next = list[list.Count - 1 - i];
        }

        if (list.Count % 2 == 1)
        {
          list[list.Count - list.Count / 2].next = list[list.Count - list.Count / 2 - 1];
          list[list.Count - list.Count / 2 - 1].next = null;
        }
      }
    }

    /**
     * Runtime: 104 ms, faster than 100.00% of C# online submissions for Reorder List.
     * Memory Usage: 30.4 MB, less than 12.50% of C# online submissions for Reorder List.
     * ?????????
     */
    public void Simple(ListNode head)
    {
      if (head == null) return;

      List<ListNode> list = new List<ListNode>();

      var node = head;

      while (node != null)
      {
        list.Add(node);
        node = node.next;
      }

      if (list.Count > 1)
      {
        for (int i = 0; i < list.Count / 2; i++)
        {
          if (list.Count - 1 - i != i + 1)
            list[list.Count - 1 - i].next = list[i].next;
          else
            list[list.Count - 1 - i].next = null;

          list[i].next = list[list.Count - 1 - i];
        }

        if (list.Count % 2 == 1)
        {
          list[list.Count - list.Count / 2].next = list[list.Count - list.Count / 2 - 1];
          list[list.Count - list.Count / 2 - 1].next = null;
        }
      }
    }
  }
}