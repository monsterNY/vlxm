using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest.MiddleQuestion
{
  /// <summary>
  /// @desc : FindDuplicate  
  /// @author :mons
  /// @create : 2019/3/28 18:00:24 
  /// @source : https://leetcode.com/problems/find-duplicate-file-in-system/
  /// </summary>
  public class FindDuplicate
  {

    public IList<IList<string>> Solution(string[] paths)
    {

      var dictionary = new Dictionary<string, IList<string>>();

      string item;

      for (int i = 0; i < paths.Length; i++)
      {
        var startIndex = paths[i].IndexOf('(');

        if (startIndex != -1)
        {
          item = paths[i].AsSpan().Slice(startIndex, paths[i].Length - startIndex - 1).ToString();

          if (dictionary.ContainsKey(item))
          {
            dictionary[item].Add(paths[i]);
          }
          else
          {
            dictionary.Add(item,new List<string>(){ paths[i] });
          }

        }

      }

      IList<IList<string>> resultList = new List<IList<string>>();

      foreach (var list in dictionary.Values)
      {
        if (list.Count > 1)
        {
          resultList.Add(list);
        }
      }

      return resultList;

    }

  }
}
