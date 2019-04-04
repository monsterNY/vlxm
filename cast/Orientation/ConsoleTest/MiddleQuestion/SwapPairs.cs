using System;
using System.Collections.Generic;
using System.Text;
using ConsoleTest.LeetCode;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : SwapPairs  
  /// @author :mons
  /// @create : 2019/4/4 16:38:09 
  /// @source : https://leetcode.com/problems/swap-nodes-in-pairs/
  /// </summary>
  public class SwapPairs
  {

    /**
     * Runtime: 88 ms, faster than 100.00% of C# online submissions for Swap Nodes in Pairs.
     * Memory Usage: 21.6 MB, less than 97.85% of C# online submissions for Swap Nodes in Pairs.
     *
     * ？？？ wa hoo ~~
     * nice
     *
     */
    public ListNode Solution(ListNode head)
    {
      if (head == null) return null;

      ListNode prev = head, lastPrev = null, next;

      if (head.next != null) head = head.next;

      while (prev?.next != null)
      {
        next = prev.next;

        prev.next = next.next;
        if (lastPrev != null)
          lastPrev.next = next;
        next.next = prev;

        lastPrev = prev;
        prev = prev.next;
      }

      return head;
    }

    /**
     * Runtime: 88 ms, faster than 100.00% of C# online submissions for Swap Nodes in Pairs.
     * Memory Usage: 22 MB, less than 27.96% of C# online submissions for Swap Nodes in Pairs.
     *
     * 递归更加清晰，但会增加空间消耗
     *
     */
    public ListNode swapPairs(ListNode head)
    {
      if ((head == null) || (head.next == null))
        return head;
      ListNode n = head.next;
      head.next = swapPairs(head.next.next);
      n.next = head;
      return n;
    }

  }
}