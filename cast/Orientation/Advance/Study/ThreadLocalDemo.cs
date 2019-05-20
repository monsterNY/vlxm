using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Advance.CusInherit;
using Newtonsoft.Json;

namespace Advance.Study
{
  /// <summary>
  /// @desc : ThreadLocalDemo  
  /// @author : mons
  /// @create : 2019/5/20 15:43:12 
  /// @source : 
  /// </summary>
  public class ThreadLocalDemo
  {
    private object _lockObj = new object();

    private List<string> list = new List<string>();

    public void Run()
    {
      //当不同线程使用ThreadLocal时 会生成一个新的实例。？


      //初始化ThreadLocal
      //  valueFactory - value获取工厂
      //  trackAllValues - 是否跟踪所有值,若为false Values无法使用。

      //public ThreadLocal(Func<T> valueFactory, bool trackAllValues);
      // Thread-Local variable that yields a name for a thread
      ThreadLocal<string> ThreadName =
        new MyThreadLocal<string>(() => { return "Thread" + Thread.CurrentThread.ManagedThreadId; }
          //,true
        ); //一个线程享有一个 ThreadLocal

      // Action that prints out ThreadName for the current thread
      Action action = () =>
      {
        // 如果是在当前线程上初始化 Value，则为 true；否则为 false。
        // 即当前线程如果已经初始化ThreadLocal 则为true 表示为同一线程二次执行
        // If ThreadName.IsValueCreated is true, it means that we are not the
        // first action to run on this thread.
        bool repeat = ThreadName.IsValueCreated;

        lock (_lockObj)
        {
          list.Add(ThreadName.Value);
        }

        Console.WriteLine("ThreadName = {0} {1}", ThreadName.Value, repeat ? "(repeat)" : "");

        Console.WriteLine(ThreadName);
      };

      Console.WriteLine($"当前线程：{Thread.CurrentThread.ManagedThreadId}");

      // Launch eight of them.  On 4 cores or less, you should see some repeat ThreadNames
      Parallel.Invoke(action, action, action, action, action, action, action, action);

      //The ThreadLocal object is not tracking values. To use the Values property, use a ThreadLocal constructor that accepts the trackAllValues parameter and set the parameter to true.”
      //ThreadLocal对象没有跟踪值。要使用Values属性，请使用接受trackAllValues参数并将参数设置为true的ThreadLocal构造函数。
      //Console.WriteLine(JsonConvert.SerializeObject(ThreadName.Values));

      Console.WriteLine(list);

      // Dispose when you are done
      ThreadName.Dispose();
    }
  }
}