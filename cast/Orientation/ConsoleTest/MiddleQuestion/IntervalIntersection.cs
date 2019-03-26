using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : IntervalIntersection  
  /// @author :monster_yj
  /// @create : 2019/3/23 13:51:59 
  /// @source : https://leetcode.com/problems/interval-list-intersections/
  /// </summary>
  public class IntervalIntersection
  {
    /**
     * Runtime: 444 ms, faster than 76.13% of C# online submissions for Interval List Intersections.
     * Memory Usage: 35.7 MB, less than 41.18% of C# online submissions for Interval List Intersections.
     *
     * 
     * Runtime: 412 ms, faster than 92.26% of C# online submissions for Interval List Intersections.
     * Memory Usage: 35.7 MB, less than 52.94% of C# online submissions for Interval List Intersections.
     *
     * 提示就是好~
     *
     */
    public Interval[] Solution(Interval[] A, Interval[] B)
    {
      List<Interval> list = new List<Interval>();

      for (int i = 0, j = 0; i < A.Length; i++)
      {
        for (; j < B.Length; j++)
        {
          if (A[i].end < B[j].start)
            break;

          var flagStartMoreThan = A[i].start > B[j].start;
          var flagStartEq = A[i].start == B[j].start;
          var flagEndMoreThan = A[i].end > B[j].end;
          var flagEndEq = A[i].end == B[j].end;

          if ((flagStartMoreThan || flagStartEq) && (flagEndEq || !flagEndMoreThan))
            list.Add(A[i]);
          else if ((flagStartEq || !flagStartMoreThan) && (flagEndEq || flagEndMoreThan))
            list.Add(B[j]);
//          else if ((!flagStartMoreThan || flagStartEq) && A[i].end >= B[j].start)
          else if ((!flagStartMoreThan) && A[i].end >= B[j].start)
            list.Add(new Interval(B[j].start, A[i].end));
//          else if (A[i].start <= B[j].end && (flagEndMoreThan || flagEndEq))
          else if (A[i].start <= B[j].end && (flagEndMoreThan))
            list.Add(new Interval(A[i].start, B[j].end));

          if (A[i].end <= B[j].end)
            break;
        }
      }

      return list.ToArray();
    }

    /**
     * Runtime: 408 ms, faster than 100.00% of C# online submissions for Interval List Intersections.
     * Memory Usage: 35.7 MB, less than 35.29% of C# online submissions for Interval List Intersections.
     *
     * great~
     *
     */
    public Interval[] OtherSolution(Interval[] A, Interval[] B)
    {
      List<Interval> list = new List<Interval>();

      for (int i = 0, j = 0; i < A.Length && j < B.Length;)
      {
        if (A[i].end < B[j].start) ++i;
        else if (B[j].end < A[i].start) ++j;
        else
        {
          list.Add(new Interval(Math.Max(A[i].start, B[j].start), Math.Min(A[i].end, B[j].end)));
          if (A[i].end < B[j].end) ++i;
          else ++j;
        }
      }

      return list.ToArray();
    }
  }

  public class Interval
  {
    public int start;
    public int end;

    public Interval()
    {
      start = 0;
      end = 0;
    }

    public Interval(int s, int e)
    {
      start = s;
      end = e;
    }
  }
}