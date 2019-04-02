using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ConsoleTest.Helper
{
  /// <summary>
  /// @desc : ListHelper  
  /// @author :mons
  /// @create : 2019/4/2 9:36:57 
  /// @source : 
  /// </summary>
  public static class ListHelper
  {
    public static void PrintList<T>(this IList<T> list)
    {
      Console.WriteLine($"-------------------{nameof(PrintList)}---------------S----------");
      foreach (var item in list)
      {
        Console.WriteLine(JsonConvert.SerializeObject(item));
      }
      Console.WriteLine($"-------------------{nameof(PrintList)}---------------E----------");
    }
  }
}