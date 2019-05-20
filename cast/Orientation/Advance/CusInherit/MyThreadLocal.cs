using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Advance.CusInherit
{
  /// <summary>
  /// @desc : MyThreadLocal  
  /// @author : mons
  /// @create : 2019/5/20 16:14:24 
  /// @source : 
  /// </summary>
  public class MyThreadLocal<T>:ThreadLocal<T>
  {
    public MyThreadLocal()
    {
      Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} 创建了一个 ThreadLocal");
    }

    public MyThreadLocal(bool trackAllValues) : base(trackAllValues)
    {
      Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} 创建了一个 ThreadLocal");
    }

    public MyThreadLocal(Func<T> valueFactory) : base(valueFactory)
    {
      Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} 创建了一个 ThreadLocal");
    }

    public MyThreadLocal(Func<T> valueFactory, bool trackAllValues) : base(valueFactory, trackAllValues)
    {
      Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} 创建了一个 ThreadLocal");
    }
  }
}
