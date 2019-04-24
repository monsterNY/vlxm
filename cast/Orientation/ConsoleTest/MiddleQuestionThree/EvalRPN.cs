using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestionThree
{
  /// <summary>
  /// @desc : EvalRPN  
  /// @author : mons
  /// @create : 2019/4/24 16:18:04 
  /// @source : https://leetcode.com/problems/evaluate-reverse-polish-notation/
  /// </summary>
  public class EvalRPN
  {
    /**
     * Runtime: 100 ms, faster than 95.92% of C# online submissions for Evaluate Reverse Polish Notation.
     * Memory Usage: 23.6 MB, less than 26.32% of C# online submissions for Evaluate Reverse Polish Notation.
     */
    public int Solution(string[] tokens)
    {
      Stack<int> stack = new Stack<int>();
      foreach (var token in tokens)
      {
        if (token == "+")
        {
          stack.Push(stack.Pop() + stack.Pop());
        }
        else if (token == "-")
        {
          stack.Push(-stack.Pop() + stack.Pop());
        }
        else if (token == "*")
        {
          stack.Push(stack.Pop() * stack.Pop());
        }
        else if (token == "/")
        {
          var num = stack.Pop();
          stack.Push(stack.Pop() / num);
        }
        else
        {
          stack.Push(int.Parse(token));
        }
      }

      return stack.Count > 0 ? stack.Peek() : 0;
    }

    /**
     * Runtime: 96 ms, faster than 100.00% of C# online submissions for Evaluate Reverse Polish Notation.
     * Memory Usage: 23.6 MB, less than 26.32% of C# online submissions for Evaluate Reverse Polish Notation.
     */
    public int Solution2(string[] tokens)
    {
      Stack<int> stack = new Stack<int>();
      foreach (var token in tokens)
      {
        if (token[0] == '+')
        {
          stack.Push(stack.Pop() + stack.Pop());
        }
        else if (token[0] == '-' && token.Length == 1)
        {
          stack.Push(-stack.Pop() + stack.Pop());
        }
        else if (token[0] == '*')
        {
          stack.Push(stack.Pop() * stack.Pop());
        }
        else if (token[0] == '/')
        {
          var num = stack.Pop();
          stack.Push(stack.Pop() / num);
        }
        else
        {
          stack.Push(int.Parse(token));
        }
      }

      return stack.Count > 0 ? stack.Peek() : 0;
    }
  }
}