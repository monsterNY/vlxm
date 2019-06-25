using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Reading.Demo
{
  /// <summary>
  /// @desc : CancellationDemo  
  /// @author : mons
  /// @create : 2019/6/20 9:54:36 
  /// @source : clr via C# p618
  /// </summary>
  public class CancellationDemo
  {

    public static void Run()
    {

      #region 简单使用

      Console.WriteLine("-----------Simple Demo--------------");
      var cts = new CancellationTokenSource();

      cts.Token.Register((() => Console.WriteLine("Canceled 1")));
      cts.Token.Register((() => Console.WriteLine("Canceled 2")));

      cts.Cancel();

      #endregion

      Console.WriteLine("-----------Linked Demo--------------");
      var cts1 = new CancellationTokenSource();
      cts1.Token.Register((() => Console.WriteLine("cts1 canceled")));

      var cts2 = new CancellationTokenSource();
      cts2.Token.Register((() => Console.WriteLine("cts2 canceled")));

      //创建一个新的CancellationTokenSource，它在cts1或cts2取消时取消
      var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(cts1.Token, cts2.Token);

      linkedCts.Token.Register((() => Console.WriteLine("linkedCts canceled")));

      cts2.Cancel();

      //显示那个对象被取消了
      Console.WriteLine($"cts1 canceled={cts1.IsCancellationRequested}, cts2={cts2.IsCancellationRequested}, linkedCts={linkedCts.IsCancellationRequested}");

      //延时取消
      cts1.CancelAfter(TimeSpan.FromSeconds(2));

    }

  }
}
