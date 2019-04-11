using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleTest.Domain.StructModel;

namespace ConsoleTest.MiddleQuestionTwo
{
  /// <summary>
  /// @desc : EraseOverlapIntervals  
  /// @author :mons
  /// @create : 2019/4/11 9:34:18 
  /// @source : https://leetcode.com/problems/non-overlapping-intervals/
  /// </summary>
  [Obsolete]
  public class EraseOverlapIntervals
  {
    public int Simple(Interval[] intervals)
    {
      ISet<int> set = new HashSet<int>();
      var count = 0;
      foreach (var item in intervals.OrderBy(u => u.end - u.start))
      {
        if (set.Contains(item.start) && set.Contains(item.end))
        {
          count++;
          continue;
        }

        for (int i = item.start; i <= item.end; i++)
        {
          set.Add(i);
        }
      }

      return count;
    }
  }
  
}