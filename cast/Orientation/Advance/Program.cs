using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Advance.Lock;
using Advance.Models;
using Advance.RefDemo;
using Newtonsoft.Json;
using Tools.CusMenu;
using Tools.RefTools;

namespace Advance
{
  //[Flags]
  enum Actions
  {
    None = 0,
    Read = 0x0001,
    Write = 0x0002,
    ReadWrite = Read | Write,
    Delete = 0x0004,
    Query = 0x0008,
    Sync = 0x0010
  }

  public class Type2
  {
    static Type2()
    {
      Console.WriteLine("Type2 ");
    }

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    public static void M()
    {
    }
  }

  class Program
  {
    public int Flag => 1;

    void LookIL()
    {
      int? num = 4;

      //success 编译器自动帮我们将Nullable<Int32>装箱为Int32

      //IL_000a: call instance void valuetype[System.Runtime]System.Nullable`1 < int32 >::.ctor(!0)
      //IL_000f: ldloc.1
      //IL_0010: box valuetype[System.Runtime]System.Nullable`1 < int32 >
      //IL_0015:  ldc.i4.4
      //IL_0016: box[System.Runtime]System.Int32

      ((IComparable) num).CompareTo(4);

      Console.WriteLine(num.GetType()); //System.Int32

      //typeof 运算符
      Console.WriteLine(typeof(int?)); //System.Nullable`1[System.Int32]

      var flag = Actions.Read | Actions.Delete;

      Console.WriteLine(flag.ToString());

      //Read, Delete
      Console.WriteLine(flag.ToString("F")); //当Enum没有[Flags]特性时，可使用"F"格式获取正确的字符串

      var program = new Program();

      Console.WriteLine(string.Concat(program.GetType().GetProperties()
        .Select(u => $"{u.Name}={u.GetValue(program)}")));

      Console.WriteLine($"{0:D}", 12321321);

      string str = "123"; //ldstr

      str = $"afsdjoiasf" + Environment.NewLine + "info";

      str = new String(new[]
      {
        '1',
        '2'
      }); //此处使用newobj

      var isInterned = string.IsInterned("123"); //返回123

      Console.WriteLine(isInterned);

      int a = 1, b = 2;

      //call       string [System.Runtime]System.String::Concat(object,
      //object,
      //object)

      //产生两次装箱
      Console.WriteLine(a + "," + b);

      //无装箱。
      Console.WriteLine(a.ToString() + "," + b.ToString());

      //依然存在装箱 format(object,...)
      Console.WriteLine($"{a},{b}");

      //IL_005b: ldtoken[System.Runtime]System.Int32
      //IL_0060:  call       class [System.Runtime]
      //System.Type[System.Runtime] System.Type::GetTypeFromHandle(valuetype[System.Runtime] System.RuntimeTypeHandle)
      //未产生装箱。
      var type = typeof(int);

      //调用基类object方法 需要进行装箱
      type = a.GetType();

      new object().GetType();

      new Program().Test();
    }

    void LookIL2()
    {
      object obj = new Program();

      //  IL_0012:  isinst     Advance.Program
      var flag = obj is Program;

      //    IL_0008:  isinst     Advance.Program
      var cast = obj as Program;

      // is 和 as 运算符使用相同的IL操作指令 isinst

    }

    protected virtual void Test()
    {
    }


    static void Main(string[] args)
    {
      var rand = new Random();
      CodeTimer timer = new CodeTimer();
      timer.Initialize();

      RuntimeHelpers.PrepareConstrainedRegions();

      try
      {
        Console.WriteLine("try");
      }
      finally
      {
        Console.WriteLine("finally");
        Type2.M();
      }

      Action fun = () => { Console.WriteLine("fun 1 -------"); };
      Action fun2 = () => { Console.WriteLine("fun 2 -------"); };

      var combineFun = (Action) Delegate.Combine(fun, fun2, fun);

      var invocationList = combineFun.GetInvocationList();

      Console.WriteLine(invocationList.Length);

      combineFun.Invoke();

      var remove = (Action) Delegate.Remove(combineFun, fun);

      remove.Invoke();

      new Program().LookIL();

      Action action = () => { Console.WriteLine("action----" + DateTime.Now.ToString()); };
      Action action2 = () => { Console.WriteLine("action2-----" + DateTime.Now.ToString()); };

      Console.WriteLine("action +=");

      action += action;

      action.Invoke(); //输出两次。


      Console.WriteLine("Delegate.Combine");

      var newDel = Delegate.Combine(action, action2);

      newDel.Method.Invoke(newDel.Target, null); //输出一次

      Console.WriteLine("Delegate Cast To Action Invoke");

      Action newAction = (Action) newDel;

      newAction.Invoke(); //输出三次

      Console.WriteLine("Delegate.Remove");

      //此处若不重新赋值，则无法影响到newAction
      //猜测Remove 和 Combine都是构建了一个新的Delegate
      newAction = (Action) Delegate.Remove(newAction, action);

      newAction.Invoke(); //输出一次

      //event实际就是通过Delegate进行操作的
      //event 就是 Delegate的实例。。差点忘了

      var info = new {key = 1, value = 2}; //类似元组

      Console.WriteLine(info);

      var info2 = new {key = 1, value = 2};

      Console.WriteLine(info.Equals(info2)); //此处为true

      var str = ">{value}asdjfisadf";

      var strings = str.Split(new[] {"{value}"}, StringSplitOptions.None);

      Console.WriteLine(DBNull.Value);

      Console.WriteLine(JsonConvert.SerializeObject(strings));

      Type type = typeof(Program);

      var typeInfo = type.GetTypeInfo();

      typeInfo.AsType();

      Type t = typeof(Dictionary<,>);

      t = t.MakeGenericType(typeof(int), typeof(string));

      Console.WriteLine(Activator.CreateInstance(t).GetType());

//      t.GetMethods()[0].GetCustomAttribute()

      //AssemblyDemo.Show();

      //AssemblyDemo.LoadAssemblyAndShowPublicTypes("System.Data, version=4.0.0.0,culture=neutral, PublicKeyToken=b77a5c561934e089");

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