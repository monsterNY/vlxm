
在涉及线程操作时，经常会出现两个参数：

1. 超时时间
2. CancellationToken

第一个比较容易理解，第二个见名思意应该是一个取消标记，既然是取消标记，那么直接一个bool或是什么值就好了

为什么会需要一个CancellationToken？

使用CancellationToken会有什么好处？

CancellationToken - 结构体，字段:

	private CancellationTokenSource m_source;

	public bool CanBeCanceled
	{
		get
		{
			return this.m_source != null && this.m_source.CanBeCanceled;
		}
	}

	public bool IsCancellationRequested
	{
		get
		{
			return this.m_source != null && this.m_source.IsCancellationRequested;
		}
	}	

	public WaitHandle WaitHandle
	{
		get
		{
			if (this.m_source == null)
				this.InitializeDefaultSource();
			return this.m_source.WaitHandle;
		}
	}

获取CancellationToken的方式：

1. CancellationToken.None

	查看定义：

		public static CancellationToken None
		{
			get
			{
				return default(CancellationToken);
			}
		}

	我们都知道int默认为0，引用类型默认为null，由此可知default(结构体)即结构体中所有字段都为默认值

2. 构造方法

		public CancellationToken(bool canceled)
		{
			this = default(CancellationToken);
			if (canceled)
			{
				this.m_source = CancellationTokenSource.InternalGetStaticSource(canceled);
			}
		}

	
	this = default(CancellationToken);
 
	注：仅限结构体使用，由于结构体构造函数必须初始化所有字段，但有时我们不需要初始化所有字段，就可以使用此种方式进行初始化

	查看CancellationTokenSource.InternalGetStaticSource:

		internal static CancellationTokenSource InternalGetStaticSource(bool set)
		{
			if (!set)
			{
				return CancellationTokenSource._staticSource_NotCancelable;
			}
			return CancellationTokenSource._staticSource_Set;
		}

	由前面逻辑可知set为true调用，所以接着查看CancellationTokenSource._staticSource_Set

		private static readonly CancellationTokenSource _staticSource_Set = new CancellationTokenSource(true);

3. 通过 CancellationTokenSource 构造

		CancellationTokenSource source = new CancellationTokenSource();
		CancellationToken token = source.Token;

	查看Token方法定义：

		public CancellationToken Token
		{
			get
			{
				this.ThrowIfDisposed();
				return new CancellationToken(this);
			}
		}

	查看CancellationToken的构造定义：

		internal CancellationToken(CancellationTokenSource source)
		{
			this.m_source = source;
		}

通过分析可以看出CancellationTokenSource(class)和CancellationToken的之间的关联关系。

再回到之前，我们CancellationToken一般是使用它的ThrowIfCancellationRequested方法

查看定义：

	public void ThrowIfCancellationRequested()
	{
		if (this.IsCancellationRequested)
		{
			this.ThrowOperationCanceledException();
		}
	}

由此可见其具体通过IsCancellationRequested属性进行判断，再查看IsCancellationRequested定义：

	public bool IsCancellationRequested
	{
		get
		{
			return this.m_source != null && this.m_source.IsCancellationRequested;
		}
	}

m_source 即CancellationTokenSource,接着查看CancellationTokenSource的IsCancellationRequested

	public bool IsCancellationRequested
	{
		get
		{
			return this.m_state >= 2;
		}
	}

再查看m_state的定义：

	private volatile int m_state;

> 首先为什么使用volatile修饰？

保证线程可见性。避免状态改变了却无法取消，通过这里可以回答为什么要使用CancellToken，

因为传递int/bool这样的类型时，需要保证：
	
	1. 参数必须使用ref修饰，否则修改无效
	2. 若是取消操作是在另外一个操作执行时，此变量需要考虑参数传递，或是将参数定义在外部
	3. 若是取消操作在另外一个线程执行，此参数就需要定义在外部且使用volatile保证可见性

由此可见使用int/bool的复杂性

🤯 猜测：为了处理这些问题，CancellToken便产生了



> 既然有了CancellToken为什么还需要CancellationTokenSource呢？

