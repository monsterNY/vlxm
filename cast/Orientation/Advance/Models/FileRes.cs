using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Advance.Models
{
  /// <summary>
  /// @desc : FileRes  
  /// @author : mons
  /// @create : 2019/5/21 10:04:27 
  /// @source : 
  /// </summary>
  public class FileRes : IDisposable
  {
    /// <summary>
    /// ThreadStatic特性是最简单的TLS使用，且只支持静态字段，只需要在字段上标记这个特性就可以了：
    /// </summary>
    [ThreadStatic]
    public static string file = "hehe";

    public void Dispose()
    {
      Console.WriteLine($"线程-{Thread.CurrentThread.ManagedThreadId},操作文件:{file}");
    }
  }
}