## wait源码分析 ##


> clr/src/vm/ecall.cpp


    FCFuncElement("Exit", JIT_MonExit)
    FCFuncElement("TryEnterTimeout", JIT_MonTryEnter)
    FCFuncElement("ObjWait", ObjectNative::WaitTimeout)

映射到ObjectNative的方法

> clr/src/vm/comobject.cpp  -- cpp存储实现

	FCIMPL3(FC_BOOL_RET, ObjectNative::WaitTimeout, CLR_BOOL exitContext, INT32 Timeout, Object* pThisUNSAFE)
	{
	    CONTRACTL
	    {
	        MODE_COOPERATIVE;
	        DISABLED(GC_TRIGGERS);  // can't use this in an FCALL because we're in forbid gc mode until we setup a H_M_F.
	        SO_TOLERANT;
	        THROWS;
	    }
	    CONTRACTL_END;
	
	    BOOL retVal = FALSE;
	    OBJECTREF pThis = (OBJECTREF) pThisUNSAFE;
	    HELPER_METHOD_FRAME_BEGIN_RET_1(pThis);
	    //-[autocvtpro]-------------------------------------------------------
	
	    if (pThis == NULL)
	        COMPlusThrow(kNullReferenceException, L"NullReference_This");
	
	    if ((Timeout < 0) && (Timeout != INFINITE_TIMEOUT))
	        COMPlusThrowArgumentOutOfRange(L"millisecondsTimeout", L"ArgumentOutOfRange_NeedNonNegNum");
	
	    retVal = pThis->Wait(Timeout, exitContext);
	
	    //-[autocvtepi]-------------------------------------------------------
	    HELPER_METHOD_FRAME_END();
	    FC_RETURN_BOOL(retVal);
	}
	FCIMPLEND

现在我们看到函数体中最终调用的是pThis->Wait，pThis是个啥玩意呢，通过分析代码，发现它就是WaitTimeOut函数的最后一个参数Object* pThisUNSAFE的一个引用，原来是一个Object类型，那这里的Object和c#的object或者.Net的Object有啥关系，大胆猜想，这其实就是托管Object对应的native Object。而事实也应如此。

那麽废话不多说，我们要来看看此Object的Wait实现，依然避免不了搜索一番，首先我们在object.h中找到了Object类的定义，摘取其说明如下，也印证了刚才的猜想：

	/*
	 * Object
	 *
	 * This is the underlying base on which objects are built.   The MethodTable
	 * 这是构建对象的基础。的方法表
	 * 每个对象都要维护自己的方法表
	 * 
 	 * pointer and the sync block index live here.  The sync block index is actually
 	 * 指针和同步块索引在这里。同步块索引实际上是 
	 * at a negative offset to the instance.  See syncblk.h for details.
	 * *在实例的负偏移量处。详见syncbl .h。
	 *
	 */

查看wait方法：

	BOOL Wait(INT32 timeOut, BOOL exitContext) 
	{ 
	    WRAPPER_CONTRACT; 
	    return GetHeader()->Wait(timeOut, exitContext); 
	}

哦，原来是先调用了GetHeader方法获取对象头，然后调用对象头的Wait方法，追下去，GetHeader方法的实现：

	// Sync Block & Synchronization services
	
	// Access the ObjHeader which is at a negative offset on the object (because of 
	// cache lines) 
	ObjHeader   *GetHeader() 
	{ 
	    LEAF_CONTRACT; 
	    return PTR_ObjHeader(PTR_HOST_TO_TADDR(this) - sizeof(ObjHeader)); 
	}

 

看来要想往下追，还必须看对象头ObjHeader类的Wait方法实现：在syncblk.h中找到了其定义，在对应的cpp文件中找到了其相应的实现如下：

	BOOL ObjHeader::Wait(INT32 timeOut, BOOL exitContext) 
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
	
	    //  The following code may cause GC, so we must fetch the sync block from 
	    //  the object now in case it moves. 
	    SyncBlock *pSB = GetBaseObject()->GetSyncBlock();
	
	    // GetSyncBlock throws on failure 
	    _ASSERTE(pSB != NULL);
	
	    // make sure we own the crst 
	    if (!pSB->DoesCurrentThreadOwnMonitor()) 
	        COMPlusThrow(kSynchronizationLockException);
	
	#ifdef _DEBUG 
	    Thread *pThread = GetThread(); 
	    DWORD curLockCount = pThread->m_dwLockCount; 
	#endif
	
	    BOOL result = pSB->Wait(timeOut,exitContext);
	
	    _ASSERTE (curLockCount == pThread->m_dwLockCount);
	
	    return result; 
	}

 

看到了嘛！！！！该Wait实现最重要的两行代码终于浮现出来了，它们就是加横线的两行。

第一行    SyncBlock *pSB = GetBaseObject()->GetSyncBlock(); 用来获取对象的索引块；

第二行    BOOL result = pSB->Wait(timeOut,exitContext); 嗯，越来越接近真相，原来又调用了索引块对象的Wait方法。

