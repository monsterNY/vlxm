using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.LeetCode
{
  /// <summary>
  /// @desc : IsPalindrome  
  /// @author :mons
  /// @create : 2019/3/15 10:55:51 
  /// @source : https://leetcode.com/problems/palindrome-linked-list/
  /// </summary>
  public class IsPalindrome
  {

    /**
     * Runtime: 100 ms, faster than 99.80% of C# online submissions for Palindrome Linked List.
     * Memory Usage: 26.4 MB, less than 28.33% of C# online submissions for Palindrome Linked List
     *
     * great~ 有点忘了回文数概念  即 2, 3, 5, 7, 11, 101, 131, 151,......
     *
     */
    public bool Solution(ListNode head)
    {
      if (head == null)
        return false;

      List<int> list = new List<int>() { head.val };

      while ((head = head.next) != null) list.Add(head.val);

      for (var i = 0; i < list.Count / 2; i++)
      {
        if (list[i] != list[list.Count - 1 - i])
          return false;
      }

      return true;

    }
  }

  public class ListNode
  {
    public int val;
    public ListNode next;

    public ListNode(int val, ListNode next)
    {
      this.val = val;
      this.next = next;
    }

    public ListNode(int x)
    {
      val = x;
    }
  }
}