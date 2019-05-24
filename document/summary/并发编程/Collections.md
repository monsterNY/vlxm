
#### ArrayList ####

测试代码:
  	
	ArrayList list = new ArrayList();
	for (int i = 0; i < 10; i++)
	{
		Task.Run((() =>
		{
		  Thread.Sleep(10);
		  list.Add(1);//异常1
		  list.Add(2);
		
		  foreach (var item in list)//异常2
		    Console.Write(item);
		
		  Console.WriteLine();
		  Console.WriteLine($"线程-{Thread.CurrentThread.ManagedThreadId} 执行完毕！");
		
		}));
	}

执行结果:

	1.Add异常: System.IndexOutOfRangeException:“Index was outside the bounds of the array.”
		why? 当执行add时其他线程也在执行add 所有容量扩充可能出现验证无效，从而导致容量不足无法添加

	2.遍历时异常： System.InvalidOperationException:“Collection was modified; enumeration operation may not execute.”
		why? 遍历时其他线程修改了数组

solution:

> ArrayList的Add定义:

	// System.Collections.ArrayList
	public virtual int Add(object value)
	{
		if (this._size == this._items.Length)//容量判断
		{
			this.EnsureCapacity(this._size + 1);//容量扩充
		}
		this._items[this._size] = value;//直接负责
		this._version++;//版本增加 后续说明
		int size = this._size;
		this._size = size + 1;//已填充数量
		return size;
	}

first chat:

	A: 既然是容量不足，那直接初始化时将容量设大一点(初始容量为4，扩倍数充为2)
	   
	B: 这是一个取巧的解决方案，但是在真实情况下，一个线程产生的数据可能是不确定的
	   
	A: 那将初始容量设为一个不可能到达的数量
	   
	B: 也可以，不过多余的容量会导致不必要的内存浪费
	   
	B: 而且 虽然这种方案可以解决报错，但是还是会出现第二个问题: 添加无效 ，数组出现null 
	   
	A: 的确，那怎么办呢？
	   
	B: 那我们给操作加上锁，通过锁，就可以保证容量验证始终正确,也能确保数据不会丢失了
	   
	A: 一箭双雕~

	B: 而且在ArrayList已经提供了这个操作 
	    public static ArrayList Synchronized(ArrayList list);
		通过这个方法就可以拿到一个加锁操作的ArrayList了

	A: 为什么使用这个方法就可以了呢?

	B: 首先，先查看这个方法的源码：
		public static ArrayList Synchronized(ArrayList list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			return new ArrayList.SyncArrayList(list);
		}

		他返回了一个SyncArrayList，查看定义可以看到SyncArrayList继承了ArrayList

		再来看他的Add操作:

		public override int Add(object value)
		{
			object root = this._root;
			int result;
			lock (root)
			{
				result = this._list.Add(value);
			}
			return result;
		}
		
		看到这里，应该就明白了，其实就跟之前聊的一样，通过加锁进行操作。

	over~

second chat:
	
	A: 那么第二个错误是如何产生的呢？

	B: 首先，当进行foreach时，实际是调用类的GetEnumerator获取IEnumerator 然后进行遍历
		IEnumerator应该都熟悉，就不说明了

		先查看ArrayList的GetEnumerator,那么

		return new ArrayList.ArrayListEnumeratorSimple(this);

		由于是在foreach时出错的，那么就说明错误在MoveNext,那么来看ArrayListEnumeratorSimple的MoveNext:

		public bool MoveNext()
		{
			if (this.version != this.list._version)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
			}
			....省略
		}

		可以看到有一个version的验证，所以version就是用于查看当前数组是否被修改了。如果不一致就直接抛出异常，由于数据已经被修改了，所以遍历旧数据并没有太多意义。

		而且还有一个关于多线程的问题，当存在多个线程操作时，你返回的可能是别人操作后的ArrayListEnumeratorSimple，因为你在返回时可能有其他线程正在进行操作

	A: 那么我们使用上面的SyncArrayList操作也会出错吗？

	B: 同样的，先看GetEnumerator方法:
		
		public override IEnumerator GetEnumerator()
		{
			object root = this._root;
			IEnumerator enumerator;
			lock (root)
			{
				enumerator = this._list.GetEnumerator();
			}
			return enumerator;
		}

		从这里可以看出，虽然添加了锁，保证了返回时保证了其他线程不会操作，
		但是返回的还是上面的ArrayListEnumeratorSimple,而返回之后其他线程是可以正常操作的，所以还是会出现之前的错误，所以这部分就应该由我们来处理。

	over~

----------
since:5/22/2019 4:34:52 PM 

direction:Collections