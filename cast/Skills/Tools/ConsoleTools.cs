using System;
using System.Collections.Generic;
using System.Text;

namespace Skills.Tools
{
  /// <summary>
  /// @desc : ConsoleTools  
  /// @author : mons
  /// @create : 2019/7/1 10:33:51 
  /// @source : 
  /// </summary>
  public class ConsoleTools
  {

    /// <summary>
    /// 控制器提问
    ///
    /// 中文不好录入，change~
    /// 
    /// </summary>
    /// <param name="title">问题标题</param>
    /// <param name="questionColor">问题显示color</param>
    /// <param name="writerColor">写入显示color</param>
    /// <returns></returns>
    public static StringBuilder Ask(string title,
      ConsoleColor questionColor = ConsoleColor.Red,
      ConsoleColor writerColor = ConsoleColor.White)
    {
      Console.ForegroundColor = questionColor;

      Console.WriteLine($"{Environment.NewLine}{title}");

      Console.ForegroundColor = writerColor;

      StringBuilder builder = new StringBuilder();

      string str;

      while (!string.IsNullOrWhiteSpace(str = Console.ReadLine()))
      {
        builder.AppendLine(str);
      }

      // recover
      Console.ForegroundColor = ConsoleColor.Gray;

      return builder;
    }
  }
}