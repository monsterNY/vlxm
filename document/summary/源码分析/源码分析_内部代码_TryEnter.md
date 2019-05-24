
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
	     CONTRACTL {
	         SO_TOLERANT;
	         THROWS;
	         DISABLED(GC_TRIGGERS);      // currently disabled because of FORBIDGC in HCIMPL
	    } CONTRACTL_END;
	
	#if !defined(_X86_) && !defined(_AMD64_)
	    {
			//aware
			//adj. 意识到的；知道的；有…方面知识的；懂世故的
			//n. (Aware)人名；(阿拉伯、索)阿瓦雷
	        AwareLock* awareLock = NULL; 
	        SyncBlock* syncBlock = NULL;//同步索引块
	        ObjHeader* objHeader = NULL;//对象头 *:引用
	        LONG state,
			oldvalue;
	        DWORD tid;// DWORD 正体 : 四位元组 [电子计算机] 
	        int spincount = 50;// spin - 旋转 
	        const int MaxSpinCount = 20000 * g_SystemInfo.dwNumberOfProcessors;
	       
	        Thread *pThread = GetThread();
	
	        if (pThread->IsAbortRequested()) //为中止线程
	        {
	            goto FramedLockHelper;
	        }
	
	        if ((NULL == obj) || (timeOut < -1))//参数不正确
	        {
	            goto FramedLockHelper;
	        }
	
	        tid = pThread->GetThreadId();//获取线程id
	        objHeader = obj->GetHeader();//获取对象头
	
	        while (true)
	        {
				//获取同步索引块的值
				//从此次可以看出同步索引块的值影响着lock
	            oldvalue = objHeader->m_SyncBlockValue;
	            
	            if ((oldvalue & (BIT_SBLK_IS_HASH_OR_SYNCBLKINDEX + 
	                            BIT_SBLK_SPIN_LOCK + 
	                            SBLK_MASK_LOCK_THREADID + 
	                            SBLK_MASK_LOCK_RECLEVEL)) ==0)//经过计算结果若为0则表示即没有锁.
	            {       
	                
	                if (tid > SBLK_MASK_LOCK_THREADID)//超过SBLK掩码锁定
	                {
	                    goto FramedLockHelper;
	                }
	                
	                LONG newvalue = oldvalue | tid;
	                if (FastInterlockCompareExchangeAcquire((LONG*)&(objHeader->m_SyncBlockValue), newvalue, oldvalue) == oldvalue)//更新同步索引块 的值
	                {
	                    pThread->IncLockCount();//实际操作： m_dwLockCount ++;
	                    FC_RETURN_BOOL(TRUE);//直接返回
	                }
	                continue;
	            }
	
				//如果已存在值，且为hash或同步索引块下标。
				//😢这里应该说明了两个点
				//	1. 存在同步索引块表 [通过下标获取]
				//  2. 同步索引块可以作用于hashcode 与 lock锁
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
	                    FC_RETURN_BOOL(TRUE);
	                }
	            }
	            else
	            {
	                // lock is held by someone else
	                if (0 == timeOut)
	                {
	                    FC_RETURN_BOOL(FALSE);
	                }
	                else 
	                {
	                    goto FramedLockHelper;
	                }
	            }
	
	            // exponential backoff
	            for (int i = 0; i < spincount; i++)
	            {
	                YieldProcessor();//无操作
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
	            goto FramedLockHelper;
	        }
	
	        syncBlock = obj->PassiveGetSyncBlock();
	        if (NULL == syncBlock)
	        {
	            goto FramedLockHelper;
	        }
	        
	        awareLock = syncBlock->QuickGetMonitor(); ✨
	        state = awareLock->m_MonitorHeld; ✨ 
	        if (state == 0)
	        {
	            if (FastInterlockCompareExchangeAcquire((LONG*)&(awareLock->m_MonitorHeld), 1, 0) == 0)//进行CAS操作
	            {
	                syncBlock->SetAwareLock(pThread,1);✨
	                pThread->IncLockCount();
	                FC_RETURN_BOOL(TRUE);
	            }
	            else
	            {
	                goto FramedLockHelper;
	            }
	        }
	        else if (awareLock->GetOwningThread() == pThread) /* monitor is held, but it could be a recursive case */
	        {
	            awareLock->m_Recursion++;//循环+1
	            FC_RETURN_BOOL(TRUE);
	        }            
	FramedLockHelper: ;//😢参数检验并返回结果
	    }
	#endif // !_X86_ && !_AMD64_
	
	    BOOL result = FALSE;
	
	    OBJECTREF objRef = ObjectToOBJECTREF(obj);
	
	    // The following makes sure that Monitor.TryEnter shows up on thread
	    // abort stack walks (otherwise Monitor.TryEnter called within a CER can
	    // block a thread abort for long periods of time). Setting the __me internal
	    // variable (normally only set for fcalls) will cause the helper frame below
	    // to be able to backtranslate into the method desc for the Monitor.TryEnter
	    // fcall.
	    __me = GetEEFuncEntryPointMacro(JIT_MonTryEnter);
	
	    // Monitor helpers are used as both hcalls and fcalls, thus we need exact depth.
	    HELPER_METHOD_FRAME_BEGIN_RET_ATTRIB_1(Frame::FRAME_ATTR_EXACT_DEPTH, objRef);
	
	    if (objRef == NULL)
	        COMPlusThrow(kArgumentNullException);
	
	    if (timeOut < -1)
	        COMPlusThrow(kArgumentException);
	
	    result = objRef->TryEnterObjMonitor(timeOut);✨
	
	    HELPER_METHOD_FRAME_END();
	
	    FC_RETURN_BOOL(result);
	}
	HCIMPLEND



