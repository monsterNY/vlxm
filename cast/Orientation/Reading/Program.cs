using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Reading.Demo;
using Reading.Extension;

namespace Reading
{
  class Program
  {
    public int Version => 1;

    private static void TimerCallback(object o)
    {
      Console.WriteLine($"In TimerCallback: {DateTime.Now.ToString()}"); ////Release 版本下 只会输出一次

      GC.Collect(); //强制执行垃圾回收
    }

    static void Main(string[] args)
    {

      CancellationTokenSource source = new CancellationTokenSource();

      source.Cancel(true);

      ContextDemo.Run();

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