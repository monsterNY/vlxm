using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace ConsoleTest.LeetCode
{
  /// <summary>
  /// @desc : ThirdMax  
  /// @author :mons
  /// @create : 2019/3/14 9:43:41 
  /// @source : https://leetcode.com/problems/third-maximum-number/
  ///
  /// 题目信息太少。，
  /// 
  /// </summary>
  [Obsolete]
  public class ThirdMax
  {
    
    public int Solution(int[] nums)
    {
      int one, two = 0, maxNum = nums[0];

      int? max = null;

      for (int i = 0; i < nums.Length; i++)
      {
        one = nums[i];
        if (one > maxNum)
        {
          maxNum = one;
        }

        for (i++; i < nums.Length; i++)
        {
          if (nums[i] != one)
          {
            two = nums[i];
            if (two > maxNum)
            {
              maxNum = two;
            }

            break;
          }
        }

        for (i++; i < nums.Length; i++)
        {
          if (nums[i] != one && nums[i] != two)
          {
            if (max == null || nums[i] > max)
            {
              max = nums[i];
            }

            break;
          }
        }
      }

      var result = max.HasValue ? max.Value : maxNum;

      CheckResult(nums, result);

      return result;
    }

    public void CheckResult(int[] arr, int result)
    {
      int max = arr[0];
      int? thirdMax = null;
      var list = new List<int>();

      for (int i = 0, index = i; i < arr.Length; i++)
      {
        if (arr[i] > max)
          max = arr[i];

        if (list.Contains(arr[i]))
        {
          index++;
          continue;
        }

        list.Add(arr[i]);

        if (index + 2 == i)
        {
          if (thirdMax == null)
          {
            thirdMax = list[2];
          }
          else if (list[2] > thirdMax)
          {
            thirdMax = list[2];
          }

          index = i + 1;
          list.Clear();
        }
      }

      if (!(result == max || (thirdMax.HasValue && thirdMax == result)))
        throw new Exception(JsonConvert.SerializeObject(arr));
    }
    
  }
}