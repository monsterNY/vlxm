using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleTest.LeetCode
{
  /// <summary>
  /// @desc : LargestTimeFromDigits  
  /// @author :mons
  /// @create : 2019/3/12 17:41:40 
  /// @source : https://leetcode.com/problems/largest-time-for-given-digits/
  /// </summary>
  public class LargestTimeFromDigits
  {

    /**
     * Runtime: 112 ms, faster than 94.44% of C# online submissions for Largest Time for Given Digits.
     * Memory Usage: 23.5 MB, less than 50.00% of C# online submissions for Largest Time for Given Digits.
     *
     * 这个有点坑... 懒得优化了
     *
     */
    public string Solution(int[] A)
    {
      List<int> result = new List<int>();

      Combination(new List<int>(A), 0, result);

      int hour, minutes, maxHour = 0, maxMinutes = 0;
      bool hasResult = false;

      foreach (var item in result)
      {
        minutes = item % 100;
        hour = item / 100;
        if (hour > 23 || minutes > 59)
          continue;
        if (!hasResult) hasResult = true;
        if (hour > maxHour)
        {
          maxHour = hour;
          maxMinutes = minutes;
        }
        else if (hour == maxHour && minutes > maxMinutes)
        {
          maxMinutes = minutes;
        }
      }

      if (!hasResult)
        return string.Empty;

      return $"{(maxHour < 10 ? "0" : "")}{maxHour}:{(maxMinutes < 10 ? "0" : "")}{maxMinutes}";
    }

    public void Combination(List<int> arr, int sum, List<int> resultList)
    {
      if (arr.Count == 0)
        resultList.Add(sum);

      for (int i = 0; i < arr.Count; i++)
      {
//        Console.WriteLine($"sum:{sum},arr:{arr.Count}");
        var temp = new List<int>(arr);
        temp.RemoveAt(i);
        Combination(temp, (sum * 10) + arr[i], resultList);
      }
    }
  }
}