using System;

namespace Reading.Tools
{
  /// <summary>
  /// @desc : ConsoleTools  
  /// @author : mons
  /// @create : 2019/6/20 10:49:49 
  /// @source : 
  /// </summary>
  public class ConsoleTools
  {

    public static void ShowLine(string title = null, Action action = null)
    {

      Console.WriteLine($"\n------------------{title} start-------------------------\n");

      action?.Invoke();

      Console.WriteLine($"\n------------------{title} end  -------------------------\n");

    }

  }
}
