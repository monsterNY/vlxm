using System;
using System.Collections.Generic;
using System.Text;
using ConsoleTest.LeetCode;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : OddEvenList  
  /// @author :mons
  /// @create : 2019/4/3 9:31:12 
  /// @source : https://leetcode.com/problems/odd-even-linked-list/
  /// </summary>
  public class OddEvenList
  {
    /**
     * Runtime: 104 ms, faster than 61.42% of C# online submissions for Odd Even Linked List.
     * Memory Usage: 23.7 MB, less than 10.71% of C# online submissions for Odd Even Linked List.
     */
    public ListNode Solution(ListNode head)
    {
      if (head == null) return head;

      IList<ListNode> list = new List<ListNode>();

      ListNode prev = head;
      ListNode node = head.next;

      while (node != null)
      {
        prev.next = node.next;
        list.Add(node);
        if (node.next != null)
        {
          prev = node.next;
          node = node.next.next;
        }
        else
        {
          break;
        }
      }

      for (int i = 0; i < list.Count; i++)
      {
        prev.next = list[i];
        prev = prev.next;
      }

      prev.next = null;

      return head;
    }

    public ListNode OtherSolution(ListNode head)
    {
      if (head != null)
      {
        ListNode odd = head, even = head.next, evenHead = even;

        while (even?.next != null)
        {
          odd.next = odd.next.next;
          even.next = even.next.next;
          odd = odd.next;
          even = even.next;
        }

        odd.next = evenHead;
      }

      return head;
    }

    /**
     * Runtime: 100 ms, faster than 62.92% of C# online submissions for Odd Even Linked List.
     * Memory Usage: 23.6 MB, less than 42.86% of C# online submissions for Odd Even Linked List.
     *
     * Runtime: 96 ms, faster than 83.90% of C# online submissions for Odd Even Linked List.
     * Memory Usage: 23.6 MB, less than 35.71% of C# online submissions for Odd Even Linked List.
     *
     * Runtime: 92 ms, faster than 100.00% of C# online submissions for Odd Even Linked List.
     * Memory Usage: 23.5 MB, less than 64.29% of C# online submissions for Odd Even Linked List.
     *
     * 测试有毒吧。。。
     *
     */
    public ListNode Solution2(ListNode head)
    {
      if (head == null) return head;

      ListNode oddNode = null, oddHead = null, prev = head, node = head.next;

      while (node != null)
      {
        prev.next = node.next;

        if (oddNode == null)
        {
          oddHead = node;
          oddNode = node;
        }
        else
        {
          oddNode.next = node;
          oddNode = oddNode.next;
        }

        if (node.next != null)
        {
          prev = node.next;
          node = node.next.next;
        }
        else
        {
          break;
        }
      }

      prev.next = oddHead;
      if (oddNode != null)
        oddNode.next = null;

      return head;
    }

    public ListNode Try(ListNode head)
    {
      if (head == null) return head;
      var index = 1;

      IList<ListNode> list = new List<ListNode>();

      ListNode prev = head;
      ListNode node = head.next;

      while (node != null)
      {
        index++;
        if (index % 2 == 0)
        {
          list.Add(node);
          prev.next = node.next;
        }

        prev = node;
        node = node.next;
      }

      for (int i = 0; i < list.Count; i++)
      {
        prev.next = list[i];
        prev = prev.next;
      }

      prev.next = null;

      return head;
    }
  }
}