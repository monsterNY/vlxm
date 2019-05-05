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

    /**
     * Runtime: 92 ms, faster than 98.45% of C# online submissions for Remove Nth Node From End of List.
     * Memory Usage: 22.7 MB, less than 16.46% of C# online submissions for Remove Nth Node From End of List.
     *
     * Runtime: 92 ms, faster than 98.45% of C# online submissions for Remove Nth Node From End of List.
     * Memory Usage: 22.6 MB, less than 22.79% of C# online submissions for Remove Nth Node From End of List.
     *
     */
    public ListNode Solution2(ListNode head, int n)
    {
      int count = 0;

      ListNode node = head, move = head;

      while (node != null)
      {
        if (count++ > n)
        {
          move = move.next;
        }

        node = node.next;
      }

      if (count == n) return head.next;

      move.next = move.next?.next;

      return head;
    }

    /**
     * Runtime: 92 ms, faster than 98.45% of C# online submissions for Remove Nth Node From End of List.
     * Memory Usage: 22.5 MB, less than 72.15% of C# online submissions for Remove Nth Node From End of List.
     */
    public ListNode Solution3(ListNode head, int n)
    {
      int count = 0;

      bool flag = false;

      ListNode node = head, move = null;

      while (node != null)
      {
        if (flag)
        {
          move = move.next;
        }
        else if (++count > n)
        {
          flag = true;
          move = head;
        }

        node = node.next;
      }

      if (move == null) return head.next;

      if (move.next != null)
        move.next = move.next.next;

      return head;
    }
  }
}