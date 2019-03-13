using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ConsoleTest.LeetCode
{
  /// <summary>
  /// @desc : FindUnsortedSubarray  
  /// @author :mons
  /// @create : 2019/3/13 16:19:23 
  /// @source : 
  /// </summary>
  public class FindUnsortedSubarray
  {
    public int Solution(int[] nums)
    {
      var arr = nums.Clone();

      int startIndex = -1, endIndex = -1;

      int max = nums[0], min = nums[0];

      for (int i = 0; i < nums.Length; i++)
      {
        for (int j = i; j < nums.Length; j++)
        {
          if (nums[i] > nums[j])
          {
            startIndex = i;
          }
        }

        if (startIndex == i)
          break;
      }

      if (startIndex == -1)
        return 0;

      for (int i = nums.Length - 1; i >= 0; i--)
      {
        for (int j = 0; j < i; j++)
        {
          if (nums[i] < nums[j])
          {
            endIndex = i;
          }
        }

        if (endIndex == i)
          break;
      }

      //      for (int i = 1; i < nums.Length; i++)
      //      {
      //        if (max < nums[i])
      //          max = nums[i];
      //
      //        if (min > nums[i])
      //          min = nums[i];
      //      }
      //
      //      for (int i = 1; i < nums.Length; i++)
      //      {
      //        if (nums[i] > max)
      //        {
      //          max = nums[i];
      //          endIndex = i;
      //        }
      //      }
      //
      //
      //      for (int i = 0; i < startIndex; i++)
      //      {
      //        if (nums[i] > max)
      //        {
      //          startIndex = i;
      //          break;
      //        }
      //      }
      //
      //      for (int i = nums.Length - 1; i >= endIndex; i--)
      //      {
      //        if (nums[i] < nums[endIndex])
      //        {
      //          endIndex = i;
      //          break;
      //        }
      //      }

      //      for (int i = 1; i < nums.Length; i++)
      //      {
      //        if (startIndex == -1 && nums[i] < nums[i - 1])
      //        {
      //          startIndex = i;
      //          max = nums[i];
      //          min = nums[i];
      //          i++;
      //
      //          for (; i < nums.Length; i++)
      //          {
      //            if (max < nums[i])
      //            {
      //              max = nums[i];
      //            }
      //            else
      //            {
      //              endIndex = i;
      //              if (min > nums[i])
      //                min = nums[i];
      //            }
      //          }
      //        }
      //      }

      //      for (int i = 0; i < startIndex; i++)
      //      {
      //        if (nums[i] > min)
      //        {
      //          startIndex = i;
      //          break;
      //        }
      //      }

      Console.WriteLine($"{nameof(startIndex)}:{startIndex},{nameof(endIndex)}:{endIndex}");

      Sort(nums, startIndex, endIndex);

      for (int i = 1; i < nums.Length; i++)
      {
        for (int j = 0; j < i; j++)
        {
          if (nums[j] > nums[i])
            throw new Exception(JsonConvert.SerializeObject(arr));
        }
      }

      return endIndex - startIndex + 1;
    }

    public void Sort(int[] arr, int startIndex, int endIndex)
    {
      bool swapped;
      int temp;
      for (int i = startIndex; i < endIndex; i++)
      {
        swapped = false;
        for (int j = startIndex; j < endIndex; j++)
        {
          if (arr[j] > arr[j + 1])
          {
            temp = arr[j];
            arr[j] = arr[j + 1];
            arr[j + 1] = temp;
            if (!swapped)
              swapped = true;
          }
        }

        if (!swapped)
          break;
      }
    }

    /**
     * Runtime: 424 ms, faster than 8.62% of C# online submissions for Shortest Unsorted Continuous Subarray.
     * Memory Usage: 29.5 MB, less than 80.00% of C# online submissions for Shortest Unsorted Continuous Subarray.
     *
     * 真尴尬=-
     *
     */
    public int Optimize(int[] nums)
    {
      int startIndex = -1, endIndex = -1;

      for (int i = 0; i < nums.Length; i++)
      {
        if (startIndex == -1 && endIndex == -1)
          break;


        if (startIndex == -1)
        {
          for (int j = i; j < endIndex; j++)
          {
            if (nums[i] > nums[j])
            {
              startIndex = i;
            }
          }
        }

        if (endIndex == -1)
        {
          for (int j = nums.Length; j > startIndex; j--)
          {
            if (nums[(nums.Length - 1 - i)] < nums[j])
            {
              endIndex = j;
            }
          }
        }

      }

//      for (int i = 0; i < nums.Length; i++)
//      {
//        for (int j = i; j < nums.Length; j++)
//        {
//          if (nums[i] > nums[j])
//          {
//            startIndex = i;
//          }
//        }
//
//        if (startIndex == i)
//          break;
//      }
//
//      if (startIndex == -1)
//        return 0;
//
//      for (int i = nums.Length - 1; i >= startIndex; i--)
//      {
//        for (int j = startIndex; j < i; j++)
//        {
//          if (nums[i] < nums[j])
//          {
//            endIndex = i;
//          }
//        }
//
//        if (endIndex == i)
//          break;
//      }

      return endIndex - startIndex + 1;
    }

    /**
     * Runtime: 136 ms, faster than 84.48% of C# online submissions for Shortest Unsorted Continuous Subarray.
     * Memory Usage: 29.6 MB, less than 66.67% of C# online submissions for Shortest Unsorted Continuous Subarray.
     *
     * so simple ....
     * amazing
     *
     */
    public int OtherSolution(int[] A)
    {
      int n = A.Length, beg = -1, end = -2, min = A[n - 1], max = A[0];
      for (int i = 1; i < n; i++)
      {
        max = Math.Max(max, A[i]);
        min = Math.Min(min, A[n - 1 - i]);
        if (A[i] < max) end = i;
        if (A[n - 1 - i] > min) beg = n - 1 - i;
      }
      return end - beg + 1;
    }

  }
}