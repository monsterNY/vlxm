> source: Monitor

		
获取指定对象的独占锁。

	[MethodImpl(MethodImplOptions.InternalCall), SecuritySafeCritical, __DynamicallyInvokable]
	public static extern void Enter(object obj);

> src/vm/ecall.cpp

	FCFuncStart(gMonitorFuncs)
	    FCFuncElement("Enter", JIT_MonEnter)
	    FCFuncElement("Exit", JIT_MonExit)
	    FCFuncElement("TryEnterTimeout", JIT_MonTryEnter)
	    FCFuncElement("ObjWait", ObjectNative::WaitTimeout)
	    FCFuncElement("ObjPulse", ObjectNative::Pulse)
	    FCFuncElement("ObjPulseAll", ObjectNative::PulseAll)
	    FCFuncElement("ReliableEnter", JIT_MonReliableEnter)
	FCFuncEnd()

next -> JIT_MonEnter

> clr/src/vm/jithelpers.cpp

	HCIMPL2(FC_BOOL_RET, JIT_MonTryEnter_Portable, Object* obj, INT32 timeOut)
	{
	     .....
	}
	HCIMPLEND

相关参数 ：

	SyncBlock* syncBlock = NULL;//😢同步索引块
    ObjHeader* objHeader = NULL;//对象头
    int spincount = 50;//spin - 轮询 😢采用了轮询机制
    const int MaxSpinCount = 20000 * g_SystemInfo.dwNumberOfProcessors;
    LONG oldvalue, state;
    DWORD tid;

	objHeader = obj->GetHeader();//获取请求头

	OBJECTREF objRef = ObjectToOBJECTREF(obj);//获取请求头的引用
	
	objRef->EnterObjMonitor();//调用EnterObjMonitor
	

### 源码追踪 ###


> 1.GetHeader()方法获取对象头ObjHeader，在ObjHeader里有对EnterObjMonitor()方法的定义：

> clr/src/vm/object.cpp

	// 获取请求头
	// 访问对象上负偏移量的ObjHeader(因为高速缓存线路)
	// Access the ObjHeader which is at a negative offset on the object (because of
    // cache lines)
    ObjHeader   *GetHeader()
    {
        LEAF_CONTRACT;
        return PTR_ObjHeader(PTR_HOST_TO_TADDR(this) - sizeof(ObjHeader));
    }

> next->查看ObjHeader的EnterObjMonitor的定义

> clr/src/vm/syncblk.cpp

	void ObjHeader::EnterObjMonitor()
	{
	    WRAPPER_CONTRACT;
	    GetSyncBlock()->EnterMonitor();
	}
 
> 又调用了GetSyncBlock的EnterMonitor 追下去。

> clr/src/vm/syncblk.h

	void EnterMonitor()
    {
        WRAPPER_CONTRACT;
        m_Monitor.Enter();
    }

> 调用了m_Monitor的Enter 

> 查看m_Monitor的定义：

	protected:
    AwareLock  m_Monitor;                    // the actual monitor

> 继续 查看AwareLock的Enter方法定义，感觉已经越来越近了😢

	void AwareLock::Enter()
	{
	    CONTRACTL
	    {
	        INSTANCE_CHECK;
	        THROWS;
	        GC_TRIGGERS;
	        MODE_ANY;
	        INJECT_FAULT(COMPlusThrowOM(););
	    }
	    CONTRACTL_END;
	
	    Thread  *pCurThread = GetThread();
	
	    for (;;) 
	    {
	        // Read existing lock state.
	        volatile LONG state = m_MonitorHeld;
	
	        if (state == 0) 
	        {
	            // Common case: lock not held, no waiters. Attempt to acquire lock by
				//常见情况:锁没锁，没有服务员。试图获得锁定
	            // switching lock bit.
				//开关锁。
	            if (FastInterlockCompareExchange((LONG*)&m_MonitorHeld, 1, 0) == 0)//cas 修改值。
	            {
	                break;
	            }
	        } 
	        else 
	        {
	            // It's possible to get here with waiters but no lock held, but in this
	            // case a signal is about to be fired which will wake up a waiter. So
	            // for fairness sake we should wait too.
	            // Check first for recursive lock attempts on the same thread.
	            if (m_HoldingThread == pCurThread)//如果为当前线程
	            {    
	                goto Recursion;
	            }
	
	            // Attempt to increment this count of waiters then goto contention
	            // handling code.
	            if (FastInterlockCompareExchange((LONG*)&m_MonitorHeld, (state + 2), state) == state)
	            {
	                goto MustWait;
	            }
	        }
	
	    }
	
	    // We get here if we successfully acquired the mutex.
	    m_HoldingThread = pCurThread;
	    m_Recursion = 1;
	    pCurThread->IncLockCount();
	
	#if defined(_DEBUG) && defined(TRACK_SYNC)
	    {
	        // The best place to grab this is from the ECall frame
	        Frame   *pFrame = pCurThread->GetFrame();
	        int      caller = (pFrame && pFrame != FRAME_TOP
	                            ? (int) pFrame->GetReturnAddress()
	                            : -1);
	        pCurThread->m_pTrackSync->EnterSync(caller, this);
	    }
	#endif
	
	    return;
	
	MustWait:
	    // Didn't manage to get the mutex, must wait. //继续等待
	    EnterEpilog(pCurThread);
	    return;
	
	Recursion:
	    // Got the mutex via recursive locking on the same thread.
	    _ASSERTE(m_Recursion >= 1);
	    m_Recursion++;//递归次数加1
	#if defined(_DEBUG) && defined(TRACK_SYNC)
	    // The best place to grab this is from the ECall frame
	    Frame   *pFrame = pCurThread->GetFrame();
	    int      caller = (pFrame && pFrame != FRAME_TOP ? (int) pFrame->GetReturnAddress() : -1);
	    pCurThread->m_pTrackSync->EnterSync(caller, this);
	#endif
	}
	
从上面的代码我们可以看到，先使用GetThread()获取当前的线程，然后取出m_MonitorHeld字段，如果现在没有线程进入临界区，则设置该字段的状态，然后将m_HoldingThread设置为当前线程，从这一点上来这与Win32的过程应该是一样的。

如果从m_MonitorHeld字段看，有线程已经进入临界区则分两种情况：第一，是否已进入的线程如当前线程是同一个线程，如果是，则把m_Recursion递加，如果不是，则通过EnterEpilog( pCurThread)方法，当前线程进入线程等待队列。

通过上面的文字描述和代码的跟踪，在我们的大脑中应该有这样一张图了：

![转载](http://www.aspphp.online/bianchen/UploadFiles_4619/201701/2017010417463872.gif)

----------
### confirm ###


> clr/src/vm/syncblk.h
> 
> ObjHeader has an index to a SyncBlock.  This index is 0 for the bulk of all
> 
> ObjHeader有一个指向同步块的索引。大多数情况下，这个指数是0


----------


### 相关链接 ###

[https://github.com/SSCLI/sscli20_20060311](https://github.com/SSCLI/sscli20_20060311 "git地址")

[http://www.aspphp.online/bianchen/dnet/gydnet/201701/14624.html](http://www.aspphp.online/bianchen/dnet/gydnet/201701/14624.html "源码分析帮助篇")

----------
author:monster

since:5/16/2019 2:12:32 PM 

direction:源码分析_内部代码_Enter