using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Advance.CusDemo
{
  /// <summary>
  /// @desc : TaskDemo  
  /// @author : monster_yj
  /// @create : 2019/7/15 21:38:45 
  /// @source : 
  /// </summary>
  public class TaskDemo
  {
    public void Test()
    {
      // 测试父任务与子任务的执行线程关系

      // 初步结果：如果是已经通过Run启动的任务，不会是同一线程

      // 如果是通过Wait后启动的任务，可能是同一线程

      // 最终结果：

      // 无论已何种方式，只要是执行子任务时，父任务有等待或其他让线程空闲的情况，都有可能出现执行子任务的线程与父线程为同一线程！！！

      Task.Run((() =>
      {
        Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} task run");

        for (int i = 0; i < 100; i++)
        {
          Task.Run((() =>
          {
            Thread.Sleep(100);
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} sub task run");
          })).Wait();
        }

        // 不可能为同一线程 因为
        //Task.Run((() =>
        //{
        //  Thread.Sleep(100);
        //  Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} sub task run");
        //})).Wait();


        //Task.Run((() =>
        //{
        //  Thread.Sleep(500);
        //  Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} sub task run");
        //}));

        //Task.Run((() =>
        //{
        //  Thread.Sleep(500);
        //  Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} sub task run");
        //}));

        //Task subTask = new Task((() =>
        //{
        //  Thread.Sleep(100);
        //  Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} sub2 task run");
        //}));
        //subTask.Start();
        //Task.WaitAll(subTask); // 此处等待，可能会出现父任务与子任务由同一线程执行


        //Task.WaitAll(Task.Run((() =>
        //{
        //  Thread.Sleep(500);
        //  Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} sub3 task run");
        //}))); // 此处等待，可能会出现父任务与子任务由同一线程执行

        Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} task over");
      }));
    }

    public async Task Test2()
    {


      await Task.Run((async () =>
      {
        Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} task run");

        for (int i = 0; i < 100; i++)
        {
          await Task.Run((() =>
          {
            Thread.Sleep(100);
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} sub task run");
          }));
        }

        Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} task over");
      }));
    }
  }
}