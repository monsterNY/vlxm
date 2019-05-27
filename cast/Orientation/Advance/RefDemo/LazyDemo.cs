using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Advance.Models;

namespace Advance.RefDemo
{
  /// <summary>
  /// @desc : LazyDemo  
  /// @author : mons
  /// @create : 2019/5/24 17:27:11 
  /// @source : 
  /// </summary>
  public class LazyDemo
  {
    public void Run()
    {
      var lazy = new Lazy<string>((() =>
      {

        Thread.Sleep(100);

        Console.WriteLine($"线程-{Thread.CurrentThread.ManagedThreadId} 调用初始化");

        return $"线程-{Thread.CurrentThread.ManagedThreadId} over";
      }), LazyThreadSafetyMode.PublicationOnly);

      Console.WriteLine(lazy);

      Action action = () =>
      {
        Console.WriteLine(lazy.Value);
      };

      for (int i = 0; i < 10; i++)
      {
        Task.Run(action);
      }

      Console.WriteLine(lazy);

    }

    static Lazy<LargeObject> lazyLargeObject = null;

    static LargeObject InitLargeObject()
    {
      LargeObject large = new LargeObject(Thread.CurrentThread.ManagedThreadId);
      // Perform additional initialization here.
      return large;
    }

    public void Office()
    {
      // The lazy initializer is created here. LargeObject is not created until the 
      // ThreadProc method executes.
      lazyLargeObject = new Lazy<LargeObject>(InitLargeObject);

      // The following lines show how to use other constructors to achieve exactly the
      // same result as the previous line: 
      // lazyLargeObject = new Lazy<LargeObject>(InitLargeObject, true);
      // lazyLargeObject = new Lazy<LargeObject>(InitLargeObject, 
      // LazyThreadSafetyMode.ExecutionAndPublication);


      Console.WriteLine(
        "\r\nLargeObject is not created until you access the Value property of the lazy" +
        "\r\ninitializer. Press Enter to create LargeObject.");
      Console.ReadKey(true);

      Console.WriteLine();

      // Create and start 3 threads, each of which uses LargeObject.
      Thread[] threads = new Thread[3];
      for (int i = 0; i < 3; i++)
      {
        threads[i] = new Thread(ThreadProc);
        threads[i].Start();
      }

      // Wait for all 3 threads to finish. 
      foreach (Thread t in threads)
      {
        t.Join();
      }

      Console.WriteLine("\r\nPress Enter to end the program");
      Console.ReadLine();

    }


    static void ThreadProc(object state)
    {
      LargeObject large = lazyLargeObject.Value;

      // IMPORTANT: Lazy initialization is thread-safe, but it doesn't protect the  
      //            object after creation. You must lock the object before accessing it,
      //            unless the type is thread safe. (LargeObject is not thread safe.)
      lock (large)
      {
        large.Data[0] = Thread.CurrentThread.ManagedThreadId;
        Console.WriteLine("Initialized by thread {0}; last used by thread {1}.",
          large.InitializedBy, large.Data[0]);
      }
    }
  }
}