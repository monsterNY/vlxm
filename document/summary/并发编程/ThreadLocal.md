
> 提供数据的线程本地存储。
> 
> 让各个线程维持自己的变量
> 
> TLS - 线程本地存储（Thread Local Storage）

## TLS相关类: ##

### ThreadStaticAttribute ###

	public class FileRes : IDisposable
	{
	  /// <summary>
	  /// ThreadStatic特性是最简单的TLS使用，且只支持静态字段，只需要在字段上标记这个特性就可以了：
	  /// </summary>
	  [ThreadStatic]
	  public static string file = "hehe";
	
	  public void Dispose()
	  {
	    Console.WriteLine($"线程-{Thread.CurrentThread.ManagedThreadId},操作文件:{file}");
	  }
	}

ThreadStatic特性是最简单的TLS使用，且只支持静态字段

使用:

	using (var info = new FileRes()) //使用using也会使得初始值无效.
    {
      FileRes.file = "file"; //若此处不赋值，file的值会是file对应的变量类型的默认值 下面同理

      Thread th = new Thread(() =>
      {
        FileRes.file = "new file"; //此处的修改并不影响到其他线程
        info.Dispose();
      });

      th.Start();

      th.Join();

    }

	//output:
	//3 new file
	//1 file	

显然ThreadStatic特性只支持静态字段太受限制了。那我们接着看下一个

### LocalDataStoreSlot ###

> 数据插槽

1. 命名方式

		private void ShowData(string key)
	    {
	      LocalDataStoreSlot dataslot = Thread.GetNamedDataSlot(key);
	
	      Console.WriteLine($"线程-{Thread.CurrentThread.ManagedThreadId},数据:{Thread.GetData(dataslot)}");
	    }

		LocalDataStoreSlot slot = Thread.AllocateNamedDataSlot("slot");

        Thread.SetData(slot, "hehe");

        Thread th = new Thread(() =>
        {
          Thread.SetData(slot, "Mgen");
          ShowData("slot");
        });


        th.Start();

        th.Join();

        ShowData("slot");

        //清除Slot
        Thread.FreeNamedDataSlot("slot");

2. 匿名方式

		static LocalDataStoreSlot slot;

		static void Main()
		
		{
		
		    //创建Slot
		
		    slot = Thread.AllocateDataSlot();
		
		    //设置TLS中的值
		
		    Thread.SetData(slot, "hehe");
		
		    //修改TLS的线程
		    Thread th = new Thread(() =>
	        {
	            Thread.SetData(slot, "Mgen");
	            Display();
	        });
		
		    th.Start();
		
		    th.Join();
		
		    Display();
		}
		
		static void Display()
		
		{
		    Console.WriteLine("{0} {1}", Thread.CurrentThread.ManagedThreadId, Thread.GetData(slot));
		}

output 同上(效果相同)

> 命名vs匿名:

	匿名无须手动释放-error
	[通过查看源码，实际上都是通过同一个方法获取，而在此方法中都new了一个LocalDataStoreSlot，所以不管是哪种方式，都尽量进行释放...]
	
	由于未命名的LocalDataStoreSlot没有名称，因此无法使用Thread.GetNamedDataSlot方法，
	只能在多个线程中引用同一个LocalDataStoreSlot才可以对TLS空间进行操作

感觉使用起来也不是很方便,还要手动判断数据的类型，而且和ThreadStaticAttribute一样没有默认值。接着看最后一个。

通过查看源码，实际是通过LocalDataStoreManager进行操作，内部维护一个Dictionary<string,LocalDataStoreSlot> 进行操作

### ThreadLocal ###

	//初始化ThreadLocal
	//  valueFactory - value获取工厂
	//  trackAllValues - 是否跟踪所有值,若为false Values无法使用。
	
	//public ThreadLocal(Func<T> valueFactory, bool trackAllValues);
	ThreadLocal<string> nameLocal =
	  new ThreadLocal<string>((() => { return $"Thread-{Thread.CurrentThread.ManagedThreadId}"; }), false);
	
	Task.Run((() =>
	{
	
	  //本线程决定要自己取名字
	  nameLocal.Value = "秦始皇";//若此处不操作 ， 将同样使用valueFactory获取值
	  Console.WriteLine(nameLocal.Value);
	}));
	
	Console.WriteLine(nameLocal.Value);
	
	//output :
	//秦始皇
	//Thread - 1

即通过了泛型类型限制，也提供了默认值(默认值由初始化的valueFactory提供),[舒服...

note:

1. 实际上 ThreadLocalMap 中使用的 key 为 ThreadLocal 的弱引用，弱引用的特点是，如果这个对象只存在弱引用，那么在下一次垃圾回收的时候必然会被清理掉。

2. 所以如果 ThreadLocal 没有被外部强引用的情况下，在垃圾回收的时候会被清理掉的，这样一来 ThreadLocalMap中使用这个 ThreadLocal 的 key 也会被清理掉。但是，value 是强引用，不会被清理，这样一来就会出现 key 为 null 的 value。

3. ThreadLocalMap实现中已经考虑了这种情况，在调用 set()、get()、remove() 方法的时候，会清理掉 key 为 null 的记录。如果说会出现内存泄漏，那只有在出现了 key 为 null 的记录后，没有手动调用 remove() 方法，并且之后也不再调用 get()、set()、remove() 方法的情况下

4. 一个线程内可以存在多个 ThreadLocal 对象

既然ThreadLocal这么神奇，那就来仔细研究下吧

----------

> slot - 槽

[https://www.cnblogs.com/luxiaoxun/p/8744826.html](https://www.cnblogs.com/luxiaoxun/p/8744826.html "ThreadLocal原理分析与使用场景")

----------
since:5/20/2019 3:39:20 PM 

direction:ThreadLocal