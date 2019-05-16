## Monitor ##

> 提供同步访问对象的机制

### 代码示例 ###

    public void Run()
    {
      //Console.WriteLine(Monitor.IsEntered(_lock));//false

      //同步索引块是.NET中解决对象同步问题的基本机制

      //在指定对象上获取排他锁。 -->创建或使用一个空闲的同步索引块
      //Enter(object obj):void

      /*Monitor.Enter(_lock);//当不进行释放时，锁会随着方法的结束而结束? - no...

      throw new Exception("test error");

      Console.WriteLine("do something2");*/

      //获取指定对象上的排他锁，并自动设置一个值，指示是否获取了该锁。
      //Enter(object obj, ref bool lockTaken):void

      //var flag = false;

      //Monitor.Enter(_lock, ref flag); //测试结果:部分线程无法走到此处，疑似死锁。

      //Thread.Sleep(500);

      //Console.WriteLine("do something");

      //Console.WriteLine(Monitor.IsEntered(_lock));//true
      //释放指定对象上的排他锁。
      //Exit(object obj):void // 退出后都能走完
      //if (flag)
      //  Monitor.Exit(_lock);

      //确定当前线程是否保留指定对象锁。
      //IsEntered(object obj):bool
      //Console.WriteLine(Monitor.IsEntered(_lock));//false

      //don't understand
      //通知等待队列中的线程锁定对象状态的更改。
      //仅当前锁的所有者可以发出信号等待对象使用Pulse。
      //Pulse(object obj):void

      //Monitor.Pulse(_lock);


      //PulseAll(object obj):void

      //尝试获取指定对象的排他锁。
      //TryEnter(object obj):bool
      /*if (Monitor.TryEnter(_lock))//TryEnter VS Enter,TryEnter可避免长时间等待而Enter会一直等待到释放锁。
      {
        Console.WriteLine("get lock");
      }
      else
      {
        Console.WriteLine("don't get lock");
        return;
      }*/

      //同上不会等待
      //尝试获取指定对象上的排他锁，并自动设置一个值，指示是否获取了该锁。 
      //TryEnter(object obj, ref bool lockTaken):void
      /*bool flag = false;
      Monitor.TryEnter(_lock, ref flag);
      if (flag)//TryEnter VS Enter,TryEnter可避免长时间等待而Enter会一直等待到释放锁。
      {
        Console.WriteLine("get lock");
      }
      else
      {
        Console.WriteLine("don't get lock");
        return;
      }*/

      //在指定的毫秒数内尝试获取指定对象上的排他锁。
      //TryEnter(object obj, TimeSpan timeout):bool

      if (Monitor.TryEnter(_lock, TimeSpan.FromMilliseconds(100)))
      {
        Console.WriteLine("get lock");
      }
      else
      {
        Console.WriteLine("don't get lock");
        return;
      }

      //在指定的毫秒数内尝试获取指定对象上的排他锁。并自动设置一个值，指示是否获取了该锁。
      //TryEnter(object obj, TimeSpan timeout, ref bool lockTaken):void

      //释放对象上的锁并阻止当前线程，直到它重新获取该锁。
      //Wait(object obj):bool
      Monitor.Wait(_lock);

      //Wait(object obj, int millisecondsTimeout):bool
      //Wait(object obj, TimeSpan timeout):bool
      //Wait(object obj, TimeSpan timeout, bool exitContext):bool
      //释放对象上的锁并阻止当前线程，直到它重新获取该锁。 如果已用指定的超时时间间隔，则线程进入就绪队列。 可以在等待之前退出同步上下文的同步域，随后重新获取该域。
      //Wait(object obj, int millisecondsTimeout, bool exitContext):bool


      Console.WriteLine("do something");

      Thread.Sleep(500);
    }

通过使用可以看出，主要就是通过Enter和Wait进行操作

### 源码分析： ###

首先先看一下Enter

#### Enter ####

	[MethodImpl(MethodImplOptions.InternalCall), SecuritySafeCritical, __DynamicallyInvokable]
	public static extern void Enter(object obj);

无参数是直接通过内部方法来调用

	[__DynamicallyInvokable]
	public static void Enter(object obj, ref bool lockTaken)
	{
	    if (lockTaken)
	    {
	        ThrowLockTakenException();
	    }
	    ReliableEnter(obj, ref lockTaken);
	}

好样的，还是内部。
	
	[MethodImpl(MethodImplOptions.InternalCall), SecuritySafeCritical]
	private static extern void ReliableEnter(object obj, ref bool lockTaken);

### 注意事项： ###


1. 同步索引块是.NET中解决对象同步问题的基本机制


2. 这个对象肯定要是引用类型，值类型可不可呢？值类型可以装箱啊！你觉得可不可以？但也不要用值类型，因为值类型多次装箱后的对象是不同的，会导致无法锁定；


1. 不要锁定this，尽量使用一个没有意义的Object对象来锁；


1. 不要锁定一个类型对象，因类型对象是全局的；


1. 不要锁定一个字符串，因为字符串可能被驻留，不同字符对象可能指向同一个字符串；


1. 不要使用[System.Runtime.CompilerServices.MethodImpl(MethodImplOptions.Synchronized)]，这个可以使用在方法上面，保证方法同一时刻只能被一个线程调用。她实质上是使用lock的，如果是实例方法，会锁定this，如果是静态方法，则会锁定类型对象；

2. 通常，应避免锁定 public 类型，否则实例将超出代码的控制范围。


----------
author:monster

since:5/16/2019 11:40:55 AM 

direction:并发编程_Monitor的使用