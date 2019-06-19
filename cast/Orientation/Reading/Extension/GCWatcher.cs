using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Reading.Extension
{
  /// <summary>
  /// @desc : GCWatcher  
  /// @author : mons
  /// @create : 2019/6/18 16:15:56 
  /// @source : p487
  /// </summary>
  public static class GCWatcher
  {

    //注意：由于字符串留用(interning)和MarshalByRefObject代理对象，所以
    //使用String要当心
    private readonly static ConditionalWeakTable<object, NotifyWhenGCd<string>> s_cwt =
      new ConditionalWeakTable<object, NotifyWhenGCd<string>>();

    public static T GCWatch<T>(this T obj, string tag) where T : class
    {
      s_cwt.Add(obj,new NotifyWhenGCd<string>(tag));
      return obj;
    }

    private sealed class NotifyWhenGCd<T>
    {
      private readonly T m_value;

      internal NotifyWhenGCd(T value)
      {
        m_value = value;
      }

      public override string ToString()
      {
        return m_value.ToString();
      }

      ~NotifyWhenGCd()
      {
        Console.WriteLine($"Gc'd: {m_value.ToString()}");
      }
    }
  }
}