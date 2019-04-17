using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleTest.LeetCode;

namespace ConsoleTest.MiddleQuestionTwo
{
  /// <summary>
  /// @desc : InsertionSortList  
  /// @author :mons
  /// @create : 2019/4/17 17:07:17 
  /// @source : https://leetcode.com/problems/insertion-sort-list/
  /// </summary>
  public class InsertionSortList
  {

    /**
     * Runtime: 172 ms, faster than 11.39% of C# online submissions for Insertion Sort List.
     * Memory Usage: 23.9 MB, less than 60.00% of C# online submissions for Insertion Sort List.
     */
    public ListNode Solution(ListNode head)
    {
      if (head == null) return null;

      var node = head;

      List<ListNode> list = new List<ListNode>();

      while (node != null)
      {
        var index = -1;
        for (int i = list.Count - 1; i >= 0; i--)
        {
          if (node.val < list[i].val)
            index = i;
          else
            break;
        }

        if (index != -1)
        {
          if (index != 0)
            list[index - 1].next = node;

          list.Insert(index, node);
          node = node.next;
          list[index].next = list[index + 1];

          list[list.Count - 1].next = null;
        }
        else
        {
          if (list.Count > 0) list[list.Count - 1].next = node;
          list.Add(node);
          node = node.next;
        }
      }

      return list[0];
    }
  }
}