using System;
using System.Collections.Generic;
using System.Text;
using ConsoleTest.LeetCode;

namespace ConsoleTest.MiddleQuestionThree
{
  /// <summary>
  /// @desc : DetectCycle  
  /// @author : mons
  /// @create : 2019/4/25 9:45:36 
  /// @source : https://leetcode.com/problems/linked-list-cycle-ii/
  /// </summary>
  public class DetectCycle
  {
    public ListNode Solution(ListNode head)
    {
      ListNode slow = head, middle = head.next, fast = head.next.next;

      while (slow != null && slow.next != null)
      {
        middle = slow.next;
        fast = middle.next;
      }

      return null;
    }

    public ListNode Simple(ListNode head)
    {
      ListNode node = head;

      List<ListNode> list = new List<ListNode>();

      while (node != null)
      {
        for (int i = 0; i < list.Count; i++)
        {
          if (list[i] == node) return node;
        }

        node = node.next;
      }

      return null;
    }
  }
}