using System;
using System.Collections.Generic;
using System.Text;
using ConsoleTest.LeetCode;

namespace ConsoleTest.MiddleQuestionThree
{
  /// <summary>
  /// @desc : RotateRight  
  /// @author : mons
  /// @create : 2019/4/26 11:54:48 
  /// @source : https://leetcode.com/problems/rotate-list/
  /// </summary>
  public class RotateRight
  {

    /**
     * Runtime: 96 ms, faster than 72.18% of C# online submissions for Rotate List.
     * Memory Usage: 23.5 MB, less than 8.33% of C# online submissions for Rotate List.
     */
    public ListNode Solution(ListNode head, int k)
    {
      if (k == 0) return head;

      List<ListNode> list = new List<ListNode>();

      var node = head;

      while (node != null)
      {
        list.Add(node);
        node = node.next;
      }

      if (list.Count == 0 || ((k %= list.Count) == 0)) return head;

      list[list.Count - 1].next = list[0];

      list[list.Count - 1 - k].next = null;

      return list[list.Count - k];

    }



  }
}