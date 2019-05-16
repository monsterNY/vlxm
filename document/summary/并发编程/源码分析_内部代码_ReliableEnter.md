> source: Monitor

获取指定对象的独占锁定，并以原子方式设置一个值，该值指示是否已执行锁定。

	[MethodImpl(MethodImplOptions.InternalCall), SecuritySafeCritical]
	private static extern void ReliableEnter(object obj, ref bool lockTaken);

> clr/src/vm/ecall.cpp

	FCFuncStart(gMonitorFuncs)
	    FCFuncElement("Enter", JIT_MonEnter)
	    FCFuncElement("Exit", JIT_MonExit)
	    FCFuncElement("TryEnterTimeout", JIT_MonTryEnter)
	    FCFuncElement("ObjWait", ObjectNative::WaitTimeout)
	    FCFuncElement("ObjPulse", ObjectNative::Pulse)
	    FCFuncElement("ObjPulseAll", ObjectNative::PulseAll)
	    FCFuncElement("ReliableEnter", JIT_MonReliableEnter)
	FCFuncEnd()

> next -> JIT_MonReliableEnter

> src/vm/comobject.cpp

	FCIMPL2(void, JIT_MonReliableEnter, Object* pThisUNSAFE, CLR_BOOL *tookLock)
	{
	    CONTRACTL
	    {
	        MODE_COOPERATIVE;
	        DISABLED(GC_TRIGGERS);  // can't use this in an FCALL because we're in forbid gc mode until we setup a H_M_F.
	        THROWS;
	        SO_TOLERANT;
	    }
	    CONTRACTL_END;
	
	    OBJECTREF obj = (OBJECTREF) pThisUNSAFE;
	    HELPER_METHOD_FRAME_BEGIN_1(obj);
	    //-[autocvtpro]-------------------------------------------------------
	
	    if (obj == NULL)
	        COMPlusThrow(kNullReferenceException, L"NullReference_This");
	
	    GCPROTECT_BEGININTERIOR(tookLock);
	
	    class AwareLock *awareLock = NULL;//AwareLock 有点熟悉...
	    SyncBlock* syncBlock = NULL;//同步索引块
	    ObjHeader* objHeader = NULL;//对象头
	    int spincount = 50;//轮询 
	    const int MaxSpinCount = 20000 * g_SystemInfo.dwNumberOfProcessors;
	    LONG oldvalue, state;
	    DWORD tid;//参数感觉都一样
	
	    Thread *pThread = GetThread();
	
	    tid = pThread->GetThreadId();
	
	    if (tid > SBLK_MASK_LOCK_THREADID)
	    {
	        goto FramedLockHelper;
	    }
	
	    objHeader = obj->GetHeader();
	
	    while (true)
	    {
	        oldvalue = objHeader->m_SyncBlockValue;
	
	        if ((oldvalue & (BIT_SBLK_IS_HASH_OR_SYNCBLKINDEX + 
	                        BIT_SBLK_SPIN_LOCK + 
	                        SBLK_MASK_LOCK_THREADID + 
	                        SBLK_MASK_LOCK_RECLEVEL)) == 0)
	        {       
	
	            LONG newvalue = oldvalue | tid;
	            if (FastInterlockCompareExchangeAcquire((LONG*)&(objHeader->m_SyncBlockValue), newvalue, oldvalue) == oldvalue)
	            {
	                pThread->IncLockCount();
	                goto UpdateLockState;
	            }
	            continue;
	        }
	
	        if (oldvalue & BIT_SBLK_IS_HASH_OR_SYNCBLKINDEX)
	        {
	            goto HaveHashOrSyncBlockIndex;
	        }
	
	        if (oldvalue & BIT_SBLK_SPIN_LOCK)
	        {
	            if (1 == g_SystemInfo.dwNumberOfProcessors)
	            {
	                goto FramedLockHelper;
	            }
	        }
	        else if (tid == (DWORD) (oldvalue & SBLK_MASK_LOCK_THREADID))
	        {
	            LONG newvalue = oldvalue + SBLK_LOCK_RECLEVEL_INC;
	
	            if ((newvalue & SBLK_MASK_LOCK_RECLEVEL) == 0)
	            {
	                goto FramedLockHelper;
	            }
	
	            if (FastInterlockCompareExchangeAcquire((LONG*)&(objHeader->m_SyncBlockValue), newvalue, oldvalue) == oldvalue)
	            {
	                goto UpdateLockState;
	            }
	        }
	
	        // exponential backoff
	        for (int i = 0; i < spincount; i++)
	        {
	            YieldProcessor();
	        }
	        if (spincount > MaxSpinCount)
	        {
	            goto FramedLockHelper;
	        }
	        spincount *= 3;
	    } /* while(true) */
	
	HaveHashOrSyncBlockIndex:
	    if (oldvalue & BIT_SBLK_IS_HASHCODE)
	    {
	        goto FramedLockHelper;;
	    }
	
	    syncBlock = obj->PassiveGetSyncBlock();
	    if (NULL == syncBlock)
	    {
	        goto FramedLockHelper;;
	    }
	
	    awareLock = syncBlock->QuickGetMonitor();
	    state = awareLock->m_MonitorHeld;
	    if (state == 0)
	    {
	        if (FastInterlockCompareExchangeAcquire((LONG*)&(awareLock->m_MonitorHeld), 1, 0) == 0)
	        {
	            syncBlock->SetAwareLock(pThread,1);
	            pThread->IncLockCount();
	            goto UpdateLockState;
	        }
	        else
	        {
	            goto FramedLockHelper;;
	        }
	    }
	    else if (awareLock->GetOwningThread() == pThread) /* monitor is held, but it could be a recursive case */
	    {
	        awareLock->m_Recursion++;
	        goto UpdateLockState;
	    }
	FramedLockHelper:
	    obj->EnterObjMonitor();
	
	UpdateLockState:
	    tookLock != NULL ? *tookLock = true : false;
	
	    GCPROTECT_END();
	    //-[autocvtepi]-------------------------------------------------------
	    HELPER_METHOD_FRAME_END();
	}
	FCIMPLEND

----------


### 相关链接 ###

[https://github.com/SSCLI/sscli20_20060311](https://github.com/SSCLI/sscli20_20060311 "git地址")

[https://www.cnblogs.com/dancewithautomation/archive/2012/03/28/2421098.html](https://www.cnblogs.com/dancewithautomation/archive/2012/03/28/2421098.html "参考博文")

----------
author:monster

since:5/16/2019 4:06:56 PM 

direction:源码分析_内部代码_ReliableEnter