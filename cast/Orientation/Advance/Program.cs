using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using Advance.Lock;
using Tools.RefTools;

namespace Advance
{
  class Program
  {
    static void Main(string[] args)
    {
      var rand = new Random();
      CodeTimer timer = new CodeTimer();
      timer.Initialize();



      Console.WriteLine("Hello World");

      Console.ReadKey(true);
    }

    private static void MonitorDemoTest()
    {
      MonitorDemo instance = new MonitorDemo();

      ThreadPool.QueueUserWorkItem((state => //销售由他人负责，故此处开启新线程执行
      {
        Parallel.For(0, 100, (num) => //假设此商家有100个销售,由于销售与销售互不干扰，所以此处并行进行销售
        {
          Thread.Sleep(100); //由于后续执行太快，无法看出效果,故此处新增延时处理
          instance.Sell();
        });
      }));

      ThreadPool.QueueUserWorkItem((state =>
      {
        Parallel.For(0, 100, (num) => //假设此商家有10个生产厂
        {
          Thread.Sleep(100);
          instance.Make();
        });
      }));

      while (true) //每过一秒查看一次销售情况
      {
        Thread.Sleep(1000);
        instance.Show();
      }

      //      demo.Run();
      //
      //      Console.ReadKey(true);

      /*Parallel.For(0, 10, (i =>
      {
        try
        {
          demo.Run();
        }
        catch (Exception e)
        {
          Console.WriteLine(e.Message);
        }
        finally
        {
          Console.WriteLine($"{i} over");
          //测试结果:部分线程无法走到此处，疑似死锁。
        }
      }));*/
    }

    /// <summary>
    /// 锁的简单场景演示
    /// </summary>
    private static void LockTest(Random rand)
    {
      Condition condition = new Condition(); //创建一个初始库存为100的商家

      ThreadPool.QueueUserWorkItem((state => //销售由他人负责，故此处开启新线程执行
      {
        Parallel.For(0, 100, (num) => //假设此商家有100个销售,由于销售与销售互不干扰，所以此处并行进行销售
        {
          Thread.Sleep(100); //由于后续执行太快，无法看出效果,故此处新增延时处理
          var buy = rand.Next(10) + 1; //假设此处为用户需要购买的数量
          //            Console.WriteLine($"销售渠道-{num}:需要购买{buy}个产品");
          //          condition.LockSell(buy);//加锁处理
          condition.Sell(buy);
        });
      }));

      ThreadPool.QueueUserWorkItem((state =>
      {
        Parallel.For(0, 100, (num) => //假设此商家有10个生产厂
        {
          Thread.Sleep(100);
          var make = rand.Next(5) + 1; //假设此处为制造出来的产品数
          //            Console.WriteLine($"生产商-{num} :生产成功");
          //          condition.LockMake(make);
          condition.Make(make);
        });
      }));

      while (true) //每过一秒查看一次销售情况
      {
        Thread.Sleep(1000);
        condition.Show();
      }


      /*
      无锁测试结果：实际与当前不一致
      总生产数量: 11,总售出数量: 8,当前库存: 3,实际库存: 3
      总生产数量: 31,总售出数量: 29,当前库存: 1,实际库存: 2
      总生产数量: 51,总售出数量: 47,当前库存: 1,实际库存: 4
      总生产数量: 73,总售出数量: 67,当前库存: 3,实际库存: 6
      总生产数量: 93,总售出数量: 71,当前库存: 19,实际库存: 22
      总生产数量: 97,总售出数量: 71,当前库存: 23,实际库存: 26
      */
    }
  }
}