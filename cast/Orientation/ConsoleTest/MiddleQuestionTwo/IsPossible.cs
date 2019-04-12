using System;
using System.Collections.Generic;
using System.Text;
using Tools.CusExtension;

namespace ConsoleTest.MiddleQuestionTwo
{
  /// <summary>
  /// @desc : IsPossible  
  /// @author :mons
  /// @create : 2019/4/12 11:38:26 
  /// @source : https://leetcode.com/problems/split-array-into-consecutive-subsequences/
  /// </summary>
  [Obsolete("no imagination")]
  public class IsPossible
  {
    /**
     *
     *We iterate through the array once to get the frequency of all the elements in the array
     * We iterate through the array once more and for each element we either see if it can be appended to a previously constructed consecutive
     * sequence or if it can be the start of a new consecutive sequence. If neither are true, then we return false.
     *
     */
    public bool isPossible(int[] nums)
    {
      Dictionary<int, int> freq = new Dictionary<int, int>(), appendfreq = new Dictionary<int, int>();
      foreach (int i in nums)
        freq.Increase(i);

      foreach (int i in nums)
      {
        if (freq.Get(i, 0) == 0) continue;

        if (appendfreq.Get(i, 0) > 0) //是否可以附加到以前构造的连续序列中
        {
          appendfreq.Decrease(i);
          appendfreq.Increase(i + 1);
        }
        else if (freq.Get(i + 1, 0) > 0 && freq.Get(i + 2, 0) > 0) //是否可以作为一个新的连续序列的开始
        {
          freq.Decrease(i + 1);
          freq.Decrease(i + 2);
          appendfreq.Increase(i + 3);
        }
        else return false;

        freq.Decrease(i);
      }

      return true;
    }
  }
}