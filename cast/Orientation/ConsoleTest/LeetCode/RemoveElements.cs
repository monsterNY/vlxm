using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.LeetCode
{
  /// <summary>
  /// @desc : RemoveElements  
  /// @author :mons
  /// @create : 2019/3/18 16:17:23 
  /// @source : 
  /// </summary>
  public class RemoveElements
  {
    /**
     * Runtime: 100 ms, faster than 100.00% of C# online submissions for Remove Linked List Elements.
     * Memory Usage: 26 MB, less than 84.00% of C# online submissions for Remove Linked List Elements.
     *
     * nice
     *
     */
    public ListNode Solution(ListNode head, int val)
    {
      while (head != null && head.val == val)
        head = head.next;
      if (head == null) return null;
      ListNode prevNode = head;
      ListNode node = head;
      while ((node = node.next) != null)
      {
        if (node.val == val)
          prevNode.next = node.next;
        else
          prevNode = node;
      }

      return head;
    }
  }
}