using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestionThree
{
  /// <summary>
  /// @desc : SearchRange  
  /// @author :mons
  /// @create : 2019/4/23 11:00:26 
  /// @source : https://leetcode.com/problems/find-first-and-last-position-of-element-in-sorted-array/
  /// </summary>
  public class SearchRange
  {
    /**
     * Runtime: 252 ms, faster than 98.28% of C# online submissions for Find First and Last Position of Element in Sorted Array.
     * Memory Usage: 30.2 MB, less than 70.77% of C# online submissions for Find First and Last Position of Element in Sorted Array.
     *
     * Runtime: 252 ms, faster than 98.28% of C# online submissions for Find First and Last Position of Element in Sorted Array.
     * Memory Usage: 30.1 MB, less than 80.00% of C# online submissions for Find First and Last Position of Element in Sorted Array.
     *
     * planB : 使用二分法找到距离target最近的位置进行搜寻
     *
     */
    public int[] Solution(int[] nums, int target)
    {
      var res = new[] {-1, -1};

      for (int i = 0; i < nums.Length; i++)
      {
        if (nums[i] > target) break;
        if (nums[i] == target)
        {
          if (res[0] == -1)
//          {
            res[0] = i;
//            res[1] = i;
//          }
          else res[1] = i;
        }
      }

      //测试使用二次验证效率更高？？？
      if (res[0] >= 0 && res[1] < 0) res[1] = res[0];

      return res;
    }

    //bug
    public int[] Solution2(int[] nums, int target)
    {

      if (nums.Length == 0) return new[] {-1, -1};

      int start = 0, end = nums.Length - 1, middle;

      while (true)
      {

        middle = (start + end) / 2;

        if (nums[middle] > target)
        {
          end = middle;
        }
        else if (nums[middle] < target)
        {
          start = middle;
        }
        else
        {
          break;
        }

        if (end <= start + 1)
        {
          return new[] { -1, -1 };
        }

      }

      var res = new int[] {middle, middle};

      for (int i = middle - 1; i >= 0; i--)
      {
        if (nums[i] == target) res[0]--;
        else break;
      }

      for (int i = middle + 1; i < nums.Length; i++)
      {
        if (nums[i] == target) res[1]++;
        else break;
      }

      return res;
    }
  }
}