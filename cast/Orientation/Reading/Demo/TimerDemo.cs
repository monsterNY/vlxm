using System;
using System.Threading;
using System.Threading.Tasks;

namespace Reading.Demo
{
  /// <summary>
  /// @desc : TimerDemo  
  /// @author : mons
  /// @create : 2019/6/20 15:11:18 
  /// @source : 
  /// </summary>
  public class TimerDemo
  {
    public static void SimpleDemo()
    {
      var timer = new Timer(
        (state => { Console.WriteLine($"当前时间为：{DateTime.Now.ToString()}"); }), //执行主体
        null, //参数传递
        TimeSpan.FromSeconds(1), //延时多少时间后开始调用
        TimeSpan.FromSeconds(2) //每调用一次后等待多长时间  -1 表示仅调用一次
      );
    }

    private static Timer timer2;

    //为避免主体执行过长 而等待时间过短 导致计时器(在上个回调还没有完成的时候)再次触发
    //采用change方法重置
    public static void Run()
    {
      timer2 = new System.Threading.Timer(
        (state =>
        {
          Console.WriteLine($"当前时间为：{DateTime.Now.ToString()}");

          Thread.Sleep(2000);

          timer2.Change(TimeSpan.FromSeconds(1), Timeout.InfiniteTimeSpan);
        }), //执行主体
        null, //参数传递
        TimeSpan.FromSeconds(1), //延时多少时间后开始调用
        Timeout.InfiniteTimeSpan //每调用一次后等待多长时间  -1 表示仅调用一次
      );

      timer2.Change(TimeSpan.FromSeconds(1), Timeout.InfiniteTimeSpan);

    }

    public static async void Status()
    {
      while (true)
      {

        Console.WriteLine($"Checking status at {DateTime.Now.ToString()}");

        //需要检查的代码放到这里...

        //在不阻塞线程的前提下延时2秒
        await Task.Delay(2000);

        // 2秒之后，某个线程会在await之后介入并继续循环

      }
    }
  }
}