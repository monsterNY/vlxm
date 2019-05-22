using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Advance.Study
{
  /// <summary>
  /// @desc : CollectionsStudy  
  /// @author : mons
  /// @create : 2019/5/22 11:24:22 
  /// @source : 
  /// </summary>
  public class CollectionsStudy
  {

    public void Run()
    {

//      ArrayList ，只要不修改该集合可以同时支持多个读取器。 若要保证的线程安全ArrayList，必须通过返回的包装器完成所有操作Synchronized(IList)方法。
//
//      枚举整个集合本质上不是一个线程安全的过程。 即使某个集合已同步，其他线程仍可以修改该集合，这会导致枚举数引发异常。 若要确保枚举过程中的线程安全性，可以在整个枚举期间锁定集合，或者捕获由其他线程进行的更改所导致的

      //first error:System.IndexOutOfRangeException:“Index was outside the bounds of the array.”
      // 这个问题


      //ArrayList 是否保障了线程安全？
      // 是。

      ArrayList list = new ArrayList(100);

      list = ArrayList.Synchronized(new ArrayList(100));//可以保障单个操作一致。

      //测试一波。
      for (int i = 0; i < 100; i++)
      {
        Task.Run((() =>
        {
          Thread.Sleep(100);

          list.Add(1);//System.IndexOutOfRangeException:“Index was outside the bounds of the array.”
          list.Add(2);
          list.Add(3);

          Console.WriteLine(list.IsSynchronized);

          var enumerator = list.GetEnumerator();

          while (enumerator.MoveNext())//此处存在验证，当版本号不一致时出错
          {
            Console.Write(enumerator.Current);
          }

          Console.WriteLine();
          Console.WriteLine($"线程-{Thread.CurrentThread.ManagedThreadId} 执行完毕！");

        }));
      }

    }

  }
}
