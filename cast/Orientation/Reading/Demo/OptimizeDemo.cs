using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Reading.Demo
{
  /// <summary>
  /// @desc : OptmizeDemo  
  /// @author : mons
  /// @create : 2019/6/25 11:01:32 
  /// @source : clr via C# p674
  /// </summary>
  public class OptimizeDemo
  {
    private static bool s_stopWorker = false;

    public static void Run()
    {

      Console.WriteLine("begin");

      Thread t = new Thread(Worker);

      t.Start();

      Thread.Sleep(5000);

      s_stopWorker = true;

      Console.WriteLine("waiting for worker to stop");

      t.Join();

    }

    private static void Worker(object o)
    {
      var x = 0;

      while (!s_stopWorker)
      {
        x++;
        Console.WriteLine($"Worker: running x={x.ToString()}");
      }

    }
  }
}