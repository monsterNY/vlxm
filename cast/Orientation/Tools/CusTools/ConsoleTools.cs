using System;
using System.Collections.Generic;
using System.Text;

namespace Tools.CusTools
{
  /// <summary>
  /// @desc : ConsoleTools  
  /// @author : mons
  /// @create : 2019/6/20 10:49:49 
  /// @source : 
  /// </summary>
  public class ConsoleTools
  {

    public static void ShowConsole(Dictionary<string, object> dictionary)
    {
      Console.WriteLine($"\n-----------------S---------------------");

      foreach (var item in dictionary)
      {
        Console.WriteLine($"\n{item.Key} : {item.Value}");
      }

      Console.WriteLine($"\n-----------------E---------------------\n");
    }

    public static void ShowConsole(Dictionary<string, Func<object>> dictionary)
    {
      Console.WriteLine($"\n-----------------S---------------------");

      foreach (var item in dictionary)
      {
        Console.WriteLine($"{item.Key}:{item.Value.Invoke()}");
      }

      Console.WriteLine($"-----------------E---------------------\n");
    }

    public static void ShowConsole(Action action)
    {
      Console.WriteLine($"\n-----------------S---------------------");

      action.Invoke();

      Console.WriteLine($"-----------------E---------------------\n");
    }

    public static void ShowLine(string title = null, Action action = null)
    {

      Console.WriteLine($"\n------------------{title} start-------------------------\n");

      action?.Invoke();

      Console.WriteLine($"\n------------------{title} end  -------------------------\n");

    }

  }
}
