using System;
using System.Collections.Generic;
using System.Text;
using ConsoleTest.LeetCode;

namespace ConsoleTest.MiddleQuestionThree
{
  /// <summary>
  /// @desc : RemoveNthFromEnd  
  /// @author :mons
  /// @create : 2019/4/22 11:35:05 
  /// @source : https://leetcode.com/problems/remove-nth-node-from-end-of-list/
  /// </summary>
  public class RemoveNthFromEnd
  {
    /**
     * Runtime: 92 ms, faster than 98.45% of C# online submissions for Remove Nth Node From End of List.
     * Memory Usage: 22.8 MB, less than 5.06% of C# online submissions for Remove Nth Node From End of List.
     *
     * Runtime: 92 ms, faster than 98.45% of C# online submissions for Remove Nth Node From End of List.
     * Memory Usage: 22.6 MB, less than 29.11% of C# online submissions for Remove Nth Node From End of List.
     *
     */
    public ListNode Solution(ListNode head, int n)
    {
      List<ListNode> list = new List<ListNode>();

      var node = head;

      while (node != null)
      {
        list.Add(node);
        node = node.next;
      }

      if (n == list.Count)
        return list.Count > 1 ? list[1] : null;

      list[list.Count - 1 - n].next = list[list.Count - n].next;

      return head;
    }
  }
}