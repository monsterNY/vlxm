using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Reading.Ref
{
  /// <summary>
  /// @desc : EventAwaiter  
  /// @author : mons
  /// @create : 2019/6/21 11:55:35 
  /// @source : clr via C# p654
  /// </summary>
  public class EventAwaiter<TEventArgs> : INotifyCompletion
  {

    private ConcurrentQueue<TEventArgs> m_events = new ConcurrentQueue<TEventArgs>();

    private Action m_continuation;

    #region 状态机调用的成员

    // 状态机先调用这个来获得awaiter;
    public EventAwaiter<TEventArgs> GetAwaiter()
    {
      Console.WriteLine("GetAwaiter");
      return this;
    }

    // 告诉状态机发生了任何事件
    public bool IsCompleted => m_events.Count > 0;

    // 状态机告诉我们以后要调用什么方法
    public void OnCompleted(Action continuation)
    {
      Console.WriteLine("OnCompleted");
      Volatile.Write(ref m_continuation,continuation);
    }

    // 状态机查询结果； 这是await操作符的结果
    public TEventArgs GetResult()
    {
      Console.WriteLine("GetResult");
      TEventArgs e;
      m_events.TryDequeue(out e);
      return e;
    }

    #endregion

    // 如果都引发了事件， 多个线程可能同时调用
    public void EventRaised(object sender, TEventArgs eventArgs)
    {
      Console.WriteLine("EventRaised");

      m_events.Enqueue(eventArgs);// 保存 EventArgs 以便从 GetResult/await返回

      // 如果有一个等待线程进行的延续任何，该线程会运行它
      Action continuation = Interlocked.Exchange(ref m_continuation, null);

      continuation?.Invoke();//恢复状态机

    }

  }
}
