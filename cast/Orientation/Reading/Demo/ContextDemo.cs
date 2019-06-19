using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Reading.Demo
{
  /// <summary>
  /// @desc : ContextDemo  通过阻止执行上下文的流动来影响线程逻辑调用上下文中的数据
  /// @author : mons
  /// @create : 2019/6/19 15:16:50 
  /// @source : clr via C# p615
  /// </summary>
  public class ContextDemo
  {

    public static void Run()
    {

      //将一些数据放置到Run线程的逻辑调用上下文中
      CallContext.LogicalSetData("Name","Jeffrey");

      ThreadPool.QueueUserWorkItem(state => Console.WriteLine($"Name={CallContext.LogicalGetData("Name")}"));

      //阻止上下文的流动
      ExecutionContext.SuppressFlow();

      ThreadPool.QueueUserWorkItem(state => Console.WriteLine($"Name={CallContext.LogicalGetData("Name")}"));

      //恢复Run线程的执行上下文的流动
      ExecutionContext.RestoreFlow();

    }

  }
}