那继续吧，看看SyncBlock 类型的Wait方法实现，依旧在syncblk.cpp中，如下：

	// We maintain two queues for SyncBlock::Wait. 
	// 1. Inside SyncBlock we queue all threads that are waiting on the SyncBlock. 
	//    When we pulse, we pick the thread from this queue using FIFO. 
	// 2. We queue all SyncBlocks that a thread is waiting for in Thread::m_WaitEventLink. 
	//    When we pulse a thread, we find the event from this queue to set, and we also 
	//    or in a 1 bit in the syncblock value saved in the queue, so that we can return 
	//    immediately from SyncBlock::Wait if the syncblock has been pulsed. 
	BOOL SyncBlock::Wait(INT32 timeOut, BOOL exitContext) 
	{ 
	    CONTRACTL 
	    { 
	        INSTANCE_CHECK; 
	        THROWS; 
	        GC_TRIGGERS; 
	        MODE_ANY; 
	        INJECT_FAULT(COMPlusThrowOM()); 
	    } 
	    CONTRACTL_END;
	
	    Thread  *pCurThread = GetThread(); 
	    BOOL     isTimedOut = FALSE; 
	    BOOL     isEnqueued = FALSE; 
	    WaitEventLink waitEventLink; 
	    WaitEventLink *pWaitEventLink;
	
	    // As soon as we flip the switch, we are in a race with the GC, which could clean 
	    // up the SyncBlock underneath us -- unless we report the object. 
	    _ASSERTE(pCurThread->PreemptiveGCDisabled());
	
	    // Does this thread already wait for this SyncBlock? 
	    WaitEventLink *walk = pCurThread->WaitEventLinkForSyncBlock(this); ✨🤔😀😎✨
	    if (walk->m_Next) { 
	        if (walk->m_Next->m_WaitSB == this) { 
	            // Wait on the same lock again. 
	            walk->m_Next->m_RefCount ++; 
	            pWaitEventLink = walk->m_Next; 
	        } 
	        else if ((SyncBlock*)(((DWORD_PTR)walk->m_Next->m_WaitSB) & ~1)== this) { 
	            // This thread has been pulsed.  No need to wait. 
	            return TRUE; 
	        } 
	    } 
	    else { 
	        // First time this thread is going to wait for this SyncBlock. ✨🤔😀😎✨
	        CLREvent* hEvent; 
	        if (pCurThread->m_WaitEventLink.m_Next == NULL) { 
	            hEvent = &(pCurThread->m_EventWait); ✨🤔😀😎✨
	        } 
	        else { 
	            hEvent = GetEventFromEventStore(); ✨🤔😀😎✨
	        } 
	        waitEventLink.m_WaitSB = this; 
	        waitEventLink.m_EventWait = hEvent; 
	        waitEventLink.m_Thread = pCurThread; 
	        waitEventLink.m_Next = NULL; 
	        waitEventLink.m_LinkSB.m_pNext = NULL; 
	        waitEventLink.m_RefCount = 1; 
	        pWaitEventLink = &waitEventLink; 
	        walk->m_Next = pWaitEventLink;
	
	        // Before we enqueue it (and, thus, before it can be dequeued), reset the event 
	        // that will awaken us. 
	        hEvent->Reset();
	
	        // This thread is now waiting on this sync block 
	        ThreadQueue::EnqueueThread(pWaitEventLink, this);✨🤔😀😎✨
	
	        isEnqueued = TRUE; 
	    }
	
	    _ASSERTE ((SyncBlock*)((DWORD_PTR)walk->m_Next->m_WaitSB & ~1)== this);
	
	    PendingSync   syncState(walk);
	
	    OBJECTREF     obj = m_Monitor.GetOwningObject();
	
	    m_Monitor.IncrementTransientPrecious();
	
	    GCPROTECT_BEGIN(obj); 
	    { 
	        GCX_PREEMP();
	
	        // remember how many times we synchronized 
	        syncState.m_EnterCount = LeaveMonitorCompletely(); 
	        _ASSERTE(syncState.m_EnterCount > 0);
	
	        Context* targetContext = pCurThread->GetContext(); 
	        _ASSERTE(targetContext); 
	        Context* defaultContext = pCurThread->GetDomain()->GetDefaultContext(); 
	        _ASSERTE(defaultContext);
	
	        if (exitContext && 
	            targetContext != defaultContext) 
	        { 
	            Context::MonitorWaitArgs waitArgs = {timeOut, &syncState, &isTimedOut}; 
	            Context::CallBackInfo callBackInfo = {Context::MonitorWait_callback, (void*) &waitArgs}; 
	            Context::RequestCallBack(CURRENT_APPDOMAIN_ID, defaultContext, &callBackInfo); 
	        } 
	        else 
	        { 
	            isTimedOut = pCurThread->Block(timeOut, &syncState); ✨🤔😀😎✨
	        } 
	    } 
	    GCPROTECT_END(); 
	    m_Monitor.DecrementTransientPrecious();
	
	    return !isTimedOut; 
	}

 

拜托，当你看到函数又臭又长的时候..尤其时还不熟悉的时候，一定要看函数的描述，该函数开头之前的函数说明解释了两件事情：

1.在SyncBlock 内部维护了一个等待所有这个SyncBlock 的线程队列，当调用pulse的时候(如Monitor.Pulse)会从该队列取出下一个线程，方式是先进先出。

