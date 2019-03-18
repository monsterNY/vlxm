using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.LeetCode
{
  /// <summary>
  /// @desc : HasCycle  
  /// @author :mons
  /// @create : 2019/3/18 11:57:55 
  /// @source : https://leetcode.com/problems/linked-list-cycle/
  /// </summary>
  public class HasCycle
  {

    /**
     * Runtime: 608 ms, faster than 5.44% of C# online submissions for Linked List Cycle.
     * Memory Usage: 24.3 MB, less than 20.27% of C# online submissions for Linked List Cycle.
     *
     * simple and not efficient 
     *
     */
    public bool Solution(ListNode head)
    {
      if (head == null) return false;

      List<ListNode> list = new List<ListNode>();

      while (head != null)
      {
        if (head.next == head) return true;

        foreach (var node in list)
        {
          if (head.next == node) return true;
        }

        list.Add(head);
        head = head.next;
      }

      return false;
    }

    /**
     *
     * 赞
     *
     * Runtime: 116 ms, faster than 38.77% of C# online submissions for Linked List Cycle.
     * Memory Usage: 23.6 MB, less than 56.76% of C# online submissions for Linked List Cycle.
     *
     * 1.Use two pointers, walker and runner.
     * 2.walker moves step by step. runner moves two steps at time.
     * 3.if the Linked List has a cycle walker and runner will meet at some
     * point.
     */
    public bool OtherSolution(ListNode head)
    {
      if (head == null) return false;
      ListNode walker = head;
      ListNode runner = head;
      while (runner.next != null && runner.next.next != null)
      {
        walker = walker.next;
        runner = runner.next.next;
        if (walker == runner) return true;
      }
      return false;
    }

  }
}