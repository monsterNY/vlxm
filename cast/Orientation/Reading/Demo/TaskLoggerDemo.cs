using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Reading.Ref;
using Reading.Tools;

namespace Reading.Demo
{
  /// <summary>
  /// @desc : TaskLoggerDemo  
  /// @author : mons
  /// @create : 2019/6/21 11:37:25 
  /// @source : clr via C# p654
  /// </summary>
  public class TaskLoggerDemo
  {
    public static async Task Run()
    {
#if DEBUG
      // 使用TaskLogger会影响内存和性能，所以仅在调试生成中启用
      TaskLogger.LogLevel = TaskLogger.TaskLogLevel.Pending;
#endif

      var cts = new CancellationTokenSource(3000);

      var tasks = new List<Task>()
      {
        Task.Delay(2000, cts.Token).Log("2s op"),
        Task.Delay(5000, cts.Token).Log("5s op"),
        Task.Delay(6000, cts.Token).Log("6s op"),
      };

      try
      {
        await Task.WhenAll(tasks);
      }
      catch (OperationCanceledException e)
      {
        Console.WriteLine(e);
      }

      foreach (var entry in TaskLogger.GetLogEntries().OrderBy(u => u.LogTime))
      {
        Console.WriteLine(entry.ToString());
      }
    }
  }
}