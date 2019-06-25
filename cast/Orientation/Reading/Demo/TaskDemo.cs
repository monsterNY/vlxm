using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Reading.Demo
{
  /// <summary>
  /// @desc : TaskDemo  
  /// @author : mons
  /// @create : 2019/6/20 10:45:02 
  /// @source : clr via C# p622
  /// </summary>
  public class TaskDemo
  {
    public static void Run()
    {
      CancellationTokenSource source = new CancellationTokenSource();

      var task = Task.Run((() => Sum(source.Token, 100000000)), source.Token);

      source.Cancel();

      try
      {
        Console.WriteLine($"The sum is {task.Result}");
      }
      catch (AggregateException e)
      {
        e.Handle((exception => exception is OperationCanceledException));
      }

      //所有异常处理完成后执行
      Console.WriteLine("Sum was canceled");

      Task<int[]> parent = new Task<int[]>((() =>
      {
        var results = new int[3];

        new Task((() => results[0] = Sum(10000)), TaskCreationOptions.AttachedToParent).Start();
        new Task((() => results[1] = Sum(20000)), TaskCreationOptions.AttachedToParent).Start();
        new Task((() => results[2] = Sum(30000)), TaskCreationOptions.AttachedToParent).Start();

        return results;
      }));
    }

    public static void RunFactory()
    {
      var parent = new Task((() =>
      {
        var cts = new CancellationTokenSource();

        var tf = new TaskFactory<int>(cts.Token, TaskCreationOptions.AttachedToParent,
          TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);

        //创建并启动3个子任务
        var childTasks = new[]
        {
          tf.StartNew((() => Sum(cts.Token, 10000))),
          tf.StartNew((() => Sum(cts.Token, 20000))),
          tf.StartNew((() => Sum(cts.Token, int.MaxValue))) //异常 : OverflowException
        };

        //任何子任务抛出异常，就取消其余子任务
        for (int i = 0; i < childTasks.Length; i++)
        {
          childTasks[i].ContinueWith(u => cts.Cancel(), TaskContinuationOptions.OnlyOnFaulted);
        }

        tf.ContinueWhenAll(childTasks,
            (tasks =>
              tasks.Where(u => !u.IsFaulted && !u.IsCanceled).Max(t => t.Result))
            , CancellationToken.None)
          .ContinueWith(t => Console.WriteLine($"The maximum is : {t.Result}"),
            TaskContinuationOptions.ExecuteSynchronously);
      }));

      //子任务完成后，也显示任何未处理的异常
      parent.ContinueWith(p =>
      {

        StringBuilder builder = new StringBuilder($"the following exception(s) occurred:{Environment.NewLine}");

        foreach (var e in p.Exception.Flatten().InnerExceptions)
        {
          builder.Append($"  {e.GetType()}");
        }

        Console.WriteLine(builder);

      },TaskContinuationOptions.OnlyOnFaulted);

      parent.Start();

    }

    private static int Sum(int num)
    {
      var sum = 0;
      while (num-- > 0)
      {
        checked
        {
          sum += num;
        }
      }

      return sum;
    }

    private static int Sum(CancellationToken sourceToken, int num)
    {
      var sum = 0;
      while (num-- > 0)
      {
        sourceToken.ThrowIfCancellationRequested();

        checked
        {
          sum += num;
        }
      }

      return sum;
    }
  }
}