using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Reading.Ref
{
  /// <summary>
  /// @desc : AnotherHybridLock  
  /// @author : mons
  /// @create : 2019/6/26 11:07:06 
  /// @source : 
  /// </summary>
  public class AnotherHybridLock:IDisposable
  {
    // Int32 由基元用户模式构造（Interlocked的方法）使用
    private int m_waiters = 0;

    // AutoResetEvent 是基元内核模式构造
    private AutoResetEvent m_waiterLock = new AutoResetEvent(false);

    // 控制自旋次数
    private int m_spincount = 4000;

    // 指出哪个线程拥有锁，以及拥有了它多少次
    private int m_owningThreadId = 0, m_recursion = 0;

    public void Enter()
    {
      var threadId = Thread.CurrentThread.ManagedThreadId;

      // 如果调用线程已经拥有锁，递增递归计数并返回
      if (threadId == m_owningThreadId)
      {
        m_recursion++;
        return;
      }

      // 调用线程不拥有锁，尝试获取它
      SpinWait spinWait = new SpinWait();

      for (int i = 0; i < m_spincount; i++)
      {
        if (Interlocked.CompareExchange(ref m_waiters, 1, 0) == 0) goto GotLock;

        // black magic : 给其他线程运行的机会，希望锁会被释放
        spinWait.SpinOnce();

      }

      // 自旋结束，锁仍未获得，再试一次
      if (Interlocked.Increment(ref m_waiters) > 1)
      {
        // 仍然是竞态条件，这个线程必须阻塞
        m_waiterLock.WaitOne();// 等待锁；性能有损失
        // 等这个线程醒来时，它拥有锁；设置一些状态并返回
      }

      GotLock:
      m_owningThreadId = threadId;
      m_recursion = 1;
    }

    public void Leave()
    {

      var threadId = Thread.CurrentThread.ManagedThreadId;

      // 如果调用线程不拥有锁，表明存在bug
      if(threadId != m_owningThreadId)
        throw new SynchronizationLockException("Lock not owned by calling thread");

      // 递减递归计数。如果这个线程仍然拥有锁，那么直接返回
      if(--m_recursion > 0)return;

      m_owningThreadId = 0; // 现在没有线程拥有锁

      // 如果没有其他线程在等待，直接返回
      if(Interlocked.Decrement(ref m_waiters) == 0)
        return;

      // 唤醒一个等待的线程
      m_waiterLock.Set();// 这里有较大的性能损失

    }

    public void Dispose()
    {
      m_waiterLock?.Dispose();
    }
  }
}