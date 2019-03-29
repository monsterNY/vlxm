using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Tools.CusAttr;
using Tools.CusMenu;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : ProductExceptSelf  
  /// @author :mons
  /// @create : 2019/3/29 10:06:41 
  /// @source : https://leetcode.com/problems/product-of-array-except-self/
  /// </summary>
  [Love(LoveTypes.Question)]
  public class ProductExceptSelf
  {
    /**
     * Runtime: 264 ms, faster than 99.20% of C# online submissions for Product of Array Except Self.
     * Memory Usage: 34.9 MB, less than 90.24% of C# online submissions for Product of Array Except Self.
     *
     * Runtime: 268 ms, faster than 84.89% of C# online submissions for Product of Array Except Self.
     * Memory Usage: 34.9 MB, less than 86.59% of C# online submissions for Product of Array Except Self.
     *
     * I love it!
     *
     */
    public int[] Solution(int[] nums)
    {
      int[] arr = new int[nums.Length];

      Array.Fill(arr, 1);//Array.Fill 快于 nums[0] = 1 ??? 神奇。。，

      var num = 1;

      //arr[0] = 1;

      for (int i = 0; i < nums.Length - 1; i++)
      {
        num *= nums[i];
        arr[i + 1] = num;
        //step2:
        //arr[i + 1] = arr[i] * nums[i];
      }

      //var num = 1;
      num = 1;
      for (int i = nums.Length - 1; i > 0; i--)
      {
        num *= nums[i];
        arr[i - 1] = num * arr[i - 1];
      }

      return arr;
    }

    public int[] Check(int[] nums)
    {
      int[] arr = new int[nums.Length];

      Array.Fill(arr, 1);

      for (int i = 0; i < nums.Length; i++)
      {
        for (int j = 0; j < nums.Length; j++)
        {
          if (j == i) continue;
          arr[j] = arr[j] * nums[i];
        }

        Console.WriteLine(JsonConvert.SerializeObject(arr));
      }

      return arr;
    }
  }
}