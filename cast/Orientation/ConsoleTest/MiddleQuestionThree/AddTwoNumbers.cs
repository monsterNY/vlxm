using System;
using System.Collections.Generic;
using System.Text;
using ConsoleTest.LeetCode;

namespace ConsoleTest.MiddleQuestionThree
{
  /// <summary>
  /// @desc : AddTwoNumbers  
  /// @author : mons
  /// @create : 2019/4/25 11:25:02 
  /// @source : https://leetcode.com/problems/add-two-numbers/
  /// </summary>
  public class AddTwoNumbers
  {
    /**
     * Runtime: 112 ms, faster than 65.29% of C# online submissions for Add Two Numbers.
     * Memory Usage: 25.6 MB, less than 18.57% of C# online submissions for Add Two Numbers.
     */
    public ListNode Solution(ListNode l1, ListNode l2)
    {
      ListNode node, prev = null, head = null;

      int num, more = 0;

      while (l1 != null || l2 != null)
      {
        num = more;
        if (l1 != null)
        {
          num += l1.val;
          l1 = l1.next;
        }

        if (l2 != null)
        {
          num += l2.val;
          l2 = l2.next;
        }

        node = new ListNode((num) % 10);

        if (prev != null)
          prev.next = node;
        else head = node;

        prev = node;

        more = num / 10;
      }

      if (more > 0) prev.next = new ListNode(more);

      return head;
    }

    /**
     * Runtime: 108 ms, faster than 99.89% of C# online submissions for Add Two Numbers.
     * Memory Usage: 25.5 MB, less than 36.29% of C# online submissions for Add Two Numbers.
     */
    public ListNode Solution2(ListNode l1, ListNode l2)
    {
      ListNode node, prev = null, head = null;

      int num, more = 0;
      bool flag, flag2;

      while (true)
      {

        flag = l1 != null;
        flag2 = l2 != null;

        if(!flag && !flag2)break;

        num = more;
        if (flag)
        {
          num += l1.val;
          l1 = l1.next;
        }

        if (flag2)
        {
          num += l2.val;
          l2 = l2.next;
        }

        node = new ListNode((num) % 10);

        if (prev != null)
          prev.next = node;
        else head = node;

        prev = node;

        more = num / 10;
      }

      if (more > 0) prev.next = new ListNode(more);

      return head;
    }
  }
}