using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : SingleNonDuplicate  
  /// @author :mons
  /// @create : 2019/3/27 17:11:26 
  /// @source : https://leetcode.com/problems/single-element-in-a-sorted-array/
  /// </summary>
  public class SingleNonDuplicate
  {
    /**
     * Runtime: 108 ms, faster than 60.61% of C# online submissions for Single Element in a Sorted Array.
     * Memory Usage: 22.3 MB, less than 40.00% of C# online submissions for Single Element in a Sorted Array.
     *
     * Runtime: 88 ms, faster than 100.00% of C# online submissions for Single Element in a Sorted Array.
     * Memory Usage: 22.4 MB, less than 40.00% of C# online submissions for Single Element in a Sorted Array.
     *
     * 测试有毒吧。
     *
     */
    public int Solution(int[] nums)
    {
      for (int i = 1; i < nums.Length; i += 2)
      {
        if (nums[i] != nums[i - 1]) return nums[i - 1];
      }

      return nums[nums.Length - 1];
    }

    /**
     * 想到了，二分法  诶于懒惰就没弄了。，
     */
    public static int OtherSolution(int[] nums)
    {
      int start = 0, end = nums.Length - 1;

      while (start < end)
      {
        // We want the first element of the middle pair,
        // which should be at an even index if the left part is sorted.
        // Example:
        // Index: 0 1 2 3 4 5 6
        // Array: 1 1 3 3 4 8 8
        //            ^
        int mid = (start + end) / 2;
        if (mid % 2 == 1) mid--;

        // We didn't find a pair. The single element must be on the left.
        // (pipes mean start & end)
        // Example: |0 1 1 3 3 6 6|
        //               ^ ^
        // Next:    |0 1 1|3 3 6 6
        if (nums[mid] != nums[mid + 1]) end = mid;

        // We found a pair. The single element must be on the right.
        // Example: |1 1 3 3 5 6 6|
        //               ^ ^
        // Next:     1 1 3 3|5 6 6|
        else start = mid + 2;
      }

      // 'start' should always be at the beginning of a pair.
      // When 'start > end', start must be the single element.
      return nums[start];
    }

  }
}