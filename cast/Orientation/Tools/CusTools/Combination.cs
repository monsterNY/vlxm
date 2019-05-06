using System;
using System.Collections.Generic;
using System.Text;

namespace Tools.CusTools
{
  /// <summary>
  /// @desc : Combination  组合处理
  /// @author :mons
  /// @create : 2019/3/28 9:55:12 
  /// @source : 
  /// </summary>
  public static class Combination
  {
    public static void GetList<T, TResult>(List<T> arr, TResult build, List<TResult> result, Func<T, TResult> appendFunc)
    {
      if (arr.Count == 0)
      {
        result.Add(build);
      }

      for (int i = 0; i < arr.Count; i++)
      {
        var temp = new List<T>(arr);
        temp.RemoveAt(i);
        GetList(temp, appendFunc.Invoke(arr[i]), result, appendFunc);
      }
    }
  }
}