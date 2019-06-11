using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Advance.RefDemo
{
  /// <summary>
  /// @desc : AssemblyDemo  
  /// @author : mons
  /// @create : 2019/6/5 13:59:58 
  /// @source : 
  /// </summary>
  public class AssemblyDemo
  {

    public static void Show()
    {
      foreach (var type in Assembly.GetCallingAssembly().ExportedTypes)
      {
        Console.WriteLine(type.FullName);
      }
    }
    

    public static void LoadAssemblyAndShowPublicTypes(string assemblyId)
    {

      var assembly = Assembly.Load(assemblyId);

      //显示已加载程序集中每个公开导出Type的全名
      foreach (var type in assembly.ExportedTypes)
      {
        Console.WriteLine(type.FullName);
      }

    }

  }
}
