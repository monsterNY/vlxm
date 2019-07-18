using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RedisDemo.Tools;
using StackExchange.Redis;

namespace RedisDemo.Demo
{
  /// <summary>
  /// @desc : BaseDemo  
  /// @author : mons
  /// @create : 2019/7/15 9:49:02 
  /// @source : 
  /// </summary>
  public class BaseDemo
  {
    static readonly TextWriter log = File.CreateText("redis_log.txt");
    static readonly ConfigurationOptions configuration = ConfigurationOptions.Parse("127.0.0.1:6379");

    #region 单例模式

    #region 懒汉式

    //private static ConnectionMultiplexer redisConn;
    //public static IConnectionMultiplexer GetConnection()
    //{
    //  return redisConn ?? ConnectionMultiplexer.Connect(configuration, log);
    //}

    #endregion

    #region 初始化式

    // C#其实并不保证实例创建的时机，因为C#规范只是在IL里标记该静态字段是BeforeFieldInit，也就是说静态字段可能在第一次被使用的时候创建，也可能你没使用了，它也帮你创建了，也就是周期更早，我们不能确定到底是什么创建的实例。
    //private static readonly ConnectionMultiplexer redisConn = ConnectionMultiplexer.Connect(configuration, log);

    //public static IConnectionMultiplexer GetConnection()
    //{
    //  return redisConn;
    //}

    #endregion

    #region lock式

    //private static volatile ConnectionMultiplexer redisConn;
    //private static object lockObj = new object();

    //public static IConnectionMultiplexer GetConnection()
    //{
    //  if (redisConn == null)
    //  {
    //    lock (lockObj)
    //    {
    //      if (redisConn == null)
    //        redisConn = ConnectionMultiplexer.Connect(configuration, log);
    //    }
    //  }

    //  return redisConn;
    //}

    #endregion

    #region 初始化式-优化

    // 这种方式，其实是很不错的，因为他确实保证了是个延迟初始化的单例（通过加静态构造函数），但是该静态构造函数里没有东西哦，所以能有时候会引起误解，尤其是在code review或者代码优化的时候，不熟悉的人可能直接帮你删除了这段代码，那就又回到了版本2了哦，所以还是需要注意的，不过如果你在这个时机正好有代码需要执行的话，那也不错。

    private static readonly ConnectionMultiplexer redisConn = ConnectionMultiplexer.Connect(configuration, log);

    public static IConnectionMultiplexer GetConnection()
    {
      return redisConn;
    }

    // 声明静态构造函数就是为了删除IL里的BeforeFieldInit标记
    // 以去北欧静态自动在使用之前被初始化
    static BaseDemo()
    {
    }

    #endregion

    #region 初始化式-扩展

    //public static IConnectionMultiplexer GetConnection()
    //{
    //  return Nested.redisConn;
    //}

    //private class Nested
    //{
    //  internal static readonly ConnectionMultiplexer redisConn = ConnectionMultiplexer.Connect(configuration, log);
    //}

    #endregion

    #region lazy式

    //private static readonly Lazy<ConnectionMultiplexer> redisConn = new Lazy<ConnectionMultiplexer>((() => ConnectionMultiplexer.Connect(configuration, log)));

    //public static IConnectionMultiplexer GetConnection()
    //{
    //  return redisConn.Value;
    //}

    #endregion

    #endregion


    public void TestConn()
    {
      var connectionMultiplexer = GetConnection();

      var database = connectionMultiplexer.GetDatabase(1);

      var key = "test_conn";

      var value = "vlxm";

      database.StringSet(key, value);

      var redisValue = database.StringGet(key);

      database.KeyDelete(key);

      if (value.Equals(redisValue.ToString()))
      {
        Console.WriteLine("connection success!");
      }
      else
      {
        Console.WriteLine("connection failure!");
      }
    }

