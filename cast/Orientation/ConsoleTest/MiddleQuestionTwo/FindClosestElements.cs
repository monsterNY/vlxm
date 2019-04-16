using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestionTwo
{
  /// <summary>
  /// @desc : FindClosestElements  
  /// @author :mons
  /// @create : 2019/4/16 17:58:28 
  /// @source : https://leetcode.com/problems/find-k-closest-elements/
  /// </summary>
  public class FindClosestElements
  {

    /**
     * Runtime: 312 ms, faster than 66.29% of C# online submissions for Find K Closest Elements.
     * Memory Usage: 40.5 MB, less than 84.62% of C# online submissions for Find K Closest Elements.
     */
    public IList<int> Solution(int[] arr, int k, int x)
    {
      int index = 0, diff = int.MaxValue, itemDiff;

      //可采用二分法优化
      for (int i = 0; i < arr.Length; i++)
      {
        itemDiff = Math.Abs(arr[i] - x);

        if (itemDiff < diff)
        {
          index = i;
          diff = itemDiff;
        }
        else if (arr[i] != arr[index]) break;
      }

      IList<int> res = new List<int>();

      int left = index, right = index + 1;

      while (k-- > 0)
      {
        if (right == arr.Length)
          res.Insert(0, arr[left--]);
        else if (left == -1)
          res.Add(arr[right++]);
        else if (Math.Abs(arr[right] - x) < Math.Abs(arr[left] - x))
          res.Add(arr[right++]);
        else
          res.Insert(0, arr[left--]);
      }

      return res;
    }
  }
}