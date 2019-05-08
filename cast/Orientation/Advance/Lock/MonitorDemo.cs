using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Advance.Lock
{
  /// <summary>
  /// @desc : MonitorDemo  
  /// @author : mons
  /// @create : 2019/5/7 16:31:49 
  /// @source : 
  /// </summary>
  public class MonitorDemo
  {
    private object _lock = new object();

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

    #region Complete Demo

    //库存
    private int _goods;

    //生产量
    private int _make;
    
    //销售量
    private int _sell;

    public void Make()
    {
      if (Monitor.TryEnter(_lock, TimeSpan.FromMilliseconds(1000)))
      {
        try
        {
          _goods++;
          _make++;
          Console.WriteLine($"制造了一个产品,当前产品数量:{_goods}");
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
        Console.WriteLine("生成失败");//当后续没有需要购买的线程时或超时时触发
      }
    }

    public void Sell()
    {
      if (Monitor.TryEnter(_lock, TimeSpan.FromMilliseconds(1000)))
      {
        while (_goods <= 0) //库存不足，当生成Pulse唤醒后，有可能被其他线程消费掉，所以此处需要循环，不能在wait后直接操作
        {
          //System.Threading.SynchronizationLockException:“Object synchronization method was called from an unsynchronized block of code.”
          //同步方法不能在非同方方法中调用
          if (!Monitor.Wait(_lock, TimeSpan.FromMilliseconds(1000))) //等待生成
          {
            Console.WriteLine("库存不足！");
            return;
          }
        }

        _sell++;
        _goods--;
        Console.WriteLine($"售出了一个产品,当前库存:{_goods}");
      }
      else
      {
        Console.WriteLine("购买失败");
      }
    }

    public void Show()
    {
      Console.WriteLine($"当前库存:{_goods},生产量:{_make},销售量:{_sell},实际库存:{_sell - _make}");
      if (_make - _sell != _goods) throw new Exception("销售异常！");
    }

    #endregion

  }
}