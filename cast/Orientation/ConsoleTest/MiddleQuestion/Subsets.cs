using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : Subsets  
  /// @author :mons
  /// @create : 2019/4/1 16:49:46 
  /// @source : https://leetcode.com/problems/subsets/
  /// </summary>
  public class Subsets
  {
    public IList<IList<int>> Try2(int[] nums)
    {
      IList<IList<int>> result = new List<IList<int>>();

      result.Add(new int[1]);
      IList<int> list;
      var step = 1;

      for (int i = 0; i < nums.Length; i++)
      {
        for (int j = 0; j < nums.Length - i; j++)
        {
          list = new List<int>(step);
          for (int k = 0; k <= i; k++)
          {
            list.Add(nums[j + k]);
          }

          result.Add(list);
        }
      }

      return result;
    }

    /**
     * Runtime: 248 ms, faster than 91.49% of C# online submissions for Subsets.
     * Memory Usage: 28.7 MB, less than 81.63% of C# online submissions for Subsets.
     *
     * nice!!!!
     *
     */
    public IList<IList<int>> Solution(int[] nums)
    {
      IList<IList<int>> result = new List<IList<int>>();

      GetList(nums, new List<int>(), result, 0);

      return result;
    }

    public void GetList(int[] arr, List<int> build, IList<IList<int>> result, int startIndex)
    {
      result.Add(build);

      for (; startIndex < arr.Length; startIndex++)
      {
        var newItem = new List<int>(build);
        newItem.Add(arr[startIndex]);

        GetList(arr, newItem, result, startIndex + 1);
      }
    }

    public void GetList(int[] arr, List<int> build, IList<IList<int>> result)
    {
      result.Add(build);

      for (int i = 0; i < arr.Length; i++)
      {
        var newArr = new int[arr.Length - i - 1];
        Array.Copy(arr, i + 1, newArr, 0, arr.Length - i - 1);

        var newItem = new List<int>(build);
        newItem.Add(arr[i]);

        GetList(newArr, newItem, result);
      }
    }
  }
}