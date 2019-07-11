using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Advance.CusDemo
{
  /// <summary>
  /// @desc : SyncDemo  
  /// @author : mons
  /// @create : 2019/7/11 13:53:53 
  /// @source : 
  /// </summary>
  public class SyncDemo
  {

    public List<int> Demo()
    {
      var list = new List<int>()
      {
        Thread.CurrentThread.ManagedThreadId
      };

      for (int i = 0; i < 100; i++)
      {
        Task.Run((() =>
        {
          list.Add(Thread.CurrentThread.ManagedThreadId);
        }));
      }

      //return list;
      return new List<int>(list.ToArray());//避免返回后 异步还影响着返回值
    }

    public async Task<List<int>> Demo2()
    {
      var list = new List<int>()
      {
        Thread.CurrentThread.ManagedThreadId
      };

      for (int i = 0; i < 100; i++)
      {
        Task.Run((() =>
        {
          list.Add(Thread.CurrentThread.ManagedThreadId);
        }));
      }

      return new List<int>(list.ToArray());//同上
    }

    public async Task<List<int>> Demo3()
    {
      var list = new List<int>()
      {
        Thread.CurrentThread.ManagedThreadId
      };

      for (int i = 0; i < 100; i++)
      {
        await Task.Run((() =>
        {
          list.Add(Thread.CurrentThread.ManagedThreadId);
        }));
      }

      return new List<int>(list.ToArray());//同上
    }

  }
}
