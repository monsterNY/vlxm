using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ConsoleTest.Test
{
  /// <summary>
  /// @desc : SortTest  
  /// @author : mons
  /// @create : 2019/7/5 15:51:51 
  /// @source : 
  /// </summary>
  public class SortTest
  {
    public static void Run()
    {
      var list = new List<string>();

      int i;
      for (i = 40; i >= 1; i--)
      {
        list.Add($"{i.ToString()}f.pdf");
      }

      for (i = 40; i >= 1; i--)
      {
        list.Add($"{i.ToString()}b.pdf");
      }

      list.Sort((s1, s2) =>
      {
        if (s1 == s2) return 0;
        var res = 0;
        int ss1 = Convert.ToInt32(s1.Substring(0, s1.LastIndexOf('.') - 1));
        int ss2 = Convert.ToInt32(s2.Substring(0, s2.LastIndexOf('.') - 1));
        if (ss1 == ss2)
        {
          res = s1.EndsWith("f.pdf") ? -1 : 1; // <--- 改这一句：如果 s1 不包含 b.pdf，就会跳过判定，使用默认值，那就是 f 在前了
        }
        else res = ss1 < ss2 ? -1 : 1;

        Console.WriteLine($"{s1} compare {s2} : {res.ToString()}");

        return res;
      });

      i = 0;

      list.ForEach(str =>
      {
        if (i % 2 == 0) Console.WriteLine();
        i++;
        Console.Write($"{str}\t");
      });

      list.Sort((str, str2) =>
      {
        var dot = str.IndexOf('.', StringComparison.CurrentCulture);

        var dot2 = str2.IndexOf('.', StringComparison.CurrentCulture);

        if (dot != dot2)
          return String.Compare(str, str2, StringComparison.Ordinal);

        for (i = 0; i < dot - 1; i++)
          if (str[i] != str2[i])
            return str[i] - str2[i];

        return str2[dot2 - 1] - str[dot - 1];
      });

      Console.WriteLine(JsonConvert.SerializeObject(list));
    }
  }
}