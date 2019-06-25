using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Reading.Ref
{
  /// <summary>
  /// @desc : TaskLogger  
  /// @author : mons
  /// @create : 2019/6/21 11:26:10 
  /// @source : clr via C# p653
  /// </summary>
  public static class TaskLogger
  {
    public enum TaskLogLevel
    {
      None,
      Pending
    }

    public static TaskLogLevel LogLevel { get; set; }

    public sealed class TaskLogEntry
    {
      public Task Task { get; set; }

      public string Tag { get; set; }

      public DateTime LogTime { get; set; }

      public int CallerLineNumber { get; set; }

      public string CallerMemberName { get; set; }

      public string CallerFilePath { get; set; }

      public override string ToString()
      {
        return
          $"{nameof(LogTime)}={LogTime.ToString()}, {nameof(Tag)}={Tag ?? "None"}, Member={CallerMemberName}, File={CallerFilePath}({CallerLineNumber.ToString()})";
      }
    }

    private static readonly ConcurrentDictionary<Task, TaskLogEntry> s_log =
      new ConcurrentDictionary<Task, TaskLogEntry>();

    public static IEnumerable<TaskLogEntry> GetLogEntries()
    {
      return s_log.Values;
    }

    public static Task<TResult> Log<TResult>(this Task<TResult> task, string tag = null,
      [CallerMemberName] string callerMemberName = null,
      [CallerFilePath] string callerFilePath = null,
      [CallerLineNumber] int callerLineNumber = -1)
    {
      return (Task<TResult>) Log((Task) task, tag, callerMemberName, callerFilePath, callerLineNumber);
    }


    public static Task Log(this Task task, string tag = null,
      [CallerMemberName] string callerMemberName = null,
      [CallerFilePath] string callerFilePath = null,
      [CallerLineNumber] int callerLineNumber = -1)
    {
      if (LogLevel == TaskLogLevel.None) return task;

      var logEntry = new TaskLogEntry()
      {
        CallerFilePath = callerFilePath,
        CallerLineNumber = callerLineNumber,
        CallerMemberName = callerMemberName,
        LogTime = DateTime.Now,
        Task = task,
        Tag = tag,
      };

      s_log[task] = logEntry;

      task.ContinueWith(t =>
      {

        Console.WriteLine("task 已完成");

        TaskLogEntry entry;
        s_log.TryRemove(t, out entry);
//      }, TaskContinuationOptions.ExecuteSynchronously);// 无演示效果
    }, TaskContinuationOptions.OnlyOnRanToCompletion);

      return task;
    }
  }
}