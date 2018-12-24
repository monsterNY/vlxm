using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Common.Cache
{
  public class MemoryCache
  {
    private static Dictionary<string, object> _cache = new Dictionary<string, dynamic>();

    public static void AddCache<T>(string key, T info) where T : class
    {
      if (_cache.ContainsKey(key))
        _cache[key] = info;
      else
        _cache.Add(key, info);
    }

    public static T GetCache<T>(string key, T defaultValue = default(T)) where T : class
    {
      if (_cache.ContainsKey(key))
      {
        object info = _cache[key];
        return (info as T) ?? defaultValue;
      }

      return defaultValue;
    }
  }
}