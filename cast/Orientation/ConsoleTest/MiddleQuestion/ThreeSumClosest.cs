using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : ThreeSumClosest  
  /// @author :mons
  /// @create : 2019/4/9 10:22:12 
  /// @source : https://leetcode.com/problems/3sum-closest/
  /// </summary>
  [Obsolete("complex")]
  public class ThreeSumClosest
  {
    #region other Solution

    /**
     * Runtime: 96 ms, faster than 100.00% of C# online submissions for 3Sum Closest.
     * Memory Usage: 22.7 MB, less than 22.73% of C# online submissions for 3Sum Closest.
     *
     * 还真要这么麻烦。。。
     *
     */
    public int threeSumClosest(int[] nums, int target)
    {
      Array.Sort(nums);
      int closestSum = nums[0] + nums[1] + nums[2];
      int minDiff = Math.Abs(target - closestSum);
      for (int i = 0; i < nums.Length - 2; i++)
      {
        if (i > 0 && nums[i] == nums[i - 1])
        {
          continue;
        }

        int l = i + 1;
        int r = nums.Length - 1;

        while (l != r)
        {
          int sum = nums[i] + nums[l] + nums[r];
          if (sum == target)
          {
            return target;
          }

          if (Math.Abs(target - sum) < minDiff)
          {
            closestSum = sum;
            minDiff = Math.Abs(target - sum);
          }

          if (sum < target)
          {
            l++;
          }
          else
          {
            r--;
          }
        }
      }

      return closestSum;
    }

    #endregion

    private int result, minDiff = int.MaxValue;

    //bug
    public int Try2(int[] nums, int target)
    {
      int firstIndex = 0,
        secondIndex = 1,
        thirdIndex = 2,
        result = nums[0] + nums[1] + nums[2],
        minDiff = result > target ? result - target : target - result,
        firstDiff,
        secondDiff,
        thirdDiff;

      for (int i = 3; i < nums.Length; i++)
      {
        if (minDiff == 0) return result;

        firstDiff = nums[i] + nums[secondIndex] + nums[thirdIndex];
        secondDiff = nums[firstIndex] + nums[i] + nums[thirdIndex];
        thirdDiff = nums[firstIndex] + nums[secondIndex] + nums[i];

        firstDiff = firstDiff > target ? firstDiff - target : target - firstDiff;
        secondDiff = secondDiff > target ? secondDiff - target : target - secondDiff;
        thirdDiff = thirdDiff > target ? thirdDiff - target : target - thirdDiff;

        if (firstDiff < minDiff || secondDiff < minDiff || thirdDiff < minDiff)
        {
          if (thirdDiff <= firstDiff && thirdDiff <= secondDiff)
          {
            thirdIndex = i;
            minDiff = thirdDiff;
          }
          else if (firstDiff <= secondDiff && firstDiff <= thirdDiff)
          {
            firstIndex = i;
            minDiff = firstDiff;
          }
          else if (secondDiff <= thirdDiff && secondDiff <= firstDiff)
          {
            secondIndex = i;
            minDiff = secondDiff;
          }

          result = nums[firstIndex] + nums[secondIndex] + nums[thirdIndex];
        }
      }

      return result;
    }

    //bug
    public int Try(int[] nums, int target)
    {
      //获取三个 距离 target最近的数？__no

      IDictionary<int, List<int>> dictionary = new Dictionary<int, List<int>>();

      for (var i = 0; i < nums.Length; i++)
      {
        var diff = nums[i] > target ? nums[i] - target : target - nums[i];
        if (dictionary.ContainsKey(diff))
          dictionary.Add(diff, new List<int>() {nums[i]});
        else
          dictionary[diff].Add(nums[i]);
      }

      var count = 0;
      var result = 0;
      foreach (var item in dictionary.OrderBy(u => u.Key))
        for (var i = 0; i < item.Value.Count; i++)
        {
          result += item.Value[i];
          count++;
          if (count == 3) return result;
        }

      return result;
    }

    //time limit
    public int Simple(int[] nums, int target)
    {
      GetList(new List<int>(nums), 0, 0, target);
      return result;
    }

    public void GetList(List<int> arr, int build, int count, int target)
    {
      if (count + arr.Count < 3) return;
      if (count == 3)
      {
        var diff = target > build ? target - build : build - target;
        if (diff < minDiff)
        {
          minDiff = diff;
          result = build;
        }

        return;
      }

      for (var i = 0; i < arr.Count; i++)
      {
        var temp = new List<int>(arr);
        temp.RemoveAt(i);
        GetList(temp, build + arr[i], count + 1, target);
      }
    }
  }
}