using System;
using System.Collections.Generic;
using System.Text;
using ConsoleTest.LeetCode;

namespace ConsoleTest.MiddleQuestionThree
{
  /// <summary>
  /// @desc : DeleteDuplicates  
  /// @author : mons
  /// @create : 2019/4/24 10:06:37 
  /// @source : https://leetcode.com/problems/remove-duplicates-from-sorted-list-ii/
  /// </summary>
  public class DeleteDuplicates
  {

    //刚开始的想法。。。
    public ListNode OtherSolution(ListNode head)
    {
      if (head == null) return null;
      ListNode FakeHead = new ListNode(0);
      FakeHead.next = head;
      ListNode pre = FakeHead;
      ListNode cur = head;
      while (cur != null)
      {
        while (cur.next != null && cur.val == cur.next.val)
        {
          cur = cur.next;
        }
        if (pre.next == cur)
        {
          pre = pre.next;
        }
        else
        {
          pre.next = cur.next;
        }
        cur = cur.next;
      }
      return FakeHead.next;
    }

    /**
     * Runtime: 96 ms, faster than 66.79% of C# online submissions for Remove Duplicates from Sorted List II.
     * Memory Usage: 24.1 MB, less than 7.14% of C# online submissions for Remove Duplicates from Sorted List II.
     */
    public ListNode Simple(ListNode head)
    {
      Dictionary<int, int> dictionary = new Dictionary<int, int>();

      var node = head;
      while (node != null)
      {
        if (dictionary.ContainsKey(node.val)) dictionary[node.val]++;
        else dictionary.Add(node.val, 1);
        node = node.next;
      }

      head = null;

      foreach (var item in dictionary)
      {
        if(item.Value>1)continue;

        var newNode = new ListNode(item.Key);
        if (head == null)
        {
          head = newNode;
        }
        else
        {
          node.next = newNode;
        }
        node = newNode;

      }

      return head;
    }
  }
}