2.使用另外一个队列维护所有有线程正在waiting的SyncBlock ,队列类型为WaitEventLink(也即是Thread::m_WaitEventLink的类型），一旦有pulse调用，会从该队列取出一个Event并set.


现在再来看函数代码部分，重点看横线的代码行：

    WaitEventLink *walk = pCurThread->WaitEventLinkForSyncBlock(this); 
先检查当前线程是否已经在等待对象的同步索引块，本示例中显然是第一次，然后通过

hEvent = &(pCurThread->m_EventWait);或者 
hEvent = GetEventFromEventStore();获取一个等待事件对象 
之后会走  ThreadQueue::EnqueueThread(pWaitEventLink, this);

从而把当前线程加入到等待队列，这时候我的脑海中又想起来MSDN上对Monitor.Wait的描述：

当线程调用 Wait 时，它释放对象的锁并进入对象的等待队列。 对象的就绪队列中的下一个线程（如果有）获取锁并拥有对对象的独占使用。

这下大概能对上号了吧。

在函数最后，还是调用了isTimedOut = pCurThread->Block(timeOut, &syncState);以实现实现当前线程的等待（或曰阻塞）。

所以依旧要看看这个Block方法的实现：

	// Called out of SyncBlock::Wait() to block this thread until the Notify occurs. 
	BOOL Thread::Block(INT32 timeOut, PendingSync *syncState) 
	{ 
	    WRAPPER_CONTRACT;
	
	    _ASSERTE(this == GetThread());
	
	    // Before calling Block, the SyncBlock queued us onto it's list of waiting threads. 
	    // However, before calling Block the SyncBlock temporarily left the synchronized 
	    // region.  This allowed threads to enter the region and call Notify, in which 
	    // case we may have been signalled before we entered the Wait.  So we aren't in the 
	    // m_WaitSB list any longer.  Not a problem: the following Wait will return 
	    // immediately.  But it means we cannot enforce the following assertion: 
	//    _ASSERTE(m_WaitSB != NULL);
	
	    return (Wait(syncState->m_WaitEventLink->m_Next->m_EventWait, timeOut, syncState) != WAIT_OBJECT_0); 
	}
 

Block又调用了Thread的Wait方法：

	// Return whether or not a timeout occured.  TRUE=>we waited successfully 
	DWORD Thread::Wait(CLREvent *pEvent, INT32 timeOut, PendingSync *syncInfo) 
	{ 
	    WRAPPER_CONTRACT;
	
	    DWORD   dwResult; 
	    DWORD   dwTimeOut32;
	
	    _ASSERTE(timeOut >= 0 || timeOut == INFINITE_TIMEOUT);
	
	    dwTimeOut32 = (timeOut == INFINITE_TIMEOUT 
	                   ? INFINITE 
	                   : (DWORD) timeOut);
	
	    dwResult = pEvent->Wait(dwTimeOut32, TRUE /*alertable*/, syncInfo);✨🤔😀😎✨
	
	    // Either we succeeded in the wait, or we timed out 
	    _ASSERTE((dwResult == WAIT_OBJECT_0) || 
	             (dwResult == WAIT_TIMEOUT));
	
	    return dwResult; 
	}

 

Wait又调用了pEvent的Wait方法，注意这里的pEvent是CLREvent类型，而该参数的值则是之前在SyncBlock::Wait获取的等待事件对象。这里我们可以大胆猜测CLREvent对应的其实是一个内核事件对象。

CLREvent的Wait实现如下，有点长，看关键的横线代码行：

	DWORD CLREvent::Wait(DWORD dwMilliseconds, BOOL alertable, PendingSync *syncState) 
	{ 
	    WRAPPER_CONTRACT; 
	    return WaitEx(dwMilliseconds, alertable?WaitMode_Alertable:WaitMode_None,syncState); 
	}

 

紧接着WaitEx的实现如下：

	DWORD CLREvent::WaitEx(DWORD dwMilliseconds, WaitMode mode, PendingSync *syncState) 
	{ 
	    BOOL alertable = (mode & WaitMode_Alertable)!=0; 
	    CONTRACTL 
	    { 
	        if (alertable) 
	        { 
	            THROWS;               // Thread::DoAppropriateWait can throw   
	        } 
	        else 
	        { 
	            NOTHROW; 
	        } 
	        if (GetThread()) 
	        { 
	            if (alertable) 
	                GC_TRIGGERS; 
	            else 
	                GC_NOTRIGGER; 
	        } 
	        else 
	        { 
	            DISABLED(GC_TRIGGERS);        
	        } 
	        SO_TOLERANT; 
	        PRECONDITION(m_handle != INVALID_HANDLE_VALUE); // Handle has to be valid 
	    } 
	    CONTRACTL_END;
	
	    _ASSERTE(Thread::AllowCallout());
	
	    Thread *pThread = GetThread();    
	#ifdef _DEBUG 
	    // If a CLREvent is OS event only, we can not wait for the event on a managed thread 
	    if (IsOSEvent()) 
	        _ASSERTE (!pThread); 
	#endif 
	    _ASSERTE (pThread || !g_fEEStarted || dbgOnly_IsSpecialEEThread());
	
	    if (IsOSEvent() || !CLRSyncHosted()) { 
	        if (pThread && alertable) { 
	            DWORD dwRet = WAIT_FAILED; 
	            BEGIN_SO_INTOLERANT_CODE_NOTHROW (pThread, return WAIT_FAILED;); 
	            dwRet = pThread->DoAppropriateWait(1, &m_handle, FALSE, dwMilliseconds, ✨🤔😀😎✨
	                                              mode, ✨🤔😀😎✨
	                                              syncState); ✨🤔😀😎✨
	            END_SO_INTOLERANT_CODE; 
	            return dwRet; 
	        } 
	        else { 
	            _ASSERTE (syncState == NULL); 
	            return CLREventWaitHelper(m_handle,dwMilliseconds,alertable); 
	        } 
	    } 
	    else {    
	       if (pThread && alertable) { 
	            DWORD dwRet = WAIT_FAILED; 
	            BEGIN_SO_INTOLERANT_CODE_NOTHROW (pThread, return WAIT_FAILED;); 
	            dwRet = pThread->DoAppropriateWait(IsAutoEvent()?HostAutoEventWait:HostManualEventWait, ✨🤔😀😎✨
	                                              m_handle,dwMilliseconds, ✨🤔😀😎✨
	                                              mode, ✨🤔😀😎✨
	                                              syncState); ✨🤔😀😎✨
	            END_SO_INTOLERANT_CODE; 
	            return dwRet; 
	        } 
	        else { 
	            _ASSERTE (syncState == NULL); 
	            DWORD option = 0; 
	            if (alertable) { 
	                option |= WAIT_ALERTABLE; 
	            } 
	            if (IsAutoEvent()) { 
	                return HostAutoEventWait((IHostAutoEvent*)m_handle,dwMilliseconds, option); 
	            } 
	            else { 
	                return HostManualEventWait((IHostManualEvent*)m_handle,dwMilliseconds, option); 
	            } 
	        } 
	    }    
	}


这里又调用了Thread的DoAppropriateWait； 
DoAppropriateWait的实现如下：

	DWORD Thread::DoAppropriateWait(int countHandles, HANDLE *handles, BOOL waitAll, 
	                                DWORD millis, WaitMode mode, PendingSync *syncState) 
	{ 
	    STATIC_CONTRACT_THROWS; 
	    STATIC_CONTRACT_GC_TRIGGERS;
	
	    INDEBUG(BOOL alertable = (mode & WaitMode_Alertable) != 0;); 
	    _ASSERTE(alertable || syncState == 0);
	
	    DWORD dwRet = (DWORD) -1;
	
	    EE_TRY_FOR_FINALLY { 
	        dwRet =DoAppropriateWaitWorker(countHandles, handles, waitAll, millis, mode); ✨🤔😀😎✨
	    } 
	    EE_FINALLY { 
	        if (syncState) { 
	            if (!GOT_EXCEPTION() && 
	                dwRet >= WAIT_OBJECT_0 && dwRet < (DWORD)(WAIT_OBJECT_0 + countHandles)) { 
	                // This thread has been removed from syncblk waiting list by the signalling thread 
	                syncState->Restore(FALSE); 
	            } 
	            else 
	                syncState->Restore(TRUE); 
	        }
	
	        _ASSERTE (dwRet != WAIT_IO_COMPLETION); 
	    } 
	    EE_END_FINALLY;
	
	    return(dwRet); 
	}

then，DoAppropriateWaitWorker的实现如下，有点长，只看最关键那一句：

	DWORD Thread::DoAppropriateWaitWorker(int countHandles, HANDLE *handles, BOOL waitAll, 
	                                      DWORD millis, WaitMode mode) 
	{ 
	    CONTRACTL { 
	        THROWS; 
	        GC_TRIGGERS; 
	    } 
	    CONTRACTL_END;
	
	    DWORD ret = 0;
	
	    BOOL alertable = (mode & WaitMode_Alertable)!= 0; 
	    BOOL ignoreSyncCtx = (mode & WaitMode_IgnoreSyncCtx)!= 0;
	
	    // Unless the ignoreSyncCtx flag is set, first check to see if there is a synchronization 
	    // context on the current thread and if there is, dispatch to it to do the wait. 
	    // If  the wait is non alertable we cannot forward the call to the sync context 
	    // since fundamental parts of the system (such as the GC) rely on non alertable 
	    // waits not running any managed code. Also if we are past the point in shutdown were we 
	    // are allowed to run managed code then we can't forward the call to the sync context. 
	    if (!ignoreSyncCtx && alertable && CanRunManagedCode(FALSE)) 
	    { 
	        GCX_COOP();
	
	        BOOL fSyncCtxPresent = FALSE; 
	        OBJECTREF SyncCtxObj = NULL; 
	        GCPROTECT_BEGIN(SyncCtxObj) 
	        { 
	            GetSynchronizationContext(&SyncCtxObj); 
	            if (SyncCtxObj != NULL) 
	            { 
	                SYNCHRONIZATIONCONTEXTREF syncRef = (SYNCHRONIZATIONCONTEXTREF)SyncCtxObj; 
	                if (syncRef->IsWaitNotificationRequired()) 
	                { 
	                    fSyncCtxPresent = TRUE; 
	                    ret = DoSyncContextWait(&SyncCtxObj, countHandles, handles, waitAll, millis); 
	                } 
	            } 
	        } 
	        GCPROTECT_END();
	
	        if (fSyncCtxPresent) 
	            return ret; 
	    }
	
	    GCX_PREEMP();
	
	    if(alertable) 
	    { 
	        DoAppropriateWaitWorkerAlertableHelper(mode); 
	    }
	
	    LeaveRuntimeHolder holder((size_t)WaitForMultipleObjectsEx); 
	    StateHolder<MarkOSAlertableWait,UnMarkOSAlertableWait> OSAlertableWait(alertable);
	
	    ThreadStateHolder tsh(alertable, TS_Interruptible | TS_Interrupted);
	
	    ULONGLONG dwStart = 0, dwEnd; 
	retry: 
	    if (millis != INFINITE) 
	    { 
	        dwStart = CLRGetTickCount64(); 
	    }
	
	    ret = DoAppropriateAptStateWait(countHandles, handles, waitAll, millis, mode);✨🤔😀😎✨
	
	    if (ret == WAIT_IO_COMPLETION) 
	    { 
	        _ASSERTE (alertable);
	
	        if (m_State & TS_Interrupted) 
	        { 
	            HandleThreadInterrupt(mode & WaitMode_ADUnload); 
	        } 
	        // We could be woken by some spurious APC or an EE APC queued to 
	        // interrupt us. In the latter case the TS_Interrupted bit will be set 
	        // in the thread state bits. Otherwise we just go back to sleep again. 
	        if (millis != INFINITE) 
	        { 
	            dwEnd = CLRGetTickCount64(); 
	            if (dwEnd >= dwStart + millis) 
	            { 
	                ret = WAIT_TIMEOUT; 
	                goto WaitCompleted; 
	            } 
	            else 
	            { 
	                millis -= (DWORD)(dwEnd - dwStart); 
	            } 
	        } 
	        goto retry; 
	    } 
	    _ASSERTE((ret >= WAIT_OBJECT_0  && ret < (WAIT_OBJECT_0  + (DWORD)countHandles)) || 
	             (ret >= WAIT_ABANDONED && ret < (WAIT_ABANDONED + (DWORD)countHandles)) || 
	             (ret == WAIT_TIMEOUT) || (ret == WAIT_FAILED)); 
	    // countHandles is used as an unsigned -- it should never be negative. 
	    _ASSERTE(countHandles >= 0);
	
	    if (ret == WAIT_FAILED) 
	    { 
	        DWORD errorCode = ::GetLastError(); 
	        if (errorCode == ERROR_INVALID_PARAMETER) 
	        { 
	            if (CheckForDuplicateHandles(countHandles, handles)) 
	                COMPlusThrow(kDuplicateWaitObjectException); 
	            else 
	                COMPlusThrowHR(HRESULT_FROM_WIN32(errorCode)); 
	        } 
	        else if (errorCode == ERROR_ACCESS_DENIED) 
	        { 
	            // A Win32 ACL could prevent us from waiting on the handle. 
	            COMPlusThrow(kUnauthorizedAccessException); 
	        }
	
	        _ASSERTE(errorCode == ERROR_INVALID_HANDLE);
	
	        if (countHandles == 1) 
	            ret = WAIT_OBJECT_0; 
	        else if (waitAll) 
	        { 
	            // Probe all handles with a timeout of zero. When we find one that's 
	            // invalid, move it out of the list and retry the wait. 
	#ifdef _DEBUG 
	            BOOL fFoundInvalid = FALSE; 
	#endif 
	            for (int i = 0; i < countHandles; i++) 
	            { 
	                // WaitForSingleObject won't pump memssage; we already probe enough space 
	                // before calling this function and we don't want to fail here, so we don't 
	                // do a transition to tolerant code here 
	                DWORD subRet = WaitForSingleObject (handles[i], 0); 
	                if (subRet != WAIT_FAILED) 
	                    continue; 
	                _ASSERTE(::GetLastError() == ERROR_INVALID_HANDLE); 
	                if ((countHandles - i - 1) > 0) 
	                    memmove(&handles[i], &handles[i+1], (countHandles - i - 1) * sizeof(HANDLE)); 
	                countHandles--; 
	#ifdef _DEBUG 
	                fFoundInvalid = TRUE; 
	#endif 
	                break; 
	            } 
	            _ASSERTE(fFoundInvalid);
	
	            // Compute the new timeout value by assume that the timeout 
	            // is not large enough for more than one wrap 
	            dwEnd = CLRGetTickCount64(); 
	            if (millis != INFINITE) 
	            { 
	                if (dwEnd >= dwStart + millis) 
	                { 
	                    ret = WAIT_TIMEOUT; 
	                    goto WaitCompleted; 
	                } 
	                else 
	                { 
	                    millis -= (DWORD)(dwEnd - dwStart); 
	                } 
	            } 
	            goto retry; 
	        } 
	        else 
	        { 
	            // Probe all handles with a timeout as zero, succeed with the first 
	            // handle that doesn't timeout. 
	            ret = WAIT_OBJECT_0; 
	            int i; 
	            for (i = 0; i < countHandles; i++) 
	            { 
	            TryAgain: 
	                // WaitForSingleObject won't pump memssage; we already probe enough space 
	                // before calling this function and we don't want to fail here, so we don't 
	                // do a transition to tolerant code here 
	                DWORD subRet = WaitForSingleObject (handles[i], 0); 
	                if ((subRet == WAIT_OBJECT_0) || (subRet == WAIT_FAILED)) 
	                    break; 
	                if (subRet == WAIT_ABANDONED) 
	                { 
	                    ret = (ret - WAIT_OBJECT_0) + WAIT_ABANDONED; 
	                    break; 
	                } 
	                // If we get alerted it just masks the real state of the current 
	                // handle, so retry the wait. 
	                if (subRet == WAIT_IO_COMPLETION) 
	                    goto TryAgain; 
	                _ASSERTE(subRet == WAIT_TIMEOUT); 
	                ret++; 
	            } 
	            _ASSERTE(i != countHandles); 
	        } 
	    }
	
	WaitCompleted:
	
	    _ASSERTE((ret != WAIT_TIMEOUT) || (millis != INFINITE));
	
	    return ret; 
	}

 

then， 还要看 DoAppropriateAptStateWait(countHandles, handles, waitAll, millis, mode)的实现：

	DWORD Thread::DoAppropriateAptStateWait(int numWaiters, HANDLE* pHandles, BOOL bWaitAll, 
	                                         DWORD timeout, WaitMode mode) 
	{ 
	    CONTRACTL { 
	        THROWS; 
	        GC_TRIGGERS; 
	    } 
	    CONTRACTL_END;
	
	    BOOL alertable = (mode&WaitMode_Alertable)!=0;
	
	    return WaitForMultipleObjectsEx_SO_TOLERANT(numWaiters, pHandles,bWaitAll, timeout,alertable); 
	}

then，再看WaitForMultipleObjectsEx_SO_TOLERANT的实现：

	DWORD WaitForMultipleObjectsEx_SO_TOLERANT (DWORD nCount, HANDLE *lpHandles, BOOL bWaitAll,DWORD dwMilliseconds, BOOL bAlertable) 
	{ 
	    DWORD dwRet = WAIT_FAILED; 
	    DWORD lastError = 0; 
	    BEGIN_SO_TOLERANT_CODE (GetThread ()); 
	    dwRet = ::WaitForMultipleObjectsEx (nCount, lpHandles, bWaitAll, dwMilliseconds, bAlertable); 
	    lastError = ::GetLastError(); 
	    END_SO_TOLERANT_CODE;
	
	    // END_SO_TOLERANT_CODE overwrites lasterror.  Let's reset it. 
	    ::SetLastError(lastError); 
	    return dwRet; 
	}

 

到这里，万水千山，我们终于搞清楚Monitor.Wait的大概实现原理（事实上我们只捋了一遍本文示例中Monitor.Enter的调用stack），内部最终还是调用了WaitForMultipleObjectsEx，不过要注意CLREvent::WaitEx的实现有好几个分支，根据情况的不同，最后调的并不一定是WaitForMultipleObjectsEx，也有可能是CLREventWaitHelper->WaitForSingleObjectEx等等。


----------
> 转载

再来加深一下印象，每一个Object实例都维护一个SyncBlock并通过这个玩意来进行线程的同步,所以Monitor.Wait最终走到这个BOOL SyncBlock::Wait(INT32 timeOut, BOOL exitContext)并不足奇。在SyncBlock内部我们维护了一个所有正在等待此同步索引块的线程的队列，那具体是通过什麽来控制的呢，通过阅读SyncBlock::Wait源码，我们知道SyncBlock内部的这个维护链表就是SLink       m_Link;

	// We thread two different lists through this link.  When the SyncBlock is 
	// active, we create a list of waiting threads here.  When the SyncBlock is 
	// released (we recycle them), the SyncBlockCache maintains a free list of 
	// SyncBlocks here. 
	// 
	// We can't afford to use an SList<> here because we only want to burn 
	// space for the minimum, which is the pointer within an SLink. 
	SLink       m_Link;

 

在SyncBlock::Wait中通过调用ThreadQueue::EnqueueThread把当前线程的WaitEventLink加入到SyncBlock的m_Link之中：

	// Enqueue is the slow one.  We have to find the end of the Q since we don't 
	// want to burn storage for this in the SyncBlock. 
	/* static */ 
	inline void ThreadQueue::EnqueueThread(WaitEventLink *pWaitEventLink, SyncBlock *psb) 
	{ 
	    LEAF_CONTRACT; 
	    COUNTER_ONLY(GetPrivatePerfCounters().m_LocksAndThreads.cQueueLength++);
	
	    _ASSERTE (pWaitEventLink->m_LinkSB.m_pNext == NULL);
	
	    SyncBlockCache::LockHolder lh(SyncBlockCache::GetSyncBlockCache());
	
	    SLink       *pPrior = &psb->m_Link;
	
	    while (pPrior->m_pNext) 
	    { 
	        // We shouldn't already be in the waiting list! 
	        _ASSERTE(pPrior->m_pNext != &pWaitEventLink->m_LinkSB);
	
	        pPrior = pPrior->m_pNext; 
	    } 
	    pPrior->m_pNext = &pWaitEventLink->m_LinkSB; 
	}

 

通过分析Thread的结构，我们知道Thread的两个私有字段：

	// For Object::Wait, Notify and NotifyAll, we use an Event inside the 
	// thread and we queue the threads onto the SyncBlock of the object they 
	// are waiting for. 
	CLREvent        m_EventWait; 
	WaitEventLink   m_WaitEventLink;

 

WaitEventLink是一个struct用来管理线程等待的事件，而CLREvent        m_EventWait显然就是当前用来阻塞线程或者线程用来等待的事件对象:
	
	// Used inside Thread class to chain all events that a thread is waiting for by Object::Wait 
	struct WaitEventLink { 
	    SyncBlock      *m_WaitSB; 
	    CLREvent       *m_EventWait; 
	    Thread         *m_Thread;       // Owner of this WaitEventLink. 
	    WaitEventLink  *m_Next;         // Chain to the next waited SyncBlock. 
	    SLink           m_LinkSB;       // Chain to the next thread waiting on the same SyncBlock. 
	    DWORD           m_RefCount;     // How many times Object::Wait is called on the same SyncBlock. 
	};

 

再返回到BOOL SyncBlock::Wait(INT32 timeOut, BOOL exitContext)

我们看到刚开始就需要检查是否已经有线程在等待本SyncBlock，方法就是：

	// Does this thread already wait for this SyncBlock? 
	   WaitEventLink *walk = pCurThread->WaitEventLinkForSyncBlock(this);

 

若果已经有了，引用数加1：

	// Wait on the same lock again. 
	walk->m_Next->m_RefCount ++;

 

如没有，则属于第一次，需要先创建一个事件对象CLREvent，创建过程：

	// First time this thread is going to wait for this SyncBlock. 
	       CLREvent* hEvent; 
	       if (pCurThread->m_WaitEventLink.m_Next == NULL) { 
	           hEvent = &(pCurThread->m_EventWait); 
	       } 
	       else { 
	           hEvent = GetEventFromEventStore(); 
	       }

 

 

而这个事件对最后真正用来WaitForMultipleObjects的那个句柄至关重要。为什麽这麽说，我们继续看SyncBlock::Wait最后调用了pCurThread->Block(timeOut, &syncState);

	// Called out of SyncBlock::Wait() to block this thread until the Notify occurs. 
	BOOL Thread::Block(INT32 timeOut, PendingSync *syncState) 
	{ 
	    WRAPPER_CONTRACT;
	
	    _ASSERTE(this == GetThread());
	
	    // Before calling Block, the SyncBlock queued us onto it's list of waiting threads. 
	    // However, before calling Block the SyncBlock temporarily left the synchronized 
	    // region.  This allowed threads to enter the region and call Notify, in which 
	    // case we may have been signalled before we entered the Wait.  So we aren't in the 
	    // m_WaitSB list any longer.  Not a problem: the following Wait will return 
	    // immediately.  But it means we cannot enforce the following assertion: 
	//    _ASSERTE(m_WaitSB != NULL);
	
	    return (Wait(syncState->m_WaitEventLink->m_Next->m_EventWait, timeOut, syncState) != WAIT_OBJECT_0); 
	}

 

这时候又紧接着调用了Wait(syncState->m_WaitEventLink->m_Next->m_EventWait, timeOut, syncState)，第一个参数明显就是刚才的CLREvent,

	// Return whether or not a timeout occured.  TRUE=>we waited successfully 
	DWORD Thread::Wait(CLREvent *pEvent, INT32 timeOut, PendingSync *syncInfo) 
	{ 
	    WRAPPER_CONTRACT;
	
	    DWORD   dwResult; 
	    DWORD   dwTimeOut32;
	
	    _ASSERTE(timeOut >= 0 || timeOut == INFINITE_TIMEOUT);
	
	    dwTimeOut32 = (timeOut == INFINITE_TIMEOUT 
	                   ? INFINITE 
	                   : (DWORD) timeOut);
	
	    dwResult = pEvent->Wait(dwTimeOut32, TRUE /*alertable*/, syncInfo);
	
	    // Either we succeeded in the wait, or we timed out 
	    _ASSERTE((dwResult == WAIT_OBJECT_0) || 
	             (dwResult == WAIT_TIMEOUT));
	
	    return dwResult; 
	}

 

而最后真正的Wait还是发生在CLREvent内部，看看它的Wait：

	DWORD CLREvent::Wait(DWORD dwMilliseconds, BOOL alertable, PendingSync *syncState) 
	{ 
	    WRAPPER_CONTRACT; 
	    return WaitEx(dwMilliseconds, alertable?WaitMode_Alertable:WaitMode_None,syncState); 
	}

 

再往下看就和之前的重复了，但是这里我们要着重的地方是CLREvent的私有字段

HANDLE m_handle;

其实你会发现这才是最后调用WaitForMupltipleObjectEx函数需要的那个句柄对象，而它就封装在CLREvent之中，这里的Handle就代表一个内核事件对象，

那麽那麽！这里的WaitForMupltipleObjectEx在什麽情况下返回呢？对的，需要事件对象的Set之后才能返回，ok，现在再让我们回忆一下Monitor.Wait在什麽

时候返回，没错，就是需要在其它的线程中调用Monitor.Pulse之后才能返回，这个Pulse名字起得很形象。由此，我们自然能推断出Pulse最后其实只不过是Event.Set,现在让我们看看Pulse：

	void SyncBlock::Pulse() 
	{ 
	    CONTRACTL 
	    { 
	        INSTANCE_CHECK; 
	        NOTHROW; 
	        GC_NOTRIGGER; 
	        MODE_ANY; 
	    } 
	    CONTRACTL_END;
	
	    WaitEventLink  *pWaitEventLink;
	
	    if ((pWaitEventLink = ThreadQueue::DequeueThread(this)) != NULL) 
	        pWaitEventLink->m_EventWait->Set(); 
	}

看到这段代码，我们再对照Monitor.Pulse的描述：从队列中取到排在最前面的线程，这里其实等价于取到那个线程的Event事件对象并Set之，由此一来，正在WaitForMupltipeObjects这个事件的线程将获得释放，对于有多个线程等待同一个Event的情况，究竟是哪个线程会被释放，还应该取决于线程的优先级等属性，但是anyway，这样的调度过程已经交给操作系统定夺了。

同理PulseAll：

	void SyncBlock::PulseAll() 
	{ 
	    CONTRACTL 
	    { 
	        INSTANCE_CHECK; 
	        NOTHROW; 
	        GC_NOTRIGGER; 
	        MODE_ANY; 
	    } 
	    CONTRACTL_END;
	
	    WaitEventLink  *pWaitEventLink;
	
	    while ((pWaitEventLink = ThreadQueue::DequeueThread(this)) != NULL) 
	        pWaitEventLink->m_EventWait->Set(); 
	}


----------
> 转载

现在我们再回到最初的示例上来，ThreadProc1和ThreadProc2之间通过lock关键字进行同步，加在在这两个线程上的lock就好比两扇大门，而这两扇门同时只允许打开一扇。我们先在第一个线程中打开了第一扇门，那第二个线程就要在第二扇门外徘徊。而要打开第二扇门就应该等待第一扇门的Monitor.Exit，Exit的调用就好比是关上当前的门，通知另外的门可以打开了。

但是现在似乎出了点”意外“。

但是现在第一扇门打开之后，突然蹦出个Monitor.Wait,这玩意是个人物，它除了让第一个线程处于阻塞状态，还通知第二扇门可以打开了。这也就是说：并不需要等到第一扇门调用Monitor.Exit,第二扇门就可以打开了。

这一切究竟是怎麽发生的？带着种种疑惑，我们慢慢来拨开云雾见青天。

还需要从BOOL SyncBlock::Wait(INT32 timeOut, BOOL exitContext)开头，

该函数在真正的Block当前线程也即是调用isTimedOut = pCurThread->Block(timeOut, &syncState)之前，有一行代码值得研究一番：

syncState.m_EnterCount = LeaveMonitorCompletely();

单看这行代码所调用的函数名称，直译成：彻底离开Monitor，听起来和Monitor.Exit有点异曲同工之妙。

再来看看其实现：

	LONG LeaveMonitorCompletely() 
	{ 
	    WRAPPER_CONTRACT; 
	    return m_Monitor.LeaveCompletely(); 
	}

 

嗯，又调用了

m_Monitor.LeaveCompletely(); 
这个m_Monitor在SyncBlock类中的定义：

	protected: 
	   AwareLock  m_Monitor;                    // the actual monitor
	
	 

注释说这是实际的Monitor，所以我们应该能猜出这就是Monitor.Enter/Exit所涉及的类（事实上也是如此，因为我很快看到了Monitor.Enter对应的实现就是AwareLock.Enter），是一个AwareLock 的变量。

Ok，我们再来看AwareLock 的LeaveCompletely实现：

	LONG AwareLock::LeaveCompletely() 
	{ 
	    WRAPPER_CONTRACT;
	
	    LONG count = 0; 
	    while (Leave()) { 
	        count++; 
	    } 
	    _ASSERTE(count > 0);            // otherwise we were never in the lock
	
	    return count; 
	}

 

再看Leave：

	BOOL AwareLock::Leave() 
	{ 
	    CONTRACTL 
	    { 
	        INSTANCE_CHECK; 
	        NOTHROW; 
	        GC_NOTRIGGER; 
	        MODE_ANY; 
	    } 
	    CONTRACTL_END;
	
	    Thread* pThread = GetThread();
	
	    AwareLock::LeaveHelperAction action = LeaveHelper(pThread);
	
	    switch(action) 
	    { 
	    case AwareLock::LeaveHelperAction_None: 
	        // We are done 
	        return TRUE; 
	    case AwareLock::LeaveHelperAction_Signal: 
	        // Signal the event 
	        Signal(); 
	        return TRUE; 
	    default: 
	        // Must be an error otherwise 
	        _ASSERTE(action == AwareLock::LeaveHelperAction_Error); 
	        return FALSE; 
	    } 
	}

 

由此可以看出所谓彻底离开不过就是遍历+Signal();那麽这个Signal函数究竟做了啥，看名字和注释知其一二：Signal the event

	void    Signal() 
	{ 
	    WRAPPER_CONTRACT; 
	    // CLREvent::SetMonitorEvent works even if the event has not been intialized yet 
	    m_SemEvent.SetMonitorEvent(); 
	}

现在问题又来了，m_SemEvent是啥？首先，定义：

	CLREvent        m_SemEvent;

是个CLREvent，然后看看其初始化，是在void AwareLock::AllocLockSemEvent()中：

	m_SemEvent.CreateMonitorEvent((SIZE_T)this);

啊哈，只看名字就知道这一个Monitor专用的Event，那麽AllocLockSemEvent又被谁调用呢，是BOOL AwareLock::EnterEpilog(Thread* pCurThread, INT32 timeOut)，而EnterEpilog又为AwareLock::Enter所调用，事实上当EnterEpilog就是第二扇门的徘回函数。我们来看看怎麽徘徊的：

	for (;;) 
	       { 
	           // We might be interrupted during the wait (Thread.Interrupt), so we need an 
	           // exception handler round the call. 
	           EE_TRY_FOR_FINALLY 
	           { 
	               // Measure the time we wait so that, in the case where we wake up 
	               // and fail to acquire the mutex, we can adjust remaining timeout 
	               // accordingly. 
	               start = CLRGetTickCount64(); 
	              ret = m_SemEvent.Wait(timeOut, TRUE); 
	               _ASSERTE((ret == WAIT_OBJECT_0) || (ret == WAIT_TIMEOUT)); 
	               if (timeOut != (INT32) INFINITE) 
	               { 
	                   end = CLRGetTickCount64(); 
	                   if (end == start) 
	                   { 
	                       duration = 1; 
	                   } 
	                   else 
	                   { 
	                       duration = end - start; 
	                   } 
	                   duration = min(duration, (DWORD)timeOut); 
	                   timeOut -= (INT32)duration; 
	               } 
	           }

要注意关键行

	ret = m_SemEvent.Wait(timeOut, TRUE); 下文还会讲到。这明显是在等待事件对象的信号有状态。
 

再来看看SetMonitorEvent的实现:

	void CLREvent::SetMonitorEvent() 
	{ 
	    CONTRACTL 
	    { 
	        NOTHROW; 
	        GC_NOTRIGGER; 
	    } 
	    CONTRACTL_END;
	
	    // SetMonitorEvent is robust against initialization races. It is possible to 
	    // call CLREvent::SetMonitorEvent on event that has not been initialialized yet by CreateMonitorEvent. 
	    // CreateMonitorEvent will signal the event once it is created if it happens.
	
	    for (;;) 
	    { 
	        LONG oldFlags = m_dwFlags;
	
	        if (oldFlags & CLREVENT_FLAGS_MONITOREVENT_ALLOCATED) 
	        { 
	            // Event has been allocated already. Use the regular codepath. 
	            Set(); 
	            break; 
	        }
	
	        LONG newFlags = oldFlags | CLREVENT_FLAGS_MONITOREVENT_SIGNALLED; 
	        if (FastInterlockCompareExchange((LONG*)&m_dwFlags, newFlags, oldFlags) != oldFlags) 
	        { 
	            // We lost the race 
	            continue; 
	        } 
	        break; 
	    } 
	}

又调用了Set函数：

	BOOL CLREvent::Set() 
	{ 
	    CONTRACTL 
	    { 
	      NOTHROW; 
	      GC_NOTRIGGER; 
	      PRECONDITION((m_handle != INVALID_HANDLE_VALUE)); 
	    } 
	    CONTRACTL_END;
	
	    _ASSERTE(Thread::AllowCallout());
	
	    if (IsOSEvent() || !CLRSyncHosted()) { 
	        return UnsafeSetEvent(m_handle); 
	    } 
	    else { 
	        if (IsAutoEvent()) { 
	            HRESULT hr; 
	            BEGIN_SO_TOLERANT_CODE_CALLING_HOST(GetThread()); 
	            hr = ((IHostAutoEvent*)m_handle)->Set(); 
	            END_SO_TOLERANT_CODE_CALLING_HOST; 
	            return hr == S_OK; 
	        } 
	        else { 
	            HRESULT hr; 
	            BEGIN_SO_TOLERANT_CODE_CALLING_HOST(GetThread()); 
	            hr = ((IHostManualEvent*)m_handle)->Set(); 
	            END_SO_TOLERANT_CODE_CALLING_HOST; 
	            return hr == S_OK; 
	        } 
	    } 
	}

 

在Set函数中我们看到最终是对m_handle的Set。从而使得事件状态被置成有信号状态，也即释放了所有的lock而使得它们重新处于被调度状态。

现在再回过头来看看AwareLock::EnterEpilog的逻辑，已经知道是通过ret = m_SemEvent.Wait(timeOut, TRUE)等待事件对象的信号状态，而我麽也已经知道在调用Monitor.Wait之后会调用事件对象的Set函数从而使得等待的线程得到锁。那麽为了加深印象，我还想通过Windbg走走。

----------

source

[https://www.cnblogs.com/dancewithautomation/archive/2012/03/25/2416260.html](https://www.cnblogs.com/dancewithautomation/archive/2012/03/25/2416260.html)