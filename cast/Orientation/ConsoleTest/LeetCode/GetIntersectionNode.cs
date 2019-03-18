using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.LeetCode
{
  /// <summary>
  /// @desc : GetIntersectionNode  
  /// @author :mons
  /// @create : 2019/3/18 14:21:50 
  /// @source : https://leetcode.com/problems/intersection-of-two-linked-lists/
  /// </summary>
  public class GetIntersectionNode
  {

    /**
     * 不擅长。
     */
    public ListNode Solution(ListNode headA, ListNode headB)
    {
      Dictionary<ListNode, bool> map = new Dictionary<ListNode, bool>();

      while (headA != null)
      {
        map.Add(headA, true);
        headA = headA.next;
      }

      while (headB!=null)
      {
        if (map.ContainsKey(headB)) return headB;
        headB = headB.next; 
      }

      List<ListNode> list = new List<ListNode>();

      while (headA != null)
      {
        list.Add(headA);
        headA = headA.next;
      }

      while (headB != null)
      {
        if (list.Contains(headA)) return headB;
        headB = headB.next;
      }

      while (headA!=null || headB!=null)
      {

        if (headB != null)
        {
          if (list.Contains(headB)) return headB;
          list.Add(headB);
          headB = headB.next;
        }

        if (headA != null)
        {
          if (list.Contains(headA)) return headA;
          list.Add(headA);
          headA = headA.next;
        }

      }

//      Dictionary<ListNode, int> map = new Dictionary<ListNode, int>();
//
//      while (headA != null || headB != null)
//      {
//        if (headB != null)
//        {
//          if (map.ContainsKey(headB)) return headB;
//          map.Add(headB, 1);
//          headB = headB.next;
//        }
//
//        if (headA != null)
//        {
//          if (map.ContainsKey(headA)) return headA;
//          map.Add(headA, 1);
//          headB = headA.next;
//        }
//      }

      return null;
    }

    public ListNode OtherSolution(ListNode headA, ListNode headB)
    {
      //boundary check
      if (headA == null || headB == null) return null;

      ListNode a = headA;
      ListNode b = headB;

      //if a & b have different len, then we will stop the loop after second iteration
      while (a != b)
      {
        //for the end of first iteration, we just reset the pointer to the head of another linkedlist
        a = a == null ? headB : a.next;//amazing 形成了环形
        b = b == null ? headA : b.next;
      }

      return a;
    }

  }
}