using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace NoSafe.Tool
{
  public class StopWatchTools
  {
    static Stopwatch timer = new Stopwatch();

    public static void ShowCountTime(Action action, string title = null)
    {
      timer.Restart();

      action.Invoke();

      timer.Stop();

      Console.WriteLine($"<{title}>spend time:{timer.Elapsed.TotalMilliseconds}");
    }
  }
}