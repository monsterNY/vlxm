using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : ValidateStackSequences  
  /// @author :mons
  /// @create : 2019/3/27 15:36:31 
  /// @source : https://leetcode.com/problems/validate-stack-sequences/
  /// </summary>
  public class ValidateStackSequences
  {

    /**
     * Runtime: 96 ms, faster than 100.00% of C# online submissions for Validate Stack Sequences.
     * Memory Usage: 24.4 MB, less than 90.91% of C# online submissions for Validate Stack Sequences.
     *
     * amazing~
     *
     * 。，
     *
     */
    public bool Solution(int[] pushed, int[] popped)
    {
      IList<int> list = new List<int>();
      bool flag;
      for (int i = 0, j = 0; i < popped.Length; i++)
      {
        if (list.Count == 0 || list[list.Count - 1] != popped[i])
        {
          flag = false;
          for (; j < pushed.Length; j++)
          {
            if (pushed[j] == popped[i])
            {
              flag = true;
              j++;
              break;
            }

            list.Add(pushed[j]);
          }

          if (!flag) return false;
        }
        else
          list.Remove(list[list.Count - 1]);
      }

      return true;
    }
  }
}