CancellationToken是一个结构体，我们都知道结构体的值无法改变，所以肯定需要使用类来辅助

接着查看CancellationTokenSource的Cancel定义：

	public void Cancel()
	{
		this.Cancel(false);
	}

	public void Cancel(bool throwOnFirstException)
	{
		this.ThrowIfDisposed();
		this.NotifyCancellation(throwOnFirstException);
	}

	private void NotifyCancellation(bool throwOnFirstException)
	{
		//验证是否已修改。
		if (this.IsCancellationRequested)
		{
			return;
		}
		if (Interlocked.CompareExchange(ref this.m_state, 2, 1) == 1)//CAS
		{
			Timer timer = this.m_timer;
			if (timer != null)
			{
				timer.Dispose();
			}
			this.ThreadIDExecutingCallbacks = Thread.CurrentThread.ManagedThreadId;
			if (this.m_kernelEvent != null)
			{
				this.m_kernelEvent.Set();
			}
			this.ExecuteCallbackHandlers(throwOnFirstException);
		}
	}

涉及的操作：

1. CAS乐观锁 保证只取消一次
2. Timer 

	由public CancellationTokenSource(TimeSpan delay)可知，m_timer是用于定义取消，类似于延时器。
3. m_kernelEvent 

		private volatile ManualResetEvent m_kernelEvent;
	
	ManualResetEvent 表示线程同步事件，收到信号时，必须手动重置该事件。 此类不能被继承。

	用于解除当前线程的相关的Wait..

4. ExecuteCallbackHandlers

		private void ExecuteCallbackHandlers(bool throwOnFirstException)
		{
			List<Exception> list = null;
			SparselyPopulatedArray<CancellationCallbackInfo>[] registeredCallbacksLists = this.m_registeredCallbacksLists;
			if (registeredCallbacksLists == null)
			{
				Interlocked.Exchange(ref this.m_state, 3);
				return;
			}
			try
			{
				for (int i = 0; i < registeredCallbacksLists.Length; i++)
				{
					SparselyPopulatedArray<CancellationCallbackInfo> sparselyPopulatedArray = Volatile.Read<SparselyPopulatedArray<CancellationCallbackInfo>>(ref registeredCallbacksLists[i]);
					if (sparselyPopulatedArray != null)
					{
						for (SparselyPopulatedArrayFragment<CancellationCallbackInfo> sparselyPopulatedArrayFragment = sparselyPopulatedArray.Tail; sparselyPopulatedArrayFragment != null; sparselyPopulatedArrayFragment = sparselyPopulatedArrayFragment.Prev)
						{
							for (int j = sparselyPopulatedArrayFragment.Length - 1; j >= 0; j--)
							{
								this.m_executingCallback = sparselyPopulatedArrayFragment[j];
								if (this.m_executingCallback != null)
								{
									CancellationCallbackCoreWorkArguments cancellationCallbackCoreWorkArguments = new CancellationCallbackCoreWorkArguments(sparselyPopulatedArrayFragment, j);
									try
									{
										if (this.m_executingCallback.TargetSyncContext != null)
										{
											this.m_executingCallback.TargetSyncContext.Send(new SendOrPostCallback(this.CancellationCallbackCoreWork_OnSyncContext), cancellationCallbackCoreWorkArguments);
											this.ThreadIDExecutingCallbacks = Thread.CurrentThread.ManagedThreadId;
										}
										else
										{
											this.CancellationCallbackCoreWork(cancellationCallbackCoreWorkArguments);
										}
									}
									catch (Exception item)
									{
										if (throwOnFirstException)
										{
											throw;
										}
										if (list == null)
										{
											list = new List<Exception>();
										}
										list.Add(item);
									}
								}
							}
						}
					}
				}
			}
			finally
			{
				this.m_state = 3;
				this.m_executingCallback = null;
				Thread.MemoryBarrier();
			}
			if (list != null)
			{
				throw new AggregateException(list);
			}
		}

最终就是将m_state修改为3，其余的后续再继续追踪

over~

----------

since:5/27/2019 10:27:35 AM 

direction:CancellationToken