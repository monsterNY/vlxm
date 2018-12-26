using System;
using System.Collections.Concurrent;

namespace Model.Common.Cache
{

  /// <summary>
  /// 简单的内存缓存
  /// 应用情景：临时缓存、部分全局缓存但可能存在频繁修改
  /// </summary>
  public class MemoryCache
  {

    private MemoryCache() { }

    /// <summary>
    /// 实例对象
    /// </summary>
    private static MemoryCache _instance;

    /// <summary>
    /// 锁对象
    /// </summary>
    private static object _lockObj = new object();

    #region volatile

    //        volatile关键字通知编译器在每个读这个字段的地方使用一个读栅栏（acquire-fence），并且在每个写这个字段的地方使用一个写栅栏（release-fence）。读栅栏防止其它读/写被移到栅栏之前，写栅栏防止其它读/写被移到栅栏之后。这种“半栅栏（half-fences）”比全栅栏更快，因为它给了运行时和硬件更大的优化空间。

    #endregion

    /// <summary>
    /// 缓存对象
    /// </summary>
    //        protected volatile Dictionary<string, object> cacheMap = new Dictionary<string, object>();
    //ConcurrentDictionary vs Dictionary 博文介绍:https://www.cnblogs.com/blogs2014/p/7682245.html
    protected ConcurrentDictionary<string, object> cacheMap = new ConcurrentDictionary<string, object>();
    //参考博文进行调整 》》 考虑线程安全
    //        protected ConcurrentDictionary<string, Lazy<object>> cacheMap = new ConcurrentDictionary<string, Lazy<object>>();

    /// <summary>
    /// 获取唯一实例
    /// </summary>
    /// <returns></returns>
    public static MemoryCache GetInstance()
    {

      #region 简单单例

      //            _instance = _instance ?? new MemoryCache();
      //
      //            _instance = _instance ?? Activator.CreateInstance(typeof(MemoryCache), true) as MemoryCache;

      #endregion

      //当同一对象的不同方法在不同的线程上并发着，可能存在两个方法获取对象内的值不一致
      //            编译器、CLR 或 CPU 可能会重新排序（reorder）程序指令以提高效率。
      //            编译器、CLR 或 CPU 可能会进行缓存优化，导致其它线程不能马上看到变量的赋值。

      //            Thread.MemoryBarrier();    // 设立屏障

      //            下列方式都会隐式的使用全栅栏：
      //
      //            C# 的lock语句（Monitor.Enter / Monitor.Exit）
      //                Interlocked类中的所有方法（马上会讲到）
      //            使用线程池的异步回调，包括异步委托、APM 回调，以及任务延续（task continuations）
      //            在信号构造上等待或对其设置（译者注：发信号、复位等等）
      //            任何依赖于信号同步的情况，比如启动或等待Task

      if (_instance == null)
      {

        lock (_lockObj)
        {
          _instance = Activator.CreateInstance(typeof(MemoryCache), true) as MemoryCache;
        }

      }
      return _instance;

    }

    /// <summary>
    /// 写入
    /// </summary>
    /// <param name="key"></param>
    /// <param name="obj"></param>
    /// <returns></returns>
    public bool TryWrite(string key, object obj)
    {

      cacheMap[key] = obj;

      #region 考虑线程安全

      //                LazyThreadSafetyMode的枚举值。
      //
      //                （1）None = 0【线程不安全】
      //
      //                （2）PublicationOnly = 1【针对于多线程，有多个线程运行初始化方法时，当第一个线程完成时其值则会设置到其他线程】
      //
      //                （3）ExecutionAndPublication = 2【针对单线程，加锁机制，每个初始化方法执行完毕，其值则相应的输出】

      //            cacheMap.AddOrUpdate(key,//key
      //                (s => new Lazy<object>((() => obj), LazyThreadSafetyMode.PublicationOnly)), //添加处理
      //                ((s, lazy) =>
      //                    new Lazy<object>((() => obj))));//修改处理

      #endregion
      return true;
    }

    /// <summary>
    /// 读取
    /// </summary>
    /// <param name="key"></param>
    /// <param name="defaultInfo"></param>
    /// <returns></returns>
    public object TryGet(string key, object defaultInfo = null)
    {

      object result;

      if (!cacheMap.TryGetValue(key, out result))
      {
        result = defaultInfo;
      }

      return result;

      #region 考虑线程安全

      //            var valueFound = cacheMap.GetOrAdd(key,
      //                x => new Lazy<object>(
      //                    () =>
      //                    {
      //                        return defaultInfo;
      //                    }));
      //
      //            return valueFound.Value;

      #endregion

    }

  }
}
