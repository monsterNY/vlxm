using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestionTwo
{
  /// <summary>
  /// @desc : SummaryRanges  
  /// @author :mons
  /// @create : 2019/4/18 16:15:18 
  /// @source : https://leetcode.com/problems/summary-ranges/
  /// </summary>
  public class SummaryRanges
  {

    /**
     * Runtime: 252 ms, faster than 98.25% of C# online submissions for Summary Ranges.
     * Memory Usage: 28.3 MB, less than 14.29% of C# online submissions for Summary Ranges.
     */
    public IList<string> Solution(int[] nums)
    {

      IList<string> res = new List<string>();

      if (nums.Length == 0) return res;

      int start = 0,end = 0;

      for (int i = 1; i < nums.Length; i++)
      {

        if (nums[i] == nums[i - 1] + 1)
        {
          end++;
        }
        else
        {
          if(start == end)
            res.Add(nums[start].ToString());
          else 
            res.Add($"{nums[start]}->{nums[end]}");

          start = i;
          end = i;
        }
      }

      if (start == end)
        res.Add(nums[start].ToString());
      else
        res.Add($"{nums[start]}->{nums[end]}");

      return res;

    }

  }
}
