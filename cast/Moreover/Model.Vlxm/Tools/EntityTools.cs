using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Model.Vlxm.CusAttr;

namespace Model.Vlxm.Tools
{
  public class EntityTools
  {
    /// <summary>
    /// 获取所有字段
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="prefix">前缀</param>
    /// <param name="suffix">后缀</param>
    /// <param name="filterFunc"></param>
    /// <returns></returns>
    public static IEnumerable<string> GetFields<T>(string prefix = null, string suffix = null,Func<PropertyInfo,bool> filterFunc = null)
    {
      IEnumerable<PropertyInfo> propertyInfos = typeof(T).GetProperties();

      if (filterFunc != null)
      {
        propertyInfos = propertyInfos.Where(filterFunc);
      }

      return propertyInfos.Select(u => prefix + (u.GetCustomAttribute<FieldAttribute>()?.Name ?? u.Name) + suffix);
    }

    /// <summary>
    /// 获取单个字段
    /// </summary>
    /// <returns></returns>
    public static string GetField<T>(string filedName)
    {
      return typeof(T).GetProperty(filedName).GetCustomAttribute<FieldAttribute>()?.Name ?? filedName;
    }

    /// <summary>
    /// 获取表名
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string GetTableName<T>()
    {
      var type = typeof(T);

      return type.GetCustomAttribute<TableMapperAttribute>()?.TableName ?? type.Name;
    }

    #region empty

//    /// <summary>
//    /// 获取特性
//    /// </summary>
//    /// <typeparam name="T"></typeparam>
//    /// <param name="instance"></param>
//    /// <returns></returns>
//    public static T GetAttribute<T>(object instance) where T : Attribute
//    {
//      return instance.GetType().GetCustomAttribute<T>();
//    }
//
//    /// <summary>
//    /// 获取属性特性
//    /// </summary>
//    /// <typeparam name="T"></typeparam>
//    /// <param name="instance"></param>
//    /// <param name="propertyName"></param>
//    /// <returns></returns>
//    public static T GetAttribute<T>(object instance, string propertyName) where T : Attribute
//    {
//      return instance.GetType().GetProperty(propertyName)?.GetCustomAttribute<T>();
//    }

    #endregion
  }
}