> 跟踪 awareLock = syncBlock->QuickGetMonitor(); ✨

> clr/src/vm/syncblk.h

	AwareLock* QuickGetMonitor()
    {
        LEAF_CONTRACT;
    // Note that the syncblock isn't marked precious, so use caution when
    // calling this method.
        return &m_Monitor;
    }

直接返回 &m_Monitor

这个m_Monitor在SyncBlock类中的定义：

	protected: 
	   AwareLock  m_Monitor;                    // the actual monitor

所以说 就是获取了一个AwareLock的对象


> state = awareLock->m_MonitorHeld; ✨
> 
> clr/src/vm/syncblk.h
>  
	
	public:
	    volatile LONG   m_MonitorHeld;
	    ULONG           m_Recursion;
	    PTR_Thread      m_HoldingThread;
	    
	  private:
	    LONG            m_TransientPrecious;
	
	
	    // This is a backpointer from the syncblock to the synctable entry.  This allows
	    // us to recover the object that holds the syncblock.
	    DWORD           m_dwSyncIndex;
	
	    CLREvent        m_SemEvent;
	
	    // Only SyncBlocks can create AwareLocks.  Hence this private constructor.
	    AwareLock(DWORD indx)
	        : m_MonitorHeld(0),
	          m_Recursion(0),
	#ifndef DACCESS_COMPILE          
	// PreFAST has trouble with intializing a NULL PTR_Thread.
	          m_HoldingThread(NULL),
	#endif // DACCESS_COMPILE          
	          m_TransientPrecious(0),
	          m_dwSyncIndex(indx)
	    {
	        LEAF_CONTRACT;
	    }

查看定义只有初始状态为0 所以 m_MonitorHeld应该是用来做CAS的相关变量

>  syncBlock->SetAwareLock(pThread,1);

> clr/src/vm/syncblk.h 查看方法定义：

	void SetAwareLock(Thread *holdingThread, DWORD recursionLevel)
    {
        LEAF_CONTRACT;
        // <NOTE>
        // DO NOT SET m_MonitorHeld HERE!  THIS IS NOT PROTECTED BY ANY LOCK!!
        // </NOTE>
        m_Monitor.m_HoldingThread = PTR_Thread(holdingThread);
        m_Monitor.m_Recursion = recursionLevel;
    }

从源码可以看出SetAwareLock就是给m_Monitor进行赋值，让m_Monitor的线程指向当前线程 且 循环次数为1

> awareLock->GetOwningThread()

😢应该就是获取m_Monitor的m_HoldingThread


> result = objRef->TryEnterObjMonitor(timeOut);

> clr/src/vm/object.h

> 查看Object的TryEnterObjMonitor定义：

 	BOOL TryEnterObjMonitor(INT32 timeOut = 0)
    {
        WRAPPER_CONTRACT;
        return GetHeader()->TryEnterObjMonitor(timeOut);
    }

调用了请求头的TryEnterObjMonitor

> clr/src/vm/syncblk.cpp

> 查看ObjHeader的TryEnterObjMonitor方法定义：

	BOOL ObjHeader::TryEnterObjMonitor(INT32 timeOut)
	{
	    WRAPPER_CONTRACT;
	    return GetSyncBlock()->TryEnterMonitor(timeOut);
	}

调用了同步索引块的TryEnterMonitor

> clr/src/vm/syncblk.h

	BOOL TryEnterMonitor(INT32 timeOut = 0)
    {TryEnter
        WRAPPER_CONTRACT;
        return m_Monitor.TryEnter(timeOut);
    }

