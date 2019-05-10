
<style type="text/css">
pre {
  max-height: 180px;
}
</style>

# study #

## 线程 ##
> 线程的开销及调度

	主要包括线程内核对象、线程环境块、1M大小的用户模式栈、内核模式栈。
	其中用户模式栈对于普通的系统线程那1M是预留的，在需要的时候才会分配，但是对于CLR线程，那1M是一开始就分类了内存空间的。

> 补充一句，CLR线程是直接对应于一个Windows线程的。

![](https://images2015.cnblogs.com/blog/151257/201603/151257-20160321141550589-1339297361.png)

	还记得以前学校里学习计算机课程里讲到，计算机的核心计算资源就是CPU核心和CPU寄存器，这也就是线程运行的主要战场。操作系统中那么多线程（一般都有上千个线程，大部分都处于休眠状态），
	对于单核CPU，一次只能有一个线程被调度执行，那么多线程怎么分配的呢？Windows系统采用时间轮询机制，CPU计算资源以时间片(大约30ms)的形式分配给执行线程。

计算鸡资源（CPU核心和CPU寄存器）一次只能调度一个线程，具体的调度流程：

- 	把CPU寄存器内的数据保存到当前线程内部（线程上下文等地方），给下一个线程腾地方；
- 	线程调度：在线程集合里取出一个需要执行的线程；
- 	加载新线程的上下文数据到CPU寄存器；
- 	新线程执行，享受她自己的CPU时间片（大约30ms），完了之后继续回到第一步，继续轮回；

对于Thread的使用太简单了，这里就不重复了，总结一下线程的主要几点性能影响：

- 	线程的创建、销毁都是很昂贵的；
- 	线程上下文切换有极大的性能开销，当然假如需要调度的新线程与当前是同一线程的话，就不需要线程上下文切换了，效率要快很多；
- 	这一点需要注意，GC执行回收时，首先要（安全的）挂起所有线程，遍历所有线程栈（根），GC回收后更新所有线程的根地址，再恢复线程调用，线程越多，GC要干的活就越多；


当然现在硬件的发展，CPU的核心越来越多，多线程技术可以极大提高应用程序的效率。但这也必须在合理利用多线程技术的前提下，了线程的基本原理，然后根据实际需求，还要注意相关资源环境，如磁盘IO、网络等情况综合考虑。

## lock ##

> 常见混合锁

### SemaphoreSlim ###

> 表示对可同时访问资源或资源池的线程数加以限制的 Semaphore 的轻量替代。

Semaphore - 信号

### 基础使用: ###

	创建一个示例,initialCount表示初始可执行线程，maxCount表示最大可执行线程，maxCount >= initialCount
    //public SemaphoreSlim(int initialCount, int maxCount);
    SemaphoreSlim semaphore = new SemaphoreSlim(0, 3);

	//初始执行时，先wait，若semaphore存在可执行数量则直接执行，否则一直等待
	semaphore.Wait();

	//do something

	//释放资源并返回上一个可执行数量
	semaphore.Release();

	//一次释放多个信号
	//releaseCount 释放数量，	
    public int Release(int releaseCount);

1. 执行前 - 先查看是否有空位

2. 若有 - 则占取位置，并开始执行
	- 执行完毕，则离开

3. 若无 - 则等待空位，直到有人离开(释放)

### 源码分析 ###

> Wait(int millisecondsTimeout, CancellationToken cancellationToken)

	//millisecondsTimeout 等待毫秒数 -1 表示无限 
	//cancellationToken 取消token 可通过此token来取消等待 【传播有关应取消操作的通知，后续再观察。】
	[__DynamicallyInvokable]
	public bool Wait(int millisecondsTimeout, CancellationToken cancellationToken)
	{
	    this.CheckDispose();
	    if (millisecondsTimeout < -1)
	    {
	        throw new ArgumentOutOfRangeException("totalMilliSeconds", millisecondsTimeout, GetResourceString("SemaphoreSlim_Wait_TimeoutWrong"));
	    }
	    cancellationToken.ThrowIfCancellationRequested();
	    uint startTime = 0;
	    if ((millisecondsTimeout != -1) && (millisecondsTimeout > 0))
	    {
	        startTime = TimeoutHelper.GetTime();//通过定时器轮询
	    }
	    bool flag = false;
	    Task<bool> task = null;
	    bool lockTaken = false;
	    CancellationTokenRegistration registration = cancellationToken.InternalRegisterWithoutEC(s_cancellationTokenCanceledEventHandler, this);
	    try
	    {
	        SpinWait wait = new SpinWait();//提供对基于自旋的等待的支持。 后续考虑
			//m_currentCount 当前可执行线程数量
			//获取对 SpinOnce() 的下一次调用是否将产生处理器，同时触发强制上下文切换。
	        while ((this.m_currentCount == 0) && !wait.NextSpinWillYield)
	        {
	            wait.SpinOnce();
	        }
	        try
	        {
	        }
	        finally
	        {
	            Monitor.Enter(this.m_lockObj, ref lockTaken);// Monitor
	            if (lockTaken)
	            {
	                this.m_waitCount++;
	            }
	        }
	        if (this.m_asyncHead != null)//查看是否有线程在等待。
	        {
	            task = this.WaitAsync(millisecondsTimeout, cancellationToken);
	        }
	        else
	        {
	            OperationCanceledException exception = null;
	            if (this.m_currentCount == 0)//暂无可执行数量
	            {
	                if (millisecondsTimeout == 0)
	                {
	                    return false;//等待超时
	                }
	                try
	                {
	                    flag = this.WaitUntilCountOrTimeout(millisecondsTimeout, startTime, cancellationToken);
	                }
	                catch (OperationCanceledException exception2)
	                {
	                    exception = exception2;
	                }
	            }
	            if (this.m_currentCount > 0)
	            {
	                flag = true;
	                this.m_currentCount--;
	            }
	            else if (exception != null)
	            {
	                throw exception;
	            }
	            if ((this.m_waitHandle != null) && (this.m_currentCount == 0))
	            {
	                this.m_waitHandle.Reset();
	            }
	        }
	    }
	    finally
	    {
	        if (lockTaken)
	        {
	            this.m_waitCount--;
	            Monitor.Exit(this.m_lockObj);
	        }
	        registration.Dispose();
	    }
	    if (task == null)
	    {
	        return flag;
	    }
	    return task.GetAwaiter().GetResult();
	}

> WaitUntilCountOrTimeout(int millisecondsTimeout, uint startTime, CancellationToken cancellationToken)
	
	private bool WaitUntilCountOrTimeout(int millisecondsTimeout, uint startTime, CancellationToken cancellationToken)
	{
	    int num = -1;
	    while (this.m_currentCount == 0)//当前暂无可执行数量
	    {
	        cancellationToken.ThrowIfCancellationRequested();
	        if (millisecondsTimeout != -1)
	        {
	            num = TimeoutHelper.UpdateTimeOut(startTime, millisecondsTimeout);
	            if (num <= 0)//超时返回
	            {
	                return false;
	            }
	        }
	        if (!Monitor.Wait(this.m_lockObj, num))
	        {
	            return false;
	        }
	    }
	    return true;
	}

> Wait(object obj, int millisecondsTimeout)

	public static bool Wait(object obj, int millisecondsTimeout)
	{
	    return Wait(obj, millisecondsTimeout, false);
	}

> Wait(object obj, int millisecondsTimeout, bool exitContext)

	[SecuritySafeCritical]
	public static bool Wait(object obj, int millisecondsTimeout, bool exitContext)
	{
	    if (obj == null)
	    {
	        throw new ArgumentNullException("obj");
	    }
	    return ObjWait(exitContext, millisecondsTimeout, obj);
	}

> ObjWait(bool exitContext, int millisecondsTimeout, object obj);

	[MethodImpl(MethodImplOptions.InternalCall), SecurityCritical]
	private static extern bool ObjWait(bool exitContext, int millisecondsTimeout, object obj);

。。。extern 见wait(Source_code)

> Release(int releaseCount)


	public int Release(int releaseCount)
	{
	    int num;
	    this.CheckDispose();
	    if (releaseCount < 1)
	    {
	        throw new ArgumentOutOfRangeException("releaseCount", releaseCount, GetResourceString("SemaphoreSlim_Release_CountWrong"));
	    }
	    object lockObj = this.m_lockObj;
	    lock (lockObj)
	    {
	        int currentCount = this.m_currentCount;
	        num = currentCount;
	        if ((this.m_maxCount - currentCount) < releaseCount)
	        {
	            throw new SemaphoreFullException();
	        }
	        currentCount += releaseCount;
	        int waitCount = this.m_waitCount;
	        if ((currentCount == 1) || (waitCount == 1))
	        {
	            Monitor.Pulse(this.m_lockObj);
	        }
	        else if (waitCount > 1)
	        {
	            Monitor.PulseAll(this.m_lockObj);
	        }
	        if (this.m_asyncHead != null)
	        {
	            int num4 = currentCount - waitCount;
	            while ((num4 > 0) && (this.m_asyncHead != null))
	            {
	                currentCount--;
	                num4--;
	                TaskNode asyncHead = this.m_asyncHead;
	                this.RemoveAsyncWaiter(asyncHead);
	                QueueWaiterTask(asyncHead);
	            }
	        }
	        this.m_currentCount = currentCount;
	        if (((this.m_waitHandle != null) && (num == 0)) && (currentCount > 0))
	        {
	            this.m_waitHandle.Set();
	        }
	    }
	    return num;
	}


### Summary ###

实际就是对于Pulse和Wait的一个封装类

----------

### ManualResetEventSlim ###

> 表示线程同步事件，收到信号时，必须手动重置该事件。


官方示例：

	// Demonstrates:
    //      ManualResetEventSlim construction
    //      ManualResetEventSlim.Wait()
    //      ManualResetEventSlim.Set()
    //      ManualResetEventSlim.Reset()
    //      ManualResetEventSlim.IsSet
    static void MRES_SetWaitReset()
    {
      //initialState 初始状态 表示当前是否可执行
      //public ManualResetEventSlim(bool initialState);
      //x用一个指示是否将初始状态设置为终止的布尔值初始化 ManualResetEventSlim 类的新实例。
      ManualResetEventSlim mres1 = new ManualResetEventSlim(false); // initialize as unsignaled
      ManualResetEventSlim mres2 = new ManualResetEventSlim(false); // initialize as unsignaled
      ManualResetEventSlim mres3 = new ManualResetEventSlim(true);  // initialize as signaled

      mres3.Wait();

      // Start an asynchronous Task that manipulates mres3 and mres2
      var observer = Task.Factory.StartNew(() =>
      {
        mres1.Wait();
        Console.WriteLine("observer sees signaled mres1!");
        Console.WriteLine("observer resetting mres3...");
        mres3.Reset(); // should switch to unsignaled
        Console.WriteLine("observer signalling mres2");
        mres2.Set();
      });

      Console.WriteLine("main thread: mres3.IsSet = {0} (should be true)", mres3.IsSet);
      Console.WriteLine("main thread signalling mres1");

      //将mres1设置为就绪状态
      mres1.Set(); // This will "kick off" the observer Task 这将“启动”观察者任务 
      mres2.Wait(); // This won't return until observer Task has finished resetting mres3
      Console.WriteLine("main thread sees signaled mres2!");
      Console.WriteLine("main thread: mres3.IsSet = {0} (should be false)", mres3.IsSet);

      // It's good form to Dispose() a ManualResetEventSlim when you're done with it
      observer.Wait(); // make sure that this has fully completed
      mres1.Dispose();
      mres2.Dispose();
      mres3.Dispose();
    }

    // Demonstrates:
    //      ManualResetEventSlim construction w/ SpinCount
    //      ManualResetEventSlim.WaitHandle
    static void MRES_SpinCountWaitHandle()
    {
      // Construct a ManualResetEventSlim with a SpinCount of 1000
      // Higher spincount => longer time the MRES will spin-wait before taking lock
      ManualResetEventSlim mres1 = new ManualResetEventSlim(false, 1000);
      ManualResetEventSlim mres2 = new ManualResetEventSlim(false, 1000);

      Task bgTask = Task.Factory.StartNew(() =>
      {
        // Just wait a little
        Thread.Sleep(100);

        // Now signal both MRESes
        Console.WriteLine("Task signalling both MRESes");
        mres1.Set();
        mres2.Set();
      });

      // A common use of MRES.WaitHandle is to use MRES as a participant in 
      // WaitHandle.WaitAll/WaitAny.  Note that accessing MRES.WaitHandle will
      // result in the unconditional inflation of the underlying ManualResetEvent.
      WaitHandle.WaitAll(new WaitHandle[] { mres1.WaitHandle, mres2.WaitHandle });
      Console.WriteLine("WaitHandle.WaitAll(mres1.WaitHandle, mres2.WaitHandle) completed.");

      // Clean up
      bgTask.Wait();
      mres1.Dispose();
      mres2.Dispose();
    }

### 方法说明： ###

> public ManualResetEventSlim();
> 
> public ManualResetEventSlim(bool initialState);
> 
> public ManualResetEventSlim(bool initialState, int spinCount);

	实际都是调用了

	private void Initialize(bool initialState, int spinCount)//initialState默认false spinCount默认10
	{
	    this.m_combinedState = initialState ? -2147483648 : 0;
	    this.SpinCount = PlatformHelper.IsSingleProcessor ? 1 : spinCount;
	}

> Wait(int millisecondsTimeout, CancellationToken cancellationToken)


	[__DynamicallyInvokable]
	public bool Wait(int millisecondsTimeout, CancellationToken cancellationToken)
	{
	    this.ThrowIfDisposed();
	    cancellationToken.ThrowIfCancellationRequested();
	    if (millisecondsTimeout < -1)
	    {
	        throw new ArgumentOutOfRangeException("millisecondsTimeout");
	    }
	    if (!this.IsSet)
	    {
	        if (millisecondsTimeout == 0)
	        {
	            return false;
	        }
	        uint startTime = 0;
	        bool flag = false;
	        int num2 = millisecondsTimeout;
	        if (millisecondsTimeout != -1)
	        {
	            startTime = TimeoutHelper.GetTime();
	            flag = true;
	        }
	        int num3 = 10;
	        int num4 = 5;
	        int num5 = 20;
	        int spinCount = this.SpinCount;
	        for (int i = 0; i < spinCount; i++)
	        {
	            if (this.IsSet)
	            {
	                return true;
	            }
	            if (i < num3)
	            {
	                if (i == (num3 / 2))
	                {
						//导致调用线程执行准备好在当前处理器上运行的另一个线程。 由操作系统选择要执行的线程。
	                    Thread.Yield();
	                }
	                else
	                {
						//导致线程等待由 iterations 参数定义的时间量。
	                    Thread.SpinWait(((int) 4) << i);
	                }
	            }
	            else if ((i % num5) == 0)
	            {
	                Thread.Sleep(1);
	            }
	            else if ((i % num4) == 0)
	            {
	                Thread.Sleep(0);//?????
	            }
	            else
	            {
	                Thread.Yield();
	            }
	            if ((i >= 100) && ((i % 10) == 0))//没10次检测一下是否取消
	            {
	                cancellationToken.ThrowIfCancellationRequested();
	            }
	        }
	        this.EnsureLockObjectCreated();
	        using (cancellationToken.InternalRegisterWithoutEC(s_cancellationTokenCallback, this))
	        {
	            object @lock = this.m_lock;
	            lock (@lock)
	            {
	                while (!this.IsSet)
	                {
	                    cancellationToken.ThrowIfCancellationRequested();
	                    if (flag)
	                    {
	                        num2 = TimeoutHelper.UpdateTimeOut(startTime, millisecondsTimeout);
	                        if (num2 <= 0)
	                        {
	                            return false;
	                        }
	                    }
	                    this.Waiters++;
	                    if (this.IsSet)
	                    {
	                        int waiters = this.Waiters;
	                        this.Waiters = waiters - 1;
	                        return true;
	                    }
	                    try
	                    {
	                        if (!Monitor.Wait(this.m_lock, num2)) ✨🤣🤣😢😢✨ 看来关键还是Wait...
	                        {
	                            return false;
	                        }
	                        continue;
	                    }
	                    finally
	                    {
	                        this.Waiters--;
	                    }
	                }
	            }
	        }
	    }
	    return true;
	}

除了封装以外，主要还是调用了wait轮询

> private void Set(bool duringCancellation)

	private void Set(bool duringCancellation)
	{
	    this.IsSet = true;
	    if (this.Waiters > 0)
	    {
	        object @lock = this.m_lock;
	        lock (@lock)
	        {
	            Monitor.PulseAll(this.m_lock);
	        }
	    }
	    ManualResetEvent eventObj = this.m_eventObj;
	    if ((eventObj != null) && !duringCancellation)
	    {
	        ManualResetEvent event3 = eventObj;
	        lock (event3)
	        {
	            if (this.m_eventObj != null)
	            {
	                this.m_eventObj.Set();
	            }
	        }
	    }
	}

😢大胆推测，所有的混合锁，实际都是基于对Wait和Pulse的封装。
🤣的确如此 

轮询 spin


----------

source

[https://www.cnblogs.com/anding/p/5301754.html#undefined](https://www.cnblogs.com/anding/p/5301754.html#undefined)
