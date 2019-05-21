
# ThreadLocal #

#### 字段成员: ####

	private Func<T>? _valueFactory;

一个获取默认值的委托 不同线程共享此成员。

		
    [ThreadStatic]
    private static LinkedSlotVolatile[]? ts_slotArray;

ThreadStatic特性，这不就是我们熟悉的ThreadStaticAttribute吗，

🤯🤯🤯所以ThreadLocal 就是一个ThreadStatic的封装类，简化了tls操作

	[ThreadStatic]
    private static FinalizationHelper? ts_finalizationHelper;

见名思义，用于释放的帮助类

	private int _idComplement;

>  Slot ID of this ThreadLocal<instance.
>  
> 这个ThreadLocal<>实例的槽ID。
> 
> We store a bitwise complement of the ID (that is ~ID), which allows us to distinguish
> 
> 我们存储ID的位补码(即~ID)，这使我们能够区分
> 
> between the case when ID is 0 and an incompletely initialized object, either due to a thread abort in the constructor, or
>          
> 在ID为0的情况和未完全初始化的对象之间，原因可能是构造函数中的线程中止，也可能是
> 
> possibly due to a memory model issue in user code.
>          
> 可能是由于用户代码中的内存模型问题。

用于区分是否初始化。

	private volatile bool _initialized;

表示对象是否完全初始化..

	private volatile bool _initialized;

是否初始化-构造函数

	private static readonly IdManager s_idManager = new IdManager();

> IdManager assigns and reuses slot IDs.
> 
> IdManager分配和重用插槽id。
> 
> Additionally, the object is also used as a global lock.
> 
> 此外，该对象还用作全局锁。

	private LinkedSlot? _linkedSlot = new LinkedSlot(null);

伪头节点

	private bool _trackAllValues;

是否支持Values属性

#### 方法 ####

	private void Initialize(Func<T>? valueFactory, bool trackAllValues)
    {
        _valueFactory = valueFactory;
        _trackAllValues = trackAllValues;

        // Assign the ID and mark the instance as initialized. To avoid leaking IDs, we assign the ID and set _initialized
        // in a finally block, to avoid a thread abort in between the two statements.
        try { }
        finally
        {
            _idComplement = ~s_idManager.GetId();

            // As the last step, mark the instance as fully initialized. (Otherwise, if _initialized=false, we know that an exception
            // occurred in the constructor.)
            _initialized = true;
        }
    }

初始化方法，所有构造通过此方法初始化。

查看IdManager的GetId方法：

	internal int GetId()
	{
	    List<bool> freeIds = this.m_freeIds;
	    lock (freeIds)
	    {
	        int nextIdToTry = this.m_nextIdToTry;
	        while (nextIdToTry < this.m_freeIds.Count)
	        {
	            if (this.m_freeIds[nextIdToTry])
	            {
	                break;
	            }
	            nextIdToTry++;
	        }
	        if (nextIdToTry == this.m_freeIds.Count)
	        {
	            this.m_freeIds.Add(false);
	        }
	        else
	        {
	            this.m_freeIds[nextIdToTry] = false;
	        }
	        this.m_nextIdToTry = nextIdToTry + 1;
	        return nextIdToTry;
	    }
	}

具体就不说明了，类似于数据库中的自增标识

注：由于ThreadLocal为泛型类，仅当构造同类型的ThreadLocal才会触发自增

这里我们也可以知道为何需要一个LinkedSlotVolatile数组

当线程中存在多个ThreadLocal<int>即存在多个泛型类型相同的ThreadLocal,就需要使用数组进行存储，而_idComplement就是充当一个数组下标的功能

	public T Value
    {
        get
        {
            LinkedSlotVolatile[]? slotArray = ts_slotArray;
            LinkedSlot? slot;
            int id = ~_idComplement;

            //
            // Attempt to get the value using the fast path
            //
            if (slotArray != null   // Has the slot array been initialized?
                && id >= 0   // Is the ID non-negative (i.e., instance is not disposed)?
                && id < slotArray.Length   // Is the table large enough?
                && (slot = slotArray[id].Value) != null   // Has a LinkedSlot object has been allocated for this ID?
                && _initialized // Has the instance *still* not been disposed (important for a race condition with Dispose)?
            )
            {
                // We verified that the instance has not been disposed *after* we got a reference to the slot.
                // This guarantees that we have a reference to the right slot.
                // 
                // Volatile read of the LinkedSlotVolatile.Value property ensures that the m_initialized read
                // will not be reordered before the read of slotArray[id].
                return slot._value;
            }

            return GetValueSlow();
        }
        set
        {
            LinkedSlotVolatile[]? slotArray = ts_slotArray;
            LinkedSlot? slot;
            int id = ~_idComplement;

            // Attempt to set the value using the fast path
            if (slotArray != null   // Has the slot array been initialized?
                && id >= 0   // Is the ID non-negative (i.e., instance is not disposed)?
                && id < slotArray.Length   // Is the table large enough?
                && (slot = slotArray[id].Value) != null   // Has a LinkedSlot object has been allocated for this ID?
                && _initialized // Has the instance *still* not been disposed (important for a race condition with Dispose)?
                )
            {
                // We verified that the instance has not been disposed *after* we got a reference to the slot.
                // This guarantees that we have a reference to the right slot.
                // 
                // Volatile read of the LinkedSlotVolatile.Value property ensures that the m_initialized read
                // will not be reordered before the read of slotArray[id].
                slot._value = value;
            }
            else
            {
                SetValueSlow(value, slotArray);
            }
        }
    }

如果slotArray中有值就操作slotArray ，否则就

- 写-更新slotArray 

- 读-从_valueFactory 取值

到这里就差不多了,over~

----------

[https://github.com/dotnet/coreclr/blob/9773db1e7b1acb3ec75c9cc0e36bd62dcbacd6d5/src/System.Private.CoreLib/shared/System/Threading/ThreadLocal.cs](https://github.com/dotnet/coreclr/blob/9773db1e7b1acb3ec75c9cc0e36bd62dcbacd6d5/src/System.Private.CoreLib/shared/System/Threading/ThreadLocal.cs "git")

----------
since:5/21/2019 11:58:46 AM 

direction:ThreadLocal<T>

version: .net core