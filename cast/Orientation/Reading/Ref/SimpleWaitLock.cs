using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Reading.Ref
{
  /// <summary>
  /// @desc : SimpleWaitLock  
  /// @author : mons
  /// @create : 2019/6/26 10:08:55 
  /// @source : clr via C# p691
  /// </summary>
  public class SimpleWaitLock : IDisposable
  {
    private readonly AutoResetEvent m_available;

    public SimpleWaitLock()
    {
      m_available = new AutoResetEvent(true); // 最开始可自由使用
    }

    public void Enter()
    {
      // 在内核中阻塞，直到资源可用
      m_available.WaitOne();
    }

    public void Leave()
    {
      // 让另一个线程访问资源
      m_available.Set();
    }

    public void Dispose()
    {
      m_available.Dispose();
    }
  }
}