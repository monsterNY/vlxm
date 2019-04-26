using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestionThree
{
  /// <summary>
  /// @desc : WiggleSort  
  /// @author : mons
  /// @create : 2019/4/26 9:38:00 
  /// @source : https://leetcode.com/problems/wiggle-sort-ii/
  /// </summary>
  public class WiggleSort
  {

    /**
     * Runtime: 284 ms, faster than 77.33% of C# online submissions for Wiggle Sort II.
     * Memory Usage: 34.1 MB, less than 38.46% of C# online submissions for Wiggle Sort II.
     */
    public void Solution(int[] nums)
    {
      Array.Sort(nums);

      int remainder = nums.Length % 2;

      var sortArr = (int[]) nums.Clone();

      for (int i = 0; i < nums.Length / 2; i++)
      {
        nums[i * 2 + 1] = sortArr[nums.Length - 1 - i];
        nums[nums.Length - 2 - remainder - i * 2] = sortArr[i + remainder];
      }

      if (remainder == 1)
      {
        nums[nums.Length - 1] = sortArr[0];
      }
    }

    //Time Limit
    public void Try2(int[] nums)
    {
      int index, remainder = nums.Length % 2;

      bool[] visited = new bool[nums.Length]; //方法可行但超时


      for (int i = 0; i < nums.Length / 2; i++)
      {
        index = GetMinIndex(nums, visited);

        var temp = nums[nums.Length - 1 - i * 2 - 1 + remainder];
        nums[nums.Length - 1 - i * 2 - 1 + remainder] = nums[index];
        nums[index] = temp;

        visited[nums.Length - 1 - i * 2 - 1 + remainder] = true;

        index = GetMaxIndex(nums, visited);

        temp = nums[i * 2 + 1];
        nums[i * 2 + 1] = nums[index];
        nums[index] = temp;


        visited[i * 2 + 1] = true;
      }
    }

    public void Try(int[] nums)
    {
      Array.Sort(nums);

      var middle = nums.Length / 2 - 1 + (nums.Length % 2);

      for (int i = 0; i < nums.Length / 2; i++)
      {
        Change(nums, 2 * i, middle - i);

        Change(nums, i * 2 + 1, nums.Length - 1 - i);
      }
    }

    public void Change(int[] arr, int i, int j)
    {
      var temp = arr[i];
      arr[i] = arr[j];
      arr[j] = temp;
    }

    public int GetMaxIndex(int[] arr, bool[] visited)
    {
      int max = int.MinValue, index = -1;
      for (int i = 0; i < arr.Length; i++)
      {
        if (visited[i]) continue;
        if (arr[i] > max)
        {
          max = arr[i];
          index = i;
        }
      }

      return index;
    }

    public int GetMinIndex(int[] arr, bool[] visited)
    {
      int min = int.MaxValue, index = -1;
      for (int i = 0; i < arr.Length; i++)
      {
        if (visited[i]) continue;
        if (arr[i] < min)
        {
          min = arr[i];
          index = i;
        }
      }

      return index;
    }
  }
}