using System;
using System.Collections.Generic;
using System.Text;
using ConsoleTest.LeetCode;

namespace ConsoleTest.MiddleQuestionTwo
{
  /// <summary>
  /// @desc : ReverseBetween  
  /// @author :mons
  /// @create : 2019/4/19 13:47:42 
  /// @source : https://leetcode.com/problems/reverse-linked-list-ii/
  /// </summary>
  public class ReverseBetween
  {
    /**
     * Runtime: 92 ms, faster than 88.77% of C# online submissions for Reverse Linked List II.
     * Memory Usage: 22 MB, less than 11.11% of C# online submissions for Reverse Linked List II.
     */
    public ListNode Solution(ListNode head, int m, int n)
    {
      if (m == n) return head;

      ListNode prev = null, node = head, start = null;

      bool flag = false;

      for (int i = 1; i <= n; i++)
      {
        if (flag)
        {
          if (start == null)//如果置换点的前一个节点为空  即置换点为root节点 则创建一个辅助节点作为root
          {
            start = new ListNode(-1)
            {
              next = prev
            };
          }

          //such as 1->2->3->4->5 2 4
          prev.next = node.next;// first 2->3->4 => 2>4
          node.next = start.next;//first 3->4 3->2
          start.next = node;//first 1->2->4->5 => 1->3->2->4->5

          node = prev.next;//
        }
        else
        {
          if (i == m)//step1:找到开始置换的节点
          {
            start = prev;//将开始位置的前一个节点存留起来
            flag = true;
          }

          prev = node;
          node = node.next;
        }
      }

      return start.val == -1 ? start.next : head;
    }

    public ListNode Optimize(ListNode head, int m, int n)
    {
      if (m == n) return head;

      ListNode prev = null, node = head, start = null;

      bool flag = false;

      for (int i = 1; i <= n; i++, prev = node, node = node.next)
      {
        if (flag)
        {
          if (start == null)
            start = new ListNode(-1) {next = prev};

          prev.next = node.next;
          node.next = start.next;
          start.next = node;
          node = prev;
        }
        else if (i == m)
        {
          start = prev;
          flag = true;
        }
      }

      return start.val == -1 ? start.next : head;
    }

    //try
    public ListNode Try(ListNode head, int m, int n)
    {
      ListNode prev = null, node = head, mNode = null, nNode = null, mPrev = null, nPrev = null, next;

      while ((mNode == null || nNode == null) && node != null)
      {
        if (node.val == m)
        {
          mPrev = prev;
          mNode = node;
        }

        if (node.val == n)
        {
          nPrev = prev;
          nNode = node;
        }

        prev = node;
        node = node.next;
      }

      if (mNode == null || nNode == null) return head;

      if (mPrev != null)
      {
        mPrev.next = nNode;
      }
      else
      {
        head = nNode;
      }

      if (nPrev != null)
      {
        nPrev.next = mNode;
      }
      else
      {
        head = mNode;
      }

      next = nNode.next;
      nNode.next = mNode.next;
      mNode.next = next;

      return head;
    }
  }
}