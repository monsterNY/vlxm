using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Advance.Lock
{
  /// <summary>
  /// @desc : Condition  场景一 : 生产与售出
  /// @author : mons
  /// @create : 2019/5/7 14:04:34 
  /// @source : 
  /// </summary>
  public class Condition
  {
    /// <summary>
    /// 产品名称【固定】
    /// </summary>
    public const string ProductName = "Spider Man";

    /// <summary>
    /// 当前库存
    /// </summary>
//    private int Products { get; set; }
    private int _products;

    /// <summary>
    /// 常见处理：仅在数据修改处加锁，不考虑前后操作，同样可以避免数据不一致
    /// </summary>
    public int Products
    {
      get => _products;
      set
      {
        //        lock (this)
        //        {
        //          _products = value;
        //        }
        //等价于
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
      }
    }

    /// <summary>
    /// 生产数量
    /// </summary>
    public int MakeCount { get; set; }

    /// <summary>
    /// 售出数量
    /// </summary>
    public int SellCount { get; set; }

    /// <summary>
    /// 初始化仓库 
    /// </summary>
    public Condition()
    {
    }


    /// <summary>
    /// 产品制造 [假设没有其他情况 100%生产成功]
    /// </summary>
    /// <param name="num"></param>
    public void LockMake(int num)
    {
      lock (this) //锁住当前对象，保障同一时间仅有一个线程可以操作此方法
      {
        Products += num;
        MakeCount += num;
        Console.WriteLine($"生产了{num}个{ProductName},当前产品总数为:{Products}");
      }
    }

    /// <summary>
    /// 出售[暂不考虑金额等]
    /// </summary>
    /// <param name="num"></param>
    public void LockSell(int num)
    {
      lock (this)
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
    }


    #region 无锁处理

    /// <summary>
    /// 产品制造 [假设没有其他情况 100%生产成功]
    /// </summary>
    /// <param name="num"></param>
    public void Make(int num)
    {
      Products += num;
      MakeCount += num;
      Console.WriteLine($"生产了{num}个{ProductName},当前产品总数为:{Products}");
    }

    /// <summary>
    /// 出售[暂不考虑金额等]
    /// </summary>
    /// <param name="num"></param>
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

    #endregion

    public void Show()
    {
      Console.WriteLine($"总生产数量:{MakeCount},总售出数量:{SellCount},当前库存:{Products},实际库存:{MakeCount - SellCount}");
      if (Products != (MakeCount - SellCount)) throw new Exception("数据异常！");
    }

    #region 测试单操作是否会出现数据错乱,result:貌似没有

    public int Single { get; set; }

    public void Add(int num)
    {
      Console.WriteLine($"线程id:{Thread.CurrentThread.ManagedThreadId},原有数量:{Single}");

      Single += num;

      Console.WriteLine($"线程id:{Thread.CurrentThread.ManagedThreadId},新增数量:{Single}");
    }

    #endregion
  }
}