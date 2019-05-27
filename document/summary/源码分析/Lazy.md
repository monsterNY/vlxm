
### Lazy<T> ###

> 说明：

提供对延迟初始化的支持。

通过官方示例进行追踪：

	lazyLargeObject = new Lazy<LargeObject>(InitLargeObject);

	static LargeObject InitLargeObject()
    {
      LargeObject large = new LargeObject(Thread.CurrentThread.ManagedThreadId);
      return large;
    }

通过指定初始化方式构造lazy对象，查看相关的构造定义：

	public Lazy(Func<T> valueFactory) : this(valueFactory, LazyThreadSafetyMode.ExecutionAndPublication)
	{
	}

	public Lazy(Func<T> valueFactory, LazyThreadSafetyMode mode)
	{
		if (valueFactory == null)
		{
			throw new ArgumentNullException("valueFactory");
		}
		this.m_threadSafeObj = Lazy<T>.GetObjectFromMode(mode);
		this.m_valueFactory = valueFactory;
	}

可以看出构造中牵扯到了 LazyThreadSafetyMode，但我们就来简单的看一下 LazyThreadSafetyMode的定义：

| Name | Value | Desc |
| :------:| :------: | :------: |
| ExecutionAndPublication | 2 | 	使用锁来确保只有一个线程可以在线程安全的方式下初始化 Lazy<T> 实例。 如果初始化方法（如果没有初始化方法，则为默认构造函数）在内部使用锁，则可能会发生死锁。 如果使用指定初始化方法（valueFactory 参数）的 Lazy<T> 构造函数，并且如果此初始化方法在你首次调用 Value 属性时引发异常（或无法处理异常），则会缓存该异常并在随后调用 Value 属性时再次引发该异常。 如果你使用不指定初始化方法的 Lazy<T> 构造函数，则不会缓存 T 默认构造函数引发的异常。 在此情况下，对 Value 属性进行后续调用可成功初始化 Lazy<T> 实例。 如果初始化方法递归访问 Lazy<T> 实例的 Value 属性，则引发 InvalidOperationException。 |
|||
| None | 0 | 	Lazy<T> 实例不是线程安全的；如果从多个线程访问该实例，则其行为不确定。 仅应在高性能至关重要并且保证决不会从多个线程初始化 Lazy<T> 实例时才使用该模式。 如果使用指定初始化方法（valueFactory 参数）的 Lazy<T> 构造函数，并且如果此初始化方法在你首次调用 Value 属性时引发异常（或无法处理异常），则会缓存该异常并在随后调用 Value 属性时再次引发该异常。 如果你使用不指定初始化方法的 Lazy<T> 构造函数，则不会缓存 T 默认构造函数引发的异常。 在此情况下，对 Value 属性进行后续调用可成功初始化 Lazy<T> 实例。 如果初始化方法递归访问 Lazy<T> 实例的 Value 属性，则引发 InvalidOperationException。 |
|||
| PublicationOnly | 1 | 当多个线程尝试同时初始化一个 Lazy<T> 实例时，允许所有线程都运行初始化方法（如果没有初始化方法，则运行默认构造函数）。 完成初始化的第一个线程设置 Lazy<T> 实例的值。 该值将返回给同时运行初始化方法的其他所有线程，除非初始化方法对这些线程引发异常。 争用线程创建的任何 T 实例都将被放弃。 如果初始化方法对任何线程引发异常，则该异常会从该线程上的 Value 属性传播出去。 不缓存该异常。 IsValueCreated 属性的值仍然为 false，并且随后通过其中引发异常的线程或通过其他线程对 Value 属性的调用会导致初始化方法再次运行。 如果初始化方法递归访问 Lazy<T> 实例的 Value 属性，则不会引发异常。 |

所以LazyThreadSafetyMode是用于确定初始化的方式(是否使用锁，value值的设置方式)

回到主线

> this.m_threadSafeObj = Lazy<T>.GetObjectFromMode(mode);

查看定义：

	private static object GetObjectFromMode(LazyThreadSafetyMode mode)
	{
		if (mode == LazyThreadSafetyMode.ExecutionAndPublication)
		{
			return new object();
		}
		if (mode == LazyThreadSafetyMode.PublicationOnly)
		{
			return LazyHelpers.PUBLICATION_ONLY_SENTINEL;
		}
		if (mode != LazyThreadSafetyMode.None)
		{
			throw new ArgumentOutOfRangeException("mode", Environment.GetResourceString("Lazy_ctor_ModeInvalid"));
		}
		return null;
	}

	internal static readonly object PUBLICATION_ONLY_SENTINEL = new object();

这里就只是返回一个object ，那再来看我们Value的获取:

	public T Value
	{
		[__DynamicallyInvokable]
		get
		{
			if (this.m_boxed != null)
			{
				.....
			}
		
			return this.LazyInitValue();
		}
	}

