using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using Reading.Ref;

namespace Reading.Demo
{
  /// <summary>
  /// @desc : EventAwaiterDemo  
  /// @author : mons
  /// @create : 2019/6/21 12:05:03 
  /// @source : clr via C# p655
  ///
  /// 异步方法的状态机示例： p650
  /// 
  /// </summary>
  public class EventAwaiterDemo
  {
    private static async void ShowExceptions()
    {
      var eventAwaiter = new EventAwaiter<FirstChanceExceptionEventArgs>();

      AppDomain.CurrentDomain.FirstChanceException += eventAwaiter.EventRaised;

      while (true)
      {
        Console.WriteLine($"AppDomain exception: {(await eventAwaiter).Exception.GetType()}");// awaiter 充当 异步方法的状态机 和 被引发事件之间的 “粘合剂”
      }
    }

    public static void Run()
    {
      ShowExceptions();

      for (int i = 0; i < 3; i++)
      {
        try
        {
          switch (i)
          {
            case 0: throw new InvalidOperationException();
            case 1: throw new ObjectDisposedException("");
            case 2: throw new ArgumentOutOfRangeException();
          }
        }
        catch (Exception e)
        {
        }
      }
    }
  }
}