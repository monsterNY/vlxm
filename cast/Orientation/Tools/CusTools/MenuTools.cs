using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Tools.CusTools
{
  /// <summary>
  /// @desc : MenuTools  
  /// @author :mons
  /// @create : 2019/3/19 9:56:09 
  /// @source : 
  /// </summary>
  public class MenuTools
  {
    public static Dictionary<string, int> GetList(Type type)
    {
      var fieldInfos = type.GetFields(BindingFlags.Static | BindingFlags.Public);

      Dictionary<string, int> dictionary = new Dictionary<string, int>();

      foreach (var item in fieldInfos)
      {
        dictionary.Add(item.Name, (int)item.GetValue(null));
      }

      return dictionary;
    }

    public static T[] GetList<T>() where T : Enum //7.3 support。
    {
      return (T[])Enum.GetValues(typeof(T));
    }

    #region enumIL

    /**
     *
     * .classprivate auto ansi sealed MyEnum
     * extends [mscorlib]System.Enum
     *
     * {
     *
     * .field publicstatic literal valuetype Mgen.MyEnum AAA = int32(0)
     *
     * .field publicstatic literal valuetype Mgen.MyEnum BBB = int32(1)
     *
     * .field publicstatic literal valuetype Mgen.MyEnum CCC = int32(2)
     *
     * .field public specialname rtspecialname int32 value__
     *
     * }
     */

    #endregion
  }
}