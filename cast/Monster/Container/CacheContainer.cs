using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Container
{
  public class CacheContainer
  {
    private static Dictionary<Type, ConstructorInfo> _cacheConstructorInfo;
    private static Dictionary<Type, object> _cacheInfo;

    static CacheContainer()
    {
      _cacheInfo = new Dictionary<Type, object>();
      _cacheConstructorInfo = new Dictionary<Type, ConstructorInfo>();
    }

    /// <summary>
    /// 给容器中添加对象
    /// </summary>
    /// <typeparam name="BaseType"></typeparam>
    /// <typeparam name="ChildType"></typeparam>
    public static void AddInstance<BaseType, ChildType>() where ChildType : BaseType
    {
      var type = typeof(ChildType);

      var constructorInfos = type.GetConstructors();

      //.net core中 以参数最长的构造方法为准，且此构造方法的参数列表满足其他所有构造函数的参数列表
      var constructorInfo = constructorInfos.First(u =>
        u.GetParameters().Length == constructorInfos.Max(k => k.GetParameters().Length));

      _cacheConstructorInfo.Add(typeof(BaseType), constructorInfo);
    }

    /// <summary>
    /// 从容器中获取对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T GetInstance<T>() where T : class
    {
      var type = typeof(T); //获取key

      _cacheInfo.TryGetValue(type, out var instance);

      if (instance != null)
        return (T) instance;

      var tryGetValue = _cacheConstructorInfo.TryGetValue(typeof(T), out var constructorInfo);

      if (!tryGetValue)
      {
        throw new Exception("容器中不存在此对象");
      }

      instance = constructorInfo.Invoke(constructorInfo.GetParameters()
//        .Select(u => u.GetType().GetConstructor(new Type[0]).Invoke(new object[0]))
        .Select(u => GetInstanceByType(u.GetType()))
        .ToArray()
      );

      _cacheInfo.Add(type, instance);

      return (T) instance;

    }

    /// <summary>
    /// 通过type访问泛型方法
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static object GetInstanceByType(Type type)
    {
      MethodInfo methodInfo =
        typeof(CacheContainer).GetMethod(nameof(GetInstance)).MakeGenericMethod(new Type[] {type});
      return methodInfo.Invoke(null, null);
    }
  }
}