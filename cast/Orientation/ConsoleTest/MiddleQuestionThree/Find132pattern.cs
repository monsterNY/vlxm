using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestionThree
{
  /// <summary>
  /// @desc : Find132pattern  
  /// @author : mons
  /// @create : 2019/4/26 10:42:37 
  /// @source : https://leetcode.com/problems/132-pattern/
  /// </summary>
  public class Find132pattern
  {
    /**
     * Runtime: 1848 ms, faster than 10.77% of C# online submissions for 132 Pattern.
     * Memory Usage: 27.4 MB, less than 100.00% of C# online submissions for 132 Pattern.
     */
    public bool Solution(int[] nums)
    {
      var arr = new int[nums.Length];

      Array.Fill(arr, -1);

      for (int i = nums.Length - 1; i > 0; i--)
      {
        for (int j = 0; j < i; j++)
        {
          if (nums[i] > nums[j])
          {
            arr[i] = j;
            break;
          }
        }
      }

      for (int i = 1; i < nums.Length - 1; i++)
      {
        for (int j = i + 1; j < nums.Length; j++)
        {
          if (arr[j] >= 0 && arr[j] < i && nums[i] > nums[j]) return true;
        }
      }

      return false;
    }

    /**
     * Runtime: 824 ms, faster than 40.00% of C# online submissions for 132 Pattern.
     * Memory Usage: 27.5 MB, less than 100.00% of C# online submissions for 132 Pattern.
     */
    public bool Solution2(int[] nums)
    {
      var arr = new int[nums.Length];

      Array.Fill(arr, -1);

      for (int i = nums.Length - 1; i > 0; i--)
      {
        for (int j = 0; j < i; j++)
        {
          if (nums[i] > nums[j])
          {
            arr[i] = j;
            break;
          }
        }

        if (arr[i] >= 0)
        {
          for (int j = arr[i] + 1; j < i; j++)
          {
            if (nums[j] > nums[i]) return true;
          }
        }
      }

      return false;
    }

    /**
     * Runtime: 840 ms, faster than 40.00% of C# online submissions for 132 Pattern.
     * Memory Usage: 27.1 MB, less than 100.00% of C# online submissions for 132 Pattern.
     */
    public bool Solution3(int[] nums)
    {
      for (int k = nums.Length - 1; k > 0; k--)
      {
        int i;
        for (i = 0; i < k; i++)
          if (nums[k] > nums[i])
            break;

        for (int j = i + 1; j < k; j++)
          if (nums[j] > nums[k])
            return true;
      }

      return false;
    }

    //think result : time limit
    public bool Simple(int[] nums)
    {
      int k, i, j;

      for (j = 1; j < nums.Length - 2; j++)
      {
        k = j + 1;

        while (k != nums.Length)
        {
          for (; k < nums.Length; k++)
          {
            if (nums[j] > nums[k]) break;
          }

          if (k == nums.Length) break;

          for (i = j - 1; i >= 0; i--)
          {
            if (nums[k] > nums[i]) break;
          }

          if (i != -1) return true;
        }
      }

      return false;
    }
  }
}