继续查看 LazyInitValue：

	private T LazyInitValue()
	{
		Lazy<T>.Boxed boxed = null;
		LazyThreadSafetyMode mode = this.Mode;
		if (mode == LazyThreadSafetyMode.None)
		{
			boxed = this.CreateValue();✨
			this.m_boxed = boxed;
		}
		else if (mode == LazyThreadSafetyMode.PublicationOnly)
		{
			boxed = this.CreateValue();✨
			if (boxed == null || Interlocked.CompareExchange(ref this.m_boxed, boxed, null) != null)
			{
				boxed = (Lazy<T>.Boxed)this.m_boxed;
			}
			else
			{
				this.m_valueFactory = Lazy<T>.ALREADY_INVOKED_SENTINEL;
			}
		}
		else
		{
			object obj = Volatile.Read<object>(ref this.m_threadSafeObj);
			bool flag = false;
			try
			{
				if (obj != Lazy<T>.ALREADY_INVOKED_SENTINEL)
				{
					Monitor.Enter(obj, ref flag);
				}
				if (this.m_boxed == null)
				{
					boxed = this.CreateValue();
					this.m_boxed = boxed;
					Volatile.Write<object>(ref this.m_threadSafeObj, Lazy<T>.ALREADY_INVOKED_SENTINEL);
				}
				else
				{
					boxed = (this.m_boxed as Lazy<T>.Boxed);
					if (boxed == null)
					{
						Lazy<T>.LazyInternalExceptionHolder lazyInternalExceptionHolder = this.m_boxed as Lazy<T>.LazyInternalExceptionHolder;
						lazyInternalExceptionHolder.m_edi.Throw();
					}
				}
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(obj);
				}
			}
		}
		return boxed.m_value;
	}

查看CreateValue定义：

	private Lazy<T>.Boxed CreateValue()
	{
		Lazy<T>.Boxed result = null;
		LazyThreadSafetyMode mode = this.Mode;
		if (this.m_valueFactory != null)
		{
			try
			{
				if (mode != LazyThreadSafetyMode.PublicationOnly && this.m_valueFactory == Lazy<T>.ALREADY_INVOKED_SENTINEL)
				{
					throw new InvalidOperationException(Environment.GetResourceString("Lazy_Value_RecursiveCallsToValue"));
				}
				Func<T> valueFactory = this.m_valueFactory;
				if (mode != LazyThreadSafetyMode.PublicationOnly)
				{
					this.m_valueFactory = Lazy<T>.ALREADY_INVOKED_SENTINEL;
				}
				else if (valueFactory == Lazy<T>.ALREADY_INVOKED_SENTINEL)
				{
					return null;
				}
				result = new Lazy<T>.Boxed(valueFactory());
				return result;
			}
			catch (Exception ex)
			{
				if (mode != LazyThreadSafetyMode.PublicationOnly)
				{
					this.m_boxed = new Lazy<T>.LazyInternalExceptionHolder(ex);
				}
				throw;
			}
		}
		try
		{
			result = new Lazy<T>.Boxed((T)((object)Activator.CreateInstance(typeof(T))));
		}
		catch (MissingMethodException)
		{
			Exception ex2 = new MissingMemberException(Environment.GetResourceString("Lazy_CreateValue_NoParameterlessCtorForT"));
			if (mode != LazyThreadSafetyMode.PublicationOnly)
			{
				this.m_boxed = new Lazy<T>.LazyInternalExceptionHolder(ex2);✨
			}
			throw ex2;
		}
		return result;
	}

当不存在初始化方法时，调用

> result = new Lazy<T>.Boxed((T)((object)Activator.CreateInstance(typeof(T))));

即直接调用无参构造获取，

当构造异常时，直接抛出异常(LazyThreadSafetyMode.LazyThreadSafetyMode.PublicationOnly会保存异常信息)

当存在初始化方法时：

1. mode 为 LazyThreadSafetyMode.LazyThreadSafetyMode.PublicationOnly

		第一通过构造方法返回，并执行this.m_valueFactory = Lazy<T>.ALREADY_INVOKED_SENTINEL;

		无法触发第二次构造。

2. mode 为 LazyThreadSafetyMode.LazyThreadSafetyMode.None

		通过初始化方法构造，返回

3. mode 为 LazyThreadSafetyMode.LazyThreadSafetyMode.mode 为 LazyThreadSafetyMode.LazyThreadSafetyMode.PublicationOnly

		第一通过构造方法返回，并执行this.m_valueFactory = Lazy<T>.ALREADY_INVOKED_SENTINEL;

		无法触发第二次构造。
		
其中catch同上(直接抛出异常(LazyThreadSafetyMode.LazyThreadSafetyMode.PublicationOnly会保存异常信息))

看到这里，None 和 PublicationOnly应该就差不多了

ExecutionAndPublication 中使用  Monitor来确保线程安全

over

----------
since:5/24/2019 5:22:49 PM 

direction:Lazy