using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using ConsoleTest.Domain;
using Tools.CusAttr;
using Tools.CusMenu;

namespace ConsoleTest.MiddleQuestionTwo
{
  /// <summary>
  /// @desc : Flatten  
  /// @author :mons
  /// @create : 2019/4/11 11:52:08 
  /// @source : https://leetcode.com/problems/flatten-a-multilevel-doubly-linked-list/
  /// </summary>
  [Love(LoveTypes.Skill)]
  [Description("recursion")]
  public class TwoFlatten
  {
    /**
     * Runtime: 168 ms, faster than 100.00% of C# online submissions for Flatten a Multilevel Doubly Linked List.
     * Memory Usage: 29.1 MB, less than 70.00% of C# online submissions for Flatten a Multilevel Doubly Linked List.
     *
     * hei hei ~~~
     *
     * Runtime: 168 ms, faster than 100.00% of C# online submissions for Flatten a Multilevel Doubly Linked List.
     * Memory Usage: 29 MB, less than 90.00% of C# online submissions for Flatten a Multilevel Doubly Linked List.
     *
     */
    public Node Solution(Node head)
    {
      /**
       * imagination:
       *
       * need ： 铺平这个node
       *
       * solution :
       * 1. 遍历node
       *
       * 2. 遇到child时
       *  2.1 使用child替换当前next 
       *  2.2 若next[旧]为null 继续步骤1  否则获取next[新]的最后一个节点
       *  2.3 使用最后一个节点绑定next[旧]
       *
       * 3. 遍历完成，返回最后一个节点
       *
       */
      GetLastNode(null, head);

      ShowNode(head);

      return head;
    }

    /// <summary>
    /// 测试输出
    /// </summary>
    /// <param name="node"></param>
    public void ShowNode(Node node)
    {
      while (node != null)
      {
        Console.WriteLine(node.val);
        node = node.next;
      }
    }

    public Node GetLastNode(Node prev, Node node)
    {
      if (node == null) return prev; //返回最后一个节点

      while (node != null) //遍历所有node
      {
        prev = node;
        if (node.child != null) //如果此node含有子节点
        {
          var next = node.next; //暂存next节点
          node.next = node.child; //用child替换next节点
          node.child = null; //使child失效
          node.next.prev = node; //给next[新]增加prev绑定
          if (next != null) //如果next[旧]节点不为空
          {
            var lastNode = GetLastNode(node, node.next); //获取next的前一个节点
            lastNode.next = next; //绑定next[旧]节点
            next.prev = lastNode; //绑定prev
            node = next.next; //更换node节点
          }
        }

        node = node.next; //更换到下一个节点
      }

      return prev; //返回最后一个节点
    }
  }
}