using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestionThree
{
  /// <summary>
  /// @desc : NextGreaterElement  
  /// @author : mons
  /// @create : 2019/4/25 15:42:13 
  /// @source : https://leetcode.com/problems/next-greater-element-iii/
  /// </summary>
  [Obsolete]
  //可用递归。
  public class NextGreaterElement
  {
    public int Solution(int n)
    {
      if (n < 10) return -1;
      var str = n.ToString().ToCharArray();

      for (int i = str.Length - 1; i >= 0; i--)
      {
        for (int j = i + 1; j > i; j--)
        {
          if (str[i] < str[j])
          {
            var temp = str[i];
            str[i] = str[j];
            str[j] = str[i];

            Sort(str, i + 1, str.Length);

            var res = int.Parse(str);

            if (res > 0) return res;

          }
        }
      }

      return -1;
    }

    public void Sort(char[] arr, int start, int limit)
    {
      bool swapped;
      for (int i = start; i < limit - 1; i++)
      {
        swapped = false;
        for (int j = start + 1; j < limit; j++)
        {
          if (arr[i] > arr[j])
          {
            var temp = arr[i];
            arr[i] = arr[j];
            arr[j] = arr[i];
            swapped = true;
          }
        }

        if (!swapped) return;
      }
    }

    public int Try(int n)
    {
      if (n < 10) return -1;

      List<int> list = new List<int>();

      while (n > 0)
      {
        list.Add(n % 10);
        n /= 10;
      }

      for (int i = 1; i < list.Count; i++)
      {
        for (int j = 0; j < i; j++)
        {
          if (list[i] < list[j])
          {
            var temp = list[i];
            list[i] = list[j];
            list[j] = temp;

            var res = Helper(list);

            if (res > 0) return res;
            list[j] = list[i];
            list[i] = temp;
          }
        }
      }

      return -1;
    }

    public int Helper(List<int> list)
    {
      var sum = 0;

      for (int i = list.Count - 1; i >= 0; i--)
      {
        sum = sum * 10 + list[i];
      }

      return sum;
    }
  }
}