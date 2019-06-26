using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Reading.Ref;

namespace Reading.Demo
{
  /// <summary>
  /// @desc : SpinAndEventTest  
  /// @author : mons
  /// @create : 2019/6/26 9:55:55 
  /// @source : clr via C# p692
  /// </summary>
  public class SpinAndEventDemo
  {
    public static void Run()
    {
      var x = 0;

      const int iterations = 10000000;

      Stopwatch sw = Stopwatch.StartNew();

      // x递增1000w,需要多长时间
      for (int i = 0; i < iterations; i++)
      {
        x++;
      }

      Console.WriteLine($"Incrementing x: {sw.ElapsedMilliseconds:N0}");

      sw.Restart();

      // 加上一个调用什么都不做的方法的开销，需要多久
      for (int i = 0; i < iterations; i++)
      {
        Nothing();
        x++;
        Nothing();
      }

      Console.WriteLine($"Incrementing x in Nothing: {sw.ElapsedMilliseconds:N0}");

      // 加上一个无竞争的SpinLock的开销，需要多久
      SpinLock sl = new SpinLock(false);

      sw.Restart();

      for (int i = 0; i < iterations; i++)
      {
        bool taken = false;
        sl.Enter(ref taken);
        x++;
        sl.Exit();
      }

      Console.WriteLine($"Incrementing x in SpinLock: {sw.ElapsedMilliseconds:N0}");

      // 加上调用一个无竞争的SimpleWaitLock的开销，需要多久

      using (SimpleWaitLock swl = new SimpleWaitLock())
      {
        sw.Restart();

        for (int i = 0; i < iterations; i++)
        {
          swl.Enter();
          x++;
          swl.Leave();
        }

        Console.WriteLine($"Incrementing x in SimpleWaitLock: {sw.ElapsedMilliseconds:N0}");

      }

    }


    [MethodImpl(MethodImplOptions.NoInlining)]
    private static void Nothing()
    {
    }
  }
}