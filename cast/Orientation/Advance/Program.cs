using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Advance.Lock;
using Advance.RefDemo;
using Tools.RefTools;

namespace Advance
{
  class Program
  {
    static void Main(string[] args)
    {
      var rand = new Random();
      CodeTimer timer = new CodeTimer();
      timer.Initialize();

      new TypeHandleDemo().Run();

      Console.WriteLine("Hello World");

      Console.ReadKey(true);
    }

    public static void Test(ref bool flag)
    {
      while (!flag)
      {
        Thread.Yield();
      }

      Console.WriteLine("over~");
    }

    public class MyClass
    {
      public string Name { get; set; }
    }

    public struct MyStruct
    {
      public CancellationTokenSource m_source { get; }

      private int money { get; }

      private string name { get; }

      public MyStruct(int money)
      {
        this = default(MyStruct);
      }
    }

    private static void TestManualResetEventSlim()
    {
      #region ManualResetEventSlim官方示例

      MRES_SetWaitReset();
      MRES_SpinCountWaitHandle();

      #endregion
    }

    private static void TestSemaphoreSlim()
    {
      #region SemaphoreSlim官方示例

      // Create the semaphore.

      //initialCount：初始可执行数量
      //maxCount：最大执行数量
      //public SemaphoreSlim(int initialCount, int maxCount);
      SemaphoreSlim semaphore = new SemaphoreSlim(1, 3);
      Console.WriteLine("{0} tasks can enter the semaphore.",
        semaphore.CurrentCount);
      Task[] tasks = new Task[5];

      int padding = 0;

      // Create and start five numbered tasks.
      for (int i = 0; i <= 4; i++)
      {
        tasks[i] = Task.Run(() =>
        {
          // Each task begins by requesting the semaphore.
          Console.WriteLine("Task {0} begins and waits for the semaphore.",
            Task.CurrentId);
          semaphore.Wait();

          //为多个线程共享的变量提供原子操作。
          Interlocked.Add(ref padding, 100);

          Console.WriteLine($"{nameof(padding)}:{padding}");

          Console.WriteLine("Task {0} enters the semaphore.", Task.CurrentId);

          // The task just sleeps for 1+ seconds.
          Thread.Sleep(1000 + padding);

          Console.WriteLine("Task {0} releases the semaphore; previous count: {1}.",
            Task.CurrentId, semaphore.Release());
        });
      }

      // Wait for half a second, to allow all the tasks to start and block.
      Thread.Sleep(500);

      // Restore the semaphore count to its maximum value.
      Console.Write("Main thread calls Release(3) --> ");
      //      semaphore.Release(3);
      Console.WriteLine("{0} tasks can enter the semaphore.",
        semaphore.CurrentCount);
      // Main thread waits for the tasks to complete.
      Task.WaitAll(tasks);

      Console.WriteLine("Main thread exits.");

      /*
        Result

        0 tasks can enter the semaphore.
        Task 1 begins and waits for the semaphore.
        Task 3 begins and waits for the semaphore.
        Task 2 begins and waits for the semaphore.
        Task 4 begins and waits for the semaphore.
        Main thread calls Release(3)-- > 3 tasks can enter the semaphore.
        Task 1 enters the semaphore.
        Task 2 enters the semaphore.
        Task 3 enters the semaphore.
        Task 5 begins and waits for the semaphore.
        Task 3 releases the semaphore; previous count: 0.
        Task 2 releases the semaphore; previous count: 2.
        Task 4 enters the semaphore.
        Task 1 releases the semaphore; previous count: 1.
        Task 5 enters the semaphore.
        Task 5 releases the semaphore; previous count: 1.
        Task 4 releases the semaphore; previous count: 2.
        Main thread exits.*/

      #endregion
    }


    #region ManualResetEventSlim office demo

    // Demonstrates:
    //      ManualResetEventSlim construction
    //      ManualResetEventSlim.Wait()
    //      ManualResetEventSlim.Set()
    //      ManualResetEventSlim.Reset()
    //      ManualResetEventSlim.IsSet
    static void MRES_SetWaitReset()
    {
      //initialState 初始状态 表示当前是否可执行
      //public ManualResetEventSlim(bool initialState);
      //x用一个指示是否将初始状态设置为终止的布尔值初始化 ManualResetEventSlim 类的新实例。
      ManualResetEventSlim mres1 = new ManualResetEventSlim(false); // initialize as unsignaled
      ManualResetEventSlim mres2 = new ManualResetEventSlim(false); // initialize as unsignaled
      ManualResetEventSlim mres3 = new ManualResetEventSlim(true); // initialize as signaled

      mres3.Wait();

      // Start an asynchronous Task that manipulates mres3 and mres2
      var observer = Task.Factory.StartNew(() =>
      {
        mres1.Wait();
        Console.WriteLine("observer sees signaled mres1!");
        Console.WriteLine("observer resetting mres3...");
        mres3.Reset(); // should switch to unsignaled
        Console.WriteLine("observer signalling mres2");
        mres2.Set();
      });

      Console.WriteLine("main thread: mres3.IsSet = {0} (should be true)", mres3.IsSet);
      Console.WriteLine("main thread signalling mres1");


      mres1.Set(); // This will "kick off" the observer Task 这将“启动”观察者任务 
      mres2.Wait(); // This won't return until observer Task has finished resetting mres3
      Console.WriteLine("main thread sees signaled mres2!");
      Console.WriteLine("main thread: mres3.IsSet = {0} (should be false)", mres3.IsSet);

      // It's good form to Dispose() a ManualResetEventSlim when you're done with it
      observer.Wait(); // make sure that this has fully completed
      mres1.Dispose();
      mres2.Dispose();
      mres3.Dispose();
    }

