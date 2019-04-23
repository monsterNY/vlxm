using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestionThree
{
  /// <summary>
  /// @desc : CanTransform  
  /// @author :mons
  /// @create : 2019/4/23 15:21:00 
  /// @source : https://leetcode.com/problems/swap-adjacent-in-lr-string/
  /// </summary>
  public class CanTransform
  {
    /**
     * Runtime: 76 ms, faster than 95.56% of C# online submissions for Swap Adjacent in LR String.
     * Memory Usage: 22.7 MB, less than 100.00% of C# online submissions for Swap Adjacent in LR String.
     */
    public bool Solution(string start, string end)
    {
      var arr = start.ToCharArray();
      for (int i = 0; i < arr.Length; i++)
      {
        if (arr[i] != end[i])
        {
          if (!GetNeedChar(i, i + 1, arr, end[i])) return false;
        }
      }

      return true;
    }

    //更低效。。。
    public bool Solution2(string start, string end)
    {
      var arr = start.ToCharArray();

      var map = new Dictionary<char, int>();

      foreach (var item in start)
      {
        if (map.ContainsKey(item)) map[item]++;
        else map.Add(item, 1);
      }

      for (int i = 0; i < arr.Length; i++)
      {
        if (arr[i] != end[i])
        {
          if (!map.ContainsKey(end[i]) || map[end[i]] == 0) return false;
          if (!GetNeedChar(i, i + 1, arr, end[i])) return false;
        }

        map[arr[i]]--;
      }

      return true;
    }

    public bool GetNeedChar(int start, int index, char[] arr, char needChar)
    {
      if (index == arr.Length) return false;

      if (arr[index] == needChar)
      {
        for (; index > start; index--)
        {
          //一个动作包括用“LX”替换一个出现的“XL”，或者用“XR”替换一个出现的“RX” 这里真的坑。
          if (!((arr[index] == 'L' && arr[index - 1] == 'X')
                || (arr[index] == 'X' && arr[index - 1] == 'R'))) return false;
          var temp = arr[index];
          arr[index] = arr[index - 1];
          arr[index - 1] = temp;
        }

        return true;
      }

      return GetNeedChar(start, index + 1, arr, needChar);
    }
  }
}