using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : ReorderedPowerOf2  
  /// @author :mons
  /// @create : 2019/4/2 17:08:53 
  /// @source : https://leetcode.com/problems/reordered-power-of-2/
  /// </summary>
  [Obsolete("no imagination ")]
  public class ReorderedPowerOf2
  {

    /**
     * Runtime: 36 ms, faster than 100.00% of C# online submissions for Reordered Power of 2.
     * Memory Usage: 13 MB, less than 100.00% of C# online submissions for Reordered Power of 2.
     *
     * amazing。。。。
     *
     */
    public bool OtherSolution(int N)
    {
      long c = counter(N);
      for (int i = 0; i < 32; i++)//遍历所有 2^n 进行比较
        if (counter(1 << i) == c) return true;
      return false;
    }

    //使用N 确定一个数字(无关顺序)
    public long counter(int N)
    {
      long res = 0;
      for (; N > 0; N /= 10) res += (int)Math.Pow(10, N % 10);// 神奇之处 ， 无关顺序的值  例如 128 = 10^1 + 10^2 + 10^8 = 182 = 812  。。。。 太秀了
      return res;
    }
    
    //time limit 
    public bool Simple(int N)
    {
      List<int> combination = new List<int>();

      List<int> arr = new List<int>();
      while (N > 0)
      {
        arr.Add(N % 10);
        N /= 10;
      }

      GetList(arr, 0, combination, arr.Count - 1);

      for (int i = 0; i < combination.Count; i++)
      {
        var num = 1;
        while (combination[i]>=num)
        {
          if (combination[i] == num) return true;
          num *= 2;
        }

      }

      return false;
    }

    public void GetList(List<int> arr, int num, List<int> result, int limit)
    {
      if (num == 0 && arr.Count == limit) return;
      if (arr.Count == 0)
      {
        result.Add(num);
      }

      for (int i = 0; i < arr.Count; i++)
      {
        var temp = new List<int>(arr);
        temp.RemoveAt(i);
        GetList(temp, num * 10 + arr[i], result, limit);
      }
    }
  }
}