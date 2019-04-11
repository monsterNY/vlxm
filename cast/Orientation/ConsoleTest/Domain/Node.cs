using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.Domain
{
  /// <summary>
  /// @desc : Node  
  /// @author :mons
  /// @create : 2019/4/11 11:52:30 
  /// @source : https://leetcode.com/problems/flatten-a-multilevel-doubly-linked-list/
  /// </summary>
  public class Node
  {
    public int val;
    public Node prev;
    public Node next;
    public Node child;

    public Node()
    {
    }

    public Node(int _val)
    {
      val = _val;
    }

    public Node(int _val, Node _prev, Node _next, Node _child)
    {
      val = _val;
      prev = _prev;
      next = _next;
      child = _child;
    }

    public static implicit operator Node(int val)
    {
      return new Node(val);
    }

  }
}