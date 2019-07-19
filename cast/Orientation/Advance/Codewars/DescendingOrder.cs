using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.App_Start
{
  public class DescendingOrder
  {
    /**
     * Your task is to make a function that can take any non-negative integer as a argument and return it with its digits in descending order. Essentially, rearrange the digits to create the highest possible number.
     *
     * 你的任务是创建一个函数：获得一个非负数参数并且返回按降序排列的数字，本质上，重新排列数字以创建尽可能高的数字。
     *
     */

    public static int Solution(int num)
    {
      List<int> list = new List<int>();

      while (num > 0)
      {
        list.Add(num % 10);
        num /= 10;
      }

      list.Sort();

      int res = 0;

      for (int i = list.Count - 1; i >= 0; i--)
      {
        res = res * 10 + list[i];
      }

      return res;
    }
  }
}