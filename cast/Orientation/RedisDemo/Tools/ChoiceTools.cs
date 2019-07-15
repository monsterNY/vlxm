using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.CusTools;

namespace RedisDemo.Tools
{
  /// <summary>
  /// @desc : ChoiceTools  
  /// @author : mons
  /// @create : 2019/7/15 12:04:33 
  /// @source : 
  /// </summary>
  public sealed class ChoiceTools
  {

    public static IEnumerable<T> WhileOption<T>(T exitOpt) where T : Enum
    {
      while (true)
      {

        var option = GetOption<T>();

        if (exitOpt.Equals(option)) yield break;

        yield return option;
      }

    }

    public static int GetOption(IEnumerable<string> optEnumerable, string title = null)
    {
      Console.WriteLine($@"
>>>>>>>>{title ?? "请选择操作项:"}
{string.Join("\n", optEnumerable.Select(((s, i) => $"{i}.{s}")))}
");

      int res = Convert.ToInt32(Console.ReadLine());

      return res;
    }

    public static T GetOption<T>(string title = null) where T : Enum
    {
      var dictionary = MenuTools.GetList(typeof(T));

      Console.WriteLine($@"
>>>>>>>>{title ?? "请选择操作项:"}
{string.Join("\n", dictionary.Select((pair => $"{pair.Value}.{pair.Key}")))}
");

      return (T) Enum.Parse(typeof(T), Console.ReadLine());
    }
  }
}