using System;
using System.Collections.Generic;
using System.IO;
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


    /**
     * Runtime: 340 ms, faster than 91.55% of C# online submissions for Find Duplicate File in System.
     * Memory Usage: 49.4 MB, less than 100.00% of C# online submissions for Find Duplicate File in System.
     *
     * 。，。 Don't even think about, Regex is more efficient
     *
     */
    public IList<IList<string>> Solution(string[] paths)
    {
      var dictionary = new Dictionary<string, IList<string>>();

      ReadOnlySpan<char> item;

      for (var i = 0; i < paths.Length; i++)
      {
        var pathIndex = paths[i].IndexOf(' ');

        if (pathIndex == -1) continue;

        item = paths[i].AsSpan();

        AddToMap(item.Slice(pathIndex + 1, paths[i].Length - pathIndex - 1), item.Slice(0, pathIndex).ToString(),
          dictionary);
      }

      IList<IList<string>> resultList = new List<IList<string>>();

      foreach (var list in dictionary.Values)
        if (list.Count > 1)
          resultList.Add(list);

      return resultList;
    }

    public void AddToMap(ReadOnlySpan<char> str, string path, IDictionary<string, IList<string>> dictionary)
    {
      int index;

      string content;
      ReadOnlySpan<char> fileName;

      while ((index = str.IndexOf(' ')) != -1)
      {
        content = GetContent(str.Slice(0, index), out fileName).ToString();

        if (dictionary.ContainsKey(content))
        {
          dictionary[content].Add($"{path}/{fileName.ToString()}");
        }
        else
        {
          dictionary.Add(content, new List<string>() {$"{path}/{fileName.ToString()}"});
        }

        str = str.Slice(index + 1, str.Length - index - 1);
      }

      content = GetContent(str, out fileName).ToString();

      if (dictionary.ContainsKey(content))
        dictionary[content].Add($"{path}/{fileName.ToString()}");
      else
        dictionary.Add(content, new List<string>() {$"{path}/{fileName.ToString()}"});
    }

    public ReadOnlySpan<char> GetContent(ReadOnlySpan<char> file, out ReadOnlySpan<char> fileName)
    {
      var index = file.IndexOf('(');
      if (index == -1)
      {
        fileName = file.ToString();
        return string.Empty;
      }

      var content = file.Slice(index + 1, file.Length - index - 2);

      fileName = file.Slice(0, index);

      return content;
    }

    public List<string> Split(ReadOnlySpan<char> str)
    {
      var result = new List<string>();
      int index;

      while ((index = str.IndexOf(' ')) != -1)
      {
        result.Add(str.Slice(0, index).ToString());
        str = str.Slice(index + 1, str.Length - index - 1);
      }

      result.Add(str.ToString());

      return result;
    }

    public IList<IList<string>> Try(string[] paths)
    {
      var dictionary = new Dictionary<string, IList<string>>();

      string item;

      for (var i = 0; i < paths.Length; i++)
      {
        var startIndex = paths[i].IndexOf('(');

        if (startIndex != -1)
        {
          item = paths[i].AsSpan().Slice(startIndex, paths[i].Length - startIndex - 1).ToString();

          if (dictionary.ContainsKey(item))
            dictionary[item].Add(paths[i]);
          else
            dictionary.Add(item, new List<string>() {paths[i]});
        }
      }

      IList<IList<string>> resultList = new List<IList<string>>();

      foreach (var list in dictionary.Values)
        if (list.Count > 1)
          resultList.Add(list);

      return resultList;
    }
  }
}