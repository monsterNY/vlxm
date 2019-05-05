using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.Dream
{
  /// <summary>
  /// @desc : Diff  
  /// @author : mons
  /// @create : 2019/5/5 11:41:33 
  /// @source : 
  /// </summary>
  public class Diff
  {
    /**
     *
     * make a strange dream
     *
     * give a string arr
     * reorder this arr make old[i] != new[i]
     *
     */

    //condition 1: arr的每个元素都不相同
    public bool Reorder(string[] arr)
    {
      if (arr.Length < 2) return false; //不可能的情况

      var len = arr.Length;

      for (int i = 0; i < len / 2; i++)
      {
        var temp = arr[i];
        arr[i] = arr[len - i];
        arr[len - i] = temp;
      }

      if (len % 2 != 1) //若长度%2==0 则只需要倒序即可完成
      {
        //将最中间的一个进行替换,替换方式很多，此处选择与前一个进行替换
        var temp = arr[len / 2];
        arr[len / 2] = arr[len / 2 - 1];
        arr[len / 2 - 1] = temp;
      }

      return true;
    }

    //condition 2: arr数组中存在重复元素
    [Obsolete]
    public bool Reorder2(string[] arr)
    {
      if (arr.Length < 2) return false; //不可能的情况

      var old = (string[]) arr;

      var len = arr.Length;

      for (int i = 0; i < len / 2; i++)
      {
        var temp = arr[i];
        arr[i] = arr[len - i];
        arr[len - i] = temp;
      }

      if (len % 2 != 1) 
      {
        var temp = arr[len / 2];
        arr[len / 2] = arr[len / 2 - 1];
        arr[len / 2 - 1] = temp;
      }

      for (int i = 0; i < len; i++)
      {
        if (old[i] == arr[i])
        {
          //往前面寻找不为old[i]的 如果没有再往后寻找 再没有就无法实现。
        }
      }

      return true;
    }
  }
}