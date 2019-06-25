using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Reading.Demo
{
  /// <summary>
  /// @desc : CanceledAsyncWarn  
  /// @author : mons
  /// @create : 2019/6/21 14:45:48 
  /// @source : 
  /// </summary>
  public static class CanceledAsyncWarn
  {


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void NoWarning(this Task task) { }

    static async Task Demo()
    {

      Task.Run((() => Console.WriteLine("domain")));// 提示


      var taks = Task.Run((() => Console.WriteLine("domain")));// 不提示

      Task.Run((() => Console.WriteLine("domain"))).NoWarning();

      Console.WriteLine("other thing");

    }

  }
}
