using System;
using System.Collections.Generic;
using System.Text;

namespace Tools.CusExtension
{
  /// <summary>
  /// @desc : Dictionary  
  /// @author :mons
  /// @create : 2019/4/12 11:46:26 
  /// @source : 
  /// </summary>
  public static class DictionaryExt
  {

    public static void AddOrSet<TKey, TResult>(this Dictionary<TKey, TResult> dictionary, TKey key, TResult value)
    {
      if (dictionary.ContainsKey(key))
        dictionary[key] = value;
      else 
        dictionary.Add(key,value);
    }

    public static void Increase<TKey>(this Dictionary<TKey, int> dictionary, TKey key)
    {
      if (dictionary.ContainsKey(key))
        dictionary[key]++;
      else
        dictionary.Add(key, 1);
    }

    public static void Decrease<TKey>(this Dictionary<TKey, int> dictionary, TKey key)
    {
      if (dictionary.ContainsKey(key))
        dictionary[key]--;
    }
    public static TResult Get<TKey,TResult>(this Dictionary<TKey, TResult> dictionary, TKey key, TResult defaultResult)
    {
      if (dictionary.ContainsKey(key))
        return dictionary[key];
      else
        return defaultResult;
    }

  }
}
