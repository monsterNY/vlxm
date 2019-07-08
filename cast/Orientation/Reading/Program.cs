using System;
using System.Runtime.InteropServices;
using System.Threading;
using Reading.Extension;

namespace Reading
{
  class Program
  {
//    public static implicit operator Program(int i)
//    {
//      return new Program();
//    }

    public int Version => 1;

    private static void TimerCallback(object o)
    {
      Console.WriteLine($"In TimerCallback: {DateTime.Now.ToString()}"); ////Release 版本下 只会输出一次

      GC.Collect(); //强制执行垃圾回收
    }

    #region fun test

    //      Model<int> t = 3;
    //      Model<int> t2 = 4;
    //      Model<int> t3 = 4;
    //
    //      List<int> list = (List<int>) (t + t2 + t3);
    //
    //      Console.WriteLine(list);

    //      List<Model> list = new Model() + new Model() + new Model();

    //      Console.WriteLine(list);

    //      Program p = 3;

    //      Program p2 = 2 + 3;

    #endregion

    static void Main(string[] args)
    {
      for (int i = 0; i < 100; i++)
      {
        new Thread((() =>
        {
          Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId.ToString()} start");
          var num = 0;
          for (int j = 0; j < 100000; j++)
            num += j;

          Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId.ToString()} over,num:{num.ToString()}");
        })).Start();

        new Thread((() =>
        {
          Console.WriteLine($"[yield Thread {Thread.CurrentThread.ManagedThreadId.ToString()} start]");

          Thread.Sleep(0);

          //if (Thread.Yield())
          //{
          //  Console.WriteLine("调度另外一个线程");
          //}

          Console.WriteLine($"[yield Thread {Thread.CurrentThread.ManagedThreadId.ToString()} over]");
        })).Start();
      }

      object obj = "123";

      var gcHandle = GCHandle.Alloc(obj, GCHandleType.Weak);
      string str = (string) obj; //castclass

      //      AsyncOneManyLock.Run();

      //      SpinAndEventDemo.Run();

      //      ThreadPool.QueueUserWorkItem((state =>
      //      {
      //        Console.WriteLine("Work Begin");
      //
      //        Thread.Sleep(0);
      //
      //        Console.WriteLine("Work Over");
      //
      //      }));

      //      OptimizeDemo.Run();

      //      CancellationExt.Run();

      //      EventAwaiterDemo.Run();

      //      TaskLoggerDemo.Run().ContinueWith((task => { Console.WriteLine("TaskLoggerDemo Over"); }));
      //
      //      var path = @"D:\work_y\empty";
      //
      //      var bytes = ParallelDemo.DirectoryBytes(path, "*", SearchOption.TopDirectoryOnly);
      //
      //      Console.WriteLine($"{path}目录下的所有文件字节长度为：{bytes}");

      //      ConsoleTools.ShowLine($"{nameof(TaskDemo)}.{nameof(TaskDemo.RunFactory)}", TaskDemo.RunFactory);
      //      ConsoleTools.ShowLine(nameof(TaskDemo), (TaskDemo.Run));
      //      ConsoleTools.ShowLine(nameof(CancellationDemo), (CancellationDemo.Run));
      //      ConsoleTools.ShowLine(nameof(ContextDemo), (ContextDemo.Run));

      Console.ReadKey(true);
    }

    private static void TestConditionalWeakTable()
    {
      object o = new object().GCWatch($"My Object created at {DateTime.Now}");

      GC.Collect(); //此时看不到GC通知

      GC.KeepAlive(o); //确定o引用的对象现在还活着

      o = null; //o引用对象现在可以死了

      GC.Collect(); //此时才会看到GC通知
    }

    private static void TestFixed()
    {
      unsafe
      {
        //分配一系列立即变成垃圾的对象
        for (int i = 0; i < 10000; i++)
          new object();

        IntPtr originalMemoryAddress;

        var bytes = new byte[1000];

        fixed (byte* pbytes = bytes) originalMemoryAddress = (IntPtr) pbytes;

        //强迫GC回收一次
        GC.Collect();

        fixed (Byte* pbytes = bytes)
          Console.WriteLine(
            $"the byte[] did{(originalMemoryAddress == (IntPtr) pbytes ? " not " : null)} move during the GC");
      }
    }

    private static void Test()
    {
      object obj = new Program();

      var type = obj.GetType();

      GCHandle.Alloc(obj, GCHandleType.Weak);

      Console.WriteLine(type);

      foreach (var prop in type.GetProperties())
      {
        Console.WriteLine($"{prop.Name}: {prop.GetValue(obj)}");
      }

      Timer t = new Timer(TimerCallback, null, 0, 1000);
    }

    ~Program()
    {
      throw new Exception("未处理的异常，进程终止。无法捕捉该异常！"); //
    }
  }
}