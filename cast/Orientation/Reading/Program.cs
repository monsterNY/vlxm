using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
      object obj = new Program();

      var type = obj.GetType();

      Console.WriteLine(type);

      foreach (var prop in type.GetProperties())
      {
        Console.WriteLine($"{prop.Name}: {prop.GetValue(obj)}");
      }

      Timer t = new Timer(TimerCallback, null, 0, 1000);

      Console.ReadLine();

      Console.ReadKey(true);
    }
  }
}