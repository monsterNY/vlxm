using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Reading.Ref
{
  /// <summary>
  /// @desc : SimpleHybridLock  
  /// @author : mons
  /// @create : 2019/6/26 10:48:15 
  /// @source : clr via C# p687
  /// </summary>
  public class SimpleHybridLock:IDisposable
  {

    // int32 由基元用户模式构造(Interlocked的方法)使用
    private int m_waiters = 0;

    // AutoResetEvent是基元内核模式构造
    private AutoResetEvent m_waiterLock = new AutoResetEvent(false);

    public void Enter()
    {

      // 指出这个线程想要获得锁
      if(Interlocked.Increment(ref m_waiters) == 1)
        return;// 锁可自由使用，无竞争，直接返回

      // 另一个线程拥有锁(发生竞争)，使这个线程等待
      m_waiterLock.WaitOne();// 这里产生较大的性能影响
      // WaitOne 返回后，这个线程拿到锁了

    }

    public void Leave()
    {

      // 这个线程准备释放锁
      if(Interlocked.Decrement(ref m_waiters) == 0)
        return;// 没有其他线程正在等待，直接返回

      // 有其他线程正在阻塞，唤醒其中一个
      m_waiterLock.Set();// 这里产生较大的性能影响

    }

    public void Dispose()
    {
      m_waiterLock.Dispose();
    }
  }
}
