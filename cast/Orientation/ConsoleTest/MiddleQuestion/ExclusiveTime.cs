using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : ExclusiveTime  
  /// @author :mons
  /// @create : 2019/4/3 10:39:47 
  /// @source : https://leetcode.com/problems/exclusive-time-of-functions/
  /// </summary>
  [Obsolete("Stack")]
  public class ExclusiveTime
  {

    /**
     * Runtime: 272 ms, faster than 90.70% of C# online submissions for Exclusive Time of Functions.
     * Memory Usage: 30.9 MB, less than 75.00% of C# online submissions for Exclusive Time of Functions.
     *
     * source : https://leetcode.com/problems/exclusive-time-of-functions/discuss/105062/Java-Stack-Solution-O(n)-Time-O(n)-Space
     *
     */
    public int[] OtherSolution(int n, IList<string> logs)
    {
      var result = new int[n];

      ReadOnlySpan<char> span;

      int firstIndex, secondIndex, id, time, prevTime = 0;
      Stack<int> stack = new Stack<int>();

      char flag;

      for (int i = 1; i < logs.Count; i++)
      {
        span = logs[i - 1].AsSpan();

        firstIndex = span.IndexOf(":");
        secondIndex = span.LastIndexOf(":");

        id = int.Parse(span.Slice(0, span.IndexOf(":")));
        time = int.Parse(span.Slice(span.LastIndexOf(":") + 1, span.Length - secondIndex - 1));
        flag = span[firstIndex + 1];

        if (stack.Count > 0) result[stack.Peek()] += time - prevTime;

        prevTime = time;
        if (flag == 's')
          stack.Push(id);
        else
        {
          result[stack.Pop()]++;
          prevTime++;
        }
      }

      return result;
    }

    public int[] Try(int n, IList<string> logs)
    {
      var result = new int[n];

      ReadOnlySpan<char> first, second, firstFlag, secondFlag;

      int firstIndex, secondIndex, firstId, firstTime, secondId, secondTime;

      for (int i = 1; i < logs.Count; i++)
      {
        first = logs[i - 1].AsSpan();

        firstIndex = first.IndexOf(":");
        secondIndex = first.LastIndexOf(":");

        firstId = int.Parse(first.Slice(0, firstIndex));
        firstTime = int.Parse(first.Slice(secondIndex + 1, first.Length - secondIndex - 1));
        firstFlag = first.Slice(firstIndex + 1, first.Length - secondIndex);

        second = logs[i].AsSpan();

        firstIndex = second.IndexOf(":");
        secondIndex = second.LastIndexOf(":");

        secondId = int.Parse(second.Slice(0, firstIndex));
        secondTime = int.Parse(second.Slice(secondIndex + 1, second.Length - secondIndex - 1));
        secondFlag = second.Slice(firstIndex + 1, second.Length - secondIndex);

        if (firstTime == secondTime) continue;
        if (firstId != secondId)
        {
          if (firstFlag[0] == 's')
          {
            result[firstId] += secondTime - firstTime;
          }
          else
          {
            result[secondId] += secondTime - firstTime;
          }
        }
        else
        {
          result[firstId] += secondTime - firstTime + (firstFlag[0] == 's' && secondFlag[0] == 'e' ? 1 : 0);
        }
      }

      return result;
    }
  }
}