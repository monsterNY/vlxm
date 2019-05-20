using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Advance.RefDemo
{
  /// <summary>
  /// @desc : CountdownEventDemo  
  /// @author : mons
  /// @create : 2019/5/10 16:00:20 
  /// @source : 
  /// </summary>
  public class CountdownEventDemo
  {
    /**
 * CountdownEvent - 线程、任务同步类。线程或任务一直阻塞到 CountdownEvent 的计数为 0 为止
 * 1、当有新的需要同步的线程或任务产生时，就调用 AddCount 增加 CountdownEvent 的计数
 * 2、当有线程或任务到达同步点时，就调用 Signal 函数减小 CountdownEvent 的计数
 * 3、当 CountdownEvent 的计数为 0 时，就表示所有需要同步的任务已经完成。通过 Wait 来阻塞线程
 */

    private string _result = "";

    private static readonly object objLock = new object();

    public void Run()
    {
      // CountdownEvent(int initialCount) - 实例化一个 CountdownEvent
      //     int initialCount - 初始计数
      using (var countdown = new CountdownEvent(1))
      {
        Thread t1 = new Thread(() => ThreadWork("aaa", TimeSpan.FromSeconds(1), countdown));
        // 增加 1 个计数
        countdown.AddCount();
        t1.Start();

        Thread t2 = new Thread(() => ThreadWork("bbb", TimeSpan.FromSeconds(2), countdown));
        countdown.AddCount();
        t2.Start();

        Thread t3 = new Thread(() => ThreadWork("ccc", TimeSpan.FromSeconds(3), countdown));
        countdown.AddCount();
        t3.Start();

        // 减少 1 个计数
        countdown.Signal();
        // 阻塞当前线程，直到 CountdownEvent 的计数为零
        countdown.Wait();
      }

      Console.WriteLine(_result);
    }

    private void ThreadWork(string name, TimeSpan sleepTime, CountdownEvent countdown)
    {
      Thread.Sleep(sleepTime);

      _result += "hello: " + name + " " + DateTime.Now.ToString("HH:mm:ss");
      _result += "\n";

      // 减少 1 个计数
      countdown.Signal();
    }
  }
}