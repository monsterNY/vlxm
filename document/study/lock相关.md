 

> why?

    当我们使用线程的时候，效率最高的方式当然是异步，即各个线程同时运行，其间不相互依赖和等待。
	但当不同的线程都需要访问某个资源的时候，就需要同步机制了，也就是说当对同一个资源进行读写的时候，
	我们要使该资源在同一时刻只能被一个线程操作，以确保每个操作都是有效即时的，也即保证其操作的原子性。
	lock是C#中最常用的同步方式，格式为lock(objectA){codeB} 。

    lock (objectA) { codeB}
    看似简单，实际上有三个意思，这对于适当地使用它至关重要：
    1.objectA被lock了吗？没有则由我来lock，否则一直等待，直至objectA被释放。
    2.lock以后在执行codeB的期间其他线程不能调用codeB，也不能使用objectA。
    3.执行完codeB之后释放objectA，并且codeB可以被其他线程访问。

 [https://www.cnblogs.com/apsnet/archive/2012/07/08/2581475.html](source:https://www.cnblogs.com/apsnet/archive/2012/07/08/2581475.html "博文转载")


    即为了保证资源的一致性

    

> when?

    需要该资源在同一时刻只能被一个线程操作

    

> how?

    通过加锁来确保在操作完成之前不会被第二个资源进行访问


----------

> 场景一:

	存在生产与售出方法
	两个方法同时操作库存

code:

    // 生产数量
    public int MakeCount { get; set; }

    // 售出数量
    public int SellCount { get; set; }

	//库存
	private int Products { get; set; }

	//产品制造
    public void Make(int num)
    {
	    Products += num;
	    MakeCount += num;
	    Console.WriteLine($"生产了{num}个{ProductName},当前产品总数为:{Products}");
    }
	
	//产品售出
	public void Sell(int num)
    {
      if (Products < num)
      {
        Console.WriteLine($"库存不足，当前库存为:{Products}");
        return;
      }

      Products -= num;
      SellCount += num;
      Console.WriteLine($"售出了{num}个{ProductName},当前产品总数为:{Products}");
    }

result:
	
	  总生产数量: 11,总售出数量: 8,当前库存: 3,实际库存: 3
      总生产数量: 31,总售出数量: 29,当前库存: 1,实际库存: 2
      总生产数量: 51,总售出数量: 47,当前库存: 1,实际库存: 4
      总生产数量: 73,总售出数量: 67,当前库存: 3,实际库存: 6
      总生产数量: 93,总售出数量: 71,当前库存: 19,实际库存: 22
      总生产数量: 97,总售出数量: 71,当前库存: 23,实际库存: 26

解决方法: 在操作数据时加锁，保证在同一时间仅有一个线程操作此数据
	
	private int _products;

    public int Products
    {
      get
      {
        return this._products;
      }
      set
      {
        lock (this)
          this._products = value;
      }
    }

note1: 单方法操作，不会导致数据异常

note2: 当执行时间越短，触发数据异常机遇越小

> lock是如何执行的？

lock <==> Monitor

即

    lock (this)
    {
      _products = value;
    }

	//等价于:

	bool lockTaken = false;
    try
    {
      Monitor.Enter(this, ref lockTaken);
      _products = value;
    }
    finally
    {
      if (lockTaken) Monitor.Exit(this);
    }

[https://docs.microsoft.com/zh-cn/dotnet/api/system.threading.monitor?view=netframework-4.8](https://docs.microsoft.com/zh-cn/dotnet/api/system.threading.monitor?view=netframework-4.8 "官方文档地址")

### 什么是Monitor?/Monitord有什么作用? ###

 提供同步访问对象的机制。

### Monitor的使用 ###
   
code:

	//库存
    private int goods { get; set; }

    //生产量
    private int make { get; set; }
    
    //销售量
    private int sell { get; set; }

    public void Make()
    {
      if (Monitor.TryEnter(_lock, TimeSpan.FromMilliseconds(1000)))
      {
        try
        {
          goods++;
          make++;
          Console.WriteLine($"制造了一个产品,当前产品数量:{goods}");
          ////System.Threading.SynchronizationLockException:“Object synchronization method was called from an unsynchronized block of code.”
          //为避免生产过量,生产完后会提醒销售售出
          Monitor.Pulse(_lock);
        }
        finally
        {
          Monitor.Exit(_lock);
        }
      }
      else
      {
        Console.WriteLine("生成失败");
      }
    }

    public void Sell()
    {
      if (Monitor.TryEnter(_lock, TimeSpan.FromMilliseconds(1000)))
      {
        while (goods <= 0) //库存不足
        {
          //System.Threading.SynchronizationLockException:“Object synchronization method was called from an unsynchronized block of code.”
          //同步方法不能在非同方方法中调用
          if (!Monitor.Wait(_lock, TimeSpan.FromMilliseconds(1000))) //等待生成
          {
            Console.WriteLine("库存不足！");
            return;
          }
        }

        sell++;
        goods--;
        Console.WriteLine($"售出了一个产品,当前库存:{goods}");
      }
      else
      {
        Console.WriteLine($"购买失败");
      }
    }

    public void Show()
    {
      Console.WriteLine($"当前库存:{goods},生产量:{make},销售量:{sell},实际库存:{sell - make}");
      if (make - sell != goods) throw new Exception("销售异常！");
    }

### note ###

1. 同步索引块是.NET中解决对象同步问题的基本机制
1. 这个对象肯定要是引用类型，值类型可不可呢？值类型可以装箱啊！你觉得可不可以？但也不要用值类型，因为值类型多次装箱后的对象是不同的，会导致无法锁定；
1. 不要锁定this，尽量使用一个没有意义的Object对象来锁；
1. 不要锁定一个类型对象，因类型对象是全局的；
1. 不要锁定一个字符串，因为字符串可能被驻留，不同字符对象可能指向同一个字符串；
1. 不要使用[System.Runtime.CompilerServices.MethodImpl(MethodImplOptions.Synchronized)]，这个可以使用在方法上面，保证方法同一时刻只能被一个线程调用。她实质上是使用lock的，如果是实例方法，会锁定this，如果是静态方法，则会锁定类型对象；

### confirm ###
通常，应避免锁定 public 类型，否则实例将超出代码的控制范围。

常见的结构 lock (this)、lock (typeof (MyType)) 和 lock ("myLock") 违反此准则：

	如果实例可以被公共访问，将出现 lock (this) 问题。
	如果 MyType 可以被公共访问，将出现 lock (typeof (MyType)) 问题。
	由于进程中使用同一字符串的任何其他代码将共享同一个锁，所以出现 lock(“myLock”) 问题。
### ext ###


[https://www.cnblogs.com/anding/p/5301754.html](https://www.cnblogs.com/anding/p/5301754.html "参考博文:.NET面试题解析(07)-多线程编程与线程同步")

[https://www.cnblogs.com/carsonzhu/p/7446953.html](https://www.cnblogs.com/carsonzhu/p/7446953.html "参考博文:[MethodImpl(MethodImplOptions.Synchronized)]与lock机制")

----------
author:monster

since:5/7/2019 2:02:24 PM 

direction:lock