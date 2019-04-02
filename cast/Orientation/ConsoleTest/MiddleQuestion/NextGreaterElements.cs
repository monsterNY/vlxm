using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : NextGreaterElements  
  /// @author :mons
  /// @create : 2019/4/2 16:33:20 
  /// @source :   https://leetcode.com/problems/next-greater-element-ii/
  /// </summary>
  public class NextGreaterElements
  {
    public int[] nextGreaterElements(int[] nums)
    {
      int n = nums.Length;
      int[] next = new int[n];
      Array.Fill(next, -1);
      Stack<int> stack = new Stack<int>(); // index stack
      for (int i = 0; i < n * 2; i++)
      {
        int num = nums[i % n];
        while (stack.Count > 0 && nums[stack.Peek()] < num)
          next[stack.Pop()] = num;
        if (i < n) stack.Push(i);
      }

      return next;
    }

    /**
     * Runtime: 408 ms, faster than 26.73% of C# online submissions for Next Greater Element II.
     * Memory Usage: 37.1 MB, less than 100.00% of C# online submissions for Next Greater Element II.
     */
    public int[] Simple(int[] nums)
    {
      int[] result = new int[nums.Length];
      for (int i = 0;
        i < nums.Length;
        i++)
      {
        if (i > 0 && nums[i] == nums[i - 1])
        {
          result[i] = result[i - 1];
          continue;
        }

        for (var j = (i == nums.Length - 1 ? 0 : i + 1); i != j; j = (j == nums.Length - 1 ? 0 : j + 1))
        {
          if (nums[j] > nums[i])
          {
            result[i] = nums[j];
            break;
          }
        }
      }

      return result;
    }
  }
}