    public void StringDemo()
    {
      // -------------介绍-------------

      //string 是 redis 最基本的类型，你可以理解成与 Memcached 一模一样的类型，一个 key 对应一个 value。

      //string 类型是二进制安全的。意思是 redis 的 string 可以包含任何数据。比如jpg图片或者序列化的对象。

      //string 类型是 Redis 最基本的数据类型，string 类型的值最大能存储 512MB。

      // ------------应用场景------------

      //缓存，这是使用非常多的地方；
      //计数器 / 限速器技术；
      //共享Session服务器也是基于该数据类型

      var database = GetConnection().GetDatabase(2);

      database.StringSet("empty", 1);

      RedisValue value = "123";

      //MSETNX key value[key value...]
      //同时设置一个或多个 key-value 对，当且仅当所有给定 key 都不存在。

      //MSET / MGET 这类命令少用，因为他们的时间复杂度是O(n)，但其实这里注意，n表示的是本次设置或读取的key个数，所以如果你批量读取的key并不是很多，每个key的内容也不是很大，那么使用批量操作命令反而能够节省网络请求、传输的时间。

      database.StringSet(new[]
      {
        new KeyValuePair<RedisKey, RedisValue>("name", "小明"),
        new KeyValuePair<RedisKey, RedisValue>("age", 18),
        new KeyValuePair<RedisKey, RedisValue>("gender", 1),
        new KeyValuePair<RedisKey, RedisValue>("popular", 88.5),
      });

      var redisValues = database.StringGet(new RedisKey[]
      {
        "empty",
        "age",
        "gender",
        "popular"
      });

      foreach (var redisValue in redisValues)
      {
        Console.WriteLine(redisValue);
      }

      // --------------存储原理------------------

      /*
       String类型的数据最终是如何在Redis中保存的呢？如果要细究的话，得先从 SDS 这个结构说起，不过今天先按下不表这源码部分的细节，只谈其内部保存的数据结构。最终我们设置的字符串都会以三种形式中的一种被存储下来。

Int，8个字节的长整型，最大值是：0x7fffffffffffffffL
Embstr，小于等于44个字节的字符串
Raw

结合代码来看看Redis对这三种数据结构是如何决策的。当我们在客户端使用命令 SET test hello,redis 时，客户端会把命令保存到一个buf中，然后按照收到的命令先后顺序依次执行。这其中有一个函数是：processMultibulkBuffer() ，它内部调用了 createStringObject() 函数
       */

      database.HashSet("user", new HashEntry[]
      {
        new HashEntry("name", "大白(●—●)"),
      });
    }

    public void ShowOptConfirm(bool flag)
    {
      if (flag) Console.WriteLine("操作成功！");
      else Console.WriteLine("操作失败！");
    }

