using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestionThree
{
  /// <summary>
  /// @desc : MajorityElement  
  /// @author : mons
  /// @create : 2019/4/24 16:47:42 
  /// @source : https://leetcode.com/problems/majority-element-ii/
  /// </summary>
  public class MajorityElement
  {
    /**
     * Runtime: 288 ms, faster than 28.14% of C# online submissions for Majority Element II.
     * Memory Usage: 31.4 MB, less than 57.14% of C# online submissions for Majority Element II.
     *
     * Runtime: 264 ms, faster than 69.26% of C# online submissions for Majority Element II.
     * Memory Usage: 31.5 MB, less than 28.57% of C# online submissions for Majority Element II
     *
     */
    public IList<int> Solution(int[] nums)
    {
      IList<int> res = new List<int>();

      Dictionary<int, int> dictionary = new Dictionary<int, int>();

      var limit = nums.Length / 3;

      foreach (var num in nums)
      {
        if (dictionary.ContainsKey(num))
        {
          if (dictionary[num] == limit) res.Add(num);
          dictionary[num]++;
        }
        else
        {
          if (0 == limit) res.Add(num);
          dictionary.Add(num, 1);
        }
      }

      return res;
    }
  }
}