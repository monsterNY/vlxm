using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Advance.CusInherit;
using Advance.Models;
using Newtonsoft.Json;

namespace Advance.Study
{
  /// <summary>
  /// @desc : ThreadLocalDemo  
  /// @author : mons
  /// @create : 2019/5/20 15:43:12 
  /// @source : 
  /// </summary>
  public class ThreadLocalDemo
  {
    private object _lockObj = new object();

    private List<string> list = new List<string>();

    /// <summary>
    /// 从使用开始...
    /// </summary>
    public void Use()
    {
      #region ThreadStaticAttribute

      /*using (var info = new FileRes()) //使用using也会使得初始值无效.
      {
        FileRes.file = "file"; //若此处不赋值，file的值会是file对应的变量类型的默认值 下面同理

        Thread th = new Thread(() =>
        {
          FileRes.file = "new file"; //此处的修改并不影响到其他线程
          info.Dispose();
        });

        th.Start();

        th.Join();

        //output:
        //3 new file
        //1 file

        //可见file静态字段在两个线程中都是独立存储的，互相不会被修改。
        //优势：简单易用
        //劣势：当线程需要独立存储的信息过多时，操作会非常复杂，而且只支持静态字段

      }*/

      #endregion

      #region LocalDataStoreSlot

      {
        //创建Slot

        LocalDataStoreSlot slot = Thread.AllocateNamedDataSlot("slot");

        //使用未命名的LocalDataStoreSlot类型
        //  无效手动释放
        var slot2 = Thread.AllocateDataSlot();
        var slot3 = Thread.AllocateDataSlot();

        Console.WriteLine(slot.GetHashCode());
        Console.WriteLine(slot2.GetHashCode());
        Console.WriteLine(slot3.GetHashCode());

        //设置TLS中的值

        Thread.SetData(slot, "hehe");

        //修改TLS的线程

        Thread th = new Thread(() =>

        {
          Thread.SetData(slot, "Mgen");

          ShowData("slot");
        });


        th.Start();

        th.Join();

        ShowData("slot");

        //清除Slot

        Thread.FreeNamedDataSlot("slot");

        //output:
        //线程 - 3,数据: Mgen
        //线程 - 1,数据: hehe

        //LocalDataStoreSlot 同样保证了独立存储 而且在默认值方面同上 此处由于都是object 则默认都为null
        //优势 对于ThreadStaticAttribute而言，LocalDataStoreSlot的操作使用键值对操作，无须提前定义，
        //劣势 当key过多复杂度也会随之增加，而且存在装拆箱。
      }

      #endregion

      #region ThreadLocal

      //初始化ThreadLocal
      //  valueFactory - value获取工厂
      //  trackAllValues - 是否跟踪所有值,若为false Values无法使用。

      //public ThreadLocal(Func<T> valueFactory, bool trackAllValues);
      ThreadLocal<string> nameLocal =
        new ThreadLocal<string>((() => { return $"Thread-{Thread.CurrentThread.ManagedThreadId}"; }), false);

      ThreadLocal<int> local2 = new ThreadLocal<int>();
      ThreadLocal<bool> local3 = new ThreadLocal<bool>();
      ThreadLocal<long> local4 = new ThreadLocal<long>();
      ThreadLocal<string> local5 = new ThreadLocal<string>();


      Console.WriteLine("----");

      Task.Run((() =>
      {
        //本线程决定要自己取名字
        nameLocal.Value = "秦始皇"; //若此处不操作 ， 将同样使用valueFactory获取值
        Console.WriteLine(nameLocal.Value);
      }));

      Console.WriteLine(nameLocal.Value);

      //output :
      //秦始皇
      //Thread - 1

      //相较于LocalDataStoreSlot优势:
      //  避免了类型的转换:要什么类型传什么类型
      //  提供了默认值
      //  降低复杂度，且耦合更低。

      //note :

      //实际上 ThreadLocalMap 中使用的 key 为 ThreadLocal 的弱引用，弱引用的特点是，如果这个对象只存在弱引用，那么在下一次垃圾回收的时候必然会被清理掉。

      //所以如果 ThreadLocal 没有被外部强引用的情况下，在垃圾回收的时候会被清理掉的，这样一来 ThreadLocalMap中使用这个 ThreadLocal 的 key 也会被清理掉。但是，value 是强引用，不会被清理，这样一来就会出现 key 为 null 的 value。

      //ThreadLocalMap实现中已经考虑了这种情况，在调用 set()、get()、remove() 方法的时候，会清理掉 key 为 null 的记录。如果说会出现内存泄漏，那只有在出现了 key 为 null 的记录后，没有手动调用 remove() 方法，并且之后也不再调用 get()、set()、remove() 方法的情况下。

      nameLocal.Dispose();

      #endregion
    }

    private void ShowData(string key)
    {
      LocalDataStoreSlot dataslot = Thread.GetNamedDataSlot(key);

      Console.WriteLine($"线程-{Thread.CurrentThread.ManagedThreadId},数据:{Thread.GetData(dataslot)}");
    }

    public void Run()
    {
      //当不同线程使用ThreadLocal时 会生成一个新的实例。？


      //初始化ThreadLocal
      //  valueFactory - value获取工厂
      //  trackAllValues - 是否跟踪所有值,若为false Values无法使用。

      //public ThreadLocal(Func<T> valueFactory, bool trackAllValues);
      // Thread-Local variable that yields a name for a thread
      ThreadLocal<string> ThreadName =
        new MyThreadLocal<string>(() => { return "Thread" + Thread.CurrentThread.ManagedThreadId; }
          //,true
        ); //一个线程享有一个 ThreadLocal

      // Action that prints out ThreadName for the current thread
      Action action = () =>
      {
        // 如果是在当前线程上初始化 Value，则为 true；否则为 false。
        // 即当前线程如果已经初始化ThreadLocal 则为true 表示为同一线程二次执行
        // If ThreadName.IsValueCreated is true, it means that we are not the
        // first action to run on this thread.
        bool repeat = ThreadName.IsValueCreated;


        ThreadName.Value = $"now Thread:{Thread.CurrentThread.ManagedThreadId}";

        lock (_lockObj)
        {
          list.Add(ThreadName.Value);
        }

        Console.WriteLine("ThreadName = {0} {1}", ThreadName.Value, repeat ? "(repeat)" : "");

        Console.WriteLine(ThreadName);
      };

      Console.WriteLine($"当前线程：{Thread.CurrentThread.ManagedThreadId}");

      // Launch eight of them.  On 4 cores or less, you should see some repeat ThreadNames
      Parallel.Invoke(action, action, action, action, action, action, action, action);

      //The ThreadLocal object is not tracking values. To use the Values property, use a ThreadLocal constructor that accepts the trackAllValues parameter and set the parameter to true.”
      //ThreadLocal对象没有跟踪值。要使用Values属性，请使用接受trackAllValues参数并将参数设置为true的ThreadLocal构造函数。
      //Console.WriteLine(JsonConvert.SerializeObject(ThreadName.Values));

      Console.WriteLine(list);

      // Dispose when you are done
      ThreadName.Dispose();
    }
  }
}