    /// <summary>
    /// 好友系统示例
    /// </summary>
    public void FriendDemo()
    {

      /**
       *
       * db 实现：
       *
       * table : 关注表、粉丝表
       *
       *  关注表 - 关注人、被关注人
       *
       *  粉丝表 - idol、用户
       *
       * 查看我的关注列表：SELECT 被关注人 FROM 关注表 WHERE 关注人 = me
       *
       * 查看我的粉丝列表: SELECT 用户 FROM 粉丝表 WHERE idol = me
       *
       * 查看互关列表: SELECT * FROM 关注表 a WHERE EXISTS(
       *  SELECT * FROM 粉丝表 WHERE idol = me AND 用户 = a.被关注人
       * )  AND 关注人 = me
       *
       * single table : 关注表
       *
       * 查看我的关注列表：SELECT 被关注人 FROM 关注表 WHERE 关注人 = me
       *
       * 查看我的粉丝列表：SELECT 关注人 FROM 关注表 WHERE 被关注人 = me
       *
       * 查看互关列表: SELECT * FROM 关注表 a WHERE EXISTS(
       *  SELECT * FROM 关注表 WHERE 关注人 = a.被关注人 AND 被关注人 = me
       * ) AND 关注人 = me
       *
       */

      var database = GetConnection().GetDatabase(3);

      foreach (var baseOption in ChoiceTools.WhileOption(BaseOption.Exit))
      {
        if (baseOption == BaseOption.UserCenter)
        {
          foreach (var userOption in ChoiceTools.WhileOption(UserOption.Exit))
          {
            if (userOption == UserOption.ShowList)
            {
              ShowUser(database);
            }
            else if (userOption == UserOption.Add)
            {
              Console.WriteLine("请输入用户姓名:");
              ShowOptConfirm(AddUser(Console.ReadLine(), database));
            }
            else if (userOption == UserOption.Remove)
            {
              Console.WriteLine("请输入用户姓名:");
              ShowOptConfirm(RemoveUser(Console.ReadLine(), database));
            }
          }
        }
        else if (baseOption == BaseOption.AttentionCenter)
        {
          Console.WriteLine("请输入用户名：");

          var name = Console.ReadLine();

          var contains = database.SetContains("user", name);

          if (!contains)
          {
            Console.WriteLine("用户不存在");
            continue;
          }

          var attentionKey = $"{name}_attention";
          var fanKey = $"{name}_fan";
          foreach (var userOption in ChoiceTools.WhileOption(AttentionOption.Exit))
          {
            if (userOption == AttentionOption.ShowAttentionList)
            {
              ShowList(database, attentionKey);
            }
            else if (userOption == AttentionOption.ShowFanList)
            {
              ShowList(database, fanKey);
            }
            else if (userOption == AttentionOption.ShowEachList)
            {
              foreach (var redisValue in database.SetCombine(SetOperation.Intersect, attentionKey, fanKey))
              {
                Console.WriteLine(redisValue);
              }
            }
            else if (userOption == AttentionOption.AddAttention)
            {
              Console.WriteLine("请输入你要关注的用户：");
              ShowUser(database);

              var attention = Console.ReadLine();

              if (!database.SetContains("user", attention))
              {
                Console.WriteLine("用户不存在");
                continue;
              }

              ShowOptConfirm(AddItem(attentionKey, attention, database)
                             && AddItem($"{attention}_fan", name, database));
            }
            else if (userOption == AttentionOption.RemoveAttention)
            {
              Console.WriteLine("请输入你要取消关注的用户：");
              ShowList(database, attentionKey);

              var cancel = Console.ReadLine();

              if (!database.SetContains(attentionKey, cancel))
              {
                Console.WriteLine("用户不存在");
                continue;
              }

              ShowOptConfirm(RemoveItem(attentionKey, cancel, database)
                             && RemoveItem($"{cancel}_fan", name, database));
            }
          }
        }
      }

//      while (true)
//      {
//        var baseOption = ChoiceTools.GetOption<BaseOption>();
//
//        if(baseOption == BaseOption.Exit)break;
//
//        if (baseOption == BaseOption.UserCenter)
//        {
//
//        }

//        Console.WriteLine(@"
//>>>>>>请选择操作项:
//      1.添加用户
//      2.好友操作
//      3.关注操作
//");
//
//        var opt = int.Parse(Console.ReadLine());
//
//        if (opt == 1)
//        {
//          Console.WriteLine("请输入用户姓名:");
//          string name = Console.ReadLine();
//          AddUser(name, database);
//        }else if (opt == 2)
//        {
//
//        }
//      }
    }

    private void ShowList(IDatabase database, string key)
    {
      foreach (var redisValue in database.SetScan(key))
      {
        Console.WriteLine(redisValue);
      }
    }

    private bool AddItem(string key, string value, IDatabase database)
    {
      return database.SetAdd(key, value);
    }

    private bool RemoveItem(string key, string name, IDatabase database)
    {
      return database.SetRemove(key, name);
    }

    private void ShowUser(IDatabase database)
    {
      foreach (var redisValue in database.SetScan("user"))
      {
        Console.WriteLine(redisValue);
      }
    }

    private bool AddUser(string name, IDatabase database)
    {
      return database.SetAdd("user", name);
    }

    private bool RemoveUser(string name, IDatabase database)
    {
      return database.SetRemove("user", name);
    }

    #region option menu

    /// <summary>
    /// 基础操作项
    /// </summary>
    private enum BaseOption
    {
      /// <summary>
      /// 用户中心
      /// </summary>
      UserCenter = 1,
      /// <summary>
      /// 关注/粉丝中心
      /// </summary>
      AttentionCenter,
      Exit
    }

    private enum UserOption
    {
      /// <summary>
      /// 查看列表
      /// </summary>
      ShowList = 1,
      /// <summary>
      /// 添加用户
      /// </summary>
      Add = 2,
      /// <summary>
      /// 删除用户
      /// </summary>
      Remove = 3,
      Exit
    }

    private enum AttentionOption
    {
      /// <summary>
      /// 关注列表
      /// </summary>
      ShowAttentionList = 1,
      /// <summary>
      /// 粉丝列表
      /// </summary>
      ShowFanList,
      /// <summary>
      /// 互关列表
      /// </summary>
      ShowEachList,
      /// <summary>
      /// 添加关注
      /// </summary>
      AddAttention,
      /// <summary>
      /// 取消关注
      /// </summary>
      RemoveAttention,
      Exit
    }

    #endregion
  }
}