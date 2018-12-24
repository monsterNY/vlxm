using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using Model.Common.CusAttr;

namespace Model.Common.Extension
{
  public static class MenuExt
  {
    public static T GetAttribute<T>(this Enum info) where T : Attribute
    {
      var fieldInfo = info.GetType().GetField(info.ToString(), BindingFlags.Static | BindingFlags.Public);

      Trace.WriteLine(fieldInfo);

      if (fieldInfo == null)
        return default(T);

      var customAttribute = Attribute.GetCustomAttribute(fieldInfo, typeof(DealAttribute), false);

      Trace.WriteLine(customAttribute);

      return customAttribute as T;
    }
  }
}