using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.LeetCode
{
  /// <summary>
  /// @desc : MyLinkedList  
  /// @author :mons
  /// @create : 2019/3/13 10:57:45 
  /// @source : 
  /// </summary>
  public class MyLinkedList
  {
    private int _len = 0;

    /** Initialize your data structure here. */

    private LinkedNode _root;

    public MyLinkedList()
    {
    }

    /** Get the value of the index-th node in the linked list. If the index is invalid, return -1. */
    public int Get(int index)
    {
      if (index >= _len)
        return -1;
      return Get(_root, index).Value;
    }

    /** Add a node of value val before the first element of the linked list. After the insertion, the new node will be the first node of the linked list. */
    public void AddAtHead(int val)
    {
      var node = new LinkedNode(val, _root);
      _root = node;
      _len++;
    }

    /** Append a node of value val to the last element of the linked list. */
    public void AddAtTail(int val)
    {
      var node = new LinkedNode(val);

      if (_root == null)
        _root = node;
      else
      {
        Get(_root, _len - 1).Next = node;
      }

      _len++;
    }

    /** Add a node of value val before the index-th node in the linked list. If index equals to the length of linked list, the node will be appended to the end of linked list. If index is greater than the length, the node will not be inserted. */
    public void AddAtIndex(int index, int val)
    {
      if (index == 0)
      {
        AddAtHead(val);
        return;
      }

      var node = new LinkedNode(val);

      if (_root == null)
        _root = node;
      else
      {
        LinkedNode searchNode;

        if (index + 1 > val)
        {
          searchNode = Get(_root, _len - 1);
        }
        else
        {
          searchNode = Get(_root, index - 1);
        }

        if (searchNode.Next == null)
        {
          searchNode.Next = node;
        }
        else
        {
          node.Next = searchNode.Next;
          searchNode.Next = node;
        }
      }

      _len++;
    }

    /** Delete the index-th node in the linked list, if the index is valid. */
    public void DeleteAtIndex(int index)
    {
      if (index < _len)
      {
        if (index == 0)
        {
          if (_root.Next == null)
            _root = null;
          else
            _root = _root.Next;
        }
        else
        {
          var node = Get(_root, index - 1);

          if (node.Next.Next == null)
          {
            node.Next = null;
          }
          else
          {
            node.Next = node.Next.Next;
          }
        }

        _len--;
      }
    }

    public void Show()
    {
      Console.WriteLine("----------------Show S----------------------");

      var node = _root;
      while (node != null)
      {
        Console.Write(node.Value);
        node = node.Next;
      }

      Console.WriteLine("\n----------------Show E----------------------");
    }

    protected LinkedNode Get(LinkedNode node, int index)
    {
      if (node == null)
        return null;
      if (index <= 0)
        return node;

      return Get(node.Next, index - 1);
    }
  }

  public class LinkedNode
  {
    public LinkedNode(int value)
    {
      Value = value;
    }

    public LinkedNode(int value, LinkedNode next)
    {
      Value = value;
      Next = next;
    }

    public int Value { get; set; }

    public LinkedNode Next { get; set; }
  }

  /**
   * Your MyLinkedList object will be instantiated and called as such:
   * MyLinkedList obj = new MyLinkedList();
   * int param_1 = obj.Get(index);
   * obj.AddAtHead(val);
   * obj.AddAtTail(val);
   * obj.AddAtIndex(index,val);
   * obj.DeleteAtIndex(index);
   */
}