    // Demonstrates:
    //      ManualResetEventSlim construction w/ SpinCount
    //      ManualResetEventSlim.WaitHandle
    static void MRES_SpinCountWaitHandle()
    {
      // Construct a ManualResetEventSlim with a SpinCount of 1000
      // Higher spincount => longer time the MRES will spin-wait before taking lock
      ManualResetEventSlim mres1 = new ManualResetEventSlim(false, 1000);
      ManualResetEventSlim mres2 = new ManualResetEventSlim(false, 1000);

      Task bgTask = Task.Factory.StartNew(() =>
      {
        // Just wait a little
        Thread.Sleep(100);

        // Now signal both MRESes
        Console.WriteLine("Task signalling both MRESes");
        mres1.Set();
        mres2.Set();
      });

      // A common use of MRES.WaitHandle is to use MRES as a participant in 
      // WaitHandle.WaitAll/WaitAny.  Note that accessing MRES.WaitHandle will
      // result in the unconditional inflation of the underlying ManualResetEvent.
      WaitHandle.WaitAll(new WaitHandle[] {mres1.WaitHandle, mres2.WaitHandle});
      Console.WriteLine("WaitHandle.WaitAll(mres1.WaitHandle, mres2.WaitHandle) completed.");

      // Clean up
      bgTask.Wait();
      mres1.Dispose();
      mres2.Dispose();
    }

    #endregion

    private static void MonitorDemoTest()
    {
      MonitorDemo instance = new MonitorDemo();

      ThreadPool.QueueUserWorkItem((state => //销售由他人负责，故此处开启新线程执行
      {
        Parallel.For(0, 100, (num) => //假设此商家有100个销售,由于销售与销售互不干扰，所以此处并行进行销售
        {
          Thread.Sleep(100); //由于后续执行太快，无法看出效果,故此处新增延时处理
          instance.Sell();
        });
      }));

      ThreadPool.QueueUserWorkItem((state =>
      {
        Parallel.For(0, 100, (num) => //假设此商家有10个生产厂
        {
          Thread.Sleep(100);
          instance.Make();
        });
      }));

      while (true) //每过一秒查看一次销售情况
      {
        Thread.Sleep(1000);
        instance.Show();
      }

      //      demo.Run();
      //
      //      Console.ReadKey(true);

      /*Parallel.For(0, 10, (i =>
      {
        try
        {
          demo.Run();
        }
        catch (Exception e)
        {
          Console.WriteLine(e.Message);
        }
        finally
        {
          Console.WriteLine($"{i} over");
          //测试结果:部分线程无法走到此处，疑似死锁。
        }
      }));*/
    }

    /// <summary>
    /// 锁的简单场景演示
    /// </summary>
    private static void LockTest(Random rand)
    {
      Condition condition = new Condition(); //创建一个初始库存为100的商家

      ThreadPool.QueueUserWorkItem((state => //销售由他人负责，故此处开启新线程执行
      {
        Parallel.For(0, 100, (num) => //假设此商家有100个销售,由于销售与销售互不干扰，所以此处并行进行销售
        {
          Thread.Sleep(100); //由于后续执行太快，无法看出效果,故此处新增延时处理
          var buy = rand.Next(10) + 1; //假设此处为用户需要购买的数量
          //            Console.WriteLine($"销售渠道-{num}:需要购买{buy}个产品");
          //          condition.LockSell(buy);//加锁处理
          condition.Sell(buy);
        });
      }));

      ThreadPool.QueueUserWorkItem((state =>
      {
        Parallel.For(0, 100, (num) => //假设此商家有10个生产厂
        {
          Thread.Sleep(100);
          var make = rand.Next(5) + 1; //假设此处为制造出来的产品数
          //            Console.WriteLine($"生产商-{num} :生产成功");
          //          condition.LockMake(make);
          condition.Make(make);
        });
      }));

      while (true) //每过一秒查看一次销售情况
      {
        Thread.Sleep(1000);
        condition.Show();
      }


      /*
      无锁测试结果：实际与当前不一致
      总生产数量: 11,总售出数量: 8,当前库存: 3,实际库存: 3
      总生产数量: 31,总售出数量: 29,当前库存: 1,实际库存: 2
      总生产数量: 51,总售出数量: 47,当前库存: 1,实际库存: 4
      总生产数量: 73,总售出数量: 67,当前库存: 3,实际库存: 6
      总生产数量: 93,总售出数量: 71,当前库存: 19,实际库存: 22
      总生产数量: 97,总售出数量: 71,当前库存: 23,实际库存: 26
      */
    }
  }
}