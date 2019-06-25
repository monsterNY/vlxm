using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Reading.Ref
{
  /// <summary>
  /// @desc : WithCancellation  取消I/O操作
  /// @author : mons
  /// @create : 2019/6/21 15:56:27 
  /// @source : clr via C# p664
  /// </summary>
  public static class CancellationExt
  {
    private struct Void
    {
    } // 没有非泛型的TaskCompletionSource类

    public static async void Run()
    {
      var cts = new CancellationTokenSource(5000);

      var ct = cts.Token;

      try
      {
        await Task.Delay(10000).WithCancellation(ct);

        Console.WriteLine("task completed");

      }
      catch (OperationCanceledException)
      {
        Console.WriteLine("Task canceled");
      }
    }

    public static async Task WithCancellation(this Task originalTask, CancellationToken ct)
    {
      // 创建在 CancellationToken被取消时完成的一个Task
      var cancelTask = new TaskCompletionSource<Void>();

      // 一旦CancellationToken 被取消，就完成Task
      using (ct.Register(t => ((TaskCompletionSource<Void>) t).TrySetResult(new Void()), cancelTask))
      {
        // 创建在原始Task或CancellationToken Task完成时都完成的一个Task
        var any = await Task.WhenAny(originalTask, cancelTask.Task);

        // 任何Task因为 CancellationToken而完成，就抛出OperationCanceledException
        if (any == cancelTask.Task) ct.ThrowIfCancellationRequested();
      }

      // 等待原始任务(以同步方式);若任务失败，等待它将抛出第一个内部异常
      // 而不是抛出AggregateException
      await originalTask;
    }

    public static async Task<TResult> WithCancellation<TResult>(this Task<TResult> originalTask, CancellationToken ct)
    {
      return await (Task<TResult>) WithCancellation((Task) originalTask, ct);
    }
  }
}