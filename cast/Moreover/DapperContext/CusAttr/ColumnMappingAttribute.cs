using System;
using System.Collections.Generic;
using System.Text;

namespace DapperContext.CusAttr
{
  /// <summary>
  /// @desc : ColumnMappingAttribute  
  /// @author :mons
  /// @create : 2019/3/12 10:05:37 
  /// @source : 
  /// </summary>
  [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
  public class ColumnMappingAttribute : Attribute
  {
    public string Name { get; set; }
  }
}
