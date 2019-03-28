using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : ScoreOfParentheses  
  /// @author :mons
  /// @create : 2019/3/28 13:40:39 
  /// @source : https://leetcode.com/problems/score-of-parentheses/
  /// </summary>
  public class ScoreOfParentheses
  {

    /**
     * Runtime: 72 ms, faster than 100.00% of C# online submissions for Score of Parentheses.
     * Memory Usage: 19.6 MB, less than 100.00% of C# online submissions for Score of Parentheses.
     *
     * cool
     *
     * 我真是爱递归 .,
     *
     */
    public int Solution(string S)
    {
      var score = 0;

      for (int i = 0; i < S.Length; i++)
      {
        //当我遇到了一个( 我需要先计算 (?) 中 ? 的分数
        if (S[i] == '(')
        {
          score += GetScore(S, ref i);
        }
      }

      return score;
    }

    //获取?的分数
    //由于在计算?时 下标发生了改变且外部需要进行跟踪 则使用ref进行跟踪。
    public int GetScore(string str, ref int startIndex)
    {
      var count = 0;
      for (startIndex++; startIndex < str.Length; startIndex++)
      {
        if (str[startIndex] == ')')
        {
          //当?为空时 返回1 否则返回 ?的分数
          return count > 0 ? count: 1;
        }
        else
        {
          //当?中存在 (?) 时 递归计算?的分数
          count += GetScore(str, ref startIndex) * 2;
        }
      }

      return count;
    }

    /**
     * same efficient solution
     *
     *
     *
     */
    public int OtherSolution(String S)
    {
      Stack<int> stack = new Stack<int>();
      int cur = 0;
      foreach (char c in S)
      {
        if (c == '(')
        {
          stack.Push(cur);
          cur = 0;
        }
        else
        {
          cur = stack.Pop() + Math.Max(cur * 2, 1);
        }
      }
      return cur;
    }

  }
}