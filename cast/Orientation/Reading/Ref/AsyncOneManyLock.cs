using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Reading.Ref
{
  /// <summary>
  /// @desc : AsyncOneManyLock  
  /// @author : mons
  /// @create : 2019/6/26 14:51:14 
  /// @source : clr via C# p721
  /// </summary>
  public class AsyncOneManyLock
  {
    #region 锁的代码

    private SpinLock m_lock = new SpinLock(true); // 自旋锁不要用readonly 因为其内部是维护着bool变量 会改变变量值。

    private void Lock()
    {
      var taken = false;
      m_lock.Enter(ref taken);
    }

    public void UnLock()
    {
      m_lock.Exit();
    }

    #endregion

    #region 锁的状态和辅助方法

    private int m_state = 0;
    private bool IsFree => m_state == 0;

    private bool IsOwnedByWriter => m_state == -1;

    private bool IsOwnedByReaders => m_state > 0;

    private int AddReaders(int count)
    {
      return m_state += count;
    }

    private int SubtractReader()
    {
      return --m_state;
    }

    public void MakeWriter()
    {
      m_state = -1;
    }

    public void MakeFree()
    {
      m_state = 0;
    }

    #endregion

    // 目的是在非竞态条件时增强性能和减少内存消耗
    private readonly Task m_noContentionAccessGranter;

    // 每个等待writer都通过它们在这里排队的TaskCompletionSource来唤醒
    private readonly Queue<TaskCompletionSource<Object>> m_qWaitingWriters = new Queue<TaskCompletionSource<object>>();

    // 一个 TaskCompletionSource收到信号，所有等待的reader都唤醒
    private TaskCompletionSource<Object> m_waitingReadersSignal = new TaskCompletionSource<object>();

    private int m_numWaitingReaders = 0;

    public Task WaitAsync(OneManyMode mode)
    {
      // 假定无竞争
      Task accressGranter = m_noContentionAccessGranter;

      Lock();

      switch (mode)
      {
        case OneManyMode.Exclusive:

          if (IsFree)
          {
            MakeWriter();
          }
          else
          {
            // 有竞争： 新的writer任务进入队列，并返回它使writer等待
            var tcs = new TaskCompletionSource<object>();
            m_qWaitingWriters.Enqueue(tcs);
            accressGranter = tcs.Task;
          }

          break;

        case OneManyMode.Shared:

          if (IsFree || (IsOwnedByReaders && m_qWaitingWriters.Count == 0))
          {
            AddReaders(1);
          }
          else
          {
            // 竞争：递增等待的reader数量，并返回reader任务使reader等待
            m_numWaitingReaders++;
            accressGranter = m_waitingReadersSignal.Task.ContinueWith(t =>
            {
              Console.WriteLine("所有写入完成了，读取开始");
            });
          }

          break;
      }

      UnLock();

      return accressGranter;
    }

    public void Release()
    {
      TaskCompletionSource<object> accessGranter = null; // 假定没有代码被释放

      Lock();

      if (IsOwnedByWriter) MakeFree(); // 一个writer离开
      else SubtractReader();// 一个reader离开

      if (IsFree)
      {
        if (m_qWaitingWriters.Count > 0)
        {
          MakeWriter();
          accessGranter = m_qWaitingWriters.Dequeue();
        }else if (m_numWaitingReaders > 0)
        {
          AddReaders(m_numWaitingReaders);
          m_numWaitingReaders = 0;
          accessGranter = m_waitingReadersSignal;

          // 为将来需要等待的readers创建一个新的TCS
          m_waitingReadersSignal = new TaskCompletionSource<object>();

        }

      }

      UnLock();

      // 唤醒锁外面的writer/reader,减少竞争机率以提高信念
      if (accessGranter != null) accessGranter.SetResult(null);
    }

    public static void Run()
    {

      AsyncOneManyLock asyncOneManyLock = new AsyncOneManyLock();

      asyncOneManyLock.WaitAsync(OneManyMode.Shared);

      // mock 1.1 : writer进来，锁自由，直接修改状态，返回无竞争

      asyncOneManyLock.WaitAsync(OneManyMode.Exclusive);

      // mock 1.2 : writer进来，锁已被获取,创建writer任务，将writer加入队列，并返回任

      asyncOneManyLock.WaitAsync(OneManyMode.Exclusive);

      // mock 1.3 : 第一个writer释放,由于队列中存在未完成的任务，将状态设为writer,在队列中移除一个任务,返回新出列的任务。

      asyncOneManyLock.Release();

      // mock 1.4 : reader 进来，存在竞争，添加reader的数量，并给当前任务添加一个延续任务

      asyncOneManyLock.WaitAsync(OneManyMode.Shared);

      asyncOneManyLock.WaitAsync(OneManyMode.Exclusive);

      asyncOneManyLock.WaitAsync(OneManyMode.Shared);

      asyncOneManyLock.Release();

      asyncOneManyLock.Release();

      asyncOneManyLock.Release();

    }

  }

  public enum OneManyMode
  {
    Exclusive,
    Shared
  }
}