之前已经知道了m_Monitor就是表示AwareLock

再到AwareLock的TryEnter：

	BOOL AwareLock::TryEnter(INT32 timeOut)
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
	
	    if (timeOut != 0)
	    {
	        LARGE_INTEGER qpFrequency, qpcStart, qpcEnd;
	        BOOL canUseHighRes = QueryPerformanceCounter(&qpcStart);
	
	        // try some more busy waiting
	        if (Contention(timeOut))
	            return TRUE;
	
	        DWORD elapsed = 0;
	        if (canUseHighRes && QueryPerformanceCounter(&qpcEnd) && QueryPerformanceFrequency(&qpFrequency))
	            elapsed = (DWORD)((qpcEnd.QuadPart-qpcStart.QuadPart)/(qpFrequency.QuadPart/1000));
	
	        if (elapsed >= (DWORD)timeOut)
	            return FALSE;
	
	        if (timeOut != (INT32)INFINITE)
	            timeOut -= elapsed;
	    }
	
	    Thread  *pCurThread = GetThread();
	    TESTHOOKCALL(AppDomainCanBeUnloaded(pCurThread->GetDomain()->GetId().m_dwId,FALSE));    
	
	    if (pCurThread->IsAbortRequested()) 
	    {
	        pCurThread->HandleThreadAbort();
	    }
	
	retry:
	    for (;;) {
	
	        // Read existing lock state.
	        LONG state = m_MonitorHeld;
	
	        if (state == 0) //初始无锁状态
	        {
	            // Common case: lock not held, no waiters. Attempt to acquire lock by
	            // switching lock bit.
	            if (FastInterlockCompareExchange((LONG*)&m_MonitorHeld, 1, 0) == 0)
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
	            if (m_HoldingThread == pCurThread)//当前线程为锁线程
	            {
	                goto Recursion;
	            }
	            else
	            {
	                goto WouldBlock;
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
	        int      caller = (pFrame && pFrame != FRAME_TOP ? (int) pFrame->GetReturnAddress() : -1);
	        pCurThread->m_pTrackSync->EnterSync(caller, this);
	    }
	#endif
	
	    return true;
	
	WouldBlock:
	    // Didn't manage to get the mutex, return failure if no timeout, else wait
	    // for at most timeout milliseconds for the mutex.
	    if (!timeOut)
	    {
	        return false;
	    }
	
	    // The precondition for EnterEpilog is that the count of waiters be bumped
	    // to account for this thread
	    for (;;)
	    {
	        // Read existing lock state.
	        volatile LONG state = m_MonitorHeld;
	        if (state == 0)
	        {
	            goto retry;
	        }
	        if (FastInterlockCompareExchange((LONG*)&m_MonitorHeld, (state + 2), state) == state)
	        {
	            break;
	        }
	    }
	    return EnterEpilog(pCurThread, timeOut);
	
	Recursion:
	    // Got the mutex via recursive locking on the same thread.
	    _ASSERTE(m_Recursion >= 1);
	    m_Recursion++;
	#if defined(_DEBUG) && defined(TRACK_SYNC)
	    // The best place to grab this is from the ECall frame
	    Frame   *pFrame = pCurThread->GetFrame();
	    int      caller = (pFrame && pFrame != FRAME_TOP ? (int) pFrame->GetReturnAddress() : -1);
	    pCurThread->m_pTrackSync->EnterSync(caller, this);
	#endif
	
	    return true;
	}


再回到ObjHeader的GetSyncBlock()

	//获取现有对象的同步块
	// get the sync block for an existing object
	SyncBlock *ObjHeader::GetSyncBlock()
	{
	    CONTRACT(SyncBlock *)
	    {
	        INSTANCE_CHECK;
	        THROWS;
	        GC_NOTRIGGER;
	        MODE_ANY;
	        INJECT_FAULT(COMPlusThrowOM(););
	        POSTCONDITION(CheckPointer(RETVAL));
	    }
	    CONTRACT_END;
	
	    SyncBlock* syncBlock = GetBaseObject()->PassiveGetSyncBlock(); ✨
	    DWORD      indx = 0;
	    BOOL indexHeld = FALSE;
	
	    if (syncBlock)
	    {
	        // Has our backpointer been correctly updated through every GC?
	        _ASSERTE(SyncTableEntry::GetSyncTableEntry()[GetHeaderSyncBlockIndex()].m_Object == GetBaseObject());
	        RETURN syncBlock;
	    }
	
		//需要从缓存中获取它
	    //Need to get it from the cache
	    {
	        SyncBlockCache::LockHolder lh(SyncBlockCache::GetSyncBlockCache());
	
	        //Try one more time
	        syncBlock = GetBaseObject()->PassiveGetSyncBlock();
	        if (syncBlock)
	            RETURN syncBlock;
	
	
	        SyncBlockMemoryHolder syncBlockMemoryHolder(SyncBlockCache::GetSyncBlockCache()->GetNextFreeSyncBlock());
	        syncBlock = syncBlockMemoryHolder;
	
	        if ((indx = GetHeaderSyncBlockIndex()) == 0)
	        {
	            indx = SyncBlockCache::GetSyncBlockCache()->NewSyncBlockSlot(GetBaseObject());
	        }
	        else
	        {
	            //We already have an index, we need to hold the syncblock
	            indexHeld = TRUE;
	        }
	
	        {
	            //! NewSyncBlockSlot has side-effects that we don't have backout for - thus, that must be the last
	            //! failable operation called.
	            CANNOTTHROWCOMPLUSEXCEPTION();
	            FAULT_FORBID();
	
	
	            syncBlockMemoryHolder.SuppressRelease();
	
	            new (syncBlock) SyncBlock(indx);
	
	            // after this point, nobody can update the index in the header to give an AD index
	            EnterSpinLock();
	
	            {
	                // If there's an appdomain index stored in the header, transfer it to the syncblock
	
	                ADIndex dwAppDomainIndex = GetAppDomainIndex();
	                if (dwAppDomainIndex.m_dwIndex)
	                    syncBlock->SetAppDomainIndex(dwAppDomainIndex);
	
	                // If the thin lock in the header is in use, transfer the information to the syncblock
	                DWORD bits = GetBits();
	                if ((bits & BIT_SBLK_IS_HASH_OR_SYNCBLKINDEX) == 0)
	                {
	                    DWORD lockThreadId = bits & SBLK_MASK_LOCK_THREADID;
	                    DWORD recursionLevel = (bits & SBLK_MASK_LOCK_RECLEVEL) >> SBLK_RECLEVEL_SHIFT;
	                    if (lockThreadId != 0 || recursionLevel != 0)
	                    {
	                        // recursionLevel can't be non-zero if thread id is 0
	                        _ASSERTE(lockThreadId != 0);
	
	                        Thread *pThread = g_pThinLockThreadIdDispenser->IdToThreadWithValidation(lockThreadId);
	
	                        if (pThread == NULL)
	                        {
	                            // The lock is orphaned.
	                            pThread = (Thread*) -1;
	                        }
	                        syncBlock->InitState();
	                        syncBlock->SetAwareLock(pThread, recursionLevel + 1);
	                    }
	                }
	                else if ((bits & BIT_SBLK_IS_HASHCODE) != 0)
	                {
	                    DWORD hashCode = bits & MASK_HASHCODE;
	
	                    syncBlock->SetHashCode(hashCode);
	                }
	            }
	
	            SyncTableEntry::GetSyncTableEntry() [indx].m_SyncBlock = syncBlock;
	
	            // in order to avoid a race where some thread tries to get the AD index and we've already nuked it,
	            // make sure the syncblock etc is all setup with the AD index prior to replacing the index
	            // in the header
	            if (GetHeaderSyncBlockIndex() == 0)
	            {
	                // We have transferred the AppDomain into the syncblock above.
	                SetIndex(BIT_SBLK_IS_HASH_OR_SYNCBLKINDEX | indx);
	            }
	
	            //If we had already an index, hold the syncblock
	            //for the lifetime of the object.
	            if (indexHeld)
	                syncBlock->SetPrecious();
	
	            ReleaseSpinLock();
	
	            // SyncBlockCache::LockHolder goes out of scope here
	        }
	    }
	
	    RETURN syncBlock;
	}

先看 SyncBlock* syncBlock = GetBaseObject()->PassiveGetSyncBlock();

> clr/src/vm/syncblk.h

	Object *GetBaseObject()
    {
        LEAF_CONTRACT;
        return (Object *) (this + 1);
    }

先返回了Object

继续查看PassiveGetSyncBlock

	//检索同步块，但不分配 
	 // retrieve sync block but don't allocate
	    SyncBlock *PassiveGetSyncBlock()
	    {
	#ifndef DACCESS_COMPILE
	        LEAF_CONTRACT;
	
	        return g_pSyncTable [GetHeaderSyncBlockIndex()].m_SyncBlock;
	#else
	        DacNotImpl();
	        return NULL;
	#endif // !DACCESS_COMPILE
	    }

g_pSyncTable 此处也证实了 同步索引块表的存在

同步索引块后续再来追踪...

----------
### confirm ###

	Every Object is preceded by an ObjHeader (at a negative offset).
	每个对象前面都有一个ObjHeader(负偏移量)。
	 The
	的
	 ObjHeader has an index to a SyncBlock.
	ObjHeader有一个指向同步块的索引。
	 This index is 0 for the bulk of all
	大多数情况下，这个指数是0
	 instances, which indicates that the object shares a dummy SyncBlock with
	实例，它指示对象与一个虚拟同步块共享一个同步块
	 most other objects.
	大多数其他对象。
	 The SyncBlock is primarily responsible for object synchronization.
	SyncBlock主要负责对象同步。
	 However,
	然而,
	 it is also a "kitchen sink" of sparsely allocated instance data.
	它也是一个由稀疏分配的实例数据组成的“厨房水槽”。
	 For instance,
	例如,
	 the default implementation of Hash() is based on the existence of a SyncTableEntry.
	Hash()的默认实现基于SyncTableEntry的存在。
	 And objects exposed to or from COM, or through context boundaries, can store sparse
	暴露于COM或来自COM或通过上下文边界的对象可以稀疏存储
	 data here.
	这里的数据。
	 SyncTableEntries and SyncBlocks are allocated in non-GC memory.
	同步表项和同步块分配在非gc内存中。
	 A weak pointer
	一个弱指针
	 from the SyncTableEntry to the instance is used to ensure that the SyncBlock and
	从SyncTableEntry到实例，用于确保SyncBlock和
	 SyncTableEntry are reclaimed (recycled) when the instance dies.
	SyncTableEntry在实例死后被回收(回收)。
	 The organization of the SyncBlocks isn't intuitive (at least to me).
	同步块的组织并不直观(至少对我来说是这样)。
	 Here's
	这是
	 the explanation:
	解释:
	 Before each Object is an ObjHeader.
	每个对象之前都有一个ObjHeader。
	 If the object has a SyncBlock, the
	如果对象有同步块，则
	 ObjHeader contains a non-0 index to it.
	ObjHeader包含一个非0索引。
	 The index is looked up in the g_pSyncTable of SyncTableEntries.
	索引在SyncTableEntries的g_pSyncTable中查找。
	 This means
	这意味着
	 the table is consecutive for all outstanding indices.
	该表连续列出所有未清偿的指数。
	 Whenever it needs to
	无论何时需要
	 grow, it doubles in size and copies all the original entries.
	增长，它的大小翻倍，复制所有原始条目。
	 The old table
	旧的表
	 is kept until GC time, when it can be safely discarded.
	保存到GC时间，在GC时间可以安全地丢弃它。
	 Each SyncTableEntry has a backpointer to the object and a forward pointer to
	每个SyncTableEntry都有一个指向该对象的反向指针和一个指向该对象的正向指针
	 the actual SyncBlock.
	实际的SyncBlock。
	 The SyncBlock is allocated out of a SyncBlockArray
	同步块是从同步块射线中分配的
	 which is essentially just a block of SyncBlocks.
	本质上就是一组同步块。
	 The SyncBlockArrays are managed by a SyncBlockCache that handles the actual
	SyncBlockArrays由一个SyncBlockCache管理，它处理实际的
	 allocations and frees of the blocks.
	分配和释放块。
	 So...
	所以…
	 Each allocation and release has to handle free lists in the table of entries
	每个分配和发布都必须处理条目表中的空闲列表
	 and the table of blocks.
	和积木桌。
	 We burn an extra 4 bytes for the pointer from the SyncTableEntry to the
	从SyncTableEntry到
	 SyncBlock.
	SyncBlock。
	 The reason for this is that many objects have a SyncTableEntry but no SyncBlock.
	原因是许多对象都有SyncTableEntry，但没有SyncBlock。
	 That's because someone (e.g. HashTable) called Hash() on them.
	这是因为有人(例如HashTable)对它们调用了Hash()。
	 Incidentally, there's a better write-up of all this stuff in the archives.
	顺便说一句，在档案馆里有一个更好的关于这些东西的记录。

----------


### 相关链接 ###

[https://github.com/SSCLI/sscli20_20060311](https://github.com/SSCLI/sscli20_20060311 "git地址")

[https://www.codeproject.com/Articles/184046/Spin-Lock-in-C](https://www.codeproject.com/Articles/184046/Spin-Lock-in-C "旋转锁")

[https://www.codeproject.com/Articles/18371/Fast-critical-sections-with-timeout](https://www.codeproject.com/Articles/18371/Fast-critical-sections-with-timeout "同步")

----------
author:monster

since:5/16/2019 2:12:32 PM 

direction:源码分析_内部代码_Enter-2