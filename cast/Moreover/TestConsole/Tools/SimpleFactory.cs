using System;
using System.Collections.Generic;
using System.Text;

namespace TestConsole.Tools
{
  /// <summary>
  /// @desc : SimpleFactory  
  /// @author :mons
  /// @create : 2019/3/12 14:36:35 
  /// @source : 
  /// </summary>
  public class SimpleFactory<T> where T : class
  {
    private SimpleFactory()
    {
      Console.WriteLine($"创建了一个：{typeof(T).Name}");
    }

    private static SimpleFactory<T> _instance;

    public static SimpleFactory<T> GetInstance()
    {
      return _instance = _instance ?? new SimpleFactory<T>();
    }
  }
}