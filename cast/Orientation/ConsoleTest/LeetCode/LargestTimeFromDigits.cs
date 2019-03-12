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
    public string Solution(int[] A)
    {
      int hour, minutes;

      var num = 0;

      return null;
    }

    public void Combox(List<int> arr, int sum)
    {
      if (arr.Count == 0)
        Console.WriteLine("------------" + sum);

      for (int i = 0; i < arr.Count; i++)
      {
        var temp = new List<int>(arr);
        temp.RemoveAt(i);
        Combox(temp, (sum * 10) + arr[i]);
      }